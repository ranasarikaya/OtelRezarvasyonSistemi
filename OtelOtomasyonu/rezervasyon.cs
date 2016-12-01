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
    public partial class rezervasyon : Form
    {
        public rezervasyon()
        {
            InitializeComponent();
        }

        private void rezervasyon_Load(object sender, EventArgs e)
        {
            string vtyolu = "Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb";
            OleDbConnection baglanti = new OleDbConnection(vtyolu);
            baglanti.Open();
            // KAYITLI ODALAR COMBOBOX A YAZILIYOR
            OleDbCommand cmd = new OleDbCommand("SELECT OdaNo FROM odalar WHERE OdaDRM='0'", baglanti);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                // EĞER ODANO VARSA DEĞERLERİ COMBOBOXA YAZDIR
                comboBox1.Items.Add(dr["OdaNo"]);
            }
            // KAYITLI MÜŞTERİLER COMBOBOX A YAZILIYOR
            OleDbCommand cmdD = new OleDbCommand("SELECT AdiSoyadi FROM musteri WHERE MusteriDRM='0'", baglanti);
            OleDbDataReader drR = cmdD.ExecuteReader();
            while (drR.Read())
            {
                // EĞER MÜŞTERİ KAYDI VARSA DEĞERLERİ COMBOBOXA YAZDIR
                comboBox3.Items.Add(drR["AdiSoyadi"]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "" || maskedTextBox1.Text == "" ||maskedTextBox2.Text == "")
            {
                MessageBox.Show("Lütfen Tüm Alanları Eksiksiz Doldurunuz...", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            { 
            string vtyolu = "Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb";
            OleDbConnection baglanti = new OleDbConnection(vtyolu);
            baglanti.Open();
            string ekle = "INSERT INTO rezervasyon(RezNo,OdaNo,OdaDurum,MAdiSoyadi,GirisTarih,CikisTarih) values (@rezno,@odano,@odadurum,@madisoyadi,@giris,@cikis)";
            string guncelle = "UPDATE odalar SET OdaDurum='" + comboBox2.Text + "',OdaDRM=1 where OdaNo='" + comboBox1.Text + "'";
            string musteriguncelle = "UPDATE musteri SET MusteriDRM=1 where AdiSoyadi='" + comboBox3.Text + "'";
            OleDbCommand komut = new OleDbCommand(ekle, baglanti);
            OleDbCommand komut2 = new OleDbCommand(guncelle, baglanti);
            OleDbCommand komut3 = new OleDbCommand(musteriguncelle, baglanti);
            komut.Parameters.AddWithValue("@rezno", textBox1.Text);
            komut.Parameters.AddWithValue("@odano", comboBox1.Text);
            komut.Parameters.AddWithValue("@odadurum", comboBox2.Text);
            komut.Parameters.AddWithValue("@madisoyadi", comboBox3.Text);
            komut.Parameters.AddWithValue("@giris", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@cikis", maskedTextBox2.Text);
            komut.ExecuteNonQuery();
            komut2.ExecuteNonQuery();
            komut3.ExecuteNonQuery();
            MessageBox.Show(comboBox1.Text + " Nolu Odaya" + comboBox3.Text + " Adlı Müşteri Rezervasyonu Yapılmıştır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show(" Kaydetmeden Çıkmak İstiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (secenek == DialogResult.Yes)
            {
                this.Close();
            }
            else if (secenek == DialogResult.No)
            {
                
            }
        }
    }
}
