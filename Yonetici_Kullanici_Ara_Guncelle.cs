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
    public partial class Yonetici_Kullanici_Ara_Guncelle : Form
    {
        public Yonetici_Kullanici_Ara_Guncelle()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-ETL1M4FA;Initial Catalog=EvKiralamaVeriTabani;Integrated Security=True");

        public static string guncellenmis_ad;
        public static string guncellenmis_soyad;

        public void Kullanici_Ara_Temizle()
        {
            
            txtTcNo.Clear();
            txtAdiniz.Clear();
            txtSoyadiniz.Clear();
            txtKullaniciAdiniz.Clear();
            txtParolaniz.Clear();
            txtMesleginiz.Clear();
            picBoxKullaniciFoto2.Image = null;

        }


        private void Kullanici_Ara_Guncelle_Load(object sender, EventArgs e)
        {
            this.Text = "Kullanıcı Bilgilerini Güncelle";

            guncellenmis_ad = txtAdiniz.Text;
            guncellenmis_soyad = txtSoyadiniz.Text;
            txtTcNo.MaxLength = 11;
            txtKullaniciAdiniz.MaxLength = 10;
            txtParolaniz.MaxLength = 10;            
            txtAdiniz.CharacterCasing = CharacterCasing.Upper;
            txtSoyadiniz.CharacterCasing = CharacterCasing.Upper;
            label11.Text = Uye_Giris_Ekrani.ad + " " + Uye_Giris_Ekrani.soyad;
            toolTip1.SetToolTip(this.txtTcNo, "TC Kimlik Numarası 11 sayıdan oluşmalıdır.");    // textbox1'in üstüne gelince yazı olarak çıkar
            
            DateTime date = DateTime.Now;
            int yil = Convert.ToInt32(date.ToString("yyyy"));
            int ay = Convert.ToInt32(date.ToString("MM"));
            int gun = Convert.ToInt32(date.ToString("dd"));

            dateTimePickerDogumTarihi.MinDate = new DateTime(1900, 1, 1);
            dateTimePickerDogumTarihi.MaxDate = new DateTime(yil - 18, ay, gun);
            dateTimePickerDogumTarihi.Format = DateTimePickerFormat.Short;

            try   // giriş yapan kişinin resmi  
            {
                picBoxKullaniciFoto.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Uye_Giris_Ekrani.tcno + ".png");
            }
            catch
            {
                picBoxKullaniciFoto.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\nouser.png");
            }


            panel2.BackgroundImage = Image.FromFile(Application.StartupPath + "\\arkaplanresimler\\arkaplan2.png");   // arkaplan resmi
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void button3_Click(object sender, EventArgs e)     // arama butonu
        {
            bool durum = false;
            if (txtTcNo.Text.Length == 11)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select * from Tbl_kullanicilar where tcno='" + txtTcNo.Text + "'", baglanti);
                SqlDataReader kayitoku = komut.ExecuteReader();
                while (kayitoku.Read())
                {
                    durum = true;

                    try    // kaydı aranan kişinin resmi
                    {
                        picBoxKullaniciFoto2.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + kayitoku.GetValue(0) + ".png");
                    }
                    catch
                    {
                        picBoxKullaniciFoto2.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\nouser.png");
                        
                    }
                    txtAdiniz.Text = kayitoku.GetValue(1).ToString();
                    txtSoyadiniz.Text = kayitoku.GetValue(2).ToString();
                    if (kayitoku.GetValue(3).ToString() == "Erkek")
                    {
                        radioButton2.Checked = true;
                    }
                    else
                    {
                        radioButton1.Checked = true;
                    }
                    if (kayitoku.GetValue(6).ToString() == "Yönetici")
                    {
                        radioButton3.Checked = true;
                    }
                    else
                    {
                        radioButton4.Checked = true;
                    }
                    txtKullaniciAdiniz.Text = kayitoku.GetValue(4).ToString();
                    txtParolaniz.Text = kayitoku.GetValue(5).ToString();
                    dateTimePickerDogumTarihi.Text = kayitoku.GetValue(8).ToString();  // ???????????? NASIL YAPILACAK
                    txtMesleginiz.Text = kayitoku.GetValue(7).ToString();
                    break;
                }
                if (durum == false)
                {
                    MessageBox.Show("Aradığınız kayıt sistemimizde bulunmamaktadır.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                baglanti.Close();
            }

            else
            {
                MessageBox.Show("Lütfen 11 karakterden oluşan TC kimlik numaranızı eksiksiz olarak giriniz.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Kullanici_Ara_Temizle();
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtTcNo.Text.Length < 11)
            {
                errorProvider1.SetError(txtTcNo, "Lütfen TC Kimlik Numaranızı 11 sayı olacak şekilde giriniz.");
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)    // Ad sınırlaması
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)   // char isletter -> klavyeye basılan karakter harf ise
            {                                                                                                                   // char iscontrol -> klavyeye basılan karakter backspace ise                                                                                                                                  
                e.Handled = false;              // sınırlama yapmaz                                                                              // char isseparator -> klavyeye basılan karakter space ise 
            }
            else
            {
                e.Handled = true;               // sınırlama yapar
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)    // Soyadı sınırlaması
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)   // char isletter -> klavyeye basılan karakter harf ise
            {                                                                                                                   // char iscontrol -> klavyeye basılan karakter backspace ise                                                                                                                                  
                e.Handled = false;        // sınırlama yapmaz                                                                   // char isseparator -> klavyeye basılan karakter space ise 
            }
            else
            {
                e.Handled = true;          // sınırlama yapar
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)   // Kullanıcı adı sınırlaması
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true || char.IsControl(e.KeyChar) == true)     // char isdigit = klavyeye basılan karakterin sayı olup olmadığına bakar
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)    // Meslek sınırlaması
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            {
                e.Handled = false;    // sınırlama yapmaz
            }
            else
            {
                e.Handled = true;    // sınırlama yapar
            }
        }


        private void button5_Click(object sender, EventArgs e)    // kayıt güncelleme butonu
        {
            string yetki = "";
            string cinsiyet = "";
            
                if (txtTcNo.Text.Length < 11)    // TC Kimlik kısmı kontrolü
                {
                    lblTcNo.ForeColor = Color.Red;
                    txtTcNo.BackColor = Color.Red;
                }
                else
                {
                    lblTcNo.ForeColor = Color.Black;
                    txtTcNo.BackColor = Color.Empty;
                }

                if (txtAdiniz.Text.Length < 2 || txtAdiniz.Text == "")      // Ad kısmı kontrolü
                {
                    lblAdiniz.ForeColor = Color.Red;
                    txtAdiniz.BackColor = Color.Red;
                }
                else
                {
                    lblAdiniz.ForeColor = Color.Black;
                    txtAdiniz.BackColor = Color.Empty;
                }

                if (txtSoyadiniz.Text.Length < 2 || txtSoyadiniz.Text == "")     // Soyad kısmı kontrolü
                {
                    lblSoyadiniz.ForeColor = Color.Red;
                    txtSoyadiniz.BackColor = Color.Red;
                }
                else
                {
                    lblSoyadiniz.ForeColor = Color.Black;
                    txtSoyadiniz.BackColor = Color.Empty;
                }

                if (txtKullaniciAdiniz.Text == "")   // Kullanıcı adı kısmı kontrolü
                {
                    lblKullaniciAdiniz.ForeColor = Color.Red;
                    txtKullaniciAdiniz.BackColor = Color.Red;
                }
                else
                {
                    lblKullaniciAdiniz.ForeColor = Color.Black;
                    txtKullaniciAdiniz.BackColor = Color.Empty;
                }
                if (txtParolaniz.Text == "")   // Parola kısmı kontrolü
                {
                    lblParolaniz.ForeColor = Color.Red;
                    txtParolaniz.BackColor = Color.Red;
                }
                else
                {
                    lblParolaniz.ForeColor = Color.Black;
                    txtParolaniz.BackColor = Color.Empty;
                }
                if (txtMesleginiz.Text == "")        // Meslek kısmı kontrolü
                {
                    lblMesleginiz.ForeColor = Color.Red;
                    txtMesleginiz.BackColor = Color.Red;
                }
                else
                {
                    lblMesleginiz.ForeColor = Color.Black;
                    txtMesleginiz.BackColor = Color.Empty;
                }
                

                if (txtTcNo.Text.Length == 11 && txtTcNo.Text != "" && txtAdiniz.Text.Length >= 2 && txtAdiniz.Text != "" && txtSoyadiniz.Text.Length >= 2 && txtSoyadiniz.Text != "" && txtKullaniciAdiniz.Text != "" && txtParolaniz.Text != "" && txtMesleginiz.Text != "" && picBoxKullaniciFoto2.Image != null)
                {
                    if (radioButton3.Checked == true)
                    {
                        yetki = "Yönetici";
                    }
                    else if (radioButton4.Checked == true)
                    {
                        yetki = "Kullanıcı";
                    }
                    if (radioButton1.Checked == true)
                    {
                        cinsiyet = "Kadın";
                    }
                    else if (radioButton2.Checked == true)
                    {
                        cinsiyet = "Erkek";
                    }

                    try       // Güncellemenin yapıldığı kısım
                    {
                    baglanti.Open();
                    SqlCommand guncellekomut = new SqlCommand("update Tbl_kullanicilar set ad='" + txtAdiniz.Text + "', soyad='" + txtSoyadiniz.Text + "', cinsiyet='" + cinsiyet + "', kullaniciadi='" + txtKullaniciAdiniz.Text + "', parola='" + txtParolaniz.Text + "', yetki='" + yetki + "', meslek='" + txtMesleginiz.Text + "', dogumtarihi='" + dateTimePickerDogumTarihi.Value.ToString("yyyy/MM/dd") + "' where tcno='" + txtTcNo.Text + "'", baglanti);
                    guncellekomut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Güncelleme işlemi başarıyla gerçekleştirildi.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                else
                    MessageBox.Show("Lütfen hatalı kısımları tekrar gözden geçiriniz.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
