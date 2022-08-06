using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matgary.DAL
{
    public class GeneralSetting : BaseModel
    {
        [DisplayName("Free Delivery Fees Order Total")]
        public double FreeDeliveryFeesOrderTotal { get; set; } = 0;
    }
}
