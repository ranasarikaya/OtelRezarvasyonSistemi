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
    public partial class musteriformu : Form
    {
        public musteriformu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* ----------------------------------  KAYIT GİRİŞ KONTROLLERİ ------------------------------------- */

            if (textBox1.Text == "" || textBox2.Text == "" || maskedTextBox1.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Lütfen İşaretli Alanları Doldurunuz...", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
            }
            else
            {

                /* ----------------------------------  KAYIT GİRİŞ KONTROLLERİ ------------------------------------- */

                /* ----------------------------------  CLASS ÇAĞIRMA VE DEĞİŞKEN ATAMA ------------------------------------- */
                musteriler musteri = new musteriler();
                musteri.tcno = textBox1.Text;
                musteri.adisoyadi = textBox2.Text;
                musteri.telno = maskedTextBox1.Text;
                musteri.toplamkisi = Convert.ToInt32(comboBox1.Text);
                musteri.cocuk = Convert.ToInt32(comboBox2.Text);
                musteri.aciklama = richTextBox1.Text;
                /* ----------------------------------  CLASS ÇAĞIRMA VE DEĞİŞKEN ATAMA ------------------------------------- */


                /* ----------------------------------  MÜŞTERİ KAYIT KODLARI ------------------------------------- */
                string vtyolu = "Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb";
                OleDbConnection baglanti = new OleDbConnection(vtyolu);
                baglanti.Open();
                // TC NO SU DAHA ÖNCE AÇILMIŞ MI DİYE KONTROL EDİYOR
                string mdurum = "SELECT TCNO FROM musteri WHERE TCNO='" + textBox1.Text + "'";
                OleDbCommand odrm = new OleDbCommand(mdurum, baglanti);
                OleDbDataReader dr = odrm.ExecuteReader();
                if (dr.HasRows)
                {
                    MessageBox.Show(textBox1.Text + "TC Nolu Kayıt Zaten Daha Önce Açılmış.Lütfen Başka Bir TC NO Yazınız...!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //----------------------------------------------------
                else
                {
                    string ekle = "insert into musteri(AdiSoyadi,TCNO,TELNO,ToplamKisi,Cocuk,Aciklama,KayitTarihi,MusteriDRM) values (@adisoyadi,@tcno,@telno,@toplamkisi,@cocuk,@aciklama,@kayittarih,@musteridrm)";
                    OleDbCommand komut = new OleDbCommand(ekle, baglanti);
                    komut.Parameters.AddWithValue("@adisoyadi", musteri.adisoyadi);
                    komut.Parameters.AddWithValue("@tcno", musteri.tcno);
                    komut.Parameters.AddWithValue("@telno", musteri.telno);
                    komut.Parameters.AddWithValue("@toplamkisi", musteri.toplamkisi);
                    komut.Parameters.AddWithValue("@cocuk", musteri.cocuk);
                    komut.Parameters.AddWithValue("@aciklama", musteri.aciklama);
                    komut.Parameters.AddWithValue("@kayittarih", textBox3.Text);
                    komut.Parameters.AddWithValue("@musteridrm", '0');
                    komut.ExecuteNonQuery();
                    MessageBox.Show(musteri.adisoyadi + " Adlı Müşteri Kaydı Tamamlanmıştır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }               
                /* ----------------------------------  MÜŞTERİ KAYIT KODLARI ------------------------------------- */
            }
        }
        private void musteriformu_Load(object sender, EventArgs e)
        {
            textBox3.Text = DateTime.Now.ToShortDateString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Kaydetmeden Çıkmak İstiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (secenek == DialogResult.Yes)
            {
                this.Close();
            }
            else if (secenek == DialogResult.No)
            {
                
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
