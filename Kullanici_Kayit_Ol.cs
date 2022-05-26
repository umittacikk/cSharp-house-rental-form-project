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
    public partial class Kullanici_Kayit_Ol : Form
    {
        public Kullanici_Kayit_Ol()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-ETL1M4FA;Initial Catalog=EvKiralamaVeriTabani;Integrated Security=True");

        private void Temizle()
        {
            txtTcNo.Clear();
            txtKullaniciAdiniz.Clear();
            txtAdiniz.Clear();
            txtSoyadiniz.Clear();
            txtKullaniciAdiniz.Clear();
            txtParolaniz.Clear();
            txtMesleginiz.Clear();
            picBoxFotoEkle.Image = null;
        }

        private void Kullanici_Kayit_Ol_Load(object sender, EventArgs e)
        {
            this.Text = "Kayıt Ol";
            txtTcNo.MaxLength = 11;
            txtKullaniciAdiniz.MaxLength = 10;
            txtParolaniz.MaxLength = 10;

            txtAdiniz.CharacterCasing = CharacterCasing.Upper;
            txtSoyadiniz.CharacterCasing = CharacterCasing.Upper;

            toolTip1.SetToolTip(txtTcNo, "TC Kimlik Numarası 11 sayıdan oluşmalıdır.");

            DateTime date = DateTime.Now;
            int yil = Convert.ToInt32(date.ToString("yyyy"));
            int ay = Convert.ToInt32(date.ToString("MM"));
            int gun = Convert.ToInt32(date.ToString("dd"));

            dateTimePickerDogumTarihiniz.MinDate = new DateTime(1900, 1, 1);
            dateTimePickerDogumTarihiniz.MaxDate = new DateTime(yil - 18, ay, gun);
            dateTimePickerDogumTarihiniz.Format = DateTimePickerFormat.Short;

            radioButton1.Checked = true;

            panel1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\arkaplanresimler\\arkaplan2.png");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_Geri_Click(object sender, EventArgs e)  // geri butonu
        {
            Uye_Giris_Ekrani giris = new Uye_Giris_Ekrani();
            giris.Show();
            this.Hide();
        }

        private void btn_Cikis_Click(object sender, EventArgs e)  // çıkış butonu
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

        private void btn_Temizle_Click(object sender, EventArgs e)  // temizle butonu
        {
            Temizle();
        }

        private void btn_Ekle_Click(object sender, EventArgs e)   // fotoğraf ekleme butonu
        {
            OpenFileDialog fotografsecme = new OpenFileDialog();
            fotografsecme.Title = "Lütfen fotoğraf seçiniz.";
            fotografsecme.Filter = "PNG Formatındaki Dosyalar (*.png) | *.png";
            if (fotografsecme.ShowDialog() == DialogResult.OK)
            {
                this.picBoxFotoEkle.Image = new Bitmap(fotografsecme.OpenFile());
            }
        }

        private void txtTcNo_TextChanged(object sender, EventArgs e) // tc no error provider
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

        private void txtTcNo_KeyPress(object sender, KeyPressEventArgs e)  // tc no karakter sınırlaması
        {
            if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57 || (int)e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtAdiniz_KeyPress(object sender, KeyPressEventArgs e)  // ad kısmı karakter sınırlaması
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

        private void txtSoyadiniz_KeyPress(object sender, KeyPressEventArgs e)   // soyad kısmı karakter sınırlaması
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

        private void txtKullaniciAdiniz_KeyPress(object sender, KeyPressEventArgs e)
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

        

        private void txtMesleginiz_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btn_Kaydet_Click(object sender, EventArgs e)  // kaydet butonu
        {
            bool kayit_var_mi = false;
            bool kullanici_adi_ayni_mi = false;
            string yetki = "Kullanıcı";
            string cinsiyet = "";
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Tbl_kullanicilar where tcno='" + txtTcNo.Text + "'", baglanti);
            SqlDataReader verioku = komut.ExecuteReader();
            while (verioku.Read())
            {
                kayit_var_mi = true;
                break;
            }
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select * from Tbl_kullanicilar where kullaniciadi='" + txtKullaniciAdiniz.Text + "'", baglanti);
            SqlDataReader verioku2 = komut2.ExecuteReader();
            while (verioku2.Read())
            {
                kullanici_adi_ayni_mi = true;
                break;
            }
            baglanti.Close();

            if (kayit_var_mi == false && kullanici_adi_ayni_mi == false)
            {
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
                    lblTcNo.ForeColor = Color.Black;
                    txtTcNo.BackColor = Color.Empty;
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
                if (picBoxFotoEkle.Image == null)     // Resmin yüklenip yüklenmediğinin kontrolü
                {
                    lblFotograf.ForeColor = Color.Red;
                    btn_FotoEkle.ForeColor = Color.Red;
                }
                else
                {
                    lblFotograf.ForeColor = Color.Black;
                    btn_FotoEkle.ForeColor = Color.Black;
                }


                if (txtTcNo.Text.Length == 11 && txtTcNo.Text != "" && txtAdiniz.Text.Length >= 2 && txtAdiniz.Text != "" && txtSoyadiniz.Text.Length >= 2 && txtSoyadiniz.Text != "" && txtKullaniciAdiniz.Text != "" && txtParolaniz.Text != "" && txtMesleginiz.Text != "" && picBoxFotoEkle.Image != null)
                {
                    if (radioButton1.Checked == true)
                    {
                        cinsiyet = "Kadın";
                    }
                    else if (radioButton2.Checked == true)
                    {
                        cinsiyet = "Erkek";
                    }

                    try    // ekleme kısmı
                    {
                        baglanti.Open();
                        SqlCommand eklekomut = new SqlCommand("insert into Tbl_kullanicilar values ('" + txtTcNo.Text + "', '" + txtAdiniz.Text + "', '" + txtSoyadiniz.Text + "', '" + cinsiyet + "', '" + txtKullaniciAdiniz.Text + "', '" + txtParolaniz.Text + "', '" + yetki + "', '" + txtMesleginiz.Text + "', '" + dateTimePickerDogumTarihiniz.Value.ToString("yyyy/MM/dd") + "' )", baglanti);
                        eklekomut.ExecuteNonQuery();
                        baglanti.Close();
                        if (!Directory.Exists(Application.StartupPath + "\\kullaniciresimler"))
                        {
                            Directory.CreateDirectory(Application.StartupPath + "\\kullaniciresimler");
                            picBoxFotoEkle.Image.Save(Application.StartupPath + "\\kullaniciresimler\\" + txtTcNo.Text + ".png");
                            MessageBox.Show("Kayıt başarıyla oluşturuldu", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            picBoxFotoEkle.Image.Save(Application.StartupPath + "\\kullaniciresimler\\" + txtTcNo.Text + ".png");
                            MessageBox.Show("Kayıt başarıyla oluşturuldu", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception errormsg)
                    {
                        MessageBox.Show(errormsg.Message, "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglanti.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Lütfen hatalı kısımları tekrar gözden geçiriniz.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Kayıt edilmek istenen TC kimlik numarası veya kullanıcı adı daha önceden sistemimize kaydedilmiştir. Lütfen bir başka TC Kimlik numarası veya kullanıcı adı giriniz.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
