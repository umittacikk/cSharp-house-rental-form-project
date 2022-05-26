using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ev_KiralamaOtomasyonu
{
    public partial class Yonetici_Kira_Sozlesmesini_Guncelle : Form
    {
        public Yonetici_Kira_Sozlesmesini_Guncelle()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-ETL1M4FA;Initial Catalog=EvKiralamaVeriTabani;Integrated Security=True");


        private void Temizle()
        {
            txtSozlesmeMadde.Clear();
            richTxtSozlesmeGuncelle.Clear();
        }

        private void Sozlesmeyi_Getir()  // sözleşme getirme kısmı
        {
            bool sozlesme_var_mi = false;
            try
            {
                baglanti.Open();
                SqlCommand sozlesme_aramasorgu = new SqlCommand("select kosullar from Tbl_sozlesme", baglanti);
                SqlDataReader verioku = sozlesme_aramasorgu.ExecuteReader();
                while (verioku.Read())
                {
                    sozlesme_var_mi = true;
                    richTxtTumSozlesme.Text += verioku["kosullar"].ToString() + "\n";
                }
                baglanti.Close();
                if (sozlesme_var_mi == false)
                {
                    MessageBox.Show("Sisteme kayıtlı herhangi bir sözleşme bulunamamıştır.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception errormsg)
            {
                MessageBox.Show(errormsg.Message, "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }
        }

        private void Yonetici_Kira_Sozlesmesini_Guncelle_Load(object sender, EventArgs e)
        {
            this.Text = "Kira Sözleşmesini Güncelle";

            panel1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\arkaplanresimler\\arkaplan2.png");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;

            Sozlesmeyi_Getir();            

            richTxtTumSozlesme.ReadOnly = true;

            lblKullaniciAdSoyad.Text = Uye_Giris_Ekrani.ad + " " + Uye_Giris_Ekrani.soyad;

            try
            {
                picBoxKullaniciFoto.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Uye_Giris_Ekrani.tcno + ".png");
            }
            catch 
            {
                picBoxKullaniciFoto.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\nouser.png");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57 ||(int)e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)  // temizleme butonu
        {
            Temizle();
            txtSozlesmeMadde.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)  // ara butonu
        {
            bool madde_kayit_durum = false;
            try
            {
                baglanti.Open();
                SqlCommand maddearasorgu = new SqlCommand("select kosullar from Tbl_sozlesme where numara='" + txtSozlesmeMadde.Text + "'", baglanti);
                SqlDataReader verioku = maddearasorgu.ExecuteReader();
                while (verioku.Read())
                {
                    madde_kayit_durum = true;
                    break;
                }
                baglanti.Close();
                if (madde_kayit_durum == true)
                {
                    try
                    {
                        baglanti.Open();
                        SqlCommand maddearasorgu2 = new SqlCommand("select kosullar from Tbl_sozlesme where numara='" + txtSozlesmeMadde.Text + "'", baglanti);
                        SqlDataReader verioku2 = maddearasorgu2.ExecuteReader();
                        while (verioku2.Read())
                        {
                            richTxtSozlesmeGuncelle.Text = verioku2["kosullar"].ToString();
                        }
                        baglanti.Close();
                        txtSozlesmeMadde.Enabled = false;
                    }
                    catch (Exception errormsg)
                    {
                        MessageBox.Show(errormsg.Message, "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglanti.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Aradığınız madde kira sözleşmesinde bulunmamaktadır.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception errormsg)
            {
                MessageBox.Show(errormsg.Message, "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();                
            }
        }

        private void button3_Click(object sender, EventArgs e)   // güncelle butonu
        {
            try
            {
                baglanti.Open();
                SqlCommand guncellesorgu = new SqlCommand("update Tbl_sozlesme set kosullar='" + richTxtSozlesmeGuncelle.Text + "' where numara='" + txtSozlesmeMadde.Text + "'", baglanti);
                guncellesorgu.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Sözleşme maddesi güncelleme işlemi başarıyla gerçekleştirildi.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                richTxtTumSozlesme.Clear();
                Sozlesmeyi_Getir();
            }
            catch (Exception errormsg)
            {
                MessageBox.Show(errormsg.Message, "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)  // geri butonu
        {
            Yonetici anamenudon = new Yonetici();
            anamenudon.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)   // çıkış butonu
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

        private void panel1_Paint(object sender, PaintEventArgs e)   // boş   yanlışlıkla açıldı
        {

        }
    }
}
