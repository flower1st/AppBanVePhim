using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BanVePhim.BusinessTier;
using BanVePhim.Models;

namespace BanVePhim
{
    public partial class frmBanVe : Form
    {
        private int maHoaDon;
        private DataTable tblChiTietHoaDon;
        private GheBusTier gheBusTier;
        private HoaDonBusTier hoaDonBusTier;
        private ChiTietHoaDonBusTier chiTietHoaDonBusTier;
        public frmBanVe()
        {
            InitializeComponent();
            maHoaDon = 1;
            tblChiTietHoaDon = new DataTable();
            gheBusTier = new GheBusTier();
            hoaDonBusTier = new HoaDonBusTier();
            chiTietHoaDonBusTier = new ChiTietHoaDonBusTier();
            tblChiTietHoaDon.Columns.Add("MaHoaDon", typeof(string));
            tblChiTietHoaDon.Columns.Add("SoGhe", typeof(int));
            tblChiTietHoaDon.Columns.Add("SoTien", typeof(double));
        }

        private void frmBanVe_Load(object sender, EventArgs e)
        {
            KhoiTaoSoGhe(3, 5);
            TaiDanhSachHoaDonLenManHinh();
            //TaiDanhSachGheDaMuaTheoNgay(DateTime.Today );
        }

        //private void TaiDanhSachGheDaMuaTheoNgay(DateTime hientai)
        //{
        //    List<int> danhSachGheDaMuaTrongNgay = gheBusTier.LayDanHSachSoGheDaMuaTheoNgay(hientai);
        //}

        private void KhoiTaoSoGhe(int dong, int cot)
        {
            List<int> danhSachGheDaMuaTrongNgay = gheBusTier.LayDanHSachSoGheDaMuaTheoNgay(DateTime.Today);
            int x, y = 15, khoangCachX =130, khoangCachY = 80, dem = 1, w =90, h = 60;
            Button btnGhe;
            for(int i = 0; i < dong; i++)
            {
                x = 26;
                for(int j = 0; j < cot; j++)
                {
                    btnGhe = new Button();
                    if (danhSachGheDaMuaTrongNgay.Contains(dem))
                    {
                        btnGhe.BackColor = Color.Yellow;
                    }
                    else
                    {
                        btnGhe.BackColor = Color.White;
                    }
                    btnGhe.Text = dem++.ToString();
                    btnGhe.Location = new Point(x,y);
                    btnGhe.Size = new Size(w, h);
                    btnGhe.Click += BtnGhe_Click;
                    pnlHangGhe.Controls.Add(btnGhe);
                    x += khoangCachX; 
                }
                y += khoangCachY; 
            }
        }

        private void BtnGhe_Click(object sender, EventArgs e)
        {
            Button btnGhe = (Button)sender;
            if (btnGhe.BackColor == Color.White)
            {
                btnGhe.BackColor = Color.Green;
            }
            else if (btnGhe.BackColor == Color.Green)
            {
                btnGhe.BackColor = Color.White;
            }
            else
                MessageBox.Show("Ghe nay da duoc mua !");
        }

