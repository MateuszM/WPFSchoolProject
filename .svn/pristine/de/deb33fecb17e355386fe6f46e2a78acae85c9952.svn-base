using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Tpa3.Models;
using Tpa3.Loggers;
using System.ComponentModel.Composition;

namespace Tpa3.Logic
{
    public class ViewLogic
    {
     
        public ILogger logger = new Logger();
       public AssemblyMetadata AMetadata;
       
      
        public void Clear()
        {
            using (var DataContext = new DataContext())
            {
                string sqlTrunc = "TRUNCATE TABLE " + "TypeMetadatas";
                string sqlTrunc1 = "TRUNCATE TABLE " + "NamespaceMetas";
                string sqlTrunc2 = "TRUNCATE TABLE " + "ParameterMetadatas";
                string sqlTrunc3 = "TRUNCATE TABLE " + "PropertyMetadatas";
                string sqlTrunc4 = "TRUNCATE TABLE " + "UserTable";
                DataContext.Database.ExecuteSqlCommand(sqlTrunc);
                DataContext.Database.ExecuteSqlCommand(sqlTrunc1);
                DataContext.Database.ExecuteSqlCommand(sqlTrunc2);
                DataContext.Database.ExecuteSqlCommand(sqlTrunc3);
                DataContext.Database.ExecuteSqlCommand(sqlTrunc4);
                DataContext.SaveChanges();
            }
        }
        
        public void SaveToDatabase(Object obj,int currentid,int ParentID,string ParentName)
        {
            using (var DataContext = new DataContext())
            {
                if (obj.GetType() == typeof(AssemblyMetadata))
                {
                    AssemblyMetadata temp = (AssemblyMetadata)obj;
                    temp.ID = currentid;
                   
                }

                if (obj.GetType() == typeof(TypeMetadata))
                {
                    TypeMetadata temp = (TypeMetadata)obj;
                    temp.ID = currentid;
                    temp.ParentID = ParentID;
                    temp.ParentType = ParentName;
                    DataContext.TypeMeta.Add(temp);
                }

                if (obj.GetType() == typeof(ParameterMetadata))
                {
                    ParameterMetadata temp = (ParameterMetadata)obj;
                    temp.ID = currentid;
                    temp.ParentID = ParentID;
                    temp.ParentType = ParentName;
                    DataContext.Parameter.Add(temp);
                }

                if (obj.GetType() == typeof(PropertyMetadata))
                {
                    PropertyMetadata temp = (PropertyMetadata)obj;
                    temp.ID = currentid;
                    temp.ParentID = ParentID;
                    temp.ParentType = ParentName;
                    DataContext.Property.Add(temp);
                }

                if (obj.GetType() == typeof(NamespaceMeta))
                {
                    NamespaceMeta temp = (NamespaceMeta)obj;
                    temp.ID = currentid;
                    temp.ParentID = ParentID;
                    temp.ParentType = ParentName;
                    DataContext.Namespace.Add(temp);
                }
                DataContext.SaveChanges();
            }
        }
       public ViewLogic()
        {
            
        }
        
        public List<String> ReadFromAmetaData(Object obj,TreeNode node,int parentid,string parentname)
        {
            int currentid = 0;
            List<String> temp = new List<String>();
            if (obj != null)
            {
                TreeNode k = new TreeNode();
                Type p = obj.GetType();
                FieldInfo[] s = p.GetFields();
                String str = "     Start          " + obj.ToString();
                String strk = "     Koniec        " + obj.ToString();
               // SaveToDatabase(obj,currentid, parentid, parentname);
              
                foreach (var prop in obj.GetType().GetProperties())
                {
                    if (prop.GetValue(obj) != null)
                    {
                        TreeNode c = new TreeNode(prop.GetValue(obj).ToString(), prop.ToString());
                        node.Childs.Add(c);
                    }
                }

                foreach (FieldInfo property in s)
                {
                    if (property.GetValue(obj) is IEnumerable)
                        foreach (var item in (IEnumerable)property.GetValue(obj))
                        {
                            TreeNode c = new TreeNode(property.ToString(), item.ToString());
                            node.Childs.Add(c);
                            ReadFromAmetaData(item, c,currentid,property.ToString());
                            currentid++;


                        }
                    else
                    {
                        TreeNode c = new TreeNode(property.ToString(), null);
                        ReadFromAmetaData(property, c,currentid,property.ToString());
                        currentid++;

                    }
                }
            }
           
            return temp;


        }
        public void StartReading(string File,ObservableCollection<TreeNode> nodes)
        {
            try
            {
                int id = 0;
                logger.Trac("StartReading");
                AMetadata = new AssemblyMetadata(Assembly.LoadFile(File));
                foreach (NamespaceMeta d in AMetadata.m_Namespaces)
                {
                    ReadFromAmetaData(d, nodes[0],id,"Asembly");
                    id++;
                }

                // Save(" ");
                //Load(" ");
            }
            catch (Exception i)
            {
                logger.Error(i.ToString());
            }
        }
        public void StartReadingExisting(ObservableCollection<TreeNode> nodes)
        {
            try
            {
                int id = 0;
                logger.Trac("StartReading");
              
                foreach (NamespaceMeta d in AMetadata.m_Namespaces)
                {
                    ReadFromAmetaData(d, nodes[0], id, "Asembly");
                    id++;
                }

                // Save(" ");
                //Load(" ");
            }
            catch (Exception i)
            {
                logger.Error(i.ToString());
            }
        }
    }
}
