using BanVePhim.DTO;
using BanVePhim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanVePhim.DataTier
{
    class ChiTietHoaDonDT
    {
        public bool LuuChiTietHoaDon(ChiTietHoaDon chiTietHoaDon, out string error)
        {
            error = string.Empty;
            try
            {
                using (var dbContext = new AppBanVeModels())
                {
                    dbContext.ChiTietHoaDons.Add(chiTietHoaDon);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        internal List<ChiTietHoaDonDTO> LayDanhSachTheoMaHoaDon(int maHoaDon)
        {
            using (var dbContext = new AppBanVeModels())
            {
                return (from ct in dbContext.ChiTietHoaDons
                        where ct.MaHoaDon == maHoaDon
                        select new ChiTietHoaDonDTO()
                        {
                            GiaTien = ct.Ghe.HangGhe.DonGia,
                            SoGhe = ct.Ghe.SoGhe,
                            MaHoaDon = ct.MaHoaDon
                        }).ToList();
            }
        }
    }
}
