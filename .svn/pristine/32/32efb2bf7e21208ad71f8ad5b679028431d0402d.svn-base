using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Tpa3;
using Tpa3.Loggers;
using Tpa3.Logic;
using Tpa3.Models;
using Tpa3.ViewModel;



namespace ConsoleApp1
{
    public class SecondViewModelCh
    {
        Logger logger = new Logger();
        AssemblyMetadata AMetadata;
        List<TreeNode> _GreatParent;
        List<String> _lista = new List<string>();
        public List<String> lista
        {
            get { return _lista; }
            set
            {
                _lista = value;

            }
        }
        public List<TreeNode> GreatParent
        {
            get { return _GreatParent; }
            set
            {
                _GreatParent = value;
                //OnPropertyChanged("GreatParent");
            }
        }
        string Filename;

        public SecondViewModelCh(String filename, string type)
        {
            if (type == "Serialization")
            {
                ActualType = typeof(Serialization);
            }
            else
            {
                ActualType = typeof(DatabaseLogic);
            }
            Filename = filename;
            l = new ViewLogic();
            cat = new Catalog();
        }
        Catalog cat;
      public  void Save()
        {

            cat.Get(ActualType).Serialize(Filename, AMetadata);
        }
      public  void Load()
        {
            AMetadata = cat.Get(ActualType).DeserializationAmeta(Filename);
            ReadFromAmetaData();
        }
        public void ReadFromDll(String filename)
        {
            GreatParent = new List<TreeNode>();
            GreatParent.Add(new TreeNode("Parent", "None"));
            try
            {
                logger.Trac("Start constructor from Secondviewmodelch");
                AMetadata = new AssemblyMetadata(Assembly.LoadFile(filename));
                lista = GetNamespaceNames(AMetadata.m_Namespaces);

                foreach (NamespaceMeta d in AMetadata.m_Namespaces)
                {

                    Lista2(d, GreatParent[0]);
                }
                Display(GreatParent);
            }
            catch (Exception d)
            {
                logger.Error(d.ToString());
            }
        }
        public void ReadFromAmetaData()
        {
            GreatParent = new List<TreeNode>();
            GreatParent.Add(new TreeNode("Parent", "None"));
            lista = GetNamespaceNames(AMetadata.m_Namespaces);

            foreach (NamespaceMeta d in AMetadata.m_Namespaces)
            {
                Lista2(d, GreatParent[0]);
            }
            Display(GreatParent);
        }

        Type _actualType;
        Type ActualType
        {
            get { return _actualType; }
            set { _actualType = value; }
        }
        ViewLogic l;
        //Button d = new Button();
        
        public void Display(List<TreeNode> d)
        {
            foreach (TreeNode s in d)
            {
                Console.WriteLine(s.TypeName);
                Console.WriteLine(s.Value);
                Display(s.Childs);
            }
        }
        //public static bool IsKeyDown(Key key);
        List<String> GetNamespaceNames(IEnumerable<NamespaceMeta> ienum)
        {
            List<string> Namespacenames = new List<string>();

            foreach (NamespaceMeta p in ienum)
            {
                Namespacenames.Add(p.MNamespaceName);
            }
            return Namespacenames;
        }
        public List<String> Lista2(Object obj, TreeNode node)
        {

            List<String> d = new List<String>();
            if (obj != null)
            {
                TreeNode k = new TreeNode();
                Type p = obj.GetType();
                FieldInfo[] s = p.GetFields();
               
       
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
                        
                            Lista2(item, c);
                      
                        }
                    else
                    {
                        TreeNode c = new TreeNode(property.ToString(), null);
                        Lista2(property, c);
                    }

                }
              //  lista.Add(strk);

            }

            return d;


        }
    }
}
