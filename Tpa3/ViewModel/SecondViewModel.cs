using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Tpa3.Loggers;
using Tpa3.Logic;
using Tpa3.Models;

namespace Tpa3.ViewModel
{
    public class SecondViewModel : INotifyPropertyChanged
    {
        // Assembly assembly;
        ViewLogic logic = new ViewLogic();

        List<String> _lista = new List<string>();
        static string ChosenFileName;
        static Microsoft.Win32.OpenFileDialog dlg;
        static Microsoft.Win32.SaveFileDialog save;
        static Microsoft.Win32.OpenFileDialog load;
        public static Microsoft.Win32.OpenFileDialog Dlg
        {
            get { return dlg; }
            set { dlg = value; }
        }
        public static Microsoft.Win32.SaveFileDialog Savedlg
        {
            get { return save; }
            set { save = value; }
        }
        public static Microsoft.Win32.OpenFileDialog Loaddlg
        {
            get { return load; }
            set { load = value; }
        }
        public List<String> Lista
        {
            get { return _lista; }
            set { _lista = value;
                OnPropertyChanged("lista");
            }
        }
        TreeNode d;
        public TreeNode LoadNode
        {
            get { return d; }

            set { d = value;
                OnPropertyChanged("LoadNode");
            }
        }

        ObservableCollection<TreeNode> listatree;
        public ObservableCollection<TreeNode> ListaTree
        {
            get { return listatree; }
            set {
                listatree = value;
                OnPropertyChanged("ListaTree"); }
        }
        Catalog j;
        Type actualType;

        ILogger logger = new Logger();
        public SecondViewModel()
        {
            j = new Catalog();
            dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".dll"; // Default file extension
            dlg.Filter = "Dll documents (.dll)|*.dll"; // Filter files by extensio
            save = new Microsoft.Win32.SaveFileDialog();
            save.FileName = "Document1"; // Default file name
            save.DefaultExt = ".xml"; // Default file extension
            save.Filter = "xml DD  (xml)|*.xml"; // Filter files by extensio
            load = new Microsoft.Win32.OpenFileDialog();
            load.FileName = "Document2"; // Default file name
            load.DefaultExt = ".xml"; // Default file extension
            load.Filter = "xml documents (.xml)|*.xml"; // Filter files by extensio

            listatree = new ObservableCollection<TreeNode>();
            d = new TreeNode();
        }



        public void Save()
        {
            String file;
          
                Nullable<bool> result = Savedlg.ShowDialog();
                if (result == true)
                {
                    file = Savedlg.FileName;
                    // j.Serialize(file, GreatParent[0]);
                    
                    j.Get(actualType).Serialize(file, logic.AMetadata);
                }
            
           
            //    file = Savedlg.FileName;
                // j.Serialize(file, GreatParent[0]);
           //     j.Get(actualType).Serialize(file, logic.AMetadata);
            

        }


        public void Load()
        {
            try
            {
                GreatParent = new ObservableCollection<TreeNode>();
                GreatParent.Add(new TreeNode("Parent", "None"));
            }
            catch (Exception)
            {

            }
            String file;
            if(actualType==typeof(Serialization))
            { 
            Nullable<bool> result = Loaddlg.ShowDialog();
                if (result == true)
                {
                    file = Loaddlg.FileName;
                    AssemblyMetadata p = j.Get(actualType).DeserializationAmeta(file);
                    logic.AMetadata = p;

                    logic.StartReadingExisting(GreatParent);

                    //listatree.Add(new TreeNode("Parent", "None"));

                }
            }
           if (actualType == typeof(DatabaseLogic))
            {
               

                logic.AMetadata = j.Get(actualType).DeserializationAmeta(SelectedID);
                logic.StartReadingExisting(GreatParent);

            }
            //  j.Serialize(@"E:\MyProjects\Tpa3\Tpa3\DeserialTest.xml", d);
        }

        TreeView tre = new TreeView();
        ObservableCollection<TreeNode> _GreatParent;
        public ObservableCollection<TreeNode> GreatParent
        {
            get { return _GreatParent; }
            set {
                _GreatParent = value;
                OnPropertyChanged("GreatParent");
            }
        }

        private RelayCommand _commandSaveToDatabase;
        private RelayCommand _commandReadFromDatabase;
        public RelayCommand CommandSateToDatabase
        {
            get
            {
                return _commandSaveToDatabase ?? (_commandSaveToDatabase = new RelayCommand(
                   x =>
                   {
                       logic.Clear();
                     
                       // p.SaveTo("null",logic.AMetadata);

                   }));
            }

        }

