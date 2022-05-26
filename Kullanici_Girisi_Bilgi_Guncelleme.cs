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
    public partial class Kullanici_Girisi_Bilgi_Guncelleme : Form
    {
        public Kullanici_Girisi_Bilgi_Guncelleme()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-ETL1M4FA;Initial Catalog=EvKiralamaVeriTabani;Integrated Security=True");

        private string cinsiyet;
        private string yetki = "Kullanıcı";

        public static string guncellenmis_ad;
        public static string guncellenmis_soyad;
        private void button4_Click(object sender, EventArgs e)   // temizleme butonu
        {
            txtTcNo.Clear();
            txtAdiniz.Clear();
            txtSoyadiniz.Clear();
            txtKullaniciAdiniz.Clear();
            txtParolaniz.Clear();
            txtMesleginiz.Clear();
        }

        private void Kullanici_Girisi_Bilgi_Guncelleme_Load(object sender, EventArgs e)
        {
            this.Text = "Bilgi Güncelleme";
            guncellenmis_ad = txtAdiniz.Text;
            guncellenmis_soyad = txtSoyadiniz.Text;
            

            txtAdiniz.CharacterCasing = CharacterCasing.Upper;
            txtSoyadiniz.CharacterCasing = CharacterCasing.Upper;

            txtTcNo.MaxLength = 11;  // TC Kimlik Numarası 11 karakter olacak
            txtKullaniciAdiniz.MaxLength = 10;  // Kullanıcı adı en fazla 10 karakter olsun
            txtParolaniz.MaxLength = 10;  // Şifre en fazla 10 karakter olsun

            lblKullaniciAdSoyad.Text = Uye_Giris_Ekrani.ad + " " + Uye_Giris_Ekrani.soyad;

            toolTip1.SetToolTip(this.txtTcNo, "TC Kimlik Numarası 11 haneden oluşmalıdır.");

            radioButton1.Checked = true;


            try
            {
                picBoxKullaniciFoto.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Uye_Giris_Ekrani.tcno + ".png");
            }
            catch 
            {
                picBoxKullaniciFoto.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\nouser.png");                
            }

            panel1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\arkaplanresimler\\arkaplan3.png");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;

            DateTime date = DateTime.Now;
            int yil = Convert.ToInt32(date.ToString("yyyy"));
            int ay = Convert.ToInt32(date.ToString("MM"));
            int gun = Convert.ToInt32(date.ToString("dd"));
            dateTimePickerDogumTarihiniz.MinDate = new DateTime(1900, 1, 1);
            dateTimePickerDogumTarihiniz.MaxDate = new DateTime(yil - 18, ay, gun);
            dateTimePickerDogumTarihiniz.Format = DateTimePickerFormat.Short;

            try    // Kullanıcının bilgilerini direkt ekrana getirir
            {
                baglanti.Open();
                SqlCommand aramasorgu = new SqlCommand("select * from Tbl_kullanicilar where tcno='" + Uye_Giris_Ekrani.tcno + "'", baglanti);
                SqlDataReader verioku = aramasorgu.ExecuteReader();
                while (verioku.Read())
                {
                    try
                    {
                        picBoxKullaniciFoto2.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Uye_Giris_Ekrani.tcno + ".png");
                    }
                    catch 
                    {
                        picBoxKullaniciFoto2.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\nouser.png");                        
                    }

                    txtTcNo.Text = verioku.GetValue(0).ToString();
                    txtAdiniz.Text = verioku.GetValue(1).ToString();
                    txtSoyadiniz.Text = verioku.GetValue(2).ToString();

                    if (verioku.GetValue(3).ToString() == "Kadın")
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }

                    txtKullaniciAdiniz.Text = verioku.GetValue(4).ToString();
                    txtParolaniz.Text = verioku.GetValue(5).ToString();
                    dateTimePickerDogumTarihiniz.Text = verioku.GetValue(8).ToString();
                    txtMesleginiz.Text = verioku.GetValue(7).ToString();

                    if (radioButton1.Checked == true)
                    {
                        cinsiyet = "Kadın";
                    }
                    else
                    {
                        cinsiyet = "Erkek";
                    }
                    break;
                }
                baglanti.Close();
                
            }
            catch (Exception errormsg)
            {
                MessageBox.Show(errormsg.Message, "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();                
            }
        }

        private void button1_Click(object sender, EventArgs e)  // geri butonu
        {
            Kullanici anamenudon = new Kullanici();
            anamenudon.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)  // çıkış butonu
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

        private void button3_Click(object sender, EventArgs e)  // güncelleme butonu
        {
            if (txtTcNo.Text.Length < 11)  // TC Kimlik Numarası 11 harften az girilirse
            {
                lblTcNo.ForeColor = Color.Red;
                txtTcNo.BackColor = Color.Red;
            }
            else
            {
                lblTcNo.ForeColor = Color.Black;
                txtTcNo.BackColor = Color.Empty;
            }
            if (txtAdiniz.Text == "")  // Ad kısmı boş bırakılırsa
            {
                lblAdiniz.ForeColor = Color.Red;
                txtAdiniz.BackColor = Color.Red;
            }
            else
            {
                lblAdiniz.ForeColor = Color.Black;
                txtAdiniz.BackColor = Color.Empty;
            }
            if (txtSoyadiniz.Text == "")   // Soyad kısmı boş bırakılırsa
            {
                lblSoyadiniz.ForeColor = Color.Red;
                txtSoyadiniz.BackColor = Color.Red;
            }
            else
            {
                lblSoyadiniz.ForeColor = Color.Black;
                txtSoyadiniz.BackColor = Color.Empty;
            }
            if (txtKullaniciAdiniz.Text == "")   // Kullanıcı adı boş bırakılırsa
            {
                lblKullaniciAdiniz.ForeColor = Color.Red;
                txtKullaniciAdiniz.BackColor = Color.Red;
            }
            else
            {
                lblKullaniciAdiniz.ForeColor = Color.Black;
                txtKullaniciAdiniz.BackColor = Color.Empty;
            }
            if (txtParolaniz.Text == "")   // Şifre kısmı boş bırakılırsa
            {
                lblParolaniz.ForeColor = Color.Red;
                txtParolaniz.BackColor = Color.Red;
            }
            else
            {
                lblParolaniz.ForeColor = Color.Black;
                txtParolaniz.BackColor = Color.Empty;
            }
            if (txtMesleginiz.Text == "")   // Meslek kısmı boş bırakılırsa
            {
                lblMesleginiz.ForeColor = Color.Red;
                txtMesleginiz.BackColor = Color.Red;
            }
            else
            {
                lblMesleginiz.ForeColor = Color.Black;
                txtMesleginiz.BackColor = Color.Empty;
            }

            if (txtTcNo.Text.Length == 11 && txtAdiniz.Text != "" && txtSoyadiniz.Text != "" && txtKullaniciAdiniz.Text != "" && txtParolaniz.Text != "" && txtMesleginiz.Text != "")
            {
                try
                {
                    baglanti.Open();
                    SqlCommand guncellesorgu = new SqlCommand("update Tbl_kullanicilar set tcno='" + txtTcNo.Text + "', ad='" + txtAdiniz.Text + "', soyad='" + txtSoyadiniz.Text + "', cinsiyet='" + cinsiyet + "', kullaniciadi='" + txtKullaniciAdiniz.Text + "', parola='" + txtParolaniz.Text + "', yetki='" + yetki + "', meslek='" + txtMesleginiz.Text + "', dogumtarihi='" + dateTimePickerDogumTarihiniz.Value.ToString("yyyy/MM/dd") + "' where tcno='" + Uye_Giris_Ekrani.tcno + "'", baglanti);
                    guncellesorgu.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Güncelleme işlemi başarıyla gerçekleştirilmiştir", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception errormsg)
                {
                    MessageBox.Show(errormsg.Message, "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglanti.Close();
                }
                try
                {
                    baglanti.Open();
                    SqlCommand guncellekomut2 = new SqlCommand("update Tbl_kiralananEvler set tcno='" + txtTcNo.Text + "', kiralayan_ad='" + txtAdiniz.Text + "', kiralayan_soyad='" + txtSoyadiniz.Text + "' where tcno='" + Uye_Giris_Ekrani.tcno + "'", baglanti);
                    guncellekomut2.ExecuteNonQuery();
                    baglanti.Close();
                }
                catch (Exception errormsg)
                {
                    MessageBox.Show(errormsg.Message, "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglanti.Close();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)  // TC Kimlik'i 11 haneden az girerse errorprovider gözükür
        {
            if (txtTcNo.Text.Length < 11)
            {
                errorProvider1.SetError(txtTcNo, "TC Kimlik Numarası 11 haneden oluşmalıdır.");
            }
            else
            {
                errorProvider1.Clear();
            }
        }   

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)   // TC Kimlik karakter girme kısıtlaması 
        {
            if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57 || (int)e.KeyChar == 8)  // sadece sayılara ve backspace'e basabilecek
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)  // Ad kısmı karakter sınırlaması 
        {   
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)   // sadece harflere, backspace'e ve space'e basabilecek
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)  // Soyadı kısmı karakter kısıtlaması
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)  // Kullanıcı adı karakter sınırlaması
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)  // Meslek kısmı karakter sınırlaması
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btn_OturumuKapat_Click(object sender, EventArgs e)   // oturumu kapat butonu
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
