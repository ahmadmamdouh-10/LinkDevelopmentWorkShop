using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matgary.BLL.Infra.Stores
{
    public class StoreDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Currency { get; set; }
    }
}
