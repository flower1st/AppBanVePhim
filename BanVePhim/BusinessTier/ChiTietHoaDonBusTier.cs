using BanVePhim.DataTier;
using BanVePhim.DTO;
using BanVePhim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanVePhim.BusinessTier
{
    class ChiTietHoaDonBusTier
    {
        private readonly ChiTietHoaDonDT chiTietHoaDonDataTier;
        public ChiTietHoaDonBusTier()
        {
            chiTietHoaDonDataTier = new ChiTietHoaDonDT();
        }
        public bool LuuChiTietHoaDon(ChiTietHoaDon chiTietHoaDon, out string error)
        {
            error = string.Empty;
            try
            {
                return chiTietHoaDonDataTier.LuuChiTietHoaDon(chiTietHoaDon, out error);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        internal List<ChiTietHoaDonDTO> LayDanhSachChiTietHoaDonTheoMaHoaDon(int maHoaDon)
        {
            return chiTietHoaDonDataTier.LayDanhSachTheoMaHoaDon(maHoaDon);
        }
    }
}
