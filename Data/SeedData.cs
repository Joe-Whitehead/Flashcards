using Flashcards.Models;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace Flashcards.Data
{
    internal static class SeedData
    {
        public static void SeedDatabase()
        {
            using (var db = new DatabaseContext())
            {
                if (!IsDatabaseSeeded())
                {
                    TestData();
                }
            }
        }

        public static bool ClearDatabase()
        {
            using (var db = new DatabaseContext())
            {
                try
                {
                    db.Flashcards.RemoveRange(db.Flashcards);
                    db.Stacks.RemoveRange(db.Stacks);
                    db.SaveChanges();

                    //Reset the PK Index
                    db.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Flashcards', RESEED, 0)");
                    db.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Stacks', RESEED, 0)");
                    return true;
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[bold red]error clearing database:[/] [orange]{ex.Message}[/]");                    
                    return false;
                }
            }
        }

        private static bool IsDatabaseSeeded()
        {
            using (var db = new DatabaseContext())
            {
                return db.Stacks.Any() || db.Flashcards.Any();
            }
        }       

        private static void TestData()
        {
            using (var db = new DatabaseContext())
            {
                var spanishStack = new Stack
                {
                    Name = "Spanish Basics",
                    CreatedAt = DateTime.UtcNow,
                    Flashcards = new List<Flashcard>
                    {
                        new Flashcard { Question = "Hello", Answer = "Hola", CreatedAt = DateTime.UtcNow },
                        new Flashcard { Question = "Thank you", Answer = "Gracias", CreatedAt = DateTime.UtcNow },
                        new Flashcard { Question = "Please", Answer = "Por favor", CreatedAt = DateTime.UtcNow },
                        new Flashcard { Question = "Goodbye", Answer = "Adiós", CreatedAt = DateTime.UtcNow },
                        new Flashcard { Question = "Yes", Answer = "Sí", CreatedAt = DateTime.UtcNow }
                    }
                };
                var mathStack = new Stack
                {
                    Name = "Basic Math",
                    CreatedAt = DateTime.UtcNow,
                    Flashcards = new List<Flashcard>
                    {
                        new Flashcard { Question = "2 + 2", Answer = "4", CreatedAt = DateTime.UtcNow },
                        new Flashcard { Question = "5 * 3", Answer = "15", CreatedAt = DateTime.UtcNow },
                        new Flashcard { Question = "10 / 2", Answer = "5", CreatedAt = DateTime.UtcNow },
                        new Flashcard { Question = "7 - 4", Answer = "3", CreatedAt = DateTime.UtcNow },
                        new Flashcard { Question = "9 + 6", Answer = "15", CreatedAt = DateTime.UtcNow }
                    }
                };
                var generalStack = new Stack
                {
                    Name = "General Knowledge",
                    CreatedAt = DateTime.UtcNow,
                    Flashcards = new List<Flashcard>
                    {
                        new Flashcard { Question = "What is the capital of France?", Answer = "Paris", CreatedAt = DateTime.UtcNow },
                        new Flashcard { Question = "Who wrote 'Hamlet'?", Answer = "William Shakespeare", CreatedAt = DateTime.UtcNow },
                        new Flashcard { Question = "What is the largest planet in our solar system?", Answer = "Jupiter", CreatedAt = DateTime.UtcNow },
                        new Flashcard { Question = "What is the boiling point of water?", Answer = "100°C (212°F)", CreatedAt = DateTime.UtcNow },
                        new Flashcard { Question = "Who painted the Mona Lisa?", Answer = "Leonardo da Vinci", CreatedAt = DateTime.UtcNow }
                    }
                };
                var englishStack = new Stack
                {
                    Name = "English Vocabulary",
                    CreatedAt = DateTime.UtcNow,
                    Flashcards = new List<Flashcard>
                    {
                        new Flashcard { Question = "What is a synonym for 'happy'?", Answer = "Joyful", CreatedAt = DateTime.UtcNow },
                        new Flashcard { Question = "What does 'benevolent' mean?", Answer = "Kind and generous", CreatedAt = DateTime.UtcNow },
                        new Flashcard { Question = "What is the antonym of 'difficult'?", Answer = "Easy", CreatedAt = DateTime.UtcNow },
                        new Flashcard { Question = "Define 'ephemeral'.", Answer = "Lasting for a very short time", CreatedAt = DateTime.UtcNow },
                        new Flashcard { Question = "What is a homophone for 'sea'?", Answer = "See", CreatedAt = DateTime.UtcNow }
                    }
                };
                var historyStack = new Stack
                {
                    Name = "World History",
                    CreatedAt = DateTime.UtcNow,
                    Flashcards = new List<Flashcard>
                    {
                        new Flashcard { Question = "Who was the first President of the United States?", Answer = "George Washington", CreatedAt = DateTime.UtcNow },
                        new Flashcard { Question = "In which year did World War II end?", Answer = "1945", CreatedAt = DateTime.UtcNow },
                        new Flashcard { Question = "What ancient civilization built the pyramids?", Answer = "The Egyptians", CreatedAt = DateTime.UtcNow },
                        new Flashcard { Question = "Who was known as the 'Maid of Orléans'?", Answer = "Joan of Arc", CreatedAt = DateTime.UtcNow },
                        new Flashcard { Question = "What was the name of the ship that brought the Pilgrims to America?", Answer = "The Mayflower", CreatedAt = DateTime.UtcNow }
                    }
                };

                db.Stacks.AddRange(spanishStack, mathStack, generalStack, englishStack, historyStack);
                db.SaveChanges();
            }
        }
    }
}
