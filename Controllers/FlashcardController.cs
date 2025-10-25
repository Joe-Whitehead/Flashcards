using Flashcards.Data;
using Flashcards.DTOs;
using Flashcards.Models;
namespace Flashcards.Controllers
{
    internal class FlashcardController
    {
        private readonly FlashcardRepository _flashcardRepository;
        private readonly StackRepository _stackRepository;
        
        public FlashcardController() 
        { 
            _flashcardRepository = new FlashcardRepository();
            _stackRepository = new StackRepository();
        }

        internal bool CreateFlashcard(FlashcardDTO flashcardDto)
        {
            // Map DTO to Model
            var flashcard = new Flashcard
            {
                Question = flashcardDto.Question,
                Answer = flashcardDto.Answer,
                CreatedAt = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            };
            return _flashcardRepository.InsertFlashcard(flashcard);
        }

        internal bool UpdateFlashcard(int id, FlashcardDTO flashcardDto)
        {
            var existingFlashcard = _flashcardRepository.GetFlashcardById(id);
            if (existingFlashcard == null)
            {
                return false;
            }
            existingFlashcard.Question = flashcardDto.Question;
            existingFlashcard.Answer = flashcardDto.Answer;
            existingFlashcard.LastUpdated = DateTime.UtcNow;
            return _flashcardRepository.UpdateFlashcard(existingFlashcard);
        }

        internal bool DeleteFlashcard(int id)
        {
            var existingFlashcard = _flashcardRepository.GetFlashcardById(id);
            if (existingFlashcard == null)
            {
                return false;
            }
            return _flashcardRepository.DeleteFlashcard(existingFlashcard);
        }

        internal FlashcardDTO? GetFlashcard(int id)
        {
            var flashcard = _flashcardRepository.GetFlashcardById(id);
            if (flashcard == null)
            {
                return null;
            }
            flashcard.LoadedFromDb = DateTime.UtcNow;
            return new FlashcardDTO
            {
                Id = flashcard.Id,
                Question = flashcard.Question,
                Answer = flashcard.Answer,
                StackName = flashcard.StackName ?? string.Empty
            };
        }

        internal List<FlashcardDTO> GetAllFlashcards()
        {
            var flashcards = _flashcardRepository.GetAllFlashcards();
            var flashcardDtos = new List<FlashcardDTO>();
            foreach (var flashcard in flashcards)
            {
                flashcard.LoadedFromDb = DateTime.UtcNow;
                flashcardDtos.Add(new FlashcardDTO
                {
                    Id = flashcard.Id,
                    Question = flashcard.Question,
                    Answer = flashcard.Answer,
                    StackName = flashcard.StackName ?? string.Empty
                });
            }
            return flashcardDtos;
        }
    }
}
