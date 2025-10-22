namespace Flashcards.Models
{
    internal class Flashcard(string question, string answer)
    {
        public int Id { get; private set; }
        public string Question { get; init; } = question;
        public string Answer { get; init; } = answer;
        public DateTime CreatedAt { get; init; } = DateTime.Now;
    }
}
