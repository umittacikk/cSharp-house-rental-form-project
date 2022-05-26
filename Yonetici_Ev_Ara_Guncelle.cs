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
    public partial class Yonetici_Ev_Ara_Guncelle : Form
    {
        public Yonetici_Ev_Ara_Guncelle()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-ETL1M4FA;Initial Catalog=EvKiralamaVeriTabani;Integrated Security=True");
        private void Temizle()
        {
            txtIlanNo.Clear();
            richTxtAciklama.Clear();
            textBox6.Clear();
            textBox7.Clear();
            comboBoxIl.SelectedIndex = -1;
            comboBoxIlce.SelectedIndex = -1;
            txtMetrekare.Clear();
            txtOdaSayisi.Clear();
            txtSalonSayisi.Clear();
            txtBanyoSayisi.Clear();
            comboBoxIsitmaTuru.SelectedIndex = -1;
            txtFiyat.Text = "0";
        }
        private void Ev_Ara_Guncelle_Load(object sender, EventArgs e)
        {
            this.Text = "Daire Bilgilerini Güncelle";          
            
            txtIlanNo.MaxLength = 11;
            richTxtAciklama.MaxLength = 255;
            txtFiyat.Text = "0";   // fiyat ilk değer olarak 0 atandı ileride hata vermemesi için
            lblKullaniciAdSoyad.Text = Uye_Giris_Ekrani.ad + " " + Uye_Giris_Ekrani.soyad;

            toolTip1.SetToolTip(this.txtIlanNo, "Ev İlan Numarası 11 haneli bir sayı olmalıdır.");

            try   // giriş yapan kişinin resmi
            {
                picBoxKullaniciFoto.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Uye_Giris_Ekrani.tcno + ".png");
            }
            catch 
            {
                picBoxKullaniciFoto.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\nouser.png");               
            }


            panel1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\arkaplanresimler\\arkaplan2.png");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;

            try   //il combobox'ına illeri getirir
            {
                baglanti.Open();
                SqlCommand combosorgu = new SqlCommand("select il from Tbl_iller", baglanti);
                SqlDataReader iloku = combosorgu.ExecuteReader();
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

            DateTime date = DateTime.Now;
            int yil = Convert.ToInt32(date.ToString("yyyy"));
            int ay = Convert.ToInt32(date.ToString("MM"));
            int gun = Convert.ToInt32(date.ToString("dd"));
            dateTimePickerIlanTarihi.MinDate = new DateTime(1900, 1, 1);
            dateTimePickerIlanTarihi.MaxDate = new DateTime(yil, ay, gun);
            dateTimePickerIlanTarihi.Format = DateTimePickerFormat.Short;
            dateTimePickerIlanTarihi.CustomFormat = "yyyy-MM-dd";

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)   // seçilen ile ait ilçeleri getirme
        {
            try
            {
                comboBoxIlce.Items.Clear();
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

        private void textBox1_TextChanged(object sender, EventArgs e)   // ilan numarası 11 haneden az girilirse errorprovider
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)    // ilan no kısmı kısıtlamaları
        {
            if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57 || (int)e.KeyChar == 8)   // ilan no kısmında sadece rakamlara ve backspace'e basmasına izin verildi
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)      // açıklama kısmı kısıtlamaları
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

            if (char.IsLetter(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)   // açıklama kısmında sadece harf, rakam, backspace ve space'e basması sağlandı
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)       // metrekare kısmı karakter kısıtlamaları
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)    // oda sayısı kısmı karakter kısıtlamaları
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)   // salon sayısı kısmı karakter kısıtlamaları
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

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)     // banyo sayısı kısmı karakter kısıtlamaları
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

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)      // fiyat kısmı karakter kısıtlamaları
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

        

        private void button3_Click(object sender, EventArgs e)    // kayıt arama butonu
        {
            bool kayit_durum = false;
            if (txtIlanNo.Text.Length == 11)
            {
                baglanti.Open();
                SqlCommand arasorgu = new SqlCommand("select * from Tbl_evler where ilanno='" + txtIlanNo.Text + "'", baglanti);
                SqlDataReader verioku = arasorgu.ExecuteReader();
                while (verioku.Read())
                {
                    kayit_durum = true;
                    
                    try   // kaydı aranan evin resmi gelecek
                    {
                        picBoxEvFoto.Image = Image.FromFile(Application.StartupPath + "\\evresimler\\" + verioku.GetValue(0) + ".png");
                    }
                    catch  // resmi yoksa nohome.png gelecek
                    {
                        picBoxEvFoto.Image = Image.FromFile(Application.StartupPath + "\\evresimler\\nohome.png");                                                
                    }

                    richTxtAciklama.Text = verioku.GetValue(1).ToString();
                    textBox6.Text = verioku.GetValue(2).ToString();
                    textBox7.Text = verioku.GetValue(3).ToString();
                    txtMetrekare.Text = verioku.GetValue(4).ToString();
                    txtOdaSayisi.Text = verioku.GetValue(5).ToString();
                    txtSalonSayisi.Text = verioku.GetValue(6).ToString();
                    txtBanyoSayisi.Text = verioku.GetValue(7).ToString();
                    comboBoxIsitmaTuru.Text = verioku.GetValue(8).ToString();
                    txtFiyat.Text = verioku.GetValue(9).ToString();
                    dateTimePickerIlanTarihi.Text = verioku.GetValue(10).ToString();
                    txtIlanNo.Enabled = false;
                    break;
                }
                if (kayit_durum == false)
                {
                    MessageBox.Show("Aradığınız ev ilanı kaydı sistemimizde bulunmamaktadır.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                baglanti.Close();
            }
            else
            {
                MessageBox.Show("Lütfen Ev İlanı Numarası'nı 11 haneli bir sayı olacak şekilde giriniz", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)    // güncelleme butonu
        {
            if (txtIlanNo.Text == "")
            {
                lblIlanNo.ForeColor = Color.Red;
                txtIlanNo.BackColor = Color.Red;
            }
            else
            {
                lblIlanNo.ForeColor = Color.Black;
                txtIlanNo.BackColor = Color.Empty;
            }
            if (richTxtAciklama.Text == "")
            {
                lblAciklama.ForeColor = Color.Red;
                richTxtAciklama.BackColor = Color.Red;
            }
            else
            {
                lblAciklama.ForeColor = Color.Black;
                richTxtAciklama.BackColor = Color.Empty;
            }
            if (comboBoxIl.Text == "")
            {
                lblIl.ForeColor = Color.Red;
                comboBoxIl.BackColor = Color.Red;
            }
            else
            {
                lblIl.ForeColor = Color.Black;
                comboBoxIl.BackColor = Color.Empty;
            }
            if (comboBoxIlce.Text == "")
            {
                lblIlce.ForeColor = Color.Red;
                comboBoxIlce.BackColor = Color.Red;
            }
            else
            {
                lblIlce.ForeColor = Color.Black;
                comboBoxIlce.BackColor = Color.Empty;
            }

            if (txtMetrekare.Text == "" || int.Parse(txtMetrekare.Text) < 0)
            {
                lblMetrekare.ForeColor = Color.Red;
                txtMetrekare.BackColor = Color.Red;
            }
            else
            {
                lblMetrekare.ForeColor = Color.Black;
                txtMetrekare.BackColor = Color.Empty;
            }

            if (txtOdaSayisi.Text == "" || int.Parse(txtOdaSayisi.Text) < 0)
            {
                lblOdaSayisi.ForeColor = Color.Red;
                txtOdaSayisi.BackColor = Color.Red;
            }
            else
            {
                lblOdaSayisi.ForeColor = Color.Black;
                txtOdaSayisi.BackColor = Color.Empty;
            }
            if (txtSalonSayisi.Text == "" || int.Parse(txtSalonSayisi.Text) < 0)
            {
                lblSalonSayisi.ForeColor = Color.Red;
                txtSalonSayisi.BackColor = Color.Red;
            }
            else
            {
                lblSalonSayisi.ForeColor = Color.Black;
                txtSalonSayisi.BackColor = Color.Empty;
            }
            if (txtBanyoSayisi.Text == "" || int.Parse(txtBanyoSayisi.Text) < 0)
            {
                lblBanyoSayisi.ForeColor = Color.Red;
                txtBanyoSayisi.BackColor = Color.Red;
            }
            else
            {
                lblBanyoSayisi.ForeColor = Color.Black;
                txtBanyoSayisi.BackColor = Color.Empty;
            }
            if (comboBoxIsitmaTuru.Text == "")
            {
                lblIsitmaTuru.ForeColor = Color.Red;
                comboBoxIsitmaTuru.BackColor = Color.Red;
            }
            else
            {
                lblIsitmaTuru.ForeColor = Color.Black;
                comboBoxIsitmaTuru.BackColor = Color.Empty;
            }
            if (txtFiyat.Text == "" || int.Parse(txtFiyat.Text) < 0)
            {
                lblFiyat.ForeColor = Color.Red;
                txtFiyat.BackColor = Color.Red;
            }
            else
            {
                lblFiyat.ForeColor = Color.Black;
                txtFiyat.BackColor = Color.Empty;
            }


            if ( txtIlanNo.Text != "" && richTxtAciklama.Text != "" && comboBoxIl.Text != "" && comboBoxIlce.Text != "" && txtMetrekare.Text != "" && int.Parse(txtMetrekare.Text) >= 0 &&  txtOdaSayisi.Text != "" && int.Parse(txtOdaSayisi.Text) >= 0 && txtSalonSayisi.Text != "" && int.Parse(txtSalonSayisi.Text) >= 0 && txtBanyoSayisi.Text != "" && int.Parse(txtBanyoSayisi.Text) >= 0 && comboBoxIsitmaTuru.Text != "" && txtFiyat.Text != "" && int.Parse(txtFiyat.Text) >= 0)
            {
                try
                {
                    baglanti.Open();
                    SqlCommand guncellesorgu = new SqlCommand("update Tbl_evler set aciklama='" + richTxtAciklama.Text + "', il='" + comboBoxIl.Text + "', ilce='" + comboBoxIlce.Text + "', metrekare='" + txtMetrekare.Text + "', oda='" + txtOdaSayisi.Text + "', salon='" + txtSalonSayisi.Text + "', banyo='" + txtBanyoSayisi.Text + "', isitma='" + comboBoxIsitmaTuru.Text + "', fiyat='" + int.Parse(txtFiyat.Text) + "', ilantarihi='" + dateTimePickerIlanTarihi.Value.ToString("yyyy/MM/dd") + "' where ilanno='" + txtIlanNo.Text + "'", baglanti);
                    guncellesorgu.ExecuteNonQuery();
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
                    SqlCommand guncellesorgu2 = new SqlCommand("update Tbl_kiralananEvler set ilanno='" + txtIlanNo.Text + "', il='" + comboBoxIl.Text + "', ilce='" + comboBoxIlce.Text + "' where ilanno='" + txtIlanNo.Text + "'", baglanti);
                    guncellesorgu2.ExecuteNonQuery();
                    baglanti.Close();
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

        private void button1_Click(object sender, EventArgs e)   // geri butonu
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

        private void picBoxEvFoto_Click(object sender, EventArgs e)
        {

        }

        private void btn_Temizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }
    }
}
