using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ev_KiralamaOtomasyonu
{
    public partial class Kullanici : Form
    {
        public Kullanici()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)    // bilgileri güncelle butonu
        {
            Kullanici_Girisi_Bilgi_Guncelleme kullaniciguncelle = new Kullanici_Girisi_Bilgi_Guncelleme();
            kullaniciguncelle.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)   // Çıkış butonu
        {
            DialogResult dialog2 = new DialogResult();
            dialog2 = MessageBox.Show("Gerçekten çıkış yapmak istiyor musunuz?", "Metrekare Emlak", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog2 == DialogResult.Yes)
            {
                MessageBox.Show("Bizi tercih ettiğiniz için teşekkür ederiz. İyi günler dileriz.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
            else if (dialog2 == DialogResult.No)
            {

            }

        }

        private void Kullanici_Load(object sender, EventArgs e)
        {
            this.Text = "Kullanıcı Ana Menü";            

            try
            {
                picBoxKullaniciFoto.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Uye_Giris_Ekrani.tcno + ".png");
            }
            catch 
            {
                picBoxKullaniciFoto.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\nouser.png");                
            }

            lblKullaniciAdSoyad.Text = Uye_Giris_Ekrani.ad + " " + Uye_Giris_Ekrani.soyad;


            pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\arkaplanresimler\\kullaniciislemleri.png");   // kullanıcı icon
            pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\arkaplanresimler\\daireislemleri.png");


            panel1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\arkaplanresimler\\arkaplan3.png");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.BackColor = Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)   // ev ara ve ev kirala butonu
        {
            Kullanici_Ev_Ara_ve_Ev_Kirala aravekirala = new Kullanici_Ev_Ara_ve_Ev_Kirala();
            aravekirala.Show();
            this.Hide();
        }

        private void btn_OturumuKapat_Click(object sender, EventArgs e)   // oturumu kapatma butonu
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Gerçekten oturumu kapatmak istiyor musunuz?", "Metrekare Emlak", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                Uye_Giris_Ekrani giris = new Uye_Giris_Ekrani();
                giris.Show();
                this.Hide();
            }
            else if (dialog == DialogResult.No)
            {

            }
        }
    }
}
