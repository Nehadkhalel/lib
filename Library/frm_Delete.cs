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
    public partial class frm_Delete : Form
    {
        SqlConnection cn = new SqlConnection("Data Source=localhost;Initial Catalog=Library_DB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();

        public DataTable dtItem = new DataTable();
        public frm_Delete()
        {

            InitializeComponent();
     
        }
        public void fill_dgv()
        {

            dtItem.Rows.Clear();
            string sqltext = "SELECT Tbl_books.id, name as'عنوان الكتاب',author as'المؤلف',author_date as'تاريخ النشر',ISBN as'ISBN' , notes as 'ملاحظات' From [Tbl_books] inner join Tbl_author on Tbl_author.id=Tbl_books.author_id where [name] like '%" + txt_search.Text + "%'";


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cn.Open();
            cmd.CommandText = sqltext;
            dtItem.Load(cmd.ExecuteReader());
            dgv.DataSource = dtItem;
            cn.Close();

            dgv.Columns[0].Width = 0;
        }
        private void frm_Delete_Load(object sender, EventArgs e)
        {
            cmd.Connection = cn;
            fill_dgv();
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            fill_dgv();
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("هل انت متأكد من حذف الكتاب لمحدد", "رسالة تأكيد", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                cn.Open();
                cmd.CommandText = "delete from [Tbl_books]  where id='" + dgv.SelectedRows[0].Cells[0].Value.ToString() + "'";
                cmd.ExecuteNonQuery();
                cn.Close();

                MessageBox.Show("تم الحذف بنجاح");

                fill_dgv();
               
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }


        }
    }
}
