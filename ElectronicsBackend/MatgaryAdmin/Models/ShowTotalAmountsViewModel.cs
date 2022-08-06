using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatgaryAdmin.Models
{
    public class ShowTotalAmountsViewModel
    {
        public List<long> OrdersIds { get; set; } = new List<long>();

        public List<string> OrdersSerials { get; set; } = new List<string>();

        public List<TotalAmountProductViewModel> Products { get; set; } = new List<TotalAmountProductViewModel>();
    }

    public class TotalAmountProductViewModel
    {
        public string TitleE { get; set; }
        public string TitleA { get; set; }

        public string Code { get; set; }

        public double TotalAmount { get; set; }
    }
}
