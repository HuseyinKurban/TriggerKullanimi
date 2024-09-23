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
    public partial class FrmSilinmiskitap : Form
    {
        public FrmSilinmiskitap()
        {
            InitializeComponent();
        }

        private void FrmSilinmiskitap_Load(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-32Q9FH5;Initial Catalog=Test;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");
            
            SqlDataAdapter da = new SqlDataAdapter("Select ID,AD,YAZAR,SAYFA,YAYINEVI,TUR From TBLKITAPYEDEK",baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
