using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelOtomasyonu
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            musteriformu musteriformu = new musteriformu();
            musteriformu.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            musterilistesi musterilistesi = new musterilistesi();
            musterilistesi.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            odakayit odakayit = new odakayit();
            odakayit.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            rezervasyon rez = new rezervasyon();
            rez.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            odalistesi odalistesi = new odalistesi();
            odalistesi.Show();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            misafirler misafirler = new misafirler();
            misafirler.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            // EĞER FORM1 DE YÖNETİCİ KULLANICI OTURUM AÇTIYSA LABEL1 E 1 DEĞERİ GELİYOR
            // LABEL1 E 1 DEĞERİ GELDİĞİNDE BUTTON7 VER BUTTON8 AKTİF OLUYOR.
            if (label1.Text == "1")
            {
                kullaniciayarlari kullaniciayar = new kullaniciayarlari();
                kullaniciayar.Show();
            }
            else
            {
                MessageBox.Show("Bu Ekrana Girme Yetkiniz Yok...!");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (label1.Text == "1")
            {
                otelbilgileri otelbilgi = new otelbilgileri();
                otelbilgi.Show();
            }
            else
            {
                MessageBox.Show("Bu Ekrana Girme Yetkiniz Yok...!");
            }
        }
    }
}
