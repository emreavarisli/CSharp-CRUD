using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormEntityFrameworkConnection.Models;

namespace WindowsFormEntityFrameworkConnection
{
    public partial class Form1 : Form
    {
        WindowsFormEntityFrameworkConnectionDbEntities db = new
            WindowsFormEntityFrameworkConnectionDbEntities();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridViewPersonel.DataSource = db.Personel.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Kayıt Et
            Personel p = new Personel();
            p.Name = txtAd.Text;
            p.LastName = txtSoyad.Text;
            p.Telefon = txtTelefon.Text;
            db.Personel.Add(p);
            db.SaveChanges();
            dataGridViewPersonel.DataSource = db.Personel.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Güncelle
            int id = int.Parse(dataGridViewPersonel.CurrentRow.Cells[0].Value.ToString());

            Personel p = db.Personel.FirstOrDefault(x => x.PersonelId == id);
            p.Name = txtAd.Text;
            p.LastName = txtSoyad.Text;
            p.Telefon = txtTelefon.Text;
            db.SaveChanges();
            dataGridViewPersonel.DataSource = db.Personel.ToList();
        }

        private void dataGridViewPersonel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtAd.Text = dataGridViewPersonel.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridViewPersonel.CurrentRow.Cells[2].Value.ToString();
            txtTelefon.Text = dataGridViewPersonel.CurrentRow.Cells[3].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Sil
            int id = int.Parse(dataGridViewPersonel.CurrentRow.Cells[0].Value.ToString());

            Personel p = db.Personel.FirstOrDefault(x => x.PersonelId == id);
            DialogResult sor = new DialogResult();
            sor = MessageBox.Show($@"{p.Name} {p.LastName} Personelini Kalıcı Olarak Silmek İstediğinize Emin Misiniz?", 
                "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(sor == DialogResult.Yes)
            {
                db.Personel.Remove(p);
                db.SaveChanges();
                dataGridViewPersonel.DataSource = db.Personel.ToList();
                txtAd.Text = txtSoyad.Text = txtTelefon.Text = null;
            }

            
        }
    }
}
