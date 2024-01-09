namespace PictureSellingWeb.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<Picture> Pictures { get; set; } 
    }
}
