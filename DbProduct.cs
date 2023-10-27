using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;

namespace ef
{
    [Table("Product")]
    public class DbProduct
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }

        //here Map the Product  VAT connection.
        [ForeignKey("VatID")]
        public DbVat Vat { get; set; }
    }
}