using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomApi.Common.ModelVM
{
    public class CategoryVM
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public bool Status { get; set; } = true;
    }
}