        public void ReadFromDatabase()
        {
            GreatParent = new ObservableCollection<TreeNode>();
            GreatParent.Add(new TreeNode("Parent", "None"));
            TreeNode t;
            using (var DataContext = new DataContext())
            {
                List<NamespaceMeta> temp = DataContext.Namespace.ToList();
                List<TypeMetadata> temp1 = DataContext.TypeMeta.ToList();
                List<ParameterMetadata> temp2 = DataContext.Parameter.ToList();
                List<Models.PropertyMetadata> temp3 = DataContext.Property.ToList();
                foreach (NamespaceMeta p in temp)
                {
                    GreatParent[0].Childs.Add(new TreeNode(p.MNamespaceName, p.ParentType));

                }
                foreach (TypeMetadata s in temp1)
                {
                    GreatParent[0].Childs.Add(new TreeNode(s.TypeName, s.ParentType));

                }
                foreach (ParameterMetadata s in temp2)
                {
                    GreatParent[0].Childs.Add(new TreeNode(s.Name, s.ParentType));
                }
                foreach (Models.PropertyMetadata s in temp3)
                {
                    GreatParent[0].Childs.Add(new TreeNode(s.Name, s.ParentType));

                }


            }

        }

        public RelayCommand CommandLoadFromdatabase
        {
            get
            {
                return _commandReadFromDatabase ?? (_commandReadFromDatabase = new RelayCommand(
                   x =>
                   {
                       ReadFromDatabase();
                   }));
            }

        }
        string _selected;
        public string Selected
        {
            get { return _selected; }
            set { _selected = value;
                OnPropertyChanged("Selected");
                    MessageBox.Show(value);
                string x = "System.Windows.Controls.ComboBoxItem: XML";
                if(value == x)
                {
              //      MessageBox.Show(value);
                    actualType = typeof(Serialization);
                }
                else
                {
                    actualType = typeof(DatabaseLogic);
                   // MessageBox.Show(actualType.ToString());
                }
             //   MessageBox.Show(actualType.ToString());
            }
        }
        string _selectedid;
        public string SelectedID
        {
            get { return _selectedid; }
            set
            {
                _selectedid = value;
                OnPropertyChanged("SelectedID");

              }
      
            }
        

        private RelayCommand _combo;
        public RelayCommand Combo
         {
            get
            {
                return _combo ?? (_combo = new RelayCommand(
                   x =>
                   {
                   
                   }));
            }
        }

        private RelayCommand _commandTrace;
        public RelayCommand CommandTrace
        {
            get
            {
                return _commandTrace ?? (_commandTrace = new RelayCommand(
                   x =>
                   {
                       if(logic.logger.GetType()==typeof(Logger))
                       {
                           logic.logger = new Trace();
                       }
                       else
                       {
                           logic.logger = new Logger();
                       }
                   }));
            }

        }
        private RelayCommand _commandSaveTrace;
        public RelayCommand CommandSaveTrace
        {
            get
            {
                return _commandSaveTrace ?? (_commandSaveTrace = new RelayCommand(
                   x =>
                   {
                       logic.logger.Write(false);
                   }));
            }

        }

        private RelayCommand _commandopen;
        private RelayCommand _commandsave;
        private RelayCommand _commandload;
        public RelayCommand CommandLoad
        {
            get
            {
                return _commandload ?? (_commandload = new RelayCommand(
                   x =>
                   {
                       Load();
                   }));
            }

        }
        public RelayCommand CommandSave
        {
            get
            {
                return _commandsave ?? (_commandsave = new RelayCommand(
                   x =>
                   {
                       Save();
                   }));
            }

        }
        public RelayCommand CommandOpen
        {
            get
            {
                return _commandopen ?? (_commandopen = new RelayCommand(
                   x =>
                   {
                      MyAction();
                   }));
            }

        }

    
    private void MyAction()
        {
           
            Nullable<bool> result = Dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                GreatParent = new ObservableCollection<TreeNode>();
                GreatParent.Add(new TreeNode("Parent", "None"));
                // Open document
                ChosenFileName = Dlg.FileName;
                MessageBox.Show(ChosenFileName);
                logic.StartReading(ChosenFileName,GreatParent);
            }
           
        }


        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
