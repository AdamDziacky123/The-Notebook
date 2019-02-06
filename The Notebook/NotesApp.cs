using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace The_Notebook
{
    public class NotesApp
    {
        protected NoteManager notesManager;
        protected INotesSerializer notesSerializer;
       // private BinaryNotesSerializer binaryNotesSerializer;

        public NotesApp(NoteManager notesManager,  INotesSerializer notesSerializer)
        {
            this.notesManager = notesManager;
            this.notesSerializer = notesSerializer;
        }

        public NoteManager GetManager()
        {
            return notesManager;
        }

        public void SerializeNotes()
        {
            notesSerializer.SerializeNotes(notesManager.GetAllNotesList());
        }

        public void DeserializeNotes(bool WriteOnConsole)
        {
            notesManager.AssignList(notesSerializer.DeserializeNotes(WriteOnConsole));
        }
    }
}
