using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using BriscolaPokemon.Core;

namespace BriscolaPokemon.Client
{
    public partial class Form1 : Form
    {
        private int _mioNumero = 0; // 1 o 2, lo scopriamo dal messaggio INFO
        private TcpClient _client;
        private NetworkStream _stream;
        private List<Carta> _mano = new List<Carta>();
        private List<Button> _bottoniCarte = new List<Button>();
        private bool _mioTurno = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnetti_Click(object sender, EventArgs e)
        {
            try
            {
                string ip = txtIP.Text;
                _client = new TcpClient();
                _client.Connect(ip, 5000);
                _stream = _client.GetStream();

                lblStato.Text = "Connesso! In attesa dell'altro giocatore...";
                btnConnetti.Enabled = false;
                txtIP.Enabled = false;

                Thread t = new Thread(RiceviMessaggi);
                t.IsBackground = true;
                t.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore connessione: " + ex.Message);
            }
        }

        // Gira in loop aspettando messaggi dal server
        private void RiceviMessaggi()
        {
            byte[] buffer = new byte[4096];

            while (true)
            {
                try
                {
                    int byteLetti = _stream.Read(buffer, 0, buffer.Length);
                    if (byteLetti == 0) break;

                    string testo = Encoding.UTF8.GetString(buffer, 0, byteLetti).Trim();

                    // Separa i messaggi — potrebbero arrivare più insieme
                    string[] messaggi = testo.Split('\n');

                    for (int i = 0; i < messaggi.Length; i++)
                    {
                        string m = messaggi[i].Trim();
                        if (m.Length == 0) continue;

                        Messaggio msg = JsonSerializer.Deserialize<Messaggio>(m);
                        if (msg != null)
                        {
                            //aggiorna la UI dal thread principale
                            this.Invoke(new Action(() => GestisciMessaggio(msg)));
                        }
                    }
                }
                catch
                {
                    break;
                }
            }
        }

