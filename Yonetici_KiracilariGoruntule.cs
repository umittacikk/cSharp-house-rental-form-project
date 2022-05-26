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
    public partial class Yonetici_KiracilariGoruntule : Form
    {
        public Yonetici_KiracilariGoruntule()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-ETL1M4FA;Initial Catalog=EvKiralamaVeriTabani;Integrated Security=True");

        private void Kiracilari_Goruntule()
        {
            try
            {
                baglanti.Open();
                SqlDataAdapter kiracigoruntule_sorgu = new SqlDataAdapter("select tcno AS[KİRACININ TC KİMLİK NUMARASI], ilanno AS[İLAN NUMARASI], il AS[İL], ilce AS[İLÇE], kiralayan_ad AS[KİRACININ ADI], kiralayan_soyad AS[KİRACININ SOYADI], sozlesme_bitis AS[SÖZLEŞME BİTİŞ TARİHİ] from Tbl_kiralananEvler order by ilanno ASC", baglanti);
                DataSet hafiza = new DataSet();
                kiracigoruntule_sorgu.Fill(hafiza);
                dataGridView1.DataSource = hafiza.Tables[0];
                baglanti.Close();
            }
            catch (Exception errormsg)
            {
                MessageBox.Show(errormsg.Message, "Metrekare Emlak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }
        }

        

        private void Yonetici_KiracilariGoruntule_Load(object sender, EventArgs e)
        {
            this.Text = "Kiracıların Listesi";

            Kiracilari_Goruntule();

            panel1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\arkaplanresimler\\arkaplan2.png");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            
          
        }

        private void btn_Cikis_Click(object sender, EventArgs e)    // çıkış butonu
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

        private void btn_Geri_Click(object sender, EventArgs e)
        {
            Yonetici anamenudon = new Yonetici();
            anamenudon.Show();
            this.Hide();
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
