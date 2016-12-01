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
    public partial class odalistesi : Form
    {
        public odalistesi()
        {
            InitializeComponent();
        }

        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;

        void odalistele() // odalistele ADINDA YENİ BİR METHOT OLUŞTURULDU.
        {
            // BAĞLANTI AYARLARI VE SORGU TANIMLARI YAPILDI.
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb");
            da = new OleDbDataAdapter("SElect * from odalar", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "odalar");
            dataGridView1.DataSource = ds.Tables["odalar"];
            con.Close();
        }
        private void odalistesi_Load(object sender, EventArgs e)
        {
            // odalistele METHODU ÇAĞIRILIYOR.
            odalistele();
            // odalistele METHODU ÇAĞIRILIYOR.

            dataGridView1.Columns[0].Visible = false; // datagrid de alan gizleme
            dataGridView1.Columns[4].Visible = false; // datagrid de alan gizleme
            dataGridView1.Columns[1].HeaderText = "Oda Numarası"; // datagrid alan adı değiştirme
            dataGridView1.Columns[2].HeaderText = "Kat Numarası"; // datagrid alan adı değiştirme7
            dataGridView1.Columns[3].HeaderText = "Oda Durumu"; // datagrid alan adı değiştirme
            dataGridView1.Columns[1].Width = 120; // datagrid alan genişliği
            dataGridView1.Columns[2].Width = 120; // datagrid alan genişliği
            dataGridView1.Columns[3].Width = 120; // datagrid alan genişliği
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Tümünü Göster")
            {
                // EĞER COMBOBOX DAKİ DEĞER TÜMÜNÜ GÖSTERSE BÜTÜN ALANLARI GÖSTERMESİ İÇİN odalistele METHODU ÇAĞIRILIYOR.
                odalistele();
            }else

            { 
                // EĞER COMBOBOX DAN FARKLI BİR DEĞER SEÇİLDİYSE O DEĞERE AİT BİLGİLERLE DATAGRİDDE FİLTRELEME YAPILIYOR.
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb");
            da = new OleDbDataAdapter("SElect * from odalar WHERE OdaDurum='"+comboBox1.Text+"'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "odalar");
            dataGridView1.DataSource = ds.Tables["odalar"];
            con.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // TEXTBOX1 DE VERİ DEĞİŞTİRĞİNDE ARAMA SORGUSU ÇALIŞTIRIYOR. 
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb");
            con.Open();
            DataTable tbl = new DataTable();
            string vara, cumle;
            vara = textBox1.Text;
            cumle = "Select * from odalar where OdaNo like '%" + textBox1.Text + "%'";
            da = new OleDbDataAdapter(cumle, con);
            da.Fill(tbl);
            con.Close();
            dataGridView1.DataSource = tbl;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Visible = true;
            textBox3.Visible = true;
            comboBox2.Visible = true;
            button4.Visible = true;
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // DATAGRİDDE SEÇİLEN ALANLAR TEXTBOX VE COMBOBOXA AKTARILIYOR.
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // GÜNCELLEME İŞLEMİNDE ALANLARIN BOŞ MU DOLU MU OLDUĞU KONTROL EDİLİYOR. 
            if (textBox2.Text == "" || textBox3.Text == "" ||  comboBox2.Text == "")
            {
                MessageBox.Show("Lütfen Kırmızı Alanları Doldurunuz...", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                // EĞER ALANLAR BOŞSA TEXTBOX VA COMBOBOX IN ARKAPLAN RENKLERİ KIRMIZI OLARAK DEĞİŞTİRİLİYOR. 
                textBox2.BackColor = Color.Red;
                textBox3.BackColor = Color.Red;
                comboBox2.BackColor = Color.Red;
            }
            else
            {   
                con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb");
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE odalar SET OdaNo='" + textBox2.Text + "',OdaKat='" + textBox3.Text + "',OdaDurum='" + comboBox2.Text + "' where ID=" + textBox4.Text + "";
                cmd.ExecuteNonQuery();
                con.Close();
                odalistele();
                textBox3.Visible = false;
                textBox2.Visible = false;
                comboBox2.Visible = false;
                button4.Visible = false;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show(textBox2.Text + " Nolu Oda Kaydını Silmek İstiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (secenek == DialogResult.Yes)
            {
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "DELETE FROM odalar WHERE ID=" + textBox4.Text + "";
                cmd.ExecuteNonQuery();
                con.Close();
                odalistele();
                MessageBox.Show("Oda Kaydı Silinmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (secenek == DialogResult.No)
            {
                MessageBox.Show("Silme İşlemi İptal Edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
