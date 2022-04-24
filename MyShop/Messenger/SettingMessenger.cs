using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MyShop.Messenger
{
    public class SettingMessenger
    {
        public int ItemPerPage { get; set; }
        public string Page { get; set; }

        public void readData()
        {

           // string workingDirectory = Environment.CurrentDirectory;

      
           // string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

           // string startupPath = Environment.CurrentDirectory;
            //Debug.WriteLine(projectDirectory);

            string file = File.ReadAllText( @"setting.json");
            var data = JsonConvert.DeserializeObject<SettingMessenger>(file);

            ItemPerPage = data.ItemPerPage;
            Page = data.Page;

            Debug.WriteLine(data.ItemPerPage);
            Debug.WriteLine(data.Page);

        }

        public void writeData(int itemPerPage, string page)
        {
            List<SettingMessenger> _data = new List<SettingMessenger>();
            _data.Add(new SettingMessenger()
            {
                ItemPerPage = itemPerPage,
                Page = page,
            });

            string json = System.Text.Json.JsonSerializer.Serialize(_data[0]);

              // string workingDirectory = Environment.CurrentDirectory;

      
           // string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            
            File.WriteAllText(@"setting.json", json);
        }
    }
}
