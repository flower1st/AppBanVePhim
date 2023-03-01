using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BanVePhim.DataTier;
using BanVePhim.DTO;
using BanVePhim.Models;

namespace BanVePhim.BusinessTier
{
    class HoaDonBusTier
    {
        private readonly HoaDonDT hoaDonDataTier;
        public HoaDonBusTier()
        {
            hoaDonDataTier = new HoaDonDT();
        }
        public bool LuuHoaDon(HoaDon hoaDon, out string error)
        {
            error = string.Empty;
            try
            {
                return hoaDonDataTier.LuuHoaDon(hoaDon, out error); 
            }
            catch(Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        internal List<HoaDonDTO> LayDanhSachHoaDon()
        {
            return hoaDonDataTier.LayDanhSachHoaDon();
        }
    }
}
