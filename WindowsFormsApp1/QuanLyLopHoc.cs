using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLySinhVien
{
    public partial class QuanLyLopHoc : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        int currentPage = 1;
        int pageSize = 5;
        public QuanLyLopHoc()
        {
            InitializeComponent();
        }

        private void QuanLyLopHoc_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            var dslh = db.LopHocs
                .OrderBy(x => x.MaID)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            dataGridView1.DataSource = dslh;

            int totalPages = (int)Math.Ceiling(
            (double)db.SinhViens.Count() / pageSize);

            label7.Text = $"Trang {currentPage}/{totalPages}";
        }


        private void btn_next_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling(
                (double)db.SinhViens.Count() / pageSize);

            if (currentPage < totalPages)
            {
                currentPage++;
                LoadData();
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadData();
            }
        }
        private int TotalPages()
        {
            return (int)Math.Ceiling(
                (double)db.SinhViens.Count() / pageSize);
        }

        private void btn_last_Click(object sender, EventArgs e)
        {
            currentPage = TotalPages();
            LoadData();
        }

        private void btn_first_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadData();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            LopHoc sinhvien = new LopHoc();
            sinhvien.MaID = int.Parse(textBox1.Text);
            sinhvien.MaLop = textBox2.Text;
            sinhvien.TenLop = textBox3.Text;
            sinhvien.GhiChu = textBox4.Text;
            try
            {
                db.LopHocs.InsertOnSubmit(sinhvien);
                db.SubmitChanges();
                MessageBox.Show("Them moi lop hoc thanh cong.");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBox1.Text);

                LopHoc sv = db.LopHocs.FirstOrDefault(x => x.MaID == id);

                if (sv != null)
                {
                    sv.MaLop = textBox2.Text;
                    sv.TenLop = textBox3.Text;
                    sv.GhiChu = textBox4.Text;
                    db.SubmitChanges();
                    MessageBox.Show("Cap nhat thanh cong!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Khong tim thay sinh vien!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Ban co chac chan muon xoa sinh vien nay?",
                "Xac nhan",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int id = int.Parse(textBox1.Text);
                LopHoc sv = db.LopHocs.FirstOrDefault(x => x.MaID == id);

                if (sv != null)
                {
                    db.LopHocs.DeleteOnSubmit(sv);
                    db.SubmitChanges();
                    MessageBox.Show("Xoa thanh cong!");
                    LoadData();
                }
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string keyword = textBox5.Text.Trim();

            var ketQua = db.LopHocs
                           .Where(sv =>
                                sv.MaLop.Contains(keyword) ||
                                sv.TenLop.Contains(keyword) ||
                                sv.GhiChu.Contains(keyword))
                           .ToList();

            dataGridView1.DataSource = ketQua;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["MaID"].Value.ToString();
                textBox2.Text = row.Cells["MaLop"].Value.ToString();
                textBox3.Text = row.Cells["TenLop"].Value.ToString();
                textBox4.Text = row.Cells["GhiChu"].Value.ToString();
            }
        }

        private void btn_QLSV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Vui long chon lop hoc!");
                return;
            }
            string maLop = textBox2.Text;
            var dssv = db.SinhViens.Where(x => x.MaLop == maLop).ToList();
            dataGridView1.DataSource = dssv;
        }
    }
}
