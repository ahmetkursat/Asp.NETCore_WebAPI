using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public abstract record BookDtoForManipulation
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public String Title { get; init; }
        [Required]
        [Range(10,1000)]
        public decimal Price { get; init; }


    }
}
