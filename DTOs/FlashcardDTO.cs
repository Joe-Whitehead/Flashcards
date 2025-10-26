using Flashcards.Models;
namespace Flashcards.DTOs
{
    public class FlashcardDTO
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string StackName { get; set; }


        public static FlashcardDTO ToDto(Flashcard flashcard)
        {
            return new FlashcardDTO
            {
                Question = flashcard.Question,
                Answer = flashcard.Answer,
                StackName = flashcard.StackName ?? string.Empty
            };
        }

        public static string ToDisplayString(Flashcard flashcard)
        {
            return $"Q: {flashcard.Question} | A: {flashcard.Answer} | Stack: {flashcard.StackName}";
        }
    }
}

            
            
