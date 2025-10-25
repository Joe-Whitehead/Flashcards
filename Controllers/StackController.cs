using Flashcards.Models;
using Flashcards.Data;
using Flashcards.DTOs;

namespace Flashcards.Controllers
{    
    public class StackController
    {
        private readonly StackRepository _stackRepository;
        private readonly FlashcardRepository _flashcardRepository;

        public StackController()
        {
            _stackRepository = new StackRepository();
            _flashcardRepository = new FlashcardRepository();
        }

        public List<Stack> GetStackList()
        {
            var stacks = _stackRepository.GetAllStacks();
            var stackDtos = stacks
                .OrderBy(s => s.Id)
                .Select((s, i) => new Stack
                {
                    Name = s.Name                    
                })
                .ToList();
            return stackDtos;
        }

        public List<StackDTO> GetAllStackDtos()
        {
            return GetAllStacks()
            .OrderBy(s => s.Id)
            .Select(StackDTO.ToDto)
            .ToList();
        }

        public List<Stack> GetAllStacks()
        {
            return _stackRepository.GetAllStacks()
            .OrderBy(s => s.Id)
            .ToList();
        }

        public bool AddNewStack(StackDTO stack)
        {
            var newStack = new Stack
            {
                Name = stack.Name,
                CreatedAt = DateTime.Now,
                Flashcards = MapFlashcards(stack.Flashcards ?? new List<FlashcardDTO>())
            };
            return _stackRepository.AddStack(newStack);           
        }

        private List<Flashcard> MapFlashcards(List<FlashcardDTO> flashcardDtos)
        {
            var flashcards = flashcardDtos.Select(dto => new Flashcard            
            {
                Question = dto.Question,
                Answer = dto.Answer,
                CreatedAt = DateTime.Now,
                LastUpdated = DateTime.Now             
            }).ToList();
            return flashcards;
        }

        public bool EditStack(Stack stack)
        {
            var existingStack = _stackRepository.GetStack(stack.Id);
            if (existingStack == null)
            {
                return false;
            }
            existingStack.Name = stack.Name;            
            return _stackRepository.UpdateStack(existingStack);
        }
    }
}
