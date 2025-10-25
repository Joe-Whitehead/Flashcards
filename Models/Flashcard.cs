﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Flashcards.Models
{
    public class Flashcard
    {
        public int Id { get; private set; }
        public required string Question { get; set; }
        public required string Answer { get; set; }
        public DateTime CreatedAt { get; init; }
        public DateTime LastUpdated { get; set; }        
        public DateTime LastReviewedAt { get; set; }
        public int StackId { get; set; }
        [NotMapped]
        public DateTime LoadedFromDb { get; set; }
        [NotMapped]
        public string? StackName { get; set; }
    }
}
