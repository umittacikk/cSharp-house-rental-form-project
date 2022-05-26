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
using System.IO;

namespace Ev_KiralamaOtomasyonu
{
    public partial class Kullanici_Sil : Form
    {
        public Kullanici_Sil()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-ETL1M4FA;Initial Catalog=EvKiralamaVeriTabani;Integrated Security=True");


        private void button3_Click(object sender, EventArgs e)   // silme butonu
        {
            if (txtKullaniciSil.Text.Length == 11)
            {
                bool kayit_durum = false;
                if (txtKullaniciSil.Text == Uye_Giris_Ekrani.tcno)
                {
                    MessageBox.Show("Kendinize ait kaydı sistemden silme yetkiniz bulunmamaktadır.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    baglanti.Open();
                    SqlCommand sorgu = new SqlCommand("select * from Tbl_kullanicilar where tcno='" + txtKullaniciSil.Text + "'", baglanti);
                    SqlDataReader verioku = sorgu.ExecuteReader();
                    while (verioku.Read())
                    {
                        kayit_durum = true;
                        baglanti.Close();
                        baglanti.Open();
                        SqlCommand silmesorgusu = new SqlCommand("delete from Tbl_kullanicilar where tcno='" + txtKullaniciSil.Text + "'", baglanti);
                        silmesorgusu.ExecuteNonQuery();
                        MessageBox.Show("Silmek istediğiniz kayıt başarıyla silinmiştir.", "Ev Kiralama Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        baglanti.Close();
                        if (File.Exists(Application.StartupPath + "\\kullaniciresimler\\" + txtKullaniciSil.Text + ".png"))
                        {
                            File.Delete(Application.StartupPath + "\\kullaniciresimler\\" + txtKullaniciSil.Text + ".png");
                        }
                        break;
                    }
                    baglanti.Close();
                    baglanti.Open();
                    SqlCommand sorgu2 = new SqlCommand("select * from Tbl_kiralananEvler where tcno='" + txtKullaniciSil.Text + "'", baglanti);
                    SqlDataReader verioku2 = sorgu2.ExecuteReader();
                    while (verioku2.Read())
                    {
                        baglanti.Close();
                        baglanti.Open();
                        SqlCommand kiracisil = new SqlCommand("delete from Tbl_kiralananEvler where tcno='" + txtKullaniciSil.Text + "'", baglanti);
                        kiracisil.ExecuteNonQuery();
                        break;
                    }
                    baglanti.Close();
                    if (kayit_durum == false)
                    {
                        MessageBox.Show("Aradığınız kayıt sistemimizde bulunmamaktadır.", "Ev Kiralama Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    baglanti.Close();
                }
                
            }
            else
            {
                MessageBox.Show("Lütfen 11 karakterden oluşan bir TC Kimlik Numarası giriniz.", "Ev Kiralama Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtKullaniciSil.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Yonetici anamenudon = new Yonetici();
            anamenudon.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtKullaniciSil.Text.Length < 11)
            {
                errorProvider1.SetError(txtKullaniciSil, "Lütfen TC Kimlik Numaranızı 11 sayı olacak şekilde giriniz.");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57 || (int)e.KeyChar == 8)    // asciide 48 ile 57 arası rakamları temsil eder o yüzden sınırlandırdık
            {                                                                           // asciide 8 backspace'i temsil eder 
                e.Handled = false;        // sınırlama yapmaz
            }
            else
            {
                e.Handled = true;          // sınırlama yapar
            }
        }

        private void Kullanici_Sil_Load(object sender, EventArgs e)
        {
            this.Text = "Kullanıcı Kaydı Silme";
            

            txtKullaniciSil.MaxLength = 11;

            lblKullaniciAdSoyad.Text = Uye_Giris_Ekrani.ad + " " + Uye_Giris_Ekrani.soyad;



            try
            {
                picBoxKullaniciFoto.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Uye_Giris_Ekrani.tcno + ".png");
            }
            catch
            {
                picBoxKullaniciFoto.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\nouser.png");
            }

            panel1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\arkaplanresimler\\arkaplan2.png");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
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

        private void lblKullaniciAdSoyad_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
