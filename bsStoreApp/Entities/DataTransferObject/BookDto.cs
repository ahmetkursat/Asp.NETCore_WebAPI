namespace Entities.DataTransferObject

{
    [Serializable] //serilize yapmak get;set; dto olmadıgı için server anlamıyor onu xml anlaması için serilize yapıyoruz 
    public record BookDto
    {
        public int Id { get; set; }

        public String Title { get; set; }

        public decimal Price { get; set; }
    };

}