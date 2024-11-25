namespace SignleRTest.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string User { get; set; }
        public DateTime MessageDate { get; set; } = DateTime.Now;
    }
}
