using BanVePhim.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanVePhim.DataTier
{
    class GheDT
    {
        public double LayGiaTienTuSoGhe(int soGhe)
        {
            using (var dbContext = new AppBanVeModels())
            {
                return dbContext.Ghes.Where(g => g.SoGhe == soGhe).FirstOrDefault().HangGhe.DonGia;
            }
        }

        internal Ghe LayGheTheoSoGhe(int soGhe)
        {
            using (var dbContext = new AppBanVeModels())
            {
                return dbContext.Ghes.Include("HangGhe").SingleOrDefault(g => g.SoGhe == soGhe);
            }
        }

        internal List<int> LayDanhSachSoGheDaMuaTheoNgay(DateTime hientai)
        {
            using (var dbContext = new AppBanVeModels())
            {
                return dbContext.ChiTietHoaDons.Where(ct => EntityFunctions.TruncateTime(ct.HoaDon.NgayMua) ==
                EntityFunctions.TruncateTime(hientai)).Select(ct => ct.Ghe.SoGhe).ToList();
                //return (from ct in dbContext.ChiTietHoaDons
                //        where ct.HoaDon.NgayMua == hientai
                //        select ct.Ghe.SoGhe).ToList(); 
            }
        }
    }
}
