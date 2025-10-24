using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Flashcards.Models
{
    public class Flashcard
    {
        public int Id { get; private set; }
        public required string Question { get; init; }
        public required string Answer { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime LastUpdated { get; set; }        
        public DateTime LastReviewedAt { get; set; }
        [NotMapped]
        public DateTime LoadedFromDb { get; init; }
    }
}
