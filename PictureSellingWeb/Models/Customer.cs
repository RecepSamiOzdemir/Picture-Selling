﻿namespace PictureSellingWeb.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
    }
}
