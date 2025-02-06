using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Library
{
    public partial class frm_Search : Form
    {
        SqlConnection cn = new SqlConnection("Data Source=localhost;Initial Catalog=Library_DB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();

        public DataTable dtItem = new DataTable();
        public frm_Search()
        {
            InitializeComponent();
        }
        public void fill_dgv()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
           

            dtItem.Rows.Clear();
            string sqltext = "SELECT Tbl_books.id, name as'عنوان الكتاب',author as'المؤلف',author_date as'تاريخ النشر',ISBN as'ISBN' , notes as 'ملاحظات' From [Tbl_books] inner join Tbl_author on Tbl_author.id=Tbl_books.author_id where 1=1";
            if (txt_name.Text != "")
                sqltext = sqltext + "and [name] like '%" + txt_name.Text + "%'";
            if (txt_auther.Text!="")
            
        
                sqltext = sqltext + "and  Author like '%" + txt_auther.Text + "%'";
            
  


            cn.Open();
            cmd.CommandText = sqltext;
            dtItem.Load(cmd.ExecuteReader());
            dgv.DataSource = dtItem;
            cn.Close();

            dgv.Columns[0].Width = 0;
            dgv.Columns[1].Width = 130;
            dgv.Columns[5].Width = 300;
        }


        private void frm_Search_Load(object sender, EventArgs e)
        {
            cmd.Connection = cn;
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            fill_dgv();
        }
    }
}
