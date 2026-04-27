using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
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

            string percorsoSfondo = Path.Combine(Application.StartupPath, "sfondo.png");
            if (File.Exists(percorsoSfondo))
            {
                this.BackgroundImage = Image.FromFile(percorsoSfondo);
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }

            Font fontGrassetto = new Font("Arial", 15, FontStyle.Bold);

            // Label — sfondo nero, testo bianco
            lblStato.BackColor = Color.Black;
            lblStato.ForeColor = Color.White;
            lblStato.Font = fontGrassetto;

            lblBriscola.BackColor = Color.Black;
            lblBriscola.ForeColor = Color.White;
            lblBriscola.Font = fontGrassetto;

            lblAvversario.BackColor = Color.Black;
            lblAvversario.ForeColor = Color.White;
            lblAvversario.Font = fontGrassetto;

            lblPunteggi.BackColor = Color.Black;
            lblPunteggi.ForeColor = Color.White;
            lblPunteggi.Font = fontGrassetto;

            lblMazzo.BackColor = Color.Black;
            lblMazzo.ForeColor = Color.White;
            lblMazzo.Font = fontGrassetto;

            // ListBox carte prese — tema scuro
            lstCartePrese.BackColor = Color.Black;
            lstCartePrese.ForeColor = Color.White;
            lstCartePrese.Font = fontGrassetto;
            lstCartePrese.BorderStyle = BorderStyle.FixedSingle;

            // Label "Carte prese:" sopra la listbox
            lblCartePrese.BackColor = Color.Black;
            lblCartePrese.ForeColor = Color.White;
            lblCartePrese.Font = fontGrassetto;

            lblTueCarte.BackColor = Color.Black;
            lblTueCarte.ForeColor = Color.White;
            lblTueCarte.Font = fontGrassetto;

            // Bottone connetti
            btnConnetti.BackColor = Color.Black;
            btnConnetti.ForeColor = Color.White;
            btnConnetti.Font = fontGrassetto;

            // TextBox IP
            txtIP.BackColor = Color.Black;
            txtIP.ForeColor = Color.White;
            txtIP.Font = fontGrassetto;
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
        // Metodo riutilizzabile per creare l'immagine di una carta
        private Bitmap CreaImmagineCarta(Carta c, int larghezza, int altezza)
        {
            string percorso = Path.Combine(
                Application.StartupPath,
                "immagini",
                c.GetNomeImmagine()
            );

            Bitmap finale = new Bitmap(larghezza, altezza);
            Graphics g = Graphics.FromImage(finale);
            g.Clear(Color.White);

            if (File.Exists(percorso))
            {
                Bitmap originale = new Bitmap(percorso);
                g.DrawImage(originale, 0, 0, larghezza, altezza);
                originale.Dispose();
            }

            // --- SEME IN ALTO AL CENTRO ---
            string seme = c.Seme.ToString();
            Font fontSeme = new Font("Arial", 9, FontStyle.Bold);
            SizeF dimSeme = g.MeasureString(seme, fontSeme);
            float xSeme = (larghezza - dimSeme.Width) / 2;

            Color coloreSeme;
            if (c.Seme == Seme.Coppe)
            {
                coloreSeme = Color.Blue;
            }
            else if (c.Seme == Seme.Denari)
            {
                coloreSeme = Color.Orange;
            }
            else if (c.Seme == Seme.Bastoni)
            {
                coloreSeme = Color.Green;
            }
            else
            {
                coloreSeme = Color.Red;
            }

            g.FillRectangle(
                new SolidBrush(Color.FromArgb(200, Color.White)),
                xSeme - 2, 2,
                dimSeme.Width + 4, dimSeme.Height
            );
            g.DrawString(seme, fontSeme, new SolidBrush(coloreSeme), xSeme, 2);

            // --- NUMERO IN BASSO A DESTRA ---
            string numero = "";
            if (c.Valore == 1)
            {
                numero = "A";
            }
            else if (c.Valore == 8)
            {
                numero = "J";
            }
            else if (c.Valore == 9)
            {
                numero = "Q";
            }
            else if (c.Valore == 10)
            {
                numero = "K";
            }
            else
            {
                numero = c.Valore.ToString();
            }

            Font fontNumero = new Font("Arial", 22, FontStyle.Bold);
            SizeF dimNumero = g.MeasureString(numero, fontNumero);
            float xNumero = larghezza - dimNumero.Width - 4;
            float yNumero = altezza - dimNumero.Height - 4;

            g.FillRectangle(
                new SolidBrush(Color.FromArgb(180, Color.Black)),
                xNumero - 4, yNumero - 2,
                dimNumero.Width + 8, dimNumero.Height + 2
            );
            g.DrawString(numero, fontNumero, new SolidBrush(Color.Yellow), xNumero, yNumero);

            g.Dispose();
            return finale;
        }

        private void GestisciMessaggio(Messaggio msg)
        {
            if (msg.Tipo == "INFO")
            {
                lblStato.Text = msg.Payload;

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
                lblBriscola.Text = "Briscola:";

                Carta cartaBriscola = StringToCarta(msg.Payload);
                if (cartaBriscola != null)
                {
                    picBriscola.Image = CreaImmagineCarta(cartaBriscola, picBriscola.Width, picBriscola.Height);
                    picBriscola.SizeMode = PictureBoxSizeMode.StretchImage;
                }
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
                // Mostra la carta dell'avversario come immagine
                Carta cartaAvversario = StringToCarta(msg.Payload);
                if (cartaAvversario != null)
                {
                    picCartaAvversario.Image = CreaImmagineCarta(cartaAvversario, picCartaAvversario.Width, picCartaAvversario.Height);
                    picCartaAvversario.SizeMode = PictureBoxSizeMode.StretchImage;
                    lblAvversario.Text = "Carta avversario:";
                }
            }
            else if (msg.Tipo == "CARTE_MAZZO")
            {
                lblMazzo.Text = "Carte nel mazzo: " + msg.Payload;
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
                lblAvversario.Text = "Carta avversario:";
                picCartaAvversario.Image = null; // pulisce la carta avversario
            }
            else if (msg.Tipo == "PUNTEGGI")
            {
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

                string[] parti = msg.Payload.Split(',');
                int puntiG1 = int.Parse(parti[0]);
                int puntiG2 = int.Parse(parti[1]);

                int miei = 0;
                int avversario = 0;
                if (_mioNumero == 1)
                {
                    miei = puntiG1;
                    avversario = puntiG2;
                }
                else
                {
                    miei = puntiG2;
                    avversario = puntiG1;
                }

                string risultato = "";
                if (miei > avversario)
                {
                    risultato = "HAI VINTO!";
                }
                else if (avversario > miei)
                {
                    risultato = "HAI PERSO!";
                }
                else
                {
                    risultato = "PAREGGIO!";
                }

                string messaggio = risultato + "\n\n"
                    + "Punti tuoi: " + miei + "\n"
                    + "Punti avversario: " + avversario + "\n\n"
                    + "Totale punti in gioco: 120";

                MessageBox.Show(messaggio, "Fine Partita", MessageBoxButtons.OK);
            }
            else if (msg.Tipo == "RIVINCITA")
            {
                DialogResult risposta = MessageBox.Show(
                    "Vuoi fare una rivincita?",
                    "Rivincita",
                    MessageBoxButtons.YesNo
                );

                if (risposta == DialogResult.Yes)
                {
                    InviaMessaggio("RISPOSTA_RIVINCITA", "SI");
                    lblStato.Text = "Aspetta la risposta dell'avversario...";
                }
                else
                {
                    InviaMessaggio("RISPOSTA_RIVINCITA", "NO");
                    lblStato.Text = "Hai rifiutato la rivincita.";
                }
            }
            else if (msg.Tipo == "RIGIOCA")
            {
                lstCartePrese.Items.Clear();
                lblAvversario.Text = "Carta avversario:";
                picCartaAvversario.Image = null;
                picBriscola.Image = null;
                lblMazzo.Text = "Carte nel mazzo: ";
                lblPunteggi.Text = "Punti tuoi: 0 - Punti avversario: 0";
                lblStato.Text = "Nuova partita in arrivo...";
                _mano.Clear();
                MostraCarte();
            }
            else if (msg.Tipo == "ABBANDONA")
            {
                lblStato.Text = "Partita terminata. Puoi chiudere il gioco.";
            }
            else if (msg.Tipo == "ERRORE")
            {
                MessageBox.Show("Errore: " + msg.Payload);
            }
        }

        private void MostraCarte()
        {
            for (int i = 0; i < _bottoniCarte.Count; i++)
            {
                this.Controls.Remove(_bottoniCarte[i]);
            }
            _bottoniCarte.Clear();

            for (int i = 0; i < _mano.Count; i++)
            {
                Carta c = _mano[i];
                Button btn = new Button();
                btn.Width = 90;
                btn.Height = 120;
                btn.Left = 20 + (i * 100);
                btn.Top = 310;
                btn.Tag = c;
                btn.Click += BtnCarta_Click;
                btn.Enabled = _mioTurno;

                string percorso = Path.Combine(
                    Application.StartupPath,
                    "immagini",
                    c.GetNomeImmagine()
                );

                if (File.Exists(percorso))
                {
                    btn.Image = CreaImmagineCarta(c, btn.Width, btn.Height);
                    btn.ImageAlign = ContentAlignment.MiddleCenter;
                    btn.Text = "";
                }
                else
                {
                    btn.Text = c.ToString();
                }

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