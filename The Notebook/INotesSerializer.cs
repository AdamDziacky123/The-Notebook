using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Notebook
{
    public interface INotesSerializer
    {
        void SerializeNotes(List<Note> notes_list);
        List<Note> DeserializeNotes(bool WriteOnConsole);

       // public INotesSerializer()
    }
}
