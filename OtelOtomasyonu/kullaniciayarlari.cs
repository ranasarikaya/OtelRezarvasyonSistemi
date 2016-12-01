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
    public partial class kullaniciayarlari : Form
    {
        public kullaniciayarlari()
        {
            InitializeComponent();
        }

        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;

        // VOİD  İLE KULLANICILARI LİSTELEME METHODU OLUŞTURUYORUZ. //////////// 
        void kullanıcılistele()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb");
            da = new OleDbDataAdapter("SElect * from login", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "login"); // DATASETİ login TABLOSUNUN VERİLERİ İLE DOLDURUYORUZ.
            dataGridView1.DataSource = ds.Tables["login"]; // DATAGRİDVİEW İ DATASETE TANIMLADIĞIMIZ LOGİN TABLOSU İLE DOLDURUYORUZ
            con.Close(); // BAĞLANTIYI KAPATIYORUZ.
        }
        private void kullaniciayarlari_Load(object sender, EventArgs e)
        {
            kullanıcılistele(); // FORM AÇILDIĞINDA KULLANICILARILİSTELE METHODU ÇALIŞIYOR VE DATAGRİDİ VERİLERLE DOLDURUYOR.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // BİLGİ GİRİŞLERİ KONTROL EDİLİYOR.
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.Text == "")
            {
                // EKSİK BİLGİ GİRİLDİYSE UYARI MESAJI VERİYOR.
                MessageBox.Show("Lütfen Tüm Kullanıcı Bilgilerini Eksiksiz Doldurunuz...!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // kullanici CLASS INDAN VERİLERİ TEXTBOXLARDAN ÇEKTİĞİMİZ VERİLERLE DOLDURUYORUZ.
                kullanici kullanici = new kullanici();
                kullanici.adisoyadi = textBox1.Text;
                kullanici.kullaniciadi = textBox2.Text;
                kullanici.sifre = textBox3.Text;
                kullanici.durum = comboBox1.Text;
                // kullanici CLASS INDAN VERİLERİ TEXTBOXLARDAN ÇEKTİĞİMİZ VERİLERLE DOLDURUYORUZ.

                string vtyolu = "Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb";
                OleDbConnection baglanti = new OleDbConnection(vtyolu);
                baglanti.Open();
                // Kullanıcı Adı DAHA ÖNCE AÇILMIŞ MI DİYE KONTROL EDİYOR
                string odurum = "SELECT KullaniciAdi FROM login WHERE KullaniciAdi='" + textBox2.Text + "'";
                OleDbCommand odrm = new OleDbCommand(odurum, baglanti);
                OleDbDataReader dr = odrm.ExecuteReader();
                if (dr.HasRows)
                {
                    // EĞER KULLANICI ADI DAHA ÖNCE AÇILMIŞSA UYARI MESAJI VER VE KAYDETME
                    MessageBox.Show(textBox2.Text + " Bu Kullanıcı Adı Kullanılıyor...!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //----------------------------------------------------
                else
                {
                    // EĞER KULLANICI ADI YOKSA KAYDET.
                    string ekle = "insert into login(AdiSoyadi,KullaniciAdi,Sifre,KDurum) values (@adisoyadi,@kadi,@sifre,@kdurum)";
                    OleDbCommand komut = new OleDbCommand(ekle, baglanti);
                    komut.Parameters.AddWithValue("@adisoyadi", kullanici.adisoyadi);
                    komut.Parameters.AddWithValue("@kadi", kullanici.kullaniciadi);
                    komut.Parameters.AddWithValue("@sifre", kullanici.sifre);
                    komut.Parameters.AddWithValue("@kdurum", kullanici.durum);
                    komut.ExecuteNonQuery();
                    MessageBox.Show(textBox2.Text + "Kullanıcı Kaydı Tamamlanmıştır", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    kullanıcılistele();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // MESSAGE BOX İLE VERİNİN SİLİNMEK İSTEDİĞİ SORULUYOR.
            DialogResult secenek = MessageBox.Show(" Seçili Kullanıcı Kaydını Silmek İstiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (secenek == DialogResult.Yes)
            {
                // EĞER MESSABOX DA EVET DÜĞMESİNE BASILDIYSA SİLME İŞLEMİ YAPILIYOR.
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "DELETE FROM login WHERE ID=" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "";
                cmd.ExecuteNonQuery();
                con.Close();
                kullanıcılistele();
                MessageBox.Show("Kullanıcı Kaydı Silinmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (secenek == DialogResult.No)
            {
                // MESSAGEBOX DA HAYIR DÜĞMESİNE BASILDIYSA SİLME İŞLEMİ İPTAL EDİLİYOR.
                MessageBox.Show("Silme İşlemi İptal Edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // BİLGİ GİRİŞLERİ KONTROL EDİLİYOR.
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.Text == "")
            {
                // BİLGİ GİRİŞİ YOKSA MESAJ PENCERESİ AÇILIYOR VE GEREKLİ ALANLARIN ARKAPLAN RENGİ KIRMIZI OLUYOR.
                MessageBox.Show("Lütfen Kırmızı Alanları Doldurunuz...", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox1.BackColor = Color.Red;
                textBox2.BackColor = Color.Red;
                textBox3.BackColor = Color.Red;
                comboBox1.BackColor = Color.Red;
            }
            else
            {
                // BİLGİLER EKSİKSİZ DOLDURULDUYSA GÜNCELLEME İŞLEMİ YAPILIYOR.
                con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb");
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE login SET AdiSoyadi='" + textBox1.Text + "',KullaniciAdi='" + textBox2.Text + "',Sifre='" + textBox3.Text + "',KDurum='" + comboBox1.Text + "' where ID=" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "";
                cmd.ExecuteNonQuery();
                con.Close();
                kullanıcılistele();
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //DATAGRIDDE SEÇİLEN ALANLAR TEXTBOX VE COMBOBOX LARA YAZILIYOR.
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // TEXTBOX VE COMBOBOX DAKİ VERİLER TEMİZLENİYOR.
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
        }
    }
}
