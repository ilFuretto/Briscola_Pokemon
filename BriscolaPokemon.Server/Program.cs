using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using BriscolaPokemon.Core;

Console.WriteLine("=== SERVER BRISCOLA ===");
Console.WriteLine("In attesa di 2 giocatori...");

TcpListener server = new TcpListener(IPAddress.Any, 5000);
server.Start();

TcpClient client1 = server.AcceptTcpClient();
Console.WriteLine("Giocatore 1 connesso!");
InviaMessaggio(client1, "INFO", "Sei il Giocatore 1. Aspetta il secondo giocatore...");

TcpClient client2 = server.AcceptTcpClient();
Console.WriteLine("Giocatore 2 connesso!");
InviaMessaggio(client2, "INFO", "Sei il Giocatore 2. La partita sta per iniziare!");

// --- INIZIO PARTITA ---
Mazzo mazzo = new Mazzo();
mazzo.Mischia();

List<Carta> manoGiocatore1 = new List<Carta>();
List<Carta> manoGiocatore2 = new List<Carta>();

for (int i = 0; i < 3; i++)
{
    manoGiocatore1.Add(mazzo.Pesca());
    manoGiocatore2.Add(mazzo.Pesca());
}

Carta briscola = mazzo.Pesca();
Console.WriteLine("Briscola: " + briscola.ToString());

InviaMessaggio(client1, "BRISCOLA", briscola.ToString());
InviaMessaggio(client2, "BRISCOLA", briscola.ToString());
// Rimette la briscola in fondo al mazzo
mazzo.AggiungiFondo(briscola);

InviaMessaggio(client1, "MANO", CarteLista(manoGiocatore1));
InviaMessaggio(client2, "MANO", CarteLista(manoGiocatore2));

int turno = 1; // chi apre la mano (1 o 2)
int puntiGiocatore1 = 0;
int puntiGiocatore2 = 0;

Console.WriteLine("Partita iniziata! Turno del Giocatore 1");

// --- LOOP DI GIOCO ---
// Ogni iterazione del while gestisce una mano completa
while (true)
{
    // Decidiamo chi apre e chi risponde in base al turno
    TcpClient clientCheApre;
    TcpClient clientCheRisponde;
    List<Carta> manoCheApre;
    List<Carta> manoCheRisponde;
    int numeroGiocatoreApre;
    int numeroGiocatoreRisponde;

    if (turno == 1)
    {
        clientCheApre = client1;
        clientCheRisponde = client2;
        manoCheApre = manoGiocatore1;
        manoCheRisponde = manoGiocatore2;
        numeroGiocatoreApre = 1;
        numeroGiocatoreRisponde = 2;
    }
    else
    {
        clientCheApre = client2;
        clientCheRisponde = client1;
        manoCheApre = manoGiocatore2;
        manoCheRisponde = manoGiocatore1;
        numeroGiocatoreApre = 2;
        numeroGiocatoreRisponde = 1;
    }

    // --- FASE 1: chi apre gioca la sua carta ---
    InviaMessaggio(clientCheApre, "TUO_TURNO", "");
    InviaMessaggio(clientCheRisponde, "ASPETTA", "");

    Carta cartaApertura = null;
    while (cartaApertura == null)
    {
        string mossa = RiceviMessaggio(clientCheApre);
        cartaApertura = TrovaCarta(manoCheApre, mossa);
        if (cartaApertura == null)
        {
            InviaMessaggio(clientCheApre, "ERRORE", "Carta non valida!");
        }
    }
    manoCheApre.Remove(cartaApertura);
    Console.WriteLine("G" + numeroGiocatoreApre + " ha giocato: " + cartaApertura.ToString());

    // Informa chi risponde della carta giocata
    InviaMessaggio(clientCheRisponde, "AVVERSARIO_HA_GIOCATO", cartaApertura.ToString());

    // --- FASE 2: chi risponde gioca la sua carta ---
    InviaMessaggio(clientCheRisponde, "TUO_TURNO", "");
    InviaMessaggio(clientCheApre, "ASPETTA", "");

    Carta cartaRisposta = null;
    while (cartaRisposta == null)
    {
        string mossa = RiceviMessaggio(clientCheRisponde);
        cartaRisposta = TrovaCarta(manoCheRisponde, mossa);
        if (cartaRisposta == null)
        {
            InviaMessaggio(clientCheRisponde, "ERRORE", "Carta non valida!");
        }
    }
    manoCheRisponde.Remove(cartaRisposta);
    Console.WriteLine("G" + numeroGiocatoreRisponde + " ha giocato: " + cartaRisposta.ToString());

    // --- FASE 3: calcola chi ha vinto la mano ---
    // Attenzione: CalcolaVincitore restituisce 1 se vince cartaApertura, 2 se vince cartaRisposta
    int risultato = CalcolaVincitore(cartaApertura, cartaRisposta, briscola.Seme);

    if (risultato == 1)
    {
        turno = numeroGiocatoreApre;
    }
    else
    {
        turno = numeroGiocatoreRisponde;
    }

    // Aggiunge i punti delle due carte al vincitore
    int puntiMano = cartaApertura.GetPunti() + cartaRisposta.GetPunti();
    if (turno == 1)
    {
        puntiGiocatore1 += puntiMano;
    }
    else
    {
        puntiGiocatore2 += puntiMano;
    }

    Console.WriteLine("Vince la mano: Giocatore " + turno);
    Console.WriteLine("Punti: G1=" + puntiGiocatore1 + " G2=" + puntiGiocatore2);

    InviaMessaggio(client1, "FINE_MANO", turno.ToString());
    InviaMessaggio(client2, "FINE_MANO", turno.ToString());

    // Manda i punteggi aggiornati ad entrambi
    InviaMessaggio(client1, "PUNTEGGI", puntiGiocatore1 + "," + puntiGiocatore2);
    InviaMessaggio(client2, "PUNTEGGI", puntiGiocatore1 + "," + puntiGiocatore2);

    // Manda le carte prese al vincitore
    string cartePrese = cartaApertura.ToString() + "," + cartaRisposta.ToString();
    InviaMessaggio(client1, "CARTE_PRESE", turno + ";" + cartePrese);
    InviaMessaggio(client2, "CARTE_PRESE", turno + ";" + cartePrese);

    // --- FASE 4: pesca nuove carte se ce ne sono ---
    if (mazzo.GetCarteRimaste() > 0)
    {
        Carta nuovaApertura = mazzo.Pesca();
        Carta nuovaRisposta = mazzo.Pesca();

        // Controlla che le carte non siano null prima di aggiungerle
        if (nuovaApertura != null)
        {
            manoCheApre.Add(nuovaApertura);
            InviaMessaggio(clientCheApre, "PESCA", nuovaApertura.ToString());
        }
        if (nuovaRisposta != null)
        {
            manoCheRisponde.Add(nuovaRisposta);
            InviaMessaggio(clientCheRisponde, "PESCA", nuovaRisposta.ToString());
        }
    }

    // --- FASE 5: controlla se la partita è finita ---
    if (manoGiocatore1.Count == 0 && manoGiocatore2.Count == 0)
    {
        InviaMessaggio(client1, "FINE_PARTITA", "");
        InviaMessaggio(client2, "FINE_PARTITA", "");
        Console.WriteLine("Partita terminata!");
        break;
    }
}

