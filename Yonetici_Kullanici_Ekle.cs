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
using System.IO;                // resim ekleme işlemleri için

namespace Ev_KiralamaOtomasyonu
{
    public partial class Yonetici_Kullanici_Ekle : Form
    {
        public Yonetici_Kullanici_Ekle()
        {
            InitializeComponent();
        }


        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-ETL1M4FA;Initial Catalog=EvKiralamaVeriTabani;Integrated Security=True");



        private void kullanici_listele()
        {
            try
            {
                baglanti.Open();
                SqlDataAdapter listele = new SqlDataAdapter("select tcno AS[TC KİMLİK NO], ad AS[ADI], soyad AS[SOYADI], cinsiyet AS[CİNSİYETİ], kullaniciadi AS[KULLANICI ADI], parola AS[PAROLA], yetki AS[YETKİ], meslek AS[MESLEK], dogumtarihi AS[DOĞUM TARİHİ] from Tbl_kullanicilar order by ad ASC", baglanti);
                DataSet hafiza = new DataSet();
                listele.Fill(hafiza);
                dataGridView1.DataSource = hafiza.Tables[0];
                baglanti.Close();
            }
            catch (Exception errormsg)
            {
                MessageBox.Show(errormsg.Message, "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }
        }


        public void Kullanici_Ekle_Temizle()   // textboxları ve picturebox'ı temizleme butonu
        {

            txtTcNo.Clear();
            txtAdiniz.Clear();
            txtSoyadiniz.Clear();
            txtKullaniciAdiniz.Clear();
            txtParolaniz.Clear();
            txtMesleginiz.Clear();
            picBoxFotoEkle.Image = null;

        }
        private void Kullanici_Ekle_Load(object sender, EventArgs e)
        {
            this.Text = "Kullanıcı Ekle";

            kullanici_listele();
            
            lblKullaniciAdSoyad.Text = Uye_Giris_Ekrani.ad + " " + Uye_Giris_Ekrani.soyad;
            txtTcNo.MaxLength = 11;     // TC Kimlik Numarası sınırlaması
            txtKullaniciAdiniz.MaxLength = 10;     // Kullanıcı adı 10 karakterden fazla olmasın
            txtParolaniz.MaxLength = 10;     // Şifre 10 karakterden fazla olmasın

            txtAdiniz.CharacterCasing = CharacterCasing.Upper;    // Ad kısmına yazılan harfleri büyük harfe çevirir
            txtSoyadiniz.CharacterCasing = CharacterCasing.Upper;    // Soyad kısmına yazılan harfleri büyük harfe çevirir

            toolTip1.SetToolTip(this.txtTcNo, "TC Kimlik Numarası 11 sayıdan oluşmalıdır.");    // textbox1'in üstüne gelince yazı olarak çıkar

            DateTime date = DateTime.Now;                           // bugünün tarihini aldı
            int yil = Convert.ToInt32(date.ToString("yyyy"));
            int ay = Convert.ToInt32(date.ToString("MM"));
            int gun = Convert.ToInt32(date.ToString("dd"));

            dateTimePickerDogumTarihiniz.MinDate = new DateTime(1900, 1, 1);    // en düşük tarih
            dateTimePickerDogumTarihiniz.MaxDate = new DateTime(yil - 18, ay, gun);     // en yüksek tarih bugünden 18 sene öncesi, bu ay ve bugün
            dateTimePickerDogumTarihiniz.Format = DateTimePickerFormat.Short;

            radioButton1.Checked = true;   // ilk başta seçili olmasını sağladık boş kalmasın diye
            radioButton3.Checked = true;   // ilk başta seçili olmasını sağladık boş kalmasın diye

            try  // giriş yapan kişinin resminin getirilmesi için
            {
                picBoxKullaniciFoto.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Uye_Giris_Ekrani.tcno + ".png");    // Kullanici.Giris formundan giriş yapılan tc ile aynı isimde olan resmi getir
            }
            catch
            {
                picBoxKullaniciFoto.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\nouser.png");

            }

            panel2.BackgroundImage = Image.FromFile(Application.StartupPath + "\\arkaplanresimler\\arkaplan2.png");
            panel2.BackgroundImageLayout = ImageLayout.Stretch;

        }

        private void button4_Click(object sender, EventArgs e)     // temizle butonu 
        {
            Kullanici_Ekle_Temizle();
        }

        private void button1_Click(object sender, EventArgs e)    // geri butonu
        {
            Yonetici anamenudon = new Yonetici();
            anamenudon.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)     // çıkış butonu
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

        private void textBox1_TextChanged(object sender, EventArgs e)      // TC Kimlik Numarası 11 sayıdan azsa errorprovider kısmı
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)    // TC Kimlik Numarası karakter sınırlama
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)  // ad kısmı karakter sınırlama
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)    // soyadı kısmı karakter sınırlama
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }     // boş 

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)    // kullanıcı adı karakter sınırlama
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

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)   // meslek kısmı karakter sınırlama
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



        private void button3_Click(object sender, EventArgs e)      // kaydetme butonu 
        {
            string yetki = "";
            string cinsiyet = "";
            bool kayit_var_mi = false;

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Tbl_kullanicilar where tcno='" + txtTcNo.Text + "'", baglanti);
            SqlDataReader verioku = komut.ExecuteReader();
            while (verioku.Read())
            {
                kayit_var_mi = true;
                break;
            }
            baglanti.Close();

            if (kayit_var_mi == false)
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



                    try       // Eklemenin yapıldığı kısım
                    {
                        baglanti.Open();
                        SqlCommand eklekomut = new SqlCommand("insert into Tbl_Kullanicilar values ('" + txtTcNo.Text + "', '" + txtAdiniz.Text + "', '" + txtSoyadiniz.Text + "', '" + cinsiyet + "' , '" + txtKullaniciAdiniz.Text + "' , '" + txtParolaniz.Text + "' , '" + yetki + "' , '" + txtMesleginiz.Text + "' , '" + dateTimePickerDogumTarihiniz.Value.ToString("yyyy/MM/dd") + "' )", baglanti);
                        eklekomut.ExecuteNonQuery();
                        baglanti.Close();
                        if (!Directory.Exists(Application.StartupPath + "\\kullaniciresimler"))    // kullaniciresimler dosyası yoksa
                        {
                            Directory.CreateDirectory(Application.StartupPath + "\\kullaniciresimler");   // dosyayı oluştur
                                                                                                          // varsa 
                            picBoxFotoEkle.Image.Save(Application.StartupPath + "\\kullaniciresimler\\" + txtTcNo.Text + ".png");   // picturebox2 deki resmi kullaniciresimler dosyasına png olarak kaydet. ismi de textbox1 e yazılan tc olsun

                            MessageBox.Show("Kayıt başarıyla oluşturuldu", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            picBoxFotoEkle.Image.Save(Application.StartupPath + "\\kullaniciresimler\\" + txtTcNo.Text + ".png");   // picturebox2 deki resmi kullaniciresimler dosyasına png olarak kaydet. ismi de textbox1 e yazılan tc olsun
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
                    MessageBox.Show("Lütfen hatalı kısımları tekrar gözden geçiriniz.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Kayıt edilmek istenen TC kimlik numarası daha önceden sistemimize kaydedilmiştir. Lütfen bir başka TC Kimlik numarası veya kullanıcı adı giriniz.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button5_Click(object sender, EventArgs e)  // resim ekleme butonu
        {
            OpenFileDialog fotografsecme = new OpenFileDialog();
            fotografsecme.Title = "Lütfen fotoğraf seçiniz.";
            fotografsecme.Filter = "PNG Formatındaki Dosyalar (*.png) | *.png";
            if (fotografsecme.ShowDialog() == DialogResult.OK)
            {
                this.picBoxFotoEkle.Image = new Bitmap(fotografsecme.OpenFile());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            kullanici_listele();
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
