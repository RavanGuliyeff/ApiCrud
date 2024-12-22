using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.DTOs.Category
{
    public record CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
