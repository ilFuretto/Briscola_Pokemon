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
            label2 = new Label();
            SuspendLayout();
            // 
            // lblStato
            // 
            lblStato.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStato.Location = new Point(12, 36);
            lblStato.Name = "lblStato";
            lblStato.Size = new Size(500, 30);
            lblStato.TabIndex = 0;
            lblStato.Text = "Stato: In attesa...";
            lblStato.Click += lblStato_Click;
            // 
            // lblBriscola
            // 
            lblBriscola.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBriscola.Location = new Point(518, 36);
            lblBriscola.Name = "lblBriscola";
            lblBriscola.Size = new Size(366, 30);
            lblBriscola.TabIndex = 1;
            lblBriscola.Text = "Briscola:";
            // 
            // lblAvversario
            // 
            lblAvversario.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAvversario.Location = new Point(12, 161);
            lblAvversario.Name = "lblAvversario";
            lblAvversario.Size = new Size(443, 30);
            lblAvversario.TabIndex = 2;
            lblAvversario.Text = "Carta avversario:";
            // 
            // btnConnetti
            // 
            btnConnetti.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnConnetti.Location = new Point(235, 459);
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
            txtIP.Click += btnConnetti_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(9, 465);
            label1.Name = "label1";
            label1.Size = new Size(36, 30);
            label1.TabIndex = 5;
            label1.Text = "IP:";
            label1.Click += btnConnetti_Click;
            // 
            // lblPunteggi
            // 
            lblPunteggi.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPunteggi.Location = new Point(469, 470);
            lblPunteggi.Name = "lblPunteggi";
            lblPunteggi.Size = new Size(366, 30);
            lblPunteggi.TabIndex = 6;
            lblPunteggi.Text = "Punti tuoi: 0 - Punti avversario: 0";
            // 
            // lstCartePrese
            // 
            lstCartePrese.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstCartePrese.FormattingEnabled = true;
            lstCartePrese.ItemHeight = 21;
            lstCartePrese.Location = new Point(469, 181);
            lstCartePrese.Name = "lstCartePrese";
            lstCartePrese.Size = new Size(309, 256);
            lstCartePrese.TabIndex = 7;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(469, 148);
            label2.Name = "label2";
            label2.Size = new Size(309, 30);
            label2.TabIndex = 8;
            label2.Text = "Carte prese:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 561);
            Controls.Add(label2);
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
        private Label label2;
    }
}