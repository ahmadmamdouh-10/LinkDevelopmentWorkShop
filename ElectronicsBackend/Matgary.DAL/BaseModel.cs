using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Matgary.DAL
{
    public class BaseModel
    {
        public long Id { get; set; }
        public DateTime DateTime { get; set; }

        //[ForeignKey(nameof(Store))]
        public long StoreId { get; set; }
        //public Store Store { get; set; }
    }
}
