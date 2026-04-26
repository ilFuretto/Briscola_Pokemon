namespace BriscolaPokemon.Client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblStato = new Label();
            lblBriscola = new Label();
            lblAvversario = new Label();
            btnConnetti = new Button();
            txtIP = new TextBox();
            label1 = new Label();
            lblPunteggi = new Label();
            lstCartePrese = new ListBox();
            lblCartePrese = new Label();
            picBriscola = new PictureBox();
            picCartaAvversario = new PictureBox();
            lblTueCarte = new Label();
            lblMazzo = new Label();
            ((System.ComponentModel.ISupportInitialize)picBriscola).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picCartaAvversario).BeginInit();
            SuspendLayout();
            // 
            // lblStato
            // 
            lblStato.AutoSize = true;
            lblStato.BackColor = Color.MintCream;
            lblStato.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStato.ForeColor = SystemColors.ControlText;
            lblStato.Location = new Point(12, 36);
            lblStato.Name = "lblStato";
            lblStato.Size = new Size(166, 30);
            lblStato.TabIndex = 0;
            lblStato.Text = "Stato: In attesa...";
            lblStato.Click += lblStato_Click;
            // 
            // lblBriscola
            // 
            lblBriscola.BackColor = Color.MintCream;
            lblBriscola.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBriscola.ForeColor = SystemColors.ControlText;
            lblBriscola.Location = new Point(568, 61);
            lblBriscola.Name = "lblBriscola";
            lblBriscola.Size = new Size(88, 30);
            lblBriscola.TabIndex = 1;
            lblBriscola.Text = "Briscola:";
            // 
            // lblAvversario
            // 
            lblAvversario.BackColor = Color.MintCream;
            lblAvversario.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAvversario.ForeColor = SystemColors.ControlText;
            lblAvversario.Location = new Point(12, 161);
            lblAvversario.Name = "lblAvversario";
            lblAvversario.Size = new Size(177, 30);
            lblAvversario.TabIndex = 2;
            lblAvversario.Text = "Carta avversario:";
            // 
            // btnConnetti
            // 
            btnConnetti.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnConnetti.Location = new Point(237, 459);
            btnConnetti.Name = "btnConnetti";
            btnConnetti.Size = new Size(103, 49);
            btnConnetti.TabIndex = 3;
            btnConnetti.Text = "Connetti";
            btnConnetti.UseVisualStyleBackColor = true;
            btnConnetti.Click += btnConnetti_Click;
            // 
            // txtIP
            // 
            txtIP.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtIP.Location = new Point(51, 465);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(165, 35);
            txtIP.TabIndex = 4;
            txtIP.Text = "127.0.0.1";
            txtIP.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.MintCream;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(9, 465);
            label1.Name = "label1";
            label1.Size = new Size(36, 30);
            label1.TabIndex = 5;
            label1.Text = "IP:";
            label1.Click += btnConnetti_Click;
            // 
            // lblPunteggi
            // 
            lblPunteggi.BackColor = Color.MintCream;
            lblPunteggi.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPunteggi.ForeColor = SystemColors.ControlText;
            lblPunteggi.Location = new Point(495, 489);
            lblPunteggi.Name = "lblPunteggi";
            lblPunteggi.Size = new Size(346, 30);
            lblPunteggi.TabIndex = 6;
            lblPunteggi.Text = "Punti tuoi: 0 - Punti avversario: 0";
            lblPunteggi.TextAlign = ContentAlignment.TopCenter;
            // 
            // lstCartePrese
            // 
            lstCartePrese.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstCartePrese.FormattingEnabled = true;
            lstCartePrese.ItemHeight = 21;
            lstCartePrese.Location = new Point(517, 208);
            lstCartePrese.Name = "lstCartePrese";
            lstCartePrese.Size = new Size(309, 256);
            lstCartePrese.TabIndex = 7;
            // 
            // lblCartePrese
            // 
            lblCartePrese.BackColor = Color.MintCream;
            lblCartePrese.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCartePrese.Location = new Point(601, 175);
            lblCartePrese.Name = "lblCartePrese";
            lblCartePrese.Size = new Size(137, 30);
            lblCartePrese.TabIndex = 8;
            lblCartePrese.Text = "Carte prese:";
            lblCartePrese.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // picBriscola
            // 
            picBriscola.BorderStyle = BorderStyle.FixedSingle;
            picBriscola.Location = new Point(662, 13);
            picBriscola.Name = "picBriscola";
            picBriscola.Size = new Size(90, 120);
            picBriscola.SizeMode = PictureBoxSizeMode.StretchImage;
            picBriscola.TabIndex = 9;
            picBriscola.TabStop = false;
            // 
            // picCartaAvversario
            // 
            picCartaAvversario.BorderStyle = BorderStyle.FixedSingle;
            picCartaAvversario.Location = new Point(195, 118);
            picCartaAvversario.Name = "picCartaAvversario";
            picCartaAvversario.Size = new Size(90, 120);
            picCartaAvversario.SizeMode = PictureBoxSizeMode.StretchImage;
            picCartaAvversario.TabIndex = 10;
            picCartaAvversario.TabStop = false;
            // 
            // lblTueCarte
            // 
            lblTueCarte.BackColor = Color.MintCream;
            lblTueCarte.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTueCarte.ForeColor = SystemColors.ControlText;
            lblTueCarte.Location = new Point(12, 274);
            lblTueCarte.Name = "lblTueCarte";
            lblTueCarte.Size = new Size(106, 30);
            lblTueCarte.TabIndex = 11;
            lblTueCarte.Text = "Tue carte:";
            // 
            // lblMazzo
            // 
            lblMazzo.BackColor = Color.MintCream;
            lblMazzo.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMazzo.ForeColor = SystemColors.ControlText;
            lblMazzo.Location = new Point(568, 145);
            lblMazzo.Name = "lblMazzo";
            lblMazzo.Size = new Size(198, 30);
            lblMazzo.TabIndex = 12;
            lblMazzo.Text = "Carte nel mazzo: 34";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MintCream;
            ClientSize = new Size(884, 561);
            Controls.Add(lblMazzo);
            Controls.Add(lblTueCarte);
            Controls.Add(picCartaAvversario);
            Controls.Add(picBriscola);
            Controls.Add(lblCartePrese);
            Controls.Add(lstCartePrese);
            Controls.Add(lblPunteggi);
            Controls.Add(label1);
            Controls.Add(txtIP);
            Controls.Add(btnConnetti);
            Controls.Add(lblAvversario);
            Controls.Add(lblBriscola);
            Controls.Add(lblStato);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)picBriscola).EndInit();
            ((System.ComponentModel.ISupportInitialize)picCartaAvversario).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblStato;
        private Label lblBriscola;
        private Label lblAvversario;
        private Button btnConnetti;
        private TextBox txtIP;
        private Label label1;
        private Label lblPunteggi;
        private ListBox lstCartePrese;
        private Label lblCartePrese;
        private PictureBox picBriscola;
        private PictureBox picCartaAvversario;
        private Label lblTueCarte;
        private Label lblMazzo;
    }
}