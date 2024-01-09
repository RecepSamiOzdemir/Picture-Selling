using Microsoft.EntityFrameworkCore;

namespace PictureSellingWeb.Models
{
    
    public class Stock
    {
        public int Id { get; set; }
        public int PictureId { get; set; }
        public Picture Picture { get; set; }
        public int StokCount { get; set; }
    }
}
