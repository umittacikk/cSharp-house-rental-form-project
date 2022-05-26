using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ev_KiralamaOtomasyonu
{
    public partial class Baslangic_Ekrani : Form
    {
        public Baslangic_Ekrani()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Giriş";

            
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\arkaplanresimler\\m2logo.png");
            pictureBox1.BackColor = Color.Transparent;

            panel1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\arkaplanresimler\\arkaplan.jpg");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void button1_Click(object sender, EventArgs e)   // devam et butonu
        {
            Uye_Giris_Ekrani giris = new Uye_Giris_Ekrani();
            giris.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)   // çıkış yap butonu
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
    }
}