Console.ReadLine();

// --- FUNZIONI DI SUPPORTO ---

void InviaMessaggio(TcpClient client, string tipo, string payload)
{
    Messaggio msg = new Messaggio();
    msg.Tipo = tipo;
    msg.Payload = payload;

    string json = JsonSerializer.Serialize(msg);
    byte[] dati = Encoding.UTF8.GetBytes(json + "\n");
    client.GetStream().Write(dati, 0, dati.Length);
}

string RiceviMessaggio(TcpClient client)
{
    NetworkStream stream = client.GetStream();
    byte[] buffer = new byte[1024];
    int byteLetti = stream.Read(buffer, 0, buffer.Length);
    string testo = Encoding.UTF8.GetString(buffer, 0, byteLetti);
    Messaggio msg = JsonSerializer.Deserialize<Messaggio>(testo);
    return msg.Payload;
}

string CarteLista(List<Carta> carte)
{
    string risultato = "";
    for (int i = 0; i < carte.Count; i++)
    {
        if (i > 0) risultato += ",";
        risultato += carte[i].ToString();
    }
    return risultato;
}

Carta TrovaCarta(List<Carta> mano, string nomeCarta)
{
    for (int i = 0; i < mano.Count; i++)
    {
        if (mano[i].ToString() == nomeCarta)
        {
            return mano[i];
        }
    }
    return null;
}

int CalcolaVincitore(Carta c1, Carta c2, Seme seme)
{
    // Se c2 è briscola e c1 no, vince c2
    if (c2.Seme == seme && c1.Seme != seme) return 2;

    // Se c1 è briscola e c2 no, vince c1
    if (c1.Seme == seme && c2.Seme != seme) return 1;

    // Se i semi sono diversi (e nessuna è briscola), vince chi ha aperto
    if (c1.Seme != c2.Seme) return 1;

    // Stesso seme: confronta i punti
    if (c1.GetPunti() > c2.GetPunti()) return 1;
    if (c2.GetPunti() > c1.GetPunti()) return 2;

    // Punti uguali (entrambe valgono 0): vince quella col numero più alto
    if (c1.Valore > c2.Valore) return 1;
    if (c2.Valore > c1.Valore) return 2;

    // Caso estremo: carte identiche, vince chi ha aperto
    return 1;
}