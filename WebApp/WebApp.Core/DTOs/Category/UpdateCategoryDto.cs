using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.DTOs.Category
{
    public record UpdateCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
