using Flashcards.Data;
using Flashcards.DTOs;
using Flashcards.Models;

namespace Flashcards.Controllers
{
    public class FlashcardController
    {
        private readonly FlashcardRepository _flashcardRepository;
        
        public FlashcardController() 
        { 
            _flashcardRepository = new FlashcardRepository();
            //_stackRepository = new StackRepository();
        }

        public void CreateFlashcard(Flashcard flashcard)
        {           
            
        }

        public bool UpdateFlashcard(int id, FlashcardDTO flashcardDto)
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

        public List<FlashcardDTO> GetAllFlashcards()
        {
            var flashcards = _flashcardRepository.GetAllFlashcards();

            var flashcardDtos = flashcards
                .OrderBy(f => f.Id)
                .Select((f, i) => new FlashcardDTO
                {
                    DisplayIndex = i + 1,
                    Id = f.Id,
                    Question = f.Question,
                    Answer = f.Answer,
                    StackName = f.StackName ?? string.Empty
                })
                .ToList();

            return flashcardDtos;

        }

        public bool DeleteFlashcard(int id)
        {
            var existingFlashcard = _flashcardRepository.GetFlashcardById(id);
            if (existingFlashcard == null)
            {
                return false;
            }
            return _flashcardRepository.DeleteFlashcard(existingFlashcard);
        }
    }
}
