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
    public partial class Yonetici_Ev_Sil : Form
    {
        public Yonetici_Ev_Sil()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-ETL1M4FA;Initial Catalog=EvKiralamaVeriTabani;Integrated Security=True");

        private void Ev_ilani_temizle()
        {
            txtIlanNo.Clear();
        }

        private void Ev_Sil_Load(object sender, EventArgs e)
        {
            this.Text = "Daire Kaydı Sil";            

            lblKullaniciAdSoyad.Text = Uye_Giris_Ekrani.ad + " " + Uye_Giris_Ekrani.soyad;

            txtIlanNo.MaxLength = 11;
            toolTip1.SetToolTip(this.txtIlanNo, "Ev İlan Numarası 11 haneli bir sayı olmalıdır.");

            

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

        private void textBox1_TextChanged(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)   // temizle butonu
        {
            Ev_ilani_temizle();
        }

        private void button3_Click(object sender, EventArgs e)    // sil butonu
        {
            if (txtIlanNo.Text.Length == 11)
            {
                bool kayit_durum = false;   
                baglanti.Open();
                SqlCommand sorgu = new SqlCommand("select * from Tbl_evler where ilanno='" + txtIlanNo.Text + "'", baglanti);
                SqlDataReader verioku = sorgu.ExecuteReader();
                while (verioku.Read())
                {
                    kayit_durum = true;
                    baglanti.Close();
                    baglanti.Open();
                    SqlCommand silmesorgusu = new SqlCommand("delete from Tbl_evler where ilanno='" + txtIlanNo.Text + "'", baglanti);
                    silmesorgusu.ExecuteNonQuery();                    
                    baglanti.Close();
                    baglanti.Open();
                    SqlCommand silmesorgusu2 = new SqlCommand("delete from Tbl_kiralananEvler where ilanno='" + txtIlanNo.Text + "'", baglanti);
                    silmesorgusu2.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Silme işlemi başarıyla gerçekleştirildi.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (File.Exists(Application.StartupPath + "\\evresimler\\" + txtIlanNo.Text + ".png"))
                    {
                        File.Delete(Application.StartupPath + "\\evresimler\\" + txtIlanNo.Text + ".png");
                    }
                    break;
                }                
                if (kayit_durum == false)
                {
                    MessageBox.Show("Aradığınız ilan numarasına ait herhangi bir kayıt sistemimizde bulunmamaktadır.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                baglanti.Close();
            }
            else
            {
                MessageBox.Show("Lütfen Ev İlan Numarası'nın 11 haneli olduğundan emin olunuz.", "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
