using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Security.Cryptography;


namespace The_Notebook
{
    public class BinaryNotesSerializer : INotesSerializer
    {
        #region Security

        private static string key = "UWBLLCMOCNHIXHKVDRLOCEBOOTVIUUXTECIFVWQFPCXSLYFMHTFHCWUPVMWRGDTHONDUYFAFBFXAFHCOOIAAOQQHGSIIAYYLHQEWUJNCTYGVYTJNKHCXCSRXWVOFVQFBGLRODETETXFPJTHMIUXRVAINYFAYUHFDSEPMBUJFNWDENBOCNBWBRNEFCDOXDTJLRBVEEUKM";

        private static string GenerateKey()
        {
            List<char> str = new List<char>();
            int i = 0;
            Random rnd = new Random();

            while (i < 200)
            {
                char c = (char)rnd.Next(65, 90);
                str.Add(c);
                i++;
            }

            return new string(str.ToArray());
        }

        private string XOR_Encrypt_Decrypt(string text)
        {
            if (key.Length > text.Length)
            {
                List<char> encrypted_text = new List<char>();
                int i = 0;

                foreach (char c in text)
                {
                    int _c = (int)c ^ (int)key[i];
                    encrypted_text.Add((char)_c);
                    i++;
                }

                return new string(encrypted_text.ToArray());
            }

            else
            {
                Console.WriteLine("Password must be longer than text to encrypt.");
                return null;
            }
        }

        public List<Note> EncryptList(List<Note> notes_list_toEncrypt)
        {
            List<Note> notes_list_copy = new List<Note>();
            notes_list_copy.AddRange(notes_list_toEncrypt);

            bool HeadlineEncrypted = false;
            bool AuthorEncrypted = false;
            bool ContentEncrypted = false;

            foreach (Note note in notes_list_copy)
            {
                for (int i = 0; i < note.headline.Length; i++)
                {
                    if (!(((int)note.headline[i] < 91 && (int)note.headline[i] > 64) || ((int)note.headline[i] > 96 && (int)note.headline[i] < 123)))
                    {
                        HeadlineEncrypted = true;
                        //Console.WriteLine("Headline already encrypted");
                    }
                }

                for (int i = 0; i < note.author.Length; i++)
                {
                    if (!(((int)note.author[i] < 91 && (int)note.author[i] > 64) || ((int)note.author[i] > 96 && (int)note.author[i] < 123)))
                    {
                        AuthorEncrypted = true;
                        //Console.WriteLine("Author already encrypted");
                    }
                }

                for (int i = 0; i < note.content.Length; i++)
                {
                    if (!(((int)note.content[i] < 91 && (int)note.content[i] > 64) || ((int)note.content[i] > 96 && (int)note.content[i] < 123)))
                    {
                        ContentEncrypted = true;
                        //Console.WriteLine("Content already encrypted");
                    }
                }

                if (!HeadlineEncrypted) note.headline = XOR_Encrypt_Decrypt(note.headline);
                if (!AuthorEncrypted) note.author = XOR_Encrypt_Decrypt(note.author);
                if(!ContentEncrypted) note.content = XOR_Encrypt_Decrypt(note.content);
            }

            return notes_list_copy;
        }

        public List<Note> DecryptList(List<Note> notes_list_toDecrypt)
        {
            List<Note> notes_list_copy = new List<Note>();
            bool HeadlineEncrypted = true;
            bool AuthorEncrypted = true;
            bool ContentEncrypted = true;

            if (notes_list_toDecrypt != null)
            {
                notes_list_copy.AddRange(notes_list_toDecrypt);

                foreach (Note note in notes_list_copy)
                {
                    for (int i = 0; i < note.headline.Length; i++)
                    {
                        if (((int)note.headline[i] < 91 && (int)note.headline[i] > 64) || ((int)note.headline[i] > 96 && (int)note.headline[i] < 123))
                        {
                            HeadlineEncrypted = false;
                            //Console.WriteLine("Headline already encrypted");
                        }
                    }

                    for (int i = 0; i < note.author.Length; i++)
                    {
                        if (((int)note.author[i] < 91 && (int)note.author[i] > 64) || ((int)note.author[i] > 96 && (int)note.author[i] < 123))
                        {
                            AuthorEncrypted = false;
                            //Console.WriteLine("Author already encrypted");
                        }
                    }

                    for (int i = 0; i < note.content.Length; i++)
                    {
                        if (((int)note.content[i] < 91 && (int)note.content[i] > 64) || ((int)note.content[i] > 96 && (int)note.content[i] < 123))
                        {
                            ContentEncrypted = false;
                            //Console.WriteLine("Content already encrypted");
                        }
                    }
                    
                    if(HeadlineEncrypted) note.headline = XOR_Encrypt_Decrypt(note.headline); //Console.WriteLine(string.Format("headline : {0} decrypted into : {1}",note.headline, XOR_Encrypt_Decrypt(note.headline)));
                    if(AuthorEncrypted) note.author = XOR_Encrypt_Decrypt(note.author); //Console.WriteLine(string.Format("author : {0} decrypted into : {1}", note.author, Decryption(note.author)));
                    if(ContentEncrypted) note.content = XOR_Encrypt_Decrypt(note.content); //Console.WriteLine(string.Format("content : {0} decrypted into : {1}", note.content, Decryption(note.content)));
                }
            }
            return notes_list_copy;
        }

        #endregion

        public void SerializeNotes(List<Note> notes_list)
        {
            FileMode mode;
            if (File.Exists("DataFile.dat"))
            {
                mode = FileMode.Open;
                //Console.WriteLine("current FileMode is : open");
            }

            else
            {
                mode = FileMode.Create;
                //Console.WriteLine("current FileMode = create");
            }

            FileStream fs_en = new FileStream("DataFile.dat", mode);

            using (fs_en)
            {
                BinaryFormatter bf = new BinaryFormatter();

                if (notes_list != null)//&& notes_list.Count > 0)
                {
                    try
                    {
                        bf.Serialize(fs_en, EncryptList(notes_list));
                    }

                    catch (SerializationException e)
                    {
                        Console.WriteLine(Environment.NewLine + "Failed to Serialize. Reason : " + e.Message);
                    }

                    finally
                    {
                        fs_en.Close();
                    }

                    Console.WriteLine(Environment.NewLine + "Data Serialized.");
                }

                else Console.WriteLine(Environment.NewLine + "Serialization failed. Notes_list is null.");
            }
        }

        public List<Note> DeserializeNotes(bool WriteOnConsole)
        {
            List<Note> notes = null;
            FileStream fs_de = new FileStream("DataFile.dat", FileMode.Open);

            using (fs_de)
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    notes = DecryptList((List<Note>)bf.Deserialize(fs_de));
                }

                catch (SerializationException e)
                {
                    if (fs_de.Length == 0) Console.WriteLine(Environment.NewLine + "The file is empty.");
                    else Console.WriteLine(Environment.NewLine + "Deserialization failed. Reason : " + e.Message);
                }

                finally
                {
                    fs_de.Close();
                }
            }

            if (notes != null && WriteOnConsole)
            {
                foreach (Note note in DecryptList(notes))
                {
                    //Console.WriteLine("Line 118 output.");
                    Console.WriteLine(note.headline);
                }
            }

            if (notes.Count <= 0)
            {
                Console.WriteLine("There are no Notes left.");
            }

            return DecryptList(notes);
        }
    }
}
