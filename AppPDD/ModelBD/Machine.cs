namespace AppPDD.ModelBD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Machine")]
    public partial class Machine
    {
        public int id { get; set; }

        [Required]
        [StringLength(10)]
        public string number { get; set; }

        [Required]
        [StringLength(50)]
        public string fine { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        public bool status { get; set; }

        public string ActualText
        {
            get
            {
                return status ? "Не оплачен" : "Оплачен";
            }
        }
    }
}
