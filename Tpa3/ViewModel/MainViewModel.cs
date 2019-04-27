using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;
using Tpa3.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace Tpa3.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged    {
        Assembly assembly;
        AssemblyMetadata AMetadata;
        List<String> _Namespaces;
        Dictionary<String, List<String>> _NameOfTypes = new Dictionary<string, List<string>>();
        Dictionary<String, List<String>> NameOfTypes
        {
            get { return _NameOfTypes; }
            set { _NameOfTypes = value; }
        }
        
        List<String> _lista;
      public  List<String> lista{
            get { return _lista; }
    set
            {
               _lista = value;
                OnPropertyChanged("lista");
}
        }
        ObservableCollection<String> _NamesOfTypes1 = new ObservableCollection<String>();
      public  ObservableCollection<String> NamesOfTypes1
        {
            get { return _NamesOfTypes1; }
            set
            {
                _NamesOfTypes1 = value;
                OnPropertyChanged("NamesOfTypes1");
            }
        }
        private String selectedItem;
        public String SelectedItem
        {
            get {

                FirstName = selectedItem;
                // MessageBox.Show(FirstName);
                if (selectedItem != null)
                {
                   NameOfTypes.TryGetValue(selectedItem, out _lista);
                   ObservableCollection<String> d = new ObservableCollection<string>(lista);
                    NamesOfTypes1 = d;
                 //  MessageBox.Show(NameOfTypes1[0]);
                }
                return selectedItem;

            }
            set { selectedItem = value;
                
                OnPropertyChanged("SelectedItem"); }
        }
        
        public List<String> Namespaces
        {
            get { return _Namespaces; }
            set
            {
                _Namespaces = value;
                OnPropertyChanged("Namespaces");

            }
          
        }
    public MainViewModel()
        {
            SaveCmd = new RelayCommand(pars => Save());
            AMetadata = new AssemblyMetadata(Assembly.LoadFile(@"E:\MyProjects\Tpa3\Tpa3\GalaSoft.MvvmLight.dll"));
            Namespaces = GetNamespaceNames(AMetadata.m_Namespaces);
            NamesOfTypes1.Add("dziwne");
            MessageBox.Show(NamesOfTypes1[0]);


        }
        List<String> GetNamespaceNames(IEnumerable<NamespaceMeta> ienum)
        {
            List<string> Namespacenames = new List<string>();
            
            foreach (NamespaceMeta p in ienum)
            {
                Namespacenames.Add(p.MNamespaceName);
               NameOfTypes.Add(p.MNamespaceName,GetNamesOfTypes(p.m_Types));
                
            }
            return Namespacenames;
        }
        List<String> GetNamesOfTypes(IEnumerable<TypeMetadata> ienum)
        {
            List<String> Typenames = new List<string>();
            foreach(TypeMetadata p in ienum)
            {
                Typenames.Add(p.TypeName);
            }
            return Typenames;
        }



#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        public ICommand SaveCmd { get; set; }


        private String _FirstName = "Name";
        public String FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }
       
        private void Save()
        {
            // logika odpowiedzialna za zapis
        }
        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}