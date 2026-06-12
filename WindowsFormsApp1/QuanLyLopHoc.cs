using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace QuanLySinhVien
{
    public partial class QuanLyLopHoc : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public QuanLyLopHoc()
        {
            InitializeComponent();
        }

        
    }
}
