using Flashcards.Data;
using Flashcards.DTOs;
using Flashcards.Models;

namespace Flashcards.Services
{
    internal class FlashcardService
    {
        private readonly FlashcardRepository _flashcardRepository;        

        public FlashcardService()
        {
            _flashcardRepository = new FlashcardRepository();
        }

        public bool CreateFlashcard(FlashcardDTO flashcardDto)
        {
            var flashcard = new Flashcard
            {
                Question = flashcardDto.Question,
                Answer = flashcardDto.Answer,
                CreatedAt = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            };

            return _flashcardRepository.InsertFlashcard(flashcard);
        }

       
    }
}
