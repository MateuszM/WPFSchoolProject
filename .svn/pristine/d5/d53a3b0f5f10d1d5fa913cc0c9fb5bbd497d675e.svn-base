using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Windows;

namespace Tpa3.Logic
{
    public class Catalog
    {
        [ImportMany(typeof(ISerialize))]
        public IEnumerable<ISerialize> List {get; set; }

        List<ISerialize> j;
        CompositionContainer container;
        public Catalog()
        {
            Compose();
            MessageBox.Show(j.Count.ToString());
        }

        public ISerialize Get(Type t)
        {
            foreach (ISerialize p in j)
            {
                if(p.GetType()==t)
                {
                    return p;
                }
            }
            return new Serialization();
        }
        private void Compose()
        {

            //Adds all the parts found in all assemblies in 
            //the same directory as the executing program

            AssemblyCatalog catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
           
            container = new CompositionContainer(catalog);

            try
            {
                container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                MessageBox.Show(compositionException.ToString());
            }
            MessageBox.Show(Environment.CurrentDirectory.ToString());
           j = this.List.ToList();
        }
    }
}
