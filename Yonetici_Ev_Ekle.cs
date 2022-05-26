using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;    // veritabanı için
using System.IO;            // resim ekleme işlemi için

namespace Ev_KiralamaOtomasyonu
{
    public partial class Yonetici_Ev_Ekle : Form
    {
        public Yonetici_Ev_Ekle()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-ETL1M4FA;Initial Catalog=EvKiralamaVeriTabani;Integrated Security=True");


        private void Ev_bilgilerini_temizle()    // ekrandakileri temizler
        {
            txtIlanNo.Clear();
            txtMetrekare.Clear();
            richTxtAciklama.Clear();
            comboBoxIl.SelectedIndex = -1;
            comboBoxIlce.SelectedIndex = -1;
            txtOdaSayisi.Clear();
            txtSalonSayisi.Clear();
            txtBanyoSayisi.Clear();
            comboBoxIsitmaTuru.SelectedIndex = -1;
            txtFiyat.Clear();
            picBoxEvFoto.Image = null;
            txtFiyat.Text = "0";
        }


        private void Ev_bilgilerini_listele()  // listeleme fonksiyonu
        {
            try
            {
                baglanti.Open();
                SqlDataAdapter listele = new SqlDataAdapter("select ilanno AS[İLAN NUMARASI], aciklama AS[AÇIKLAMA], il AS[İL], ilce AS[İLÇE], metrekare AS[m² (NET)], oda AS[ODA SAYISI], salon AS[SALON SAYISI], banyo AS[BANYO SAYISI], isitma AS[ISITMA TÜRÜ], fiyat AS[KİRA ÜCRETİ], ilantarihi AS[İLAN TARİHİ], kiralik AS[KİRALANMA DURUMU] from Tbl_evler order by ilanno ASC", baglanti);
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


        private void button1_Click(object sender, EventArgs e)    // Geri butonu
        {
            Yonetici anamenudon = new Yonetici();
            anamenudon.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)    // Çıkış butonu
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

        private void button5_Click(object sender, EventArgs e)   // Temizleme butonu
        {
            Ev_bilgilerini_temizle();
        }

        private void Ev_Ekle_Load(object sender, EventArgs e)
        {
            this.Text = "Daire Kaydı Ekle";
            
            Ev_bilgilerini_listele();
            txtIlanNo.MaxLength = 11;   // ilan no'ya 11'den fazla karakter giremez
            richTxtAciklama.MaxLength = 255;   // Açıklamaya 255'ten fazla karakter giremez
            txtFiyat.Text = "0";    // evin başlangıç fiyatı 0 olarak belirlendi kaydederken hata olmasın diye                     

            lblKullaniciAdSoyad.Text = Uye_Giris_Ekrani.ad + " " + Uye_Giris_Ekrani.soyad;   // giriş yapan kişinin adı

            toolTip1.SetToolTip(this.txtIlanNo, "Ev İlan Numarası 11 haneli bir sayı olmalıdır");
            
            DateTime date = DateTime.Now;                        // datetimepicker ayarları
            int yil = Convert.ToInt32(date.ToString("yyyy"));
            int ay = Convert.ToInt32(date.ToString("MM"));
            int gun = Convert.ToInt32(date.ToString("dd"));
            dateTimePickerIlanTarihi.MinDate = new DateTime(yil, ay, gun);
            dateTimePickerIlanTarihi.MaxDate = new DateTime(yil, ay, gun);
            dateTimePickerIlanTarihi.Format = DateTimePickerFormat.Short;
            dateTimePickerIlanTarihi.CustomFormat = "yyyy-MM-dd";

            try  // giriş yapan kişinin resminin getirilmesi için
            {
                picBoxKullaniciFoto.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Uye_Giris_Ekrani.tcno + ".png");    // Kullanici.Giris formundan giriş yapılan tc ile aynı isimde olan resmi getir
            }
            catch
            {
                picBoxKullaniciFoto.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\nouser.png");

            }

            panel1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\arkaplanresimler\\arkaplan2.png");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;


            try
            {
                baglanti.Open();
                SqlCommand combo1sorgu = new SqlCommand("select il from Tbl_iller", baglanti);
                SqlDataReader iloku = combo1sorgu.ExecuteReader();
                while (iloku.Read())
                {
                    comboBoxIl.Items.Add(iloku["il"].ToString());
                }
                baglanti.Close();
            }
            catch (Exception errormsg)
            {

                MessageBox.Show(errormsg.Message, "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }
            
            comboBoxIsitmaTuru.Items.Add("Kalorifer");   // Isıtma için
            comboBoxIsitmaTuru.Items.Add("Klima");
            comboBoxIsitmaTuru.Items.Add("Soba");
            comboBoxIsitmaTuru.Items.Add("Yok");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)   // il seçilince o ilin ilçelerinin geldiği kısım
        {
            comboBoxIlce.Items.Clear();  // yeni il seçimi yapıldığında eskiler silinsin diye
            try
            {
                baglanti.Open();
                SqlCommand sorgu = new SqlCommand("select ilce from Tbl_ilceler where filtre='" + comboBoxIl.SelectedItem + "'", baglanti);
                SqlDataReader ilceoku = sorgu.ExecuteReader();
                while (ilceoku.Read())
                {
                    comboBoxIlce.Items.Add(ilceoku["ilce"].ToString());
                }
                baglanti.Close();
            }
            catch (Exception errormsg)
            {
                MessageBox.Show(errormsg.Message, "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)    // Ev ilan numarası için karakter uzunluğu uyarısı
        {
            if (txtIlanNo.Text.Length < 11)
            {
                errorProvider1.SetError(txtIlanNo, "Ev İlan Numarası 11 haneli bir sayı olmalıdır.");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)   // Ev ilanı kısmında sadece sayıya ve backspace'e basabilecek
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57 || (int)e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }  // metrekare karakter sınırlandırmaları

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)   // Açıklama kısmının sınırlamaları  --> sadece harflere, sayılara, space'e ve backspace'e basabilecek
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
            if (char.IsLetter(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            {
                e.Handled = false;
            } 
            else
            {
                e.Handled = true;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)   // boş  yanlışlıkla açıldı
        {
            
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)  // Oda sayısı kısmının kısıtlamaları --> sadece sayıya ve backspace'e basabilecek
        {
            if (char.IsDigit(e.KeyChar) == true || char.IsControl(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }        

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }   // boş

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)  // Salon sayısı kısıtlamaları  --> sadece sayıya ve backspace'e basabilecek
        {
            if (char.IsDigit(e.KeyChar) == true || char.IsControl(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)   // Banyo sayısı kısıtlamaları --> sadece sayıya ve backspace'e basabilecek
        {
            if(char.IsDigit(e.KeyChar) == true || char.IsControl(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)  // Fiyat kısmı sınırlaması  --> sadece sayıya ve backspace'e basabilecek
        {
            if (char.IsDigit(e.KeyChar) == true || char.IsControl(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)    // Kaydetme butonu
        {
            
            int kiralik = 0;
            bool kayit_durum = false;
            baglanti.Open();
            SqlCommand sorgu = new SqlCommand("select * from Tbl_evler where ilanno='" + txtIlanNo.Text + "'", baglanti);
            SqlDataReader verioku = sorgu.ExecuteReader();
            while (verioku.Read())
            {
                kayit_durum = true;
                break;
            }
            baglanti.Close();
            if (kayit_durum == false)
            {
                if (txtIlanNo.Text.Length < 11)   // ilanno 11 sayıdan az girilirse
                {
                    lblIlanNo.ForeColor = Color.Red;
                    txtIlanNo.BackColor = Color.Red;
                }
                else
                {
                    lblIlanNo.ForeColor = Color.Black;
                    txtIlanNo.BackColor = Color.Empty;
                }
                if (richTxtAciklama.Text == "")   // açıklama kısmı boş bırakılırsa
                {
                    lblAciklama.ForeColor = Color.Red;
                    richTxtAciklama.BackColor = Color.Red;
                }
                else
                {
                    lblAciklama.ForeColor = Color.Black;
                    richTxtAciklama.BackColor = Color.Empty;
                }
                if (comboBoxIl.Text == "")    // il seçilmezse
                {
                    lblIl.ForeColor = Color.Red;
                    comboBoxIl.BackColor = Color.Red;
                }
                else
                {
                    lblIl.ForeColor = Color.Black;
                    comboBoxIl.BackColor = Color.Empty;
                }
                if (comboBoxIlce.Text == "")   // ilçe seçilmezse
                {
                    lblIlce.ForeColor = Color.Red;
                    comboBoxIlce.BackColor = Color.Red;
                }
                else
                {
                    lblIlce.ForeColor = Color.Black;
                    comboBoxIlce.BackColor = Color.Empty;
                }

                if (txtMetrekare.Text == "" || int.Parse(txtMetrekare.Text) < 0)   // metrekare kısmı boş bırakılırsa veya sıfırdan küçük değer girilirse
                {
                    lblMetreKare.ForeColor = Color.Red;
                    txtMetrekare.BackColor = Color.Red;
                }
                else
                {
                    lblMetreKare.ForeColor = Color.Black;
                    txtMetrekare.BackColor = Color.Empty;
                }

                if (txtOdaSayisi.Text == "" || int.Parse(txtOdaSayisi.Text) < 0)    // oda sayısı boş bırakılırsa veya sıfırdan küçük değer girilirse
                {
                    lblOdaSayisi.ForeColor = Color.Red;
                    txtOdaSayisi.BackColor = Color.Red;
                }
                else
                {
                    lblOdaSayisi.ForeColor = Color.Black;
                    txtOdaSayisi.BackColor = Color.Empty;
                }
                if (txtSalonSayisi.Text == "" || int.Parse(txtSalonSayisi.Text) < 0)   // salon sayısı yazılmazsa veya sıfırdan küçük değer girilirse
                {
                    lblSalonSayisi.ForeColor = Color.Red;
                    txtSalonSayisi.BackColor = Color.Red;
                }
                else
                {
                    lblSalonSayisi.ForeColor = Color.Black;
                    txtSalonSayisi.BackColor = Color.Empty;
                }
                if (txtBanyoSayisi.Text == "" || int.Parse(txtBanyoSayisi.Text) < 0 )    // banyo sayısı yazılmazsa veya sıfırdan küçük değer girilirse
                {
                    lblBanyoSayisi.ForeColor = Color.Red;
                    txtBanyoSayisi.BackColor = Color.Red;
                }
                else
                {
                    lblBanyoSayisi.ForeColor = Color.Black;
                    txtBanyoSayisi.BackColor = Color.Empty;
                }
                if (comboBoxIsitmaTuru.Text == "")   // ısıtma türü seçilmezse
                {
                    lblIsitmaTuru.ForeColor = Color.Red;
                    comboBoxIsitmaTuru.BackColor = Color.Red;
                }
                else
                {
                    lblIsitmaTuru.ForeColor = Color.Black;
                    comboBoxIsitmaTuru.BackColor = Color.Empty;
                }
                if (txtFiyat.Text == "" || int.Parse(txtFiyat.Text) < 0)   // fiyat kısmı boş bırakılırsa veya fiyat sıfırdan küçük girilirse
                {
                    lblFiyat.ForeColor = Color.Red;
                    txtFiyat.BackColor = Color.Red;
                }
                else
                {
                    lblFiyat.ForeColor = Color.Black;
                    txtFiyat.BackColor = Color.Empty;
                }
                if (picBoxEvFoto.Image == null)   // fotoğraf yüklenmezse
                {
                    lblFotograf.ForeColor = Color.Red;
                    btn_FotoEkle.ForeColor = Color.Red;
                }
                else
                {
                    lblFotograf.ForeColor = Color.Black;
                    btn_FotoEkle.ForeColor = Color.Black;
                }

                if (txtIlanNo.Text.Length == 11 && richTxtAciklama.Text != "" && comboBoxIl.Text != "" && comboBoxIlce.Text != "" &&  txtMetrekare.Text != "" && int.Parse(txtMetrekare.Text) >= 0 &&  txtOdaSayisi.Text != "" && int.Parse(txtOdaSayisi.Text) >= 0 && txtSalonSayisi.Text != "" && int.Parse(txtSalonSayisi.Text) >= 0 &&  txtBanyoSayisi.Text != "" && int.Parse(txtBanyoSayisi.Text) >= 0 && comboBoxIsitmaTuru.Text != "" && txtFiyat.Text != "" && int.Parse(txtFiyat.Text) >= 0 && picBoxEvFoto.Image != null)
                {

                    try
                    {
                        baglanti.Open();
                        SqlCommand eklemesorgusu = new SqlCommand("insert into Tbl_evler values ('" + txtIlanNo.Text + "', '" + richTxtAciklama.Text + "', '" + comboBoxIl.Text + "', '" + comboBoxIlce.Text + "', '" + txtMetrekare.Text + "', '" + txtOdaSayisi.Text + "', '" + txtSalonSayisi.Text + "', '" + txtBanyoSayisi.Text + "', '" + comboBoxIsitmaTuru.Text + "', '" + int.Parse(txtFiyat.Text) + "', '" + dateTimePickerIlanTarihi.Value.ToString("yyyy/MM/dd") + "', '" + kiralik + "')", baglanti);
                        eklemesorgusu.ExecuteNonQuery();
                        baglanti.Close();
                        if(!Directory.Exists(Application.StartupPath + "\\evresimler"))
                        {
                            Directory.CreateDirectory(Application.StartupPath + "\\evresimler");

                            picBoxEvFoto.Image.Save(Application.StartupPath + "\\evresimler\\" + txtIlanNo.Text + ".png");
                            MessageBox.Show("Kayıt işlemi başarıyla gerçekleştirildi.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            picBoxEvFoto.Image.Save(Application.StartupPath + "\\evresimler\\" + txtIlanNo.Text + ".png");
                            MessageBox.Show("Kayıt işlemi başarıyla gerçekleştirildi.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Lütfen hatalı kısımları tekrar kontrol ediniz.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Bu ilan numarasına ait kayıt sistemimizde bulunmaktadır.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)   // resim ekleme butonu
        {
            OpenFileDialog fotografsec = new OpenFileDialog();
            fotografsec.Title = "Lütfen ilan için fotoğraf seçiniz.";
            fotografsec.Filter = "PNG Formatındaki Dosyalar (*.png) | *.png ";
            if (fotografsec.ShowDialog() == DialogResult.OK)
            {
                this.picBoxEvFoto.Image = new Bitmap(fotografsec.OpenFile());
            }
        }

        private void button6_Click(object sender, EventArgs e)   // listeleme butonu
        {
            Ev_bilgilerini_listele();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)   // boş
        {

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
