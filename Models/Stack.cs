namespace Flashcards.Models
{
    internal class Stack(int id, string name, List<Flashcard> flashcards)
    {
        public int Id { get; init; } = id;
        public string Name { get; init; } = name;
        public DateTime CreatedAt { get; init; } = DateTime.Now;
        public List<Flashcard> Flashcards { get; set; } = flashcards ?? [];

        public bool AddFlashcard(Flashcard card)
        {
            ArgumentNullException.ThrowIfNull(card);
            if (Flashcards.Any(c => c.Id == card.Id))
            {
                return false; // Flashcard with the same ID already exists
            }
            Flashcards.Add(card);
            return true;
        }

        public bool RemoveFlashcard(int flashcardId)
        {
            var card = Flashcards.FirstOrDefault(c => c.Id == flashcardId);
            if (card == null)
            {
                return false;
            }
            return Flashcards.Remove(card);            
        }

    }
}
