using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Tpa3.Models;

namespace Tpa3.ViewModel
{
   public class Json
    {
        public Json()
        {
          //  Serialize(file, Tr);
        }
        
        public void Serialize(String filename, TreeNode Tree)
        { 
        var serializer = new DataContractSerializer(typeof(TreeNode));
            using (FileStream stream = File.Create(filename))
            {
                serializer.WriteObject(stream, Tree);
            }
} 
        public TreeNode Deserialization(String filename)
        {
            var serializer = new DataContractSerializer(typeof(TreeNode));
            using (FileStream stream = File.OpenRead(filename))
            {
                TreeNode data = (TreeNode)serializer.ReadObject(stream);
                return data;
            }
        }

    }
}
