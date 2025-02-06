using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Library
{
    public partial class frm_Main : Form
    {
        public frm_Main()
        {
            InitializeComponent();
        }
        public void p_red()
        {
            p0.BackColor = Color.Red;
            p1.BackColor = Color.Red;
            p2.BackColor = Color.Red;
            p3.BackColor = Color.Red;
            p4.BackColor = Color.Red;
            p5.BackColor = Color.Red;
        }
        private void btn_exit_Click(object sender, EventArgs e)
        {
            p_red();
            p5.BackColor = Color.Green;
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            p_red();
            p0.BackColor = Color.Green;
            frm_Add x = new frm_Add();
            x.ShowDialog();
        }

  

        private void btn_del_Click(object sender, EventArgs e)
        {
            p_red();
            p2.BackColor = Color.Green;
            frm_Delete x = new frm_Delete();
            x.ShowDialog();
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            p_red();
            p3.BackColor = Color.Green;
            frm_All_books x = new frm_All_books();
            x.ShowDialog();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            p_red();
            p4.BackColor = Color.Green;
            frm_Search x = new frm_Search();
            x.ShowDialog();
        }

        private void btn_updat_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            p_red();
            p1.BackColor = Color.Green;
            frm_Edit x = new frm_Edit();
            x.ShowDialog();
        }
    }
}
