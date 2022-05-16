namespace Notes_app.Models
{
    public class Notes
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
