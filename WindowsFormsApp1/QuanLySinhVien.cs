using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace QuanLySinhVien
{
    public partial class QuanLySinhVien : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public QuanLySinhVien()
        {
            InitializeComponent();
        }

        private void QuanLySinhVien_Load(object sender, EventArgs e)
        {
            List<SinhVien> dssv = db.SinhViens.ToList();
            dataGridView1.DataSource = dssv;
            LoadDSLH();
        }

        public void LoadDSLH() // load danh sach du lieu cho combo box o lop hoc
        {
            List<LopHoc> dslh = db.LopHocs.ToList();
            comboBox2.DataSource = dslh;
            comboBox2.DisplayMember = "TenLop";
            comboBox2.ValueMember = "MaLop";
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            SinhVien sinhvien = new SinhVien();
            sinhvien.MaSV = textBox1.Text;
            sinhvien.HoTen = textBox2.Text;
            sinhvien.GioiTinh = comboBox1.Text;
            sinhvien.NgaySinh = DateTime.Parse(dateTimePicker1.Text);
            sinhvien.MaLop = comboBox2.SelectedValue.ToString();
            try
            {
                db.SinhViens.InsertOnSubmit(sinhvien);
                db.SubmitChanges();
                MessageBox.Show("Them moi sinh vien thanh cong.");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadData()
        {
            List<SinhVien> dssv = db.SinhViens.ToList();
            dataGridView1.DataSource = dssv;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["MaSV"].Value.ToString();
                textBox2.Text = row.Cells["HoTen"].Value.ToString();
                comboBox1.Text = row.Cells["GioiTinh"].Value.ToString();

                dateTimePicker1.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);

                comboBox2.SelectedValue = row.Cells["MaLop"].Value.ToString();
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                String id = textBox1.Text;

                SinhVien sv = db.SinhViens.FirstOrDefault(x => x.MaSV == id);

                if (sv != null)
                {
                    sv.HoTen = textBox2.Text;
                    sv.GioiTinh = comboBox1.Text;
                    sv.NgaySinh = dateTimePicker1.Value;
                    sv.MaLop = comboBox2.SelectedValue.ToString();
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
                String id = textBox1.Text;
                SinhVien sv = db.SinhViens.FirstOrDefault(x => x.MaSV == id);

                if (sv != null)
                {
                    db.SinhViens.DeleteOnSubmit(sv);
                    db.SubmitChanges();
                    MessageBox.Show("Xoa thanh cong!");
                    LoadData();
                }
            }
        }
    }
}
