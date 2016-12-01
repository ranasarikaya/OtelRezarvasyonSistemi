using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb; // Access bağlantısı kurabilmek için.

namespace OtelOtomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection con;  // ACESS BAĞLANTI DEĞİŞKENİ  //
        OleDbCommand cmd;    //  ACCESS SORGU DEĞİŞKENİ   //
        OleDbDataReader dr; //   DATA OKUYUCU            //
        private void Form1_Load(object sender, EventArgs e)
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb"); // ACCESS BAĞLANTI CÜMLESİ (BAĞLANTI TANIMLARI BE ACCESS ADI) //
            cmd = new OleDbCommand(); // SORGU TANIMLARI //
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM otelbilgileri ";
            dr = cmd.ExecuteReader(); // SORGU OKUNUYOR // 
            if (dr.Read())  // EĞER SORGUNUN İÇİNDE KAYIT VARSA
            {
                label3.Text = dr["OtelAdi"].ToString();
            }
            else // KULLANICI ADI VE ŞİFRE YANLIŞSA MESAJ VER //
            {

            }
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string kullanici = textBox1.Text; // Kullanıcı adında bir değişken tanımlıyoruz
            string sifre = textBox2.Text; // Kullanıcı adında bir değişken tanımlıyoruz
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=otelotomasyon.accdb"); // ACCESS BAĞLANTI CÜMLESİ (BAĞLANTI TANIMLARI BE ACCESS ADI) //
            cmd = new OleDbCommand(); // SORGU TANIMLARI //
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM login where KullaniciAdi='" + textBox1.Text + "' AND Sifre='" + textBox2.Text + "'"; // KULLANICI ADI VE ŞİFRE KONTROL SORGUSU
            dr = cmd.ExecuteReader(); // SORGU OKUNUYOR // 
            if (dr.Read()) // KULLANICI ADI VE ŞİFRE DOĞRUYSA FORM2 Yİ AÇ //
            {
                Form2 f2 = new Form2(); // form2 yi tanımlıyoruz
                f2.Show(); // form 2 açılıyor
                this.Hide(); // form1 gizleniyor
                f2.label1.Text = dr["KDurum"].ToString(); // admin kontrolü için form2 de ki label1 in text ine login tablosunda KDurumu gönderiyoruz.
            }
            else // KULLANICI ADI VE ŞİFRE YANLIŞSA MESAJ VER //
            {
                MessageBox.Show("Kullanıcı Adı Yada Şifre Yanlış...!","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Hand);
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit(); // program kapanıyor.
        }
    }
}
