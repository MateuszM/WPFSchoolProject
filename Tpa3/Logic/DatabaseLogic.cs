using System;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Tpa3.Models;

namespace Tpa3.Logic
{
    [Export(typeof(ISerialize))]
    public class DatabaseLogic : ISerialize
    {
        public DatabaseLogic(String d,AssemblyMetadata user)
        {
         //  Serialize(d, user);
        }

        public DatabaseLogic()
        {
        }

      
        public void Serialize(string userID, AssemblyMetadata userDetail)
        {
            using (SqlConnection sqlconnection = new SqlConnection(new DataContext().Database.Connection.ConnectionString))
            {
                sqlconnection.Open();
                string[] s = userID.Split('.');
                string resultString = Regex.Match(userID, @"\d+").Value;
                // create table if not exists 
                try
                {
                    string createTableQuery = @"Create Table [UserTable] (ID int, [UserObject] xml)";
                    SqlCommand command = new SqlCommand(createTableQuery, sqlconnection);
                    command.ExecuteNonQuery();
                }
                catch (Exception o)
                {
                    MessageBox.Show("Jest Connected");
                }

                // Convert C# class object into xml string 
                string xmlData = (String)ToXMLL(userDetail);
                
                string insertQuery = string.Format(@"Insert Into [UserTable] (ID,[UserObject])
                                                 Values({0},@UserObject)", Int32.Parse(resultString));

                // Insert XMl Value into Sql Table by SqlParameter
                SqlCommand insertCommand = new SqlCommand(insertQuery, sqlconnection);
                SqlParameter sqlParam = insertCommand.Parameters.AddWithValue("@UserObject", xmlData);
                sqlParam.DbType = DbType.Xml;
                insertCommand.ExecuteNonQuery();
            }
        }

        public string ConvertObjectToXMLString(object classObject)
        {
            string xmlString = null;
            XmlSerializer xmlSerializer = new XmlSerializer(classObject.GetType());
            using (System.IO.MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, classObject);
                memoryStream.Position = 0;
                xmlString = new StreamReader(memoryStream).ReadToEnd();
            }
            return xmlString;
        }
        public String ToXMLL(object o)
        {
            Type t = o.GetType();

            Type[] extraTypes = t.GetProperties()
                .Where(p => p.PropertyType.IsInterface)
                .Select(p => p.GetValue(o, null).GetType())
                .ToArray();

            DataContractSerializer serializer = new DataContractSerializer(t, extraTypes);
            StringWriter sw = new StringWriter();
            XmlTextWriter xw = new XmlTextWriter(sw);
            serializer.WriteObject(xw, o);
            return sw.ToString();
        }
        public AssemblyMetadata DeserializationAmeta(string userID)
        {
            int userid = Int32.Parse(userID);
            AssemblyMetadata userDetail = null;
            using (SqlConnection sqlconnection = new SqlConnection(new DataContext().Database.Connection.ConnectionString))
            {
                sqlconnection.Open();

                string selectQuery = string.Format(@"Select [UserObject] From [UserTable] Where ID={0}"
                                    , userID);

                // Read Xml Value from Sql Table 
                SqlCommand selectCommand = new SqlCommand(selectQuery, sqlconnection);
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    string xmlValue = reader[0].ToString();
                    userDetail = (AssemblyMetadata)ConvertXmlStringtoObject<AssemblyMetadata>(xmlValue);
                }
            }
            return userDetail;
        }

       public AssemblyMetadata ConvertXmlStringtoObject<T>(string xmlString)
        {
            AssemblyMetadata s;

            Type t = typeof(AssemblyMetadata);

            //Type[] extraTypes = t.GetProperties()
            //    .Where(p => p.PropertyType.IsInterface)
            //    .Select(p => p.GetValue(t, null).GetType())
            //    .ToArray();

            DataContractSerializer serializer = new DataContractSerializer(t);

           
            StringReader stringReader = new StringReader(xmlString);
            XmlTextReader d = new XmlTextReader(stringReader);


            s = (AssemblyMetadata)serializer.ReadObject(d);
            return s;
        }
    }
}
    