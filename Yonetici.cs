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
    public partial class Yonetici : Form
    {
        public Yonetici()
        {
            InitializeComponent();
        }

        

        private void button5_Click(object sender, EventArgs e)   // ara ve güncelle butonu
        {
            Yonetici_Kullanici_Ara_Guncelle arama_guncelleme = new Yonetici_Kullanici_Ara_Guncelle();
            arama_guncelleme.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)   // kullanıcı sil butonu
        {
            Kullanici_Sil kullanicisil = new Kullanici_Sil();
            kullanicisil.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)   // kullanıcı ekle butonu
        {
            Yonetici_Kullanici_Ekle ekleme = new Yonetici_Kullanici_Ekle();
            ekleme.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)   // daire ekle butonu
        {
            Yonetici_Ev_Ekle ekle = new Yonetici_Ev_Ekle();
            ekle.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)    // ev ara ve güncelle butonu
        {
            Yonetici_Ev_Ara_Guncelle ev_ara_guncelle = new Yonetici_Ev_Ara_Guncelle();
            ev_ara_guncelle.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)   // ev sil butonu
        {
            Yonetici_Ev_Sil sil = new Yonetici_Ev_Sil();
            sil.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)   // çıkış butonu
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

        private void Yonetici_Load(object sender, EventArgs e)
        {
            this.Text = "Yönetici Ana Menü";
            picBoxKullaniciFoto.Width = 150;
            picBoxKullaniciFoto.Height = 150;
            try
            {
                picBoxKullaniciFoto.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Uye_Giris_Ekrani.tcno + ".png");
            }
            catch 
            {
                picBoxKullaniciFoto.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\nouser.png");
            }
            lblKullaniciAdSoyad.Text = Uye_Giris_Ekrani.ad + " " + Uye_Giris_Ekrani.soyad;

            panel1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\arkaplanresimler\\arkaplan2.png");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;

            picBoxKullaniciIslemleri.Image = Image.FromFile(Application.StartupPath + "\\arkaplanresimler\\kullaniciislemleri.png");
            picBoxDaireIslemleri.Image = Image.FromFile(Application.StartupPath + "\\arkaplanresimler\\daireislemleri.png");
            picBoxSozlesmeIslemleri.Image = Image.FromFile(Application.StartupPath + "\\arkaplanresimler\\sozlesmeislemleri.png");
        }

        private void button8_Click(object sender, EventArgs e)   // kiralık daireleri güncelle butonu
        {
            Yonetici_Kiralik_Daireleri_Guncelle daireguncelle = new Yonetici_Kiralik_Daireleri_Guncelle();
            daireguncelle.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)   // kira sözleşmesini güncelle butonu
        {
            Yonetici_Kira_Sozlesmesini_Guncelle sozlesmeguncelle = new Yonetici_Kira_Sozlesmesini_Guncelle();
            sozlesmeguncelle.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)   // kiracıları görüntüle butonu
        {
            Yonetici_KiracilariGoruntule kiracigoruntule = new Yonetici_KiracilariGoruntule();
            kiracigoruntule.Show();
            this.Hide();
        }

        private void btn_OturumuKapat_Click(object sender, EventArgs e)  // oturumu kapat butonu
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
