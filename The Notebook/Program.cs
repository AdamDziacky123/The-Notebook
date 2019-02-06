using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Notebook
{
    class Program
    {
        static void Main(string[] args)
        {
            NotesApp notesApp = new NotesApp(new NoteManager(), new BinaryNotesSerializer());

            do
            {
                Console.WriteLine("What would you like to do ? Type in the number of action.");
                Console.WriteLine("1. Create new Note.");
                Console.WriteLine("2. Remove Note.");
                Console.WriteLine("3. Show all Notes");
                Console.WriteLine("4. More info about Note.");
                Console.WriteLine("5. Find Notes from author.");
                Console.WriteLine("6. Find Notes by part of the content.");
                Console.WriteLine("7. Find Notes from month and later.");
                Console.WriteLine("8. Clear all Notes");
                Console.WriteLine("9. Close the program." + Environment.NewLine);

                switch (Console.ReadLine())
                {
                    case "1":
                        notesApp.GetManager().CreateNote();
                        notesApp.SerializeNotes();
                        break;

                    case "2":
                        notesApp.GetManager().RemoveFromList(); // pories cez headline
                        break;
                        
                    case "3":
                        notesApp.DeserializeNotes(true);
                        break;

                    case "4":
                        Console.WriteLine(notesApp.GetManager().GetMoreInfo());
                        break;

                    case "5":
                        notesApp.GetManager().GetNoteFromAuthor(); 
                        break;

                    case "6":
                        notesApp.GetManager().GetNoteFromContent(); 
                        break;

                    case "7":
                        notesApp.GetManager().GetAllNotesFromMonth(); 
                        break;

                    case "8":
                        notesApp.GetManager().ClearNotes();
                        break;

                    case "9":
                        Environment.Exit(0);
                        break;
                }
            } while (Console.ReadLine() != "9");

        }
    }
}
