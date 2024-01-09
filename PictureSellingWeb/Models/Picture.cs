namespace PictureSellingWeb.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public decimal Price  { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool Locked { get; set; } = false;
    }
    
    public class PictureEdit
    {
        public Picture Picture { get; set; }
        public List<Category> Categories { get; set; }
    } 
}
