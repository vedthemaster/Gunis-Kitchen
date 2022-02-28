using Microsoft.AspNetCore.Http;

namespace Gunis.Kitchen.ViewModel
{
    public class ItemViewModel
    {
        public string ItemName { get; set; }
        public int ItemPrice { get; set; }
        public string ItemSize { get; set; }
        public IFormFile ItemImage { get; set; }
    }
}
