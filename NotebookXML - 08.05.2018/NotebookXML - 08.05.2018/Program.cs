using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;

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
                Console.WriteLine("Title : " + Note.Name + " ID: " + (Note.ID + 1));
                Console.WriteLine("Content : " + Note.Content);
                Console.WriteLine();
            }
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine();

            Console.WriteLine("Would you like to create a note? [Yes/No]");
            Console.WriteLine("[NB: Saying no will advance you to the editing phase]");
            var CreatingDecision = Console.ReadLine();
            if(CreatingDecision == "Yes")
            {
                #region New Note
                Console.WriteLine("Please enter the Title of the Note");
                var UserInputName = Console.ReadLine();

                Console.WriteLine("Please enter the contents of the Note");
                var UserInputContent = Console.ReadLine();

                var NewNote = new Note() { Name = UserInputName, Content = UserInputContent };
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
                #endregion
            }
            if (CreatingDecision == "No")
            {

            }

            Console.WriteLine("Would you like to edit a note? [Yes/No]");
            Console.WriteLine("[NB: Saying no will take you to the end of the application.]");
            var EditingDecision = Console.ReadLine();
            if (EditingDecision == "Yes")
            {
                #region Edit Note
                Console.WriteLine("What note would you like to edit?");
                Console.WriteLine("[NB: Please enter Note ID(Stated above) for this task.]");
                Console.WriteLine();

                var GetItem = Console.ReadLine();
                var ITEM = Int32.Parse(GetItem) - 1;
                var SelectedItem = Notes[ITEM];

                //ADD A REFRENCE FOR WINDOWS FORMS!!!
                Console.WriteLine("Edit the name of the Note:");
                SendKeys.SendWait(SelectedItem.Name);
                SelectedItem.Name = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Edit the Content of the Note:");
                SendKeys.SendWait(SelectedItem.Content);
                SelectedItem.Content = Console.ReadLine();
                Console.WriteLine();
                #endregion
            }
            if (EditingDecision == "No")
            {

            }

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
