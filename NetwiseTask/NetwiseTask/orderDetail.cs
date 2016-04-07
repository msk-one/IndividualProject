namespace NetwiseTask
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class orderDetail
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        public int? order_id { get; set; }

        [StringLength(256)]
        public string prod_name { get; set; }

        [Key]
        [Column(Order = 1)]
        public float quantity { get; set; }
    }
}
