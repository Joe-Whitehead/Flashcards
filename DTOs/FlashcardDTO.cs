namespace Flashcards.DTOs
{
    public class FlashcardDTO
    {
        public int Id { get; set; }
        public int DisplayIndex { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string StackName { get; set; }
    }
        
}
