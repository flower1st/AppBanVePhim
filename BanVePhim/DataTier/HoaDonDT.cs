using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BanVePhim.DTO;
using BanVePhim.Models;

namespace BanVePhim.DataTier
{
    class HoaDonDT
    {
        public bool LuuHoaDon(HoaDon hoaDon, out string error)
        {
            error = string.Empty; 
            try
            {
                using (var dbContext = new AppBanVeModels())
                {
                    dbContext.HoaDons.Add(hoaDon);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        internal List<HoaDonDTO> LayDanhSachHoaDon()
        {
            using (var dbContext = new AppBanVeModels())
            {
                return (from hd in dbContext.HoaDons
                        select new HoaDonDTO() { 
                            MaHoaDon = hd.MaHoaDon,
                            NgayMua = hd.NgayMua,
                            Tongtien = hd.TongTien
                        }).ToList();
            }
        }
    }
}
