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
    public partial class misafirler : Form
    {
        public misafirler()
        {
            InitializeComponent();
        }

        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        void misafirliste()  // misafirler ADINDA METHOT OLUŞTURULDU.
        {
            // METHODUN BAĞLANTI TANIMLARI VE SORGUSU TANIMLANDI.
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb");
            da = new OleDbDataAdapter("SElect * from rezervasyon", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "rezervasyon");
            dataGridView1.DataSource = ds.Tables["rezervasyon"];
            con.Close();
        }
        private void misafirler_Load(object sender, EventArgs e)
        {
            // MİSAFİRLER FORMU AÇILDIĞINDA misafirlistele METHODU ÇALIŞARAK misafir TABLOSUNDAKİ KAYITLAR LİSTELENDİ
            misafirliste();

            // DATAGRID BAŞLIKLARI VE GENİŞLİKLERİ AYARLANDI.
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Rezervasyon No";
            dataGridView1.Columns[1].Width = 130;
            dataGridView1.Columns[2].HeaderText = "Oda No";
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].HeaderText = "Oda Durum";
            dataGridView1.Columns[4].Width = 120;
            dataGridView1.Columns[5].HeaderText = "Müşteri";
            dataGridView1.Columns[5].Width = 130;
            dataGridView1.Columns[6].HeaderText = "Giriş Tarihi";
            dataGridView1.Columns[7].HeaderText = "Çıkış Tarihi";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // TEXTBOX1 DE VERİ DEĞİŞTİĞİNDE COMBOBOX DA SEÇİLEN ALANLA GÖRE ARAMA YAP.
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb");
            con.Open();
            DataTable tbl = new DataTable();
            string vara, cumle;
            vara = textBox1.Text;
            cumle = "Select * from rezervasyon where "+comboBox1.Text+" like '%" + textBox1.Text + "%'";
            da = new OleDbDataAdapter(cumle, con);
            da.Fill(tbl);
            con.Close();
            dataGridView1.DataSource = tbl;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Enabled = true;
            maskedTextBox1.Enabled = true;
            maskedTextBox2.Enabled = true;
            button4.Enabled = true;
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // DATAGRİDDE SEÇİLEN DEĞERLER TEXTBOX VE MASKEDTEXTBOX LARA YAZILDI.
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            maskedTextBox2.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            DialogResult secenek = MessageBox.Show(dataGridView1.CurrentRow.Cells[1].Value.ToString() + " Nolu Rezervasyon Kaydını Silmek İstiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (secenek == DialogResult.Yes)
            {
                // müsteriler TABLOSUNDA MusteriDRM EĞER 0 DEĞERİNDEYSE OTEL REZERVASYONU YAPILMAMIŞ 1 İSE REZERVASYON YAPILMIŞ ANLAMINDA.

                // ODALAR TABLOSUNDA OdaDRM EĞER 0 DEĞERİNDEYSE O ODAYA MÜŞTERİ REZERVASYONU YAPILMAMIŞ 1 İSE REZERVASYON YAPIŞMIŞ ANLAMINDA.
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "DELETE FROM rezervasyon WHERE ID=" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "";
                string guncelle = "UPDATE odalar SET OdaDurum='Boş',OdaDRM=0 where OdaNo='" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "'";
                string musteriguncelle = "UPDATE musteri SET MusteriDRM=0 where AdiSoyadi='" + dataGridView1.CurrentRow.Cells[5].Value.ToString() + "'";
                OleDbCommand komut = new OleDbCommand(guncelle, con);
                OleDbCommand komut2 = new OleDbCommand(musteriguncelle, con);
                cmd.ExecuteNonQuery();
                komut.ExecuteNonQuery();
                komut2.ExecuteNonQuery();
                con.Close();
                misafirliste();
                MessageBox.Show("Rezervasyon Kaydı Silinmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (secenek == DialogResult.No)
            {
                MessageBox.Show("Silme İşlemi İptal Edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // BİLGİ GİRİŞİ KONTROL EDİLİYOR
            if (textBox2.Text == "" || textBox3.Text == "" || maskedTextBox1.Text == "" || maskedTextBox2.Text == "")
            {
                // EĞER BİLGİ GİRİŞİ YAPILMADIYSA MESAJ VER VE GEREKLİ ALANLARIN ARKAPLAN RENGİNİ KIRMIZI YAP.
                MessageBox.Show("Lütfen Kırmızı Alanları Doldurunuz...", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox2.BackColor = Color.Red;
                textBox3.BackColor = Color.Red;
                maskedTextBox1.BackColor = Color.Red;
                maskedTextBox2.BackColor = Color.Red;
            }
            else
            {
                // BİLGİLER GİRİLDİYSE UPDATE KOMUTU İLE YENİ BİLGİLERİ GÜNCELLE

                // odayeniguncelle SORGUSU İLE ODA NO DEĞİŞTİĞİNDE ESKİ ODA NUMARASI KULLANIMA AÇILARAK YENİ ODA NO REZERVE OLMUŞ DURUMA GELİYOR.
                con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb");
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE rezervasyon SET OdaNo='" + textBox2.Text + "',GirisTarih='" + maskedTextBox1.Text + "',CikisTarih='" + maskedTextBox2.Text + "' where ID=" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "";
                string guncelle = "UPDATE odalar SET OdaDRM=0 where OdaNo='" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "'";
                string odayeniguncelle = "UPDATE odalar SET OdaDRM=1 where OdaNo='" + textBox2.Text + "'";
                OleDbCommand komut = new OleDbCommand(guncelle, con);
                OleDbCommand komut2 = new OleDbCommand(odayeniguncelle, con);
                cmd.ExecuteNonQuery();
                komut.ExecuteNonQuery();
                komut2.ExecuteNonQuery();
                con.Close();
                misafirliste();
                textBox2.Enabled = false;
                maskedTextBox1.Enabled= false;
                maskedTextBox2.Enabled = false;
                button4.Enabled = false;

            }
        }
    }
}
