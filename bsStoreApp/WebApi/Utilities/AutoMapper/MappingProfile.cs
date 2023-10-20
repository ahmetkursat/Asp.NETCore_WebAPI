using AutoMapper;
using Entities.DataTransferObject;
using Entities.Models;

namespace WebApi.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDtoForUpdate, Book>().ReverseMap(); //veriyi upgrade edip değiştirdiğimiz için
            CreateMap<Book, BookDto>(); //getall methodunbda veriyi önce bulup cektigimiz için
            CreateMap<BookDtoForInsert, Book>();
            
        }
    }
}
