using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TriggerKullanimi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-32Q9FH5;Initial Catalog=Test;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBLKITAPLAR", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void sayac()
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Select * From TBLSAYAC", baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtkitapadet.Text = dr[0] + " Adet Kitap Mevcut";
            }
            baglanti.Close();
        }
   

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            sayac();


        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLKITAPLAR (AD,YAZAR,SAYFA,YAYINEVI,TUR) values (@p1,@p2,@p3,@p4,@p5)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtyazar.Text);
            komut.Parameters.AddWithValue("@p3", msksayfa.Text);
            komut.Parameters.AddWithValue("@p4", txtyayinevi.Text);
            komut.Parameters.AddWithValue("@p5", txttur.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            sayac();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtyazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            msksayfa.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtyayinevi.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txttur.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {

            DialogResult sil = MessageBox.Show(txtad.Text + " Adlı kitabı silmek istediğinize emin misin ?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

           
            if (sil == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Delete From TBLKITAPLAR where ID=" + txtid.Text, baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kitap başarıyla silindi.", "Bilgi");
               

            }
            listele();
            sayac();

        }

        private void btnkitapgor_Click(object sender, EventArgs e)
        {
            FrmSilinmiskitap fr=  new FrmSilinmiskitap();
            fr.Show();
        }
    }
}
