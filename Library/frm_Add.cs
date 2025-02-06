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
    public partial class frm_Add : Form
    {
        SqlConnection cn = new SqlConnection("Data Source=localhost;Initial Catalog=Library_DB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        public frm_Add()
        {
            cmd.Connection = cn;
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string auth_id="";
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
            if (auth_id=="0")
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
            cmd.CommandText = "insert into [Tbl_books]([name],[author_id],[author_date],[ISBN],[notes]) values ('" + txt_name.Text + "','"+ auth_id +"','" +txt_year.Text+ "','" + txt_isbn.Text + "','" + txt_notes.Text + "')";
            cmd.ExecuteNonQuery();
            cn.Close();

            MessageBox.Show("تم الحفظ بنجاح");
            txt_name.Text = "";
            txt_auther.Text = "";
            txt_year.Text = "";
            txt_isbn.Text = "";
            txt_notes.Text = "";


        }

        private void txt_auther_TextChanged(object sender, EventArgs e)
        {
            string strquery = "Select Author from Tbl_author where Author like '%" + txt_auther.Text +"%'";
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
    }
}
