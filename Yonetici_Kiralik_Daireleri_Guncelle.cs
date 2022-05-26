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
    public partial class Yonetici_Kiralik_Daireleri_Guncelle : Form
    {
        public Yonetici_Kiralik_Daireleri_Guncelle()
        {
            InitializeComponent();
        }
        
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-ETL1M4FA;Initial Catalog=EvKiralamaVeriTabani;Integrated Security=True");


        private void Evleri_Listele()
        {
            try
            {
                baglanti.Open();
                SqlDataAdapter listele = new SqlDataAdapter("select ilanno AS[İLAN NUMARASI], il AS[İL], ilce AS[İLÇE], metrekare AS[m² (NET)], oda AS[ODA SAYISI], salon AS[SALON SAYISI], banyo AS[BANYO SAYISI], isitma AS[ISITMA TÜRÜ], fiyat AS[KİRA ÜCRETİ], ilantarihi AS[İLAN TARİHİ], kiralik AS[KİRALANMA DURUMU] from Tbl_evler order by ilanno ASC", baglanti);
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
            pictureBox2.Image = null;
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
            
        }

        private void Yonetici_Kiralik_Daireleri_Guncelle_Load(object sender, EventArgs e)
        {
            this.Text = "Kiralık Daireleri Güncelle";

            Evleri_Listele();            

            txtIlanNo.MaxLength = 11;   // ilan numarası 11 sayıdan oluşacak

            toolTip1.SetToolTip(this.txtIlanNo, "Ev İlan Numarası 11 haneli bir sayıdan oluşmalıdır.");

            lblKullaniciAdSoyad.Text = Uye_Giris_Ekrani.ad + " " + Uye_Giris_Ekrani.soyad;

            try
            {
                picBoxKullaniciGiris.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Uye_Giris_Ekrani.tcno + ".png");   
            }
            catch 
            {
                picBoxKullaniciGiris.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\nouser.png");
            }


            panel1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\arkaplanresimler\\arkaplan2.png");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;

            DateTime date = DateTime.Now;                      // tarih kısmı için
            int yil = Convert.ToInt32(date.ToString("yyyy"));
            int ay = Convert.ToInt32(date.ToString("MM"));
            int gun = Convert.ToInt32(date.ToString("dd"));
            dateTimePickerIlanTarihi.MinDate = new DateTime(1900, 1, 1);
            dateTimePickerIlanTarihi.MaxDate = new DateTime(yil, ay, gun);
            dateTimePickerIlanTarihi.Format = DateTimePickerFormat.Short;
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

        private void button6_Click(object sender, EventArgs e)   // temizleme butonu
        {
            Temizle();
        }

        private void button4_Click(object sender, EventArgs e)   // listeleme butonu
        {
            Evleri_Listele();
        }

        private void button5_Click(object sender, EventArgs e)   // arama butonu
        {
            bool kayit_durum = false;
            if (txtIlanNo.Text.Length == 11)
            {
                try
                {
                    baglanti.Open();
                    SqlCommand arasorgu = new SqlCommand("select * from Tbl_evler where ilanno='" + txtIlanNo.Text + "'", baglanti);
                    SqlDataReader verioku = arasorgu.ExecuteReader();
                    while (verioku.Read())
                    {
                        kayit_durum = true;
                        try
                        {
                            pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\evresimler\\" + txtIlanNo.Text + ".png");
                        }
                        catch 
                        {
                            pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\evresimler\\nohome.png");
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
                        
                        if (verioku.GetValue(11).ToString() == "False")
                        {
                            radioButton2.Checked = true;
                        }
                        break;
                    }
                    baglanti.Close();
                    if (kayit_durum == false)
                    {
                        MessageBox.Show("Sistemimizde bu ilan numarasına sahip bir ev ilanı bulunmamaktadır.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Lütfen 11 haneli bir Ev İlan Numarası giriniz.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtIlanNo.Text.Length < 11)
            {
                errorProvider1.SetError(txtIlanNo, "Ev İlanı Numarası 11 haneli bir sayı olmalıdır.");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void button3_Click(object sender, EventArgs e)  // kira durumu güncelleme butonu
        {
            bool kiraci_var_mi = false;
            int kiralik = 0;
            try
            {                
                if (radioButton2.Checked == true)    // kiralanmadı seçilirse
                {
                    kiralik = 0;
                    baglanti.Open();          // kiracıların gösterildiği tabloda eğer bu evde kiracı varsa onu siler
                    SqlCommand kiraci_var_mi_sorgu = new SqlCommand("select * from Tbl_kiralananEvler where ilanno='" + txtIlanNo.Text + "'", baglanti);
                    SqlDataReader verioku = kiraci_var_mi_sorgu.ExecuteReader();
                    while (verioku.Read())
                    {
                        kiraci_var_mi = true;
                        break;
                    }
                    baglanti.Close();
                    if (kiraci_var_mi == true)    // kiracı varsa kiracıyı da Tbl_kiralananEvler tablosundan siler
                    {
                        try
                        {
                            baglanti.Open();
                            SqlCommand kiraci_sil_sorgu = new SqlCommand("delete from Tbl_kiralananEvler where ilanno='" + txtIlanNo.Text + "'", baglanti);
                            kiraci_sil_sorgu.ExecuteNonQuery();
                            baglanti.Close();
                        }
                        catch (Exception errormsg)
                        {
                            MessageBox.Show(errormsg.Message, "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            baglanti.Close();
                        }
                    }
                }
                baglanti.Open();
                SqlCommand guncellesorgu = new SqlCommand("update Tbl_evler set kiralik='" + kiralik + "' where ilanno='" + txtIlanNo.Text + "'", baglanti);
                guncellesorgu.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kira durumu güncelleme işlemi başarıyla gerçekleştirildi.", "Ev Kiralama Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Evleri_Listele();
            }
            catch (Exception errormsg)
            {
                MessageBox.Show(errormsg.Message, "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
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
    }
}
