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
    public partial class otelbilgileri : Form
    {
        public otelbilgileri()
        {
            InitializeComponent();
        }

        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        void otelbilgilistele()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb");
            da = new OleDbDataAdapter("SElect * from otelbilgileri", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "otelbilgileri");
            dataGridView1.DataSource = ds.Tables["otelbilgileri"];
            con.Close();
        }
        private void otelbilgileri_Load(object sender, EventArgs e)
        {
            otelbilgilistele();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || richTextBox1.Text == "" || maskedTextBox1.Text == "" || maskedTextBox2.Text == "")
            {
                MessageBox.Show("Lütfen Tüm Kullanıcı Bilgilerini Eksiksiz Doldurunuz...!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                otelbilgi otelbilgi = new otelbilgi();
                otelbilgi.oteladi = textBox1.Text;
                otelbilgi.adres = richTextBox1.Text;
                otelbilgi.telefon = maskedTextBox1.Text;
                otelbilgi.faks = maskedTextBox2.Text;

                string vtyolu = "Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb";
                OleDbConnection baglanti = new OleDbConnection(vtyolu);
                baglanti.Open();
                string ekle = "insert into otelbilgileri(OtelAdi,Adres,Telefon,Faks) values (@adi,@adres,@telefon,@faks)";
                OleDbCommand komut = new OleDbCommand(ekle, baglanti);
                komut.Parameters.AddWithValue("@adi", otelbilgi.oteladi);
                komut.Parameters.AddWithValue("@adres", otelbilgi.adres);
                komut.Parameters.AddWithValue("@telefon", otelbilgi.telefon);
                komut.Parameters.AddWithValue("@faks", otelbilgi.faks);
                komut.ExecuteNonQuery();
                MessageBox.Show("Otel Kaydı Tamamlanmıştır", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                otelbilgilistele();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show(" Seçili  Kaydı Silmek İstiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (secenek == DialogResult.Yes)
            {
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "DELETE FROM otelbilgileri WHERE ID=" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "";
                cmd.ExecuteNonQuery();
                con.Close();
                otelbilgilistele();
                MessageBox.Show("Kayıt Silinmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (secenek == DialogResult.No)
            {
                MessageBox.Show("Silme İşlemi İptal Edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            richTextBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            maskedTextBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || richTextBox1.Text == "" || maskedTextBox1.Text == "" || maskedTextBox2.Text == "")
            {
                MessageBox.Show("Lütfen Tüm Kullanıcı Bilgilerini Eksiksiz Doldurunuz...!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb");
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE otelbilgileri SET OtelAdi='" + textBox1.Text + "',Adres='" + richTextBox1.Text + "',Telefon='" + maskedTextBox1.Text + "',Faks='" + maskedTextBox2.Text+ "' where ID=" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "";
                cmd.ExecuteNonQuery();
                con.Close();
                otelbilgilistele();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            maskedTextBox1.Text = "";
            maskedTextBox2.Text = "";
            richTextBox1.Text = "";

        }
    }
}
