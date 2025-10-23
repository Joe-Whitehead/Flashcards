using System.ComponentModel.DataAnnotations;
namespace Flashcards.Models
{
    public class Flashcard
    {
        public int Id { get; private set; }
        public string Question { get; init; }
        public string Answer { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
