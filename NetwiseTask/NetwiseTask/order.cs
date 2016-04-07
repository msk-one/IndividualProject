namespace NetwiseTask
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class order
    {
        public int id { get; set; }

        [StringLength(128)]
        public string name { get; set; }

        [StringLength(128)]
        public string surname { get; set; }

        public decimal? amount { get; set; }
    }
}
