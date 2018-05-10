using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace NotebookXML___08._05._2018
{
    class Program
    {
       
        static void Main(string[] args)
        {
            #region Read Note
            int SetID = 0;
            var Notes = new List<Note>();
            var serializer = new XmlSerializer(typeof(List<Note>));
            using (var reader = XmlReader.Create("../../resources/note-doc.xml"))
            {
                Notes = (List<Note>)serializer.Deserialize(reader);
            }
            #endregion

            foreach (var Note in Notes)
            {
                Console.WriteLine("Title : " + Note.Name);
                Console.WriteLine("Content : " +Note.Content);
                Console.WriteLine();
            }

            Console.WriteLine("Please enter the Title of the Note");
            var UserInputName = Console.ReadLine();

            Console.WriteLine("Please enter the contents of the Note");
            var UserInputContent = Console.ReadLine();

            var NewNote = new Note() { Name = UserInputName, Content = UserInputContent};
            if (Notes.Count > 0)
            {
                var lastnote = Notes.Last();
                SetID = lastnote.ID;
                NewNote.ID = SetID + 1;
            }
            else
            {
                NewNote.ID = 0;
            }
            Notes.Add(NewNote);

            #region Save Note
            var Serializer = new XmlSerializer(Notes.GetType());
            using (var writer = XmlWriter.Create("../../resources/note-doc.xml"))
            {
                serializer.Serialize(writer, Notes);
            }
            #endregion

            Console.WriteLine("Thank you for using XMLNote");
            Console.ReadLine();
            
        }
    }
}
