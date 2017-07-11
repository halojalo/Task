using NLog;
using System;
using System.Linq;
using System.Xml.Linq;
using System.Data;

namespace PersonLibrary
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Sex { get; set; }
        public BirthDate BirthDate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public bool SaveToDatabase()
        {
            bool? saved = false;
            SimpleDataContext dbContext = new SimpleDataContext();
            dbContext.PersonLog(ref saved, Name, Surname);
            return saved.HasValue ? saved.Value : false;
        }

        public bool SaveFile(string type)
        {
            bool saved = false;
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\logs\\all_logs." + type;
            try
            {
                switch (type)
                {
                    case "txt":
                        var txt = System.IO.File.ReadAllText(path, System.Text.Encoding.Default);
                        if (!txt.Contains(string.Format("{0}{1}{0}{2}", ",", Name, Surname)))
                        {
                            logger.Info("{0}{1}{0}{2}", ",", Name, Surname);
                            saved = true;
                        }
                        break;
                    case "xml":
                        var xml = XDocument.Load(path);
                        if (!(xml.Element("people").Elements("person").Where(x => x.Value == Name + Surname).Any()))
                        {
                            xml.Element("people").Add(new XElement("person", new XElement("name", Name), new XElement("surname", Surname)));
                            xml.Save(path);
                            saved = true;
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                if (e is System.IO.FileNotFoundException || e is System.IO.DirectoryNotFoundException)
                {
                    switch (type)
                    {
                        case "txt":
                            logger.Info("{0}{1}{0}{2}", ",", Name, Surname);
                            saved = true;
                            break;
                        case "xml":
                            new XDocument(
                            new XElement("people",
                            new XElement("person",
                            new XElement("name", Name),
                            new XElement("surname", Surname)))).Save(path);
                            saved = true;
                            break;
                    }
                }
                else throw;
            }
            return saved;
        }
    }

    public class BirthDate
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public string GetBirthDateString()
        {
            return string.Format("{0}-{1}-{2}", Day, Month, Year);
        }
    }
}