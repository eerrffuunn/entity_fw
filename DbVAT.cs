using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ef
{
    [Table("VAT")]

    public class DbVat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Percentage { get; set; }
        public List<DbProduct> Products { get; set; }
    }
}
