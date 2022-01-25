using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace dosya_kullanim_odevi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string dosyaYolu = @"C:\Users\User\Desktop\ogrenciler.dat";
        DataTable dt = new DataTable();

        void dosyaOlustur()
        {
            FileStream fs = File.Create(dosyaYolu);
            fs.Close();
        }

        void kisiEkle(string ad, string soyad, string numara, string bolum, string cinsiyet, string dYeri, string yas, string telNo)
        {
            FileStream fs = new FileStream(dosyaYolu, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Close();
            string text =ad + "/" + soyad + "/" + numara + "/" + bolum + "/" + cinsiyet + "/" + dYeri + "/" + yas + "/" + telNo;
            File.AppendAllText(dosyaYolu, Environment.NewLine + text);
            MessageBox.Show("Kişi eklenip,kayıtlar listelendi");
        }
        void listele()
        {
            string[] satir = File.ReadAllLines(dosyaYolu);
            int a = dataGridView1.RowCount-1;
            for (int i = a; i < satir.Length; i++)
            {
                string[] veri = satir[i].ToString().Split('/');
                string[] satir2 = new string[veri.Length];
                for (int j = 0; j < veri.Length; j++)
                {
                    satir2[j] = veri[j].Trim();
                }
                dt.Rows.Add(satir2);
            }
            
        }

        void guncelle()
        {
            TextWriter yazma = new StreamWriter(dosyaYolu);
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (j == 7)
                    {
                        yazma.Write(dataGridView1.Rows[i].Cells[j].Value.ToString());
                    }
                    else
                    {
                        yazma.Write(dataGridView1.Rows[i].Cells[j].Value.ToString() + "/");
                    }
                }
                yazma.WriteLine("");
            }
            yazma.Close();
        }
        void txtTemizle()
        {
            adTxt.Text = "";
            soyadTxt.Text = "";
            noTxt.Text = "";
            bolumTxt.Text = "";
            comboBox1.Text = "";
            dyeriTxt.Text = "";
            yasTxt.Text = "";
            telnoTxt.Text = "";
        }
        void txtDoldur()
        {
            adTxt.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            soyadTxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            noTxt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            bolumTxt.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            dyeriTxt.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            yasTxt.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            telnoTxt.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }
        void dataDoldur()
        {
            dt.Columns.Add("Adı", typeof(string));
            dt.Columns.Add("Soyadı", typeof(string));
            dt.Columns.Add("Numarası", typeof(string));
            dt.Columns.Add("Bölümü", typeof(string));
            dt.Columns.Add("Cinsiyeti", typeof(string));
            dt.Columns.Add("Doğum Yeri", typeof(string));
            dt.Columns.Add("Yaşı", typeof(string));
            dt.Columns.Add("Telefon Numara", typeof(string));
            dataGridView1.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            if (File.Exists(dosyaYolu))
            {
            }
            else
            {
                dosyaOlustur();
            }
            dataDoldur();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cins;
            if (comboBox1.SelectedIndex == 0)
            {
                cins = "Erkek";
            }
            else
                cins = "Kadın";
            kisiEkle(adTxt.Text, soyadTxt.Text, noTxt.Text, bolumTxt.Text, cins, dyeriTxt.Text, yasTxt.Text, telnoTxt.Text);
            listele();
            txtTemizle();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            txtDoldur();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            txtDoldur();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            if (aramaCmb.SelectedIndex == 0)
            {
                dv.RowFilter = "Adı LIKE '" + aramaTxt.Text + "%'";
                dataGridView1.DataSource = dv;
            }
            else
            {
                dv.RowFilter = "Numarası LIKE '" + aramaTxt.Text + "%'";
                dataGridView1.DataSource = dv;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            guncelle();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            txtTemizle();
        }


    }
}
