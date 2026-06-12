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
    }
}
