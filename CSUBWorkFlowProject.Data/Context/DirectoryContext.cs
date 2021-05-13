using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Directory = CSUBWorkFlowProject.Shared.Models.Directory;
using IODirectory = System.IO.Directory;

namespace CSUBWorkFlowProject.Data.Context
{
    public class DirectoryContext
    {
        //this is what we should change and toy around with... each new test should reset the copy
        private static string tempFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Output.json");

        //i really don't want to lose this
        private static string teldatafile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @".\Data\teldata.json");
        public List<Directory> Directories { get; set; }
        static DirectoryContext()
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);

            File.Copy(teldatafile, tempFile);
        }
        public DirectoryContext()
        {
            Directories = LoadDirectoryItems();
        }

        private List<Directory> LoadDirectoryItems()
        {
            var itemvalue = new List<Directory>();

            using (StreamReader r = new StreamReader(tempFile))
            {
                string json = r.ReadToEnd();
                itemvalue = JsonConvert.DeserializeObject<List<Directory>>(json);
            }
            return itemvalue;
        }

        public void SaveChanges(List<Directory> newDirectory)
        {
            string output = JsonConvert.SerializeObject(newDirectory);
            File.WriteAllText(tempFile, output);
        }
    }
}
