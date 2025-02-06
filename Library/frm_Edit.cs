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
    public partial class frm_Edit : Form

    {
        SqlConnection cn = new SqlConnection("Data Source=localhost;Initial Catalog=Library_DB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        string id = "";
        public DataTable dtItem = new DataTable();
        public frm_Edit()
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
        private void textBox1_TextChanged(object sender, EventArgs e)
        {


            fill_dgv();

        }

        private void txt_auther_TextChanged(object sender, EventArgs e)
        {
            string strquery = "Select Author from Tbl_author where Author like '%" + txt_auther.Text + "%'";
            //initialize new Sql commands
            cmd = new SqlCommand();
            //hold the data to be executed.
            cmd.Connection = cn;
            cmd.CommandText = strquery;
            //initialize new Sql data adapter
            SqlDataAdapter da = new SqlDataAdapter();
            //fetching query in the database.
            da.SelectCommand = cmd;
            //initialize new datatable
            DataTable dt = new DataTable();
            //refreshes the rows in specified range in the datasource.
            da.Fill(dt);

            txt_auther.AutoCompleteCustomSource.Clear();
            foreach (DataRow r in dt.Rows)
            {
                //getting all rows in the specific field|Column
                var rw = r.Field<string>("Author");

                //Set the properties of a textbox to make it auto suggest and append.
                txt_auther.AutoCompleteMode = AutoCompleteMode.Suggest;
                txt_auther.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //adding all rows into the textbox
                txt_auther.AutoCompleteCustomSource.Add(rw);

            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_name.Text = dgv.SelectedRows[0].Cells[1].Value.ToString();
            txt_auther.Text = dgv.SelectedRows[0].Cells[2].Value.ToString();
            txt_year.Text = dgv.SelectedRows[0].Cells[3].Value.ToString();
            txt_isbn.Text = dgv.SelectedRows[0].Cells[4].Value.ToString();
            txt_notes.Text = dgv.SelectedRows[0].Cells[5].Value.ToString();
            id= dgv.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string auth_id = "";
   
            SqlDataReader dr;
            cn.Open();

            cmd.CommandText = "Select id from Tbl_author where Author = '" + txt_auther.Text + "'";

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                auth_id = dr.GetValue(0).ToString();
            }
            else
                auth_id = "0";
            cn.Close();
            if (auth_id == "0")
            {
                cn.Open();
                cmd.CommandText = "insert into [Tbl_author](Author) values ('" + txt_auther.Text + "')";
                cmd.ExecuteNonQuery();
                cn.Close();
                cn.Open();
                cmd.CommandText = "Select max(id) from Tbl_author ";

                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    auth_id = dr.GetValue(0).ToString();
                }
                cn.Close();
            }
            cn.Open();
            cmd.CommandText = "update [Tbl_books] set[name] ='" + txt_name.Text + "' ,[author_id]='" + auth_id + "',[author_date]='" + txt_year.Text + "',[ISBN]='" + txt_isbn.Text + "',[notes]='" + txt_notes.Text + "' where id='"+ id +"'";
            cmd.ExecuteNonQuery();
            cn.Close();

            MessageBox.Show("تم التعديل بنجاح");
            txt_name.Text = "";
            txt_auther.Text = "";
            txt_year.Text = "";
            txt_isbn.Text = "";
            txt_notes.Text = "";
            fill_dgv();

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Edit_Load(object sender, EventArgs e)
        {
            fill_dgv();
        }
    }
}
