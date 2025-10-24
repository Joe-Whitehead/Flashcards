using System;
using Flashcards.Data;
using Flashcards.Models;
using Flashcards.Views;
using Microsoft.EntityFrameworkCore;

DbContext db = new DatabaseContext();
FlashcardView app = new FlashcardView();

using (db)
{
    db.Database.Migrate();    
}

// Import some seed data here

app.Run();

