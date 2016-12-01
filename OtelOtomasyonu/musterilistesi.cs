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
    public partial class musterilistesi : Form
    {
        public musterilistesi()
        {
            InitializeComponent();
        }

        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;


        void musterilistele()  // MUSTERİ LİSTELE ADINDA BİR METOT OLUŞTURULDU.
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb");
            da = new OleDbDataAdapter("SElect *from musteri", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "musteri");
            dataGridView1.DataSource = ds.Tables["musteri"];
            con.Close();
        }
        private void musterilistesi_Load(object sender, EventArgs e)
        {
            musterilistele(); // musterilistele metodu çağırılıyor.
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // DATAGRID DE TIKLANAN SATIRIN VERİLERİ TEXTBOX LARA AKTARILIYOR.
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            richTextBox1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // TEXTBOX 2 DE VERİ DEĞİŞTİĞİNDE ARAMA SORGUSU ÇALIŞIYOR.
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb");
            con.Open();
            DataTable tbl = new DataTable();
            string vara, cumle;
            vara = textBox2.Text;
            cumle = "Select * from musteri where AdiSoyadi like '%" + textBox2.Text + "%'";
            da = new OleDbDataAdapter(cumle, con);
            da.Fill(tbl);
            con.Close();
            dataGridView1.DataSource = tbl;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            DialogResult secenek = MessageBox.Show(textBox3.Text + " Adlı Müşteri Kaydını Silmek İstiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (secenek == DialogResult.Yes)
            {
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "DELETE FROM musteri WHERE ID=" + textBox1.Text + "";
                cmd.ExecuteNonQuery();
                con.Close();
                musterilistele();
                MessageBox.Show("Müşteri Kaydı Silinmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (secenek == DialogResult.No)
            {
                MessageBox.Show("Silme İşlemi İptal Edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || textBox4.Text == "" || maskedTextBox1.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Lütfen Kırmızı Alanları Doldurunuz...", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox3.BackColor = Color.Red;
                textBox4.BackColor = Color.Red;
                maskedTextBox1.BackColor = Color.Red;
            }
            else
            {
                con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb");
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE musteri SET AdiSoyadi='" + textBox3.Text + "',TCNO='" + textBox4.Text + "',TelNO='" + maskedTextBox1.Text + "',ToplamKisi='" + Convert.ToInt32(comboBox1.Text) + "',Cocuk='" + Convert.ToInt32(comboBox2.Text) + "',Aciklama='" + richTextBox1.Text + "' where ID=" + textBox1.Text + "";
                cmd.ExecuteNonQuery();
                con.Close();
                musterilistele();
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                maskedTextBox1.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                richTextBox1.Enabled = false;
                button3.Visible = false;
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            maskedTextBox1.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            richTextBox1.Enabled = true;
            button3.Visible = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
