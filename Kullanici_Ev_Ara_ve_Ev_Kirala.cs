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
    public partial class Kullanici_Ev_Ara_ve_Ev_Kirala : Form
    {
        public Kullanici_Ev_Ara_ve_Ev_Kirala()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-ETL1M4FA;Initial Catalog=EvKiralamaVeriTabani;Integrated Security=True");

        private void Ev_Ilani_Listele()
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


        private void Temizle()
        {
            picBoxEvFoto.Image = null;
            txtIlanNo.Clear();
            richTxtAciklama.Clear();
            txtIl.Clear();
            txtIlce.Clear();
            txtMetrekare.Clear();
            txtOdaSayisi.Clear();
            txtSalonSayisi.Clear();
            txtBanyoSayisi.Clear();
            txtIsitmaTuru.Clear();
            txtFiyat.Clear();
            richTxtSozlesme.Clear();
            lblGenelKosullar.Visible = false;
            richTxtSozlesme.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            btn_SozlesmeyiOnayla.Visible = false;
            lblSayfayiKaydir.Visible = false;
            panel2.Visible = false;
            txtIlanNo.Enabled = true;
        }


        private void button1_Click(object sender, EventArgs e)    // geri butonu
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

        private void Kullanici_Ev_Ara_ve_Ev_Kirala_Load(object sender, EventArgs e)
        {
            this.Text = "Ev Kirala";
            Ev_Ilani_Listele();

            lblGenelKosullar.Visible = false;
            lblSayfayiKaydir.Visible = false;
            richTxtSozlesme.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            btn_SozlesmeyiOnayla.Visible = false;
            btn_SozlesmeyiOnayla.Enabled = false;
            label2.Visible = false;
            comboBoxAy.Visible = false;
            panel2.Visible = false;

            comboBoxAy.Items.Add("1");
            comboBoxAy.Items.Add("2");
            comboBoxAy.Items.Add("3");
            comboBoxAy.Items.Add("4");
            comboBoxAy.Items.Add("5");
            comboBoxAy.Items.Add("6");
            comboBoxAy.Items.Add("7");
            comboBoxAy.Items.Add("8");
            comboBoxAy.Items.Add("9");
            comboBoxAy.Items.Add("10");
            comboBoxAy.Items.Add("11");
            comboBoxAy.Items.Add("12");
          
            txtIlanNo.MaxLength = 11;
            lblKullaniciAdSoyad.Text = Uye_Giris_Ekrani.ad + " " + Uye_Giris_Ekrani.soyad;

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

            panel2.BackgroundImage = Image.FromFile(Application.StartupPath + "\\arkaplanresimler\\arkaplan3.png");
            panel2.BackgroundImageLayout = ImageLayout.Stretch;

            toolTip1.SetToolTip(this.txtIlanNo, "Ev İlan Numarası 11 haneli bir sayıdan oluşmalıdır.");

            richTxtAciklama.ReadOnly = true;    // İlan No textbox'ı dışındaki textboxlardaki bilgileri değiştirmesin diye yazmayı engelledim
            txtIl.ReadOnly = true;
            txtIlce.ReadOnly = true;
            txtMetrekare.ReadOnly = true;
            txtOdaSayisi.ReadOnly = true;
            txtSalonSayisi.ReadOnly = true;
            txtBanyoSayisi.ReadOnly = true;
            txtIsitmaTuru.ReadOnly = true;
            txtFiyat.ReadOnly = true;

            dateTimePickerIlanTarihi.Enabled = false;
            DateTime date = DateTime.Now;    // Tarih ayarı
            int yil = Convert.ToInt32(date.ToString("yyyy"));
            int ay = Convert.ToInt32(date.ToString("MM"));
            int gun = Convert.ToInt32(date.ToString("dd"));
           
            dateTimePickerIlanTarihi.Format = DateTimePickerFormat.Short;

            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)  // ilan numarası ünlem
        {
            if (txtIlanNo.Text.Length < 11)
            {
                errorProvider1.SetError(txtIlanNo, "Ev İlan Numarası 11 haneli bir sayıdan oluşmalıdır.");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)   // ilan numarasının karakter sınırlaması
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

        private void button3_Click(object sender, EventArgs e)   // Ara butonu
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

                    try
                    {
                        picBoxEvFoto.Image = Image.FromFile(Application.StartupPath + "\\evresimler\\" + txtIlanNo.Text + ".png");
                    }
                    catch 
                    {
                        picBoxEvFoto.Image = Image.FromFile(Application.StartupPath + "\\evresimler\\nohome.png");
                    }
                    richTxtAciklama.Text = verioku.GetValue(1).ToString();
                    txtIl.Text = verioku.GetValue(2).ToString();
                    txtIlce.Text = verioku.GetValue(3).ToString();
                    txtMetrekare.Text = verioku.GetValue(4).ToString();
                    txtOdaSayisi.Text = verioku.GetValue(5).ToString();
                    txtSalonSayisi.Text = verioku.GetValue(6).ToString();
                    txtBanyoSayisi.Text = verioku.GetValue(7).ToString();
                    txtIsitmaTuru.Text = verioku.GetValue(8).ToString();
                    txtFiyat.Text = verioku.GetValue(9).ToString();
                    dateTimePickerIlanTarihi.Text = verioku.GetValue(10).ToString();
                    txtIlanNo.Enabled = false;   // ev kiralarken ilan numarasını değiştirip yanlış güncelleme yapmasın diye engelledim
                    break;
                }
                if(kayit_durum == false)
                {
                    MessageBox.Show("Aradığınız ev kaydı sistemimizde bulunmamaktadır.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                baglanti.Close();
            }
            else
            {
                MessageBox.Show("Lütfen Ev İlanı Numarasını 11 haneli olacak şekilde giriniz.");
            }
        }

        private void button5_Click(object sender, EventArgs e)   // listeleme butonu
        {
            Ev_Ilani_Listele();
        }

        private void button4_Click(object sender, EventArgs e)   // Kirala butonu
        {
            bool kiralik_durumu = false;
            if (txtIlanNo.Text.Length == 11)
            {
                try
                {
                    baglanti.Open();
                    SqlCommand kiralikara_sorgu = new SqlCommand("select * from Tbl_evler where ilanno='" + txtIlanNo.Text + "'", baglanti);
                    SqlDataReader verioku = kiralikara_sorgu.ExecuteReader();
                    while (verioku.Read())
                    {
                        if (verioku.GetValue(11).ToString() == "True")
                        {
                            kiralik_durumu = true;
                        }
                        break;
                    }
                    baglanti.Close();
                    if (kiralik_durumu == false)
                    {
                        lblGenelKosullar.Visible = true;
                        lblSayfayiKaydir.Visible = true;
                        richTxtSozlesme.Visible = true;
                        radioButton1.Visible = true;
                        radioButton2.Visible = true;
                        btn_SozlesmeyiOnayla.Visible = true;
                        label2.Visible = true;
                        comboBoxAy.Visible = true;
                        panel2.Visible = true;

                        try
                        {
                            baglanti.Open();
                            SqlCommand sozlesme_sorgu = new SqlCommand("select kosullar from Tbl_sozlesme", baglanti);
                            SqlDataReader sozlesmeoku = sozlesme_sorgu.ExecuteReader();
                            while (sozlesmeoku.Read())
                            {
                                richTxtSozlesme.Text += sozlesmeoku["kosullar"].ToString() + "\n";
                            }
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
                        MessageBox.Show("Kiralamak istediğiniz ev başkası tarafından kiralanmıştır. Lütfen başka bir ev arayınız.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Lütfen geçerli bir ev ilanı numarası giriniz.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            btn_SozlesmeyiOnayla.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            btn_SozlesmeyiOnayla.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)   // Temizle butonu
        {
            Temizle();
            txtIlanNo.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)    // Sözleşmeyi onaylama butonu
        {
            int kiralik_mi = 1;
            DateTime date = DateTime.Now;
            DateTime updated_date = date.AddMonths((int.Parse(comboBoxAy.Text)));
            try
            {
                baglanti.Open();
                SqlCommand kiralikguncelle = new SqlCommand("update Tbl_evler set kiralik='" + kiralik_mi + "' where ilanno='" + txtIlanNo.Text + "'", baglanti);
                kiralikguncelle.ExecuteNonQuery();
                baglanti.Close();
                try
                {
                    baglanti.Open();
                    SqlCommand kiraliktablo = new SqlCommand("insert into Tbl_kiralananEvler values ('" + Uye_Giris_Ekrani.tcno +  "', '" + txtIlanNo.Text  + "', '" + txtIl.Text + "', '" + txtIlce.Text + "', '" + Uye_Giris_Ekrani.ad + "', '" + Uye_Giris_Ekrani.soyad + "', '" + updated_date.ToString("yyyy/MM/dd") + "' )", baglanti);
                    kiraliktablo.ExecuteNonQuery();
                    baglanti.Close();
                }
                catch (Exception errormsg)
                {
                    MessageBox.Show(errormsg.Message, "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglanti.Close();
                }
                MessageBox.Show("Ev kiralama işleminiz başarıyla gerçekleşmiştir. Hayırlı olsun.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Ev_Ilani_Listele();
                Temizle();
            }
            catch (Exception errormsg)
            {
                MessageBox.Show(errormsg.Message, "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
                
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
