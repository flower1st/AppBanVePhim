namespace BanVePhim.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHoaDon")]
    public partial class ChiTietHoaDon
    {
        [Key]
        public int MaChiTietHoaDon { get; set; }

        public int MaHoaDon { get; set; }

        public int MaGhe { get; set; }

        public virtual Ghe Ghe { get; set; }

        public virtual HoaDon HoaDon { get; set; }
    }
}
