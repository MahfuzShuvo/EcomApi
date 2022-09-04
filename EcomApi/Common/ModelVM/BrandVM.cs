using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomApi.Common.ModelVM
{
    public class BrandVM
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public bool Status { get; set; } = true;
    }
}