        // Gestisce ogni messaggio ricevuto dal server
        private void GestisciMessaggio(Messaggio msg)
        {
            if (msg.Tipo == "INFO")
            {
                lblStato.Text = msg.Payload;

                // Capisce se siamo G1 o G2 dal messaggio di benvenuto
                if (msg.Payload.Contains("Giocatore 1"))
                {
                    _mioNumero = 1;
                }
                else
                {
                    _mioNumero = 2;
                }
            }
            else if (msg.Tipo == "BRISCOLA")
            {
                lblBriscola.Text = "Briscola: " + msg.Payload;
            }
            else if (msg.Tipo == "MANO")
            {
                string[] nomi = msg.Payload.Split(',');
                _mano.Clear();
                for (int i = 0; i < nomi.Length; i++)
                {
                    Carta c = StringToCarta(nomi[i]);
                    if (c != null) _mano.Add(c);
                }
                MostraCarte();
            }
            else if (msg.Tipo == "TUO_TURNO")
            {
                _mioTurno = true;
                lblStato.Text = "Tocca a te! Scegli una carta.";
                AbilitaCarte(true);
            }
            else if (msg.Tipo == "ASPETTA")
            {
                _mioTurno = false;
                lblStato.Text = "Aspetta... è il turno dell'avversario.";
                AbilitaCarte(false);
            }
            else if (msg.Tipo == "AVVERSARIO_HA_GIOCATO")
            {
                lblAvversario.Text = "Carta avversario: " + msg.Payload;
            }
            else if (msg.Tipo == "PESCA")
            {
                Carta nuova = StringToCarta(msg.Payload);
                if (nuova != null)
                {
                    _mano.Add(nuova);
                    MostraCarte();
                }
            }
            else if (msg.Tipo == "FINE_MANO")
            {
                lblStato.Text = "Ha vinto la mano: Giocatore " + msg.Payload;
                lblAvversario.Text = "Carta avversario: ";
            }
            else if (msg.Tipo == "PUNTEGGI")
            {
                // Il payload arriva tipo "15,22"
                string[] parti = msg.Payload.Split(',');
                int puntiG1 = int.Parse(parti[0]);
                int puntiG2 = int.Parse(parti[1]);

                if (_mioNumero == 1)
                {
                    lblPunteggi.Text = "Punti tuoi: " + puntiG1 + " - Punti avversario: " + puntiG2;
                }
                else
                {
                    lblPunteggi.Text = "Punti tuoi: " + puntiG2 + " - Punti avversario: " + puntiG1;
                }
            }
            else if (msg.Tipo == "CARTE_PRESE")
            {
                // Il payload arriva tipo "1;Coppe_3,Bastoni_7"
                string[] parti = msg.Payload.Split(';');
                int vincitore = int.Parse(parti[0]);
                string[] carte = parti[1].Split(',');

                string prefisso = "";
                if (vincitore == _mioNumero)
                {
                    prefisso = "TUE: ";
                }
                else
                {
                    prefisso = "AVV: ";
                }

                for (int i = 0; i < carte.Length; i++)
                {
                    lstCartePrese.Items.Add(prefisso + carte[i]);
                }
            }
            else if (msg.Tipo == "FINE_PARTITA")
            {
                lblStato.Text = "Partita finita!";
                AbilitaCarte(false);
                MessageBox.Show("La partita è terminata!");
            }
            else if (msg.Tipo == "ERRORE")
            {
                MessageBox.Show("Errore: " + msg.Payload);
            }
        }
        // Crea un bottone per ogni carta nella mano
        private void MostraCarte()
        {
            // Rimuove i bottoni precedenti
            for (int i = 0; i < _bottoniCarte.Count; i++)
            {
                this.Controls.Remove(_bottoniCarte[i]);
            }
            _bottoniCarte.Clear();

            // Crea un bottone per ogni carta
            for (int i = 0; i < _mano.Count; i++)
            {
                Carta c = _mano[i];
                Button btn = new Button();
                btn.Text = c.ToString();
                btn.Width = 80;
                btn.Height = 100;
                btn.Left = 20 + (i * 90);
                btn.Top = 200;
                btn.Tag = c; // salviamo la carta nel bottone
                btn.Click += BtnCarta_Click;
                btn.Enabled = _mioTurno;

                this.Controls.Add(btn);
                _bottoniCarte.Add(btn);
            }
        }

        private void BtnCarta_Click(object sender, EventArgs e)
        {
            if (!_mioTurno) return;

            Button btn = (Button)sender;
            Carta cartaScelta = (Carta)btn.Tag;

            // Invia la carta al server
            InviaMessaggio("GIOCA_CARTA", cartaScelta.ToString());

            // Rimuove la carta dalla mano
            _mano.Remove(cartaScelta);
            MostraCarte();

            _mioTurno = false;
            AbilitaCarte(false);
            lblStato.Text = "Carta giocata! Aspetta l'avversario...";
        }

        private void AbilitaCarte(bool abilitato)
        {
            for (int i = 0; i < _bottoniCarte.Count; i++)
            {
                _bottoniCarte[i].Enabled = abilitato;
            }
        }

        private void InviaMessaggio(string tipo, string payload)
        {
            Messaggio msg = new Messaggio();
            msg.Tipo = tipo;
            msg.Payload = payload;

            string json = JsonSerializer.Serialize(msg);
            byte[] dati = Encoding.UTF8.GetBytes(json + "\n");
            _stream.Write(dati, 0, dati.Length);
        }

        // Converte una stringa tipo "Coppe_1" in un oggetto Carta
        private Carta StringToCarta(string testo)
        {
            string[] parti = testo.Split('_');
            if (parti.Length != 2) return null;

            Carta c = new Carta();
            c.Seme = (Seme)Enum.Parse(typeof(Seme), parti[0]);
            c.Valore = int.Parse(parti[1]);
            return c;
        }

        private void lblStato_Click(object sender, EventArgs e)
        {

        }
    }
}