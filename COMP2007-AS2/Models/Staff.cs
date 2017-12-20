namespace COMP2007_AS2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Staff")]
    public partial class Staff
    {
        public int staffId { get; set; }

        public int positionId { get; set; }

        [StringLength(50)]
        public string firstName { get; set; }

        [StringLength(50)]
        public string lastName { get; set; }

        public decimal? shiftHours { get; set; }

        public virtual Position Position { get; set; }
    }
}
