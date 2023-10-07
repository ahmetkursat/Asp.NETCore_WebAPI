using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public record BookDtoForUpdate(int id, String Title, decimal Price);

    //{
    //public int Id { get; init; }

    //public String Title { get; init; } Dtoları bu şekilde de tanımlayabilir

    //public decimal Price { get; init; }
    //}
}
