
namespace Ev_KiralamaOtomasyonu
{
    partial class Yonetici_Ev_Sil
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIlanNo = new System.Windows.Forms.TextBox();
            this.btn_Geri = new System.Windows.Forms.Button();
            this.btn_Cikis = new System.Windows.Forms.Button();
            this.btn_Sil = new System.Windows.Forms.Button();
            this.btn_Temizle = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.picBoxKullaniciFoto = new System.Windows.Forms.PictureBox();
            this.lblKullaniciAdSoyad = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_OturumuKapat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxKullaniciFoto)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(80, 127);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(308, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Silmek istediğiniz evin ilan numarasını giriniz:";
            // 
            // txtIlanNo
            // 
            this.txtIlanNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtIlanNo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtIlanNo.Location = new System.Drawing.Point(395, 127);
            this.txtIlanNo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtIlanNo.Name = "txtIlanNo";
            this.txtIlanNo.Size = new System.Drawing.Size(157, 23);
            this.txtIlanNo.TabIndex = 0;
            this.txtIlanNo.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.txtIlanNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // btn_Geri
            // 
            this.btn_Geri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btn_Geri.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Geri.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_Geri.Location = new System.Drawing.Point(9, 9);
            this.btn_Geri.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Geri.Name = "btn_Geri";
            this.btn_Geri.Size = new System.Drawing.Size(88, 52);
            this.btn_Geri.TabIndex = 3;
            this.btn_Geri.Text = "Geri";
            this.btn_Geri.UseVisualStyleBackColor = false;
            this.btn_Geri.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Cikis
            // 
            this.btn_Cikis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btn_Cikis.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Cikis.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_Cikis.Location = new System.Drawing.Point(752, 9);
            this.btn_Cikis.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Cikis.Name = "btn_Cikis";
            this.btn_Cikis.Size = new System.Drawing.Size(88, 52);
            this.btn_Cikis.TabIndex = 4;
            this.btn_Cikis.Text = "Çıkış";
            this.btn_Cikis.UseVisualStyleBackColor = false;
            this.btn_Cikis.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_Sil
            // 
            this.btn_Sil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btn_Sil.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Sil.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_Sil.Location = new System.Drawing.Point(395, 168);
            this.btn_Sil.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Sil.Name = "btn_Sil";
            this.btn_Sil.Size = new System.Drawing.Size(71, 29);
            this.btn_Sil.TabIndex = 1;
            this.btn_Sil.Text = "Sil";
            this.btn_Sil.UseVisualStyleBackColor = false;
            this.btn_Sil.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_Temizle
            // 
            this.btn_Temizle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btn_Temizle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Temizle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_Temizle.Location = new System.Drawing.Point(480, 168);
            this.btn_Temizle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Temizle.Name = "btn_Temizle";
            this.btn_Temizle.Size = new System.Drawing.Size(71, 29);
            this.btn_Temizle.TabIndex = 2;
            this.btn_Temizle.Text = "Temizle";
            this.btn_Temizle.UseVisualStyleBackColor = false;
            this.btn_Temizle.Click += new System.EventHandler(this.button4_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // picBoxKullaniciFoto
            // 
            this.picBoxKullaniciFoto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picBoxKullaniciFoto.Location = new System.Drawing.Point(670, 74);
            this.picBoxKullaniciFoto.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picBoxKullaniciFoto.Name = "picBoxKullaniciFoto";
            this.picBoxKullaniciFoto.Size = new System.Drawing.Size(114, 123);
            this.picBoxKullaniciFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxKullaniciFoto.TabIndex = 4;
            this.picBoxKullaniciFoto.TabStop = false;
            // 
            // lblKullaniciAdSoyad
            // 
            this.lblKullaniciAdSoyad.AutoSize = true;
            this.lblKullaniciAdSoyad.BackColor = System.Drawing.Color.Transparent;
            this.lblKullaniciAdSoyad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblKullaniciAdSoyad.Location = new System.Drawing.Point(668, 209);
            this.lblKullaniciAdSoyad.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblKullaniciAdSoyad.Name = "lblKullaniciAdSoyad";
            this.lblKullaniciAdSoyad.Size = new System.Drawing.Size(40, 15);
            this.lblKullaniciAdSoyad.TabIndex = 5;
            this.lblKullaniciAdSoyad.Text = "label2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_OturumuKapat);
            this.panel1.Controls.Add(this.btn_Geri);
            this.panel1.Controls.Add(this.lblKullaniciAdSoyad);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtIlanNo);
            this.panel1.Controls.Add(this.picBoxKullaniciFoto);
            this.panel1.Controls.Add(this.btn_Cikis);
            this.panel1.Controls.Add(this.btn_Temizle);
            this.panel1.Controls.Add(this.btn_Sil);
            this.panel1.Location = new System.Drawing.Point(0, -2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(851, 339);
            this.panel1.TabIndex = 6;
            // 
            // btn_OturumuKapat
            // 
            this.btn_OturumuKapat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btn_OturumuKapat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_OturumuKapat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_OturumuKapat.Location = new System.Drawing.Point(658, 9);
            this.btn_OturumuKapat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_OturumuKapat.Name = "btn_OturumuKapat";
            this.btn_OturumuKapat.Size = new System.Drawing.Size(88, 52);
            this.btn_OturumuKapat.TabIndex = 6;
            this.btn_OturumuKapat.Text = "Oturumu Kapat";
            this.btn_OturumuKapat.UseVisualStyleBackColor = false;
            this.btn_OturumuKapat.Click += new System.EventHandler(this.btn_OturumuKapat_Click);
            // 
            // Yonetici_Ev_Sil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 335);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Yonetici_Ev_Sil";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ev_Sil";
            this.Load += new System.EventHandler(this.Ev_Sil_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxKullaniciFoto)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIlanNo;
        private System.Windows.Forms.Button btn_Geri;
        private System.Windows.Forms.Button btn_Cikis;
        private System.Windows.Forms.Button btn_Sil;
        private System.Windows.Forms.Button btn_Temizle;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblKullaniciAdSoyad;
        private System.Windows.Forms.PictureBox picBoxKullaniciFoto;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_OturumuKapat;
    }
}