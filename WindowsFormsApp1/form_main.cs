using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class form_main : Form
    {
        public form_main()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void quảnLýSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLySinhVien QLSV = new QuanLySinhVien();
            pnl_main.Controls.Clear();
            pnl_main.Controls.Add(QLSV);
        }

        private void form_main_Load(object sender, EventArgs e)
        {
            QuanLySinhVien QLSV = new QuanLySinhVien();
            pnl_main.Controls.Clear();
            pnl_main.Controls.Add(QLSV);
        }

        private void quảnLýLớpHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyLopHoc QLLH = new QuanLyLopHoc();
            pnl_main.Controls.Clear();
            pnl_main.Controls.Add(QLLH);
        }
    }
}
