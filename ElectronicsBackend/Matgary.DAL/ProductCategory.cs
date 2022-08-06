using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matgary.DAL
{
    public class ProductCategory
    {
        public int Id { get; set; }
        
        public virtual Product Product { get; set; }
        
        [ForeignKey(nameof(Product))]
        public long ProductId { get; set; }
        public virtual Category Category { get; set; }
        
        [ForeignKey(nameof(Category))]
        public long CategoryId { get; set; }
    }
}
