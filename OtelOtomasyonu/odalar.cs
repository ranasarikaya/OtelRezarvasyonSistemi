using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace OtelOtomasyonu
{
    public partial class odakayit : Form
    {
        public odakayit()
        {
            InitializeComponent();
        }

       
        private void odakayit_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Lütfen Tüm Oda Bilgilerini Eksiksiz Doldurunuz...!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //  odacls CLASS IN ODALAR OLARAK TANIMLADIK VE KULLANIMA AÇTIK
                odacls odalar = new odacls();
                odalar.odano = Convert.ToInt32(textBox1.Text); // ODANO YU INT DEĞERİNDE TANIMLADIĞIMIZ İÇİN CONVER METODUYLA TEXTBOXTAKİ VERİYİ INT DEĞERİNE ÇEVİREREK ODANO DEĞİŞKENİNE ALDIK
                odalar.odakat = textBox2.Text;
                odalar.odadurum = comboBox1.Text;

                string vtyolu = "Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb";
                OleDbConnection baglanti = new OleDbConnection(vtyolu);
                baglanti.Open();
                // ODA NO SU DAHA ÖNCE AÇILMIŞ MI DİYE KONTROL EDİYOR
                string odurum = "SELECT OdaNo FROM odalar WHERE OdaNo='" + textBox1.Text +"'";
                OleDbCommand odrm = new OleDbCommand(odurum, baglanti);
                OleDbDataReader dr = odrm.ExecuteReader();
                if (dr.HasRows) // ODA NUMARASI VAR MI DİYE KONTROL EDİYOR.
                {
                    // EĞER ODA NO DAHA ÖNCEDEN TANIMLANDIYSA
                    MessageBox.Show(textBox1.Text + "Nolu Oda Zaten Daha Önce Açılmış.Lütfen Başka Bir Oda No Yazınız...!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //----------------------------------------------------
                else
                {
                    // EĞER ODA NO YOKSA YENİ ODA KAYDI YAPILIYOR.
                    string ekle = "insert into odalar(OdaNo,OdaKat,OdaDurum,OdaDRM) values (@odano,@odakat,@odadurum,@odadrm)";
                    OleDbCommand komut = new OleDbCommand(ekle, baglanti);
                    komut.Parameters.AddWithValue("@odano", odalar.odano);
                    komut.Parameters.AddWithValue("@odakat", odalar.odakat);
                    komut.Parameters.AddWithValue("@odadurum", odalar.odadurum);
                    komut.Parameters.AddWithValue("@odadrm", "0");
                    komut.ExecuteNonQuery();
                    MessageBox.Show(textBox1.Text + "Nolu Oda Kaydı Tamamlanmıştır", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
