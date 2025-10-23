using System;
using Flashcards.Data;
using Flashcards.Models;
using Microsoft.EntityFrameworkCore;

using (var context = new DatabaseContext())
{
    var stacks = context.Stacks
        .Include(s => s.Flashcards)
        .ToList();

    foreach (var stack in stacks)
    {
        Console.WriteLine($"Stack: {stack.Name} (Created at: {stack.CreatedAt})");  
        foreach (var card in stack.Flashcards)
        {
            Console.WriteLine($"\tQuestion: {card.Question}");
            Console.WriteLine($"\tAnswer: {card.Answer}");
            Console.WriteLine();
        }
    }
}
