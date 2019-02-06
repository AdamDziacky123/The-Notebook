using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace The_Notebook
{
    [Serializable()]
    
    public class Note //: ISerializable
    {
        public string headline;
        public string author;
        public string content;
        private string tmp;
        private bool validInput = false;
        private bool alreadyUsed = false;
        public DateTime dateTime = new DateTime();

        public Note()
        {
            #region Headline 
            do // cant be blank
            {
                Console.WriteLine(Environment.NewLine + "Please, type in the headline of the note");
                tmp = Console.ReadLine();
                
            } while (tmp == "");

            if (tmp != null)
            {
                headline = tmp;
                tmp = string.Empty;
            }
            #endregion

            #region Author
            Console.WriteLine("Please, type in the author of the note");
            tmp = Console.ReadLine();

            if (tmp != null)
            {
                author = tmp;
                tmp = string.Empty;
            }

            else author = "---";
            #endregion

            dateTime = dateTime.AddYears(AssignYear() - 1).AddMonths(AssignMonth() - 1).AddDays(AssignDay() - 1);

            #region Content
            do
            {
                Console.WriteLine("Please, type in the content of the note");
                tmp = Console.ReadLine();
            } while (tmp == "");

            if (tmp != null)
            {
                content = tmp;
                tmp = string.Empty;
            }
            #endregion
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Headline", headline);
            info.AddValue("Author", author);
            info.AddValue("Date", dateTime);
            info.AddValue("Content", content);
        }

        private int AssignDay()
        {
            string input;
            int day;
            validInput = false;
            
            do
            {
                Console.WriteLine("Please, type in the day of the month (in number) when Note was created.");
                input = Console.ReadLine();

                if (int.TryParse(input, out day) && int.Parse(input) > 0 && int.Parse(input) < 32)
                {
                    validInput = true;
                    day = int.Parse(input);
                }

            } while (!validInput);

            return day;
        }

        private int AssignMonth()
        {
            string input;
            int month;

            do
            {
                Console.WriteLine("Please, type in the month (in number) when Note was created.");
                input = Console.ReadLine();

                if (int.TryParse(input, out month) && int.Parse(input) < 13 && int.Parse(input) > 0)
                {
                    validInput = true;
                    month = int.Parse(input);
                }

            } while (!validInput);

            validInput = false;
            return month;
        }

        private int AssignYear()
        {
            string input;
            int year;

            do
            {
                Console.WriteLine("Please, type in the year when Note was created.");
                input = Console.ReadLine();

                if (int.TryParse(input, out year))
                {
                    validInput = true;
                    year = int.Parse(input);
                }

            } while (!validInput);

            validInput = false;
            return year;
        }
    }

    public class BlankNote
    {
        public string headline;
        public string author;
        public string content;
        public DateTime dateTime = new DateTime();

        public BlankNote()
        {
            headline = "";
            author = "";
            content = "";            
        }
    }
}
