using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace The_Notebook
{
    public class NoteManager
    {
        #region variables
        public string path = "D:/Programko/Microsoft/Notes/note.txt";
        public List<Note> notes_list = new List<Note>();
        private bool isFound = false;
        #endregion

        public void CreateNote()
        {
            Note note = new Note();
            AddToList(note);
        }
        
        public void RemoveFromList() // nacitat zo suboru do listu, odstranit, zapisat do suboru
        {
            Console.WriteLine("Please, type in headline of Note you want to remove.");
            string headline = Console.ReadLine();

            BinaryNotesSerializer serializer = new BinaryNotesSerializer();
            List<Note> tmp = new List<Note>();
            tmp = serializer.DeserializeNotes(false);
            Note toRemove = null;

            foreach (Note note in tmp)
            {
                if (headline != "" && note.headline == headline)
                {
                    toRemove = note;
                }
            }

            if (toRemove != null)
            {
                tmp.Remove(toRemove);
                serializer.SerializeNotes(tmp);
                Console.WriteLine(string.Format("Note {0} removed.", headline));
            }

            else Console.WriteLine("Removal failed, no such Note was found.");
        }
        
        public void ClearNotes() // clearing list and DataFile
        {
            FileStream stream = File.Open("DataFile.dat", FileMode.Open);
            stream.SetLength(0);
            stream.Close();
            Console.WriteLine("DataFile cleared.");

            if (notes_list != null && notes_list.Count > 0)
            {
                notes_list.Clear();
                notes_list = new List<Note>();
                Console.WriteLine("List cleared." + Environment.NewLine);
            }

            else Console.WriteLine("List is already empty.");

        }

        public void AssignList(List<Note> notes_list)
        {
            if (notes_list != null)
            {
                this.notes_list = notes_list;
            }

            else
            {
                Console.WriteLine("List not assigned, parameter is null");
            }
        }

        private void AddToList(Note note)
        {
            bool headlineAlreadyUsed = false;

            if (notes_list != null && note != null)
            {
                foreach (Note Note in notes_list) // aby sa neopakovali headline
                {
                    if (note.headline == Note.headline) headlineAlreadyUsed = true;
                }

                if (!headlineAlreadyUsed)
                {
                    notes_list.Add(note);
                    Console.WriteLine(Environment.NewLine + "Note added to the list" + Environment.NewLine);
                }

                else Console.WriteLine("Headline already used. Note was NOT added to the list.");
            }

            else
            {
                Console.WriteLine(Environment.NewLine + "Operation AddToList failed.");
            }
        }

        #region Get Functions 

        public string GetMoreInfo()
        {
            Console.WriteLine("Please, type in headline of Note you want to know more about.");

            string headline = Console.ReadLine();
            string message = string.Empty;

            foreach (Note note in notes_list)
            {
                if (note.headline == headline)
                {
                    isFound = true;
                    message = string.Format(Environment.NewLine + "Headline : {0}" + Environment.NewLine + "Author : {1}" + Environment.NewLine + "Date : {2}" + Environment.NewLine + "Content : {3}" + Environment.NewLine, note.headline, note.author, note.dateTime, note.content);

                }
            }

            if (!isFound)
            {
                Console.WriteLine(string.Format(Environment.NewLine + "No Note with the headline : {0}  was found.", headline));
            }

            isFound = false;
            return message;
        }

        public void GetListContent()
        {
            if (notes_list != null)
            {
                foreach (Note note in notes_list)
                {
                    if (note != null)
                    {
                        Console.WriteLine(note.headline);
                    }
                }
            }

            else Console.WriteLine(Environment.NewLine + "There are no Notes left.");
        }

        public List<Note> GetAllNotesList()
        {
            return notes_list;
        }

        public void GetNoteFromAuthor()
        {
            Console.WriteLine("What author are you looking for ? Please, type the name in.");
            string author = Console.ReadLine();

            foreach (Note note in notes_list)
            {
                if (author != "" && note.author == author)
                {
                    isFound = true;
                    Console.WriteLine(Environment.NewLine + "Result : " + note.headline);
                }
            }

            if (!isFound)
            {
                Console.WriteLine(string.Format(Environment.NewLine + "No Notes from the author : {0}  were found.",author));
            }

            isFound = false;
        }

        public void GetNoteFromContent()
        {
            Console.WriteLine("What content / part of content are you looking for ? Please, type it in.");
            string content = Console.ReadLine();

            foreach (Note note in notes_list)
            {
                if (content != "" && note.content.Contains(content))
                {
                    isFound = true;
                    Console.WriteLine(Environment.NewLine + "Result : " + note.headline);
                }
            }

            if (!isFound)
            {
                Console.WriteLine(string.Format(Environment.NewLine + "Content : - {0} - was not found", content));
            }

            isFound = false;
        }

        public void GetAllNotesFromMonth() 
        {
            Console.WriteLine("What month are you looking for ? Please, type in the number of it.");
            int month = Convert.ToInt16(Console.ReadLine());

            foreach (Note note in notes_list)
            {
                if (month <= note.dateTime.Month)
                {
                    isFound = true;
                    Console.WriteLine(Environment.NewLine + "Result : " + note.headline);
                }
            }

            if (!isFound)
            {
                Console.WriteLine(string.Format(Environment.NewLine + "No Notes older than month {0} were found.", month));
            }

            isFound = false;
        }

        #endregion
    }
}
