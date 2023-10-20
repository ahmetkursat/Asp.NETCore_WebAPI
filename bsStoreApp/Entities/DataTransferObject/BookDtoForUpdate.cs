using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public record BookDtoForUpdate : BookDtoForManipulation
    {
        [Required]
        public int Id { get; set; }
    }

    //{
    //public int Id { get; init; }

    //public String Title { get; init; } Dtoları bu şekilde de tanımlayabilir

    //public decimal Price { get; init; }
    //}
}