        private void btnMua_Click(object sender, EventArgs e)
        {
            Dictionary<int, double> danhSachGheKemGiaTien = new Dictionary<int, double>();
            List<int> danhSachGheDaMuaCuaHoaDon = new List<int>();
            double tongTien = 0;
            int soGhe;
            Ghe ghe;
            foreach(Button item in pnlHangGhe.Controls)
            {
                if(item.BackColor == Color.Green)
                {
                    soGhe = int.Parse(item.Text);
                    ghe = gheBusTier.LayGheTheoSoGhe(soGhe);
                    //giatienTheoSoGhe = gheBusTier.LayGiaGheTheoSoGhe(soGhe);/*TinhTien(int.Parse(item.Text));*/
                    //danhSachGheKemGiaTien.Add(soGhe, giatienTheoSoGhe);
                    tongTien += ghe.HangGhe.DonGia;
                    danhSachGheDaMuaCuaHoaDon.Add(ghe.MaGhe);
                    item.BackColor = Color.Pink;
                }
            }
            txtThanhTien.Text = tongTien.ToString();
            string error; 
            HoaDon hoaDon = new HoaDon();
            hoaDon.NgayMua = DateTime.Now;
            hoaDon.TongTien = tongTien;
            if(hoaDonBusTier.LuuHoaDon(hoaDon, out error))
            {
                //luu chi tiet
                ChiTietHoaDon chiTietHoaDon;
                foreach (int maGhe in danhSachGheDaMuaCuaHoaDon)
                {
                    chiTietHoaDon = new ChiTietHoaDon();
                    chiTietHoaDon.MaGhe = maGhe;
                    chiTietHoaDon.MaHoaDon = hoaDon.MaHoaDon;
                    if (chiTietHoaDonBusTier.LuuChiTietHoaDon(chiTietHoaDon, out error))
                    {
                        TaiDanhSachHoaDonLenManHinh();     
                    }
                    else
                    {
                        MessageBox.Show("Co loi xay ra khi luu hoa don !!. LOI = " + error);
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Co loi xay ra khi luu hoa don !!. LOI = " + error);
            }
            //ThemHoaDon(tongTien, maHoaDon++, danhSachGheKemGiaTien);
        }

        private void TaiDanhSachHoaDonLenManHinh()
        {
            dgvHoaDon.DataSource = hoaDonBusTier.LayDanhSachHoaDon();
        }

        private void ThemHoaDon(double tongTien, int soHoaDon, Dictionary<int, double> danhSachGheKemGiaTien)
        {
            var index = dgvHoaDon.Rows.Add();
            dgvHoaDon.Rows[index].Cells[0].Value = dgvHoaDon.Rows.Count;
            dgvHoaDon.Rows[index].Cells[1].Value = "HD" + soHoaDon;
            dgvHoaDon.Rows[index].Cells[2].Value = tongTien;
            /*DataGridViewRow row = (DataGridViewRow)dgvHoaDon.Rows[0].Clone();
            row.Cells[0].Value = dgvHoaDon.Rows.Count;
            row.Cells[1].Value = "HD" + soHoaDon;
            row.Cells[2].Value = tongTien;
            dgvHoaDon.Rows.Add(row);*/

           ThemChiTietHoaDon(danhSachGheKemGiaTien, soHoaDon);
        }

        private void ThemChiTietHoaDon(Dictionary<int, double> danhSachGheKemGiaTien, int soHoaDon)
        {
            DataRow row;
            foreach(KeyValuePair<int, double> entry in danhSachGheKemGiaTien)
            {
                row = tblChiTietHoaDon.NewRow();
                row["MaHoaDon"] = "HD" + soHoaDon;
                row["SoGhe"] = entry.Key;
                row["SoTien"] = entry.Value;
                tblChiTietHoaDon.Rows.Add(row);
            }
           
        }

        //private double TinhTien(int soGhe)
        //{
        //    if(soGhe <= 5)
        //    {
        //        return 5000;
        //    }else if (soGhe <= 10)
        //    {
        //        return 6500;
        //    }return 8000;
        //}

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex < 0)
            {
                return;
            }
            int maHoaDon = int.Parse(dgvHoaDon.Rows[rowIndex].Cells[0].Value.ToString());
            dgvChiTietHoaDon.DataSource = chiTietHoaDonBusTier.LayDanhSachChiTietHoaDonTheoMaHoaDon(maHoaDon);
            //DataTable tblChiTietHDtheomaHD = tblChiTietHoaDon.AsEnumerable()
            //                        .Where(row => row.Field<String>("MaHoaDon") == maHoaDon)
            //                        .CopyToDataTable();
            //dgvChiTietHoaDon.DataSource = tblChiTietHDtheomaHD;
        }

        private void dgvHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
