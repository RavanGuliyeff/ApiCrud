using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Entities.Common;

namespace WebApp.Core.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
