namespace Entities.RequestFeature
{
    public class BookParameters : RequestParams
	{
        public uint MinPrice { get; set; }

        public uint MaxPrice { get; set; } = 1000;

        public bool ValidPriceRange => MaxPrice > MinPrice;

        public string? SearchTerm { get; set; }

        public BookParameters()
        {
            OrderBy = "id";
        }
    }
}
