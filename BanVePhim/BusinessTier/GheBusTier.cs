using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BanVePhim.DataTier;
using BanVePhim.Models;

namespace BanVePhim.BusinessTier
{
    class GheBusTier
    {
        private readonly GheDT gheDataTier;
        public GheBusTier()
        {
            gheDataTier = new GheDT();
        }
        public double LayGiaGheTheoSoGhe(int soGhe)
        {
            return gheDataTier.LayGiaTienTuSoGhe(soGhe);
        }

        internal Ghe LayGheTheoSoGhe(int soGhe)
        {
            return gheDataTier.LayGheTheoSoGhe(soGhe);
        }

        internal List<int> LayDanHSachSoGheDaMuaTheoNgay(DateTime hientai)
        {
            return gheDataTier.LayDanhSachSoGheDaMuaTheoNgay(hientai);
        }
    }
}
