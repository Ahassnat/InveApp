namespace Test.API.Models
{
    public class BodyDetail
    {
        public int Id { get; set; }
        public string High { get; set; }
        public string Width { get; set; }
        public string Wight { get; set; }
        public User User{ get; set; }
        public int UserId { get; set; }
    }
}