namespace ShufflingTest.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<Option> Options { get; set; }
        public int CorrectOptionId { get; set; }
    }
}
