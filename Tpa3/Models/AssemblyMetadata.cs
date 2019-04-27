using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace Tpa3.Models
{
    [KnownType(typeof(Tpa3.Models.AssemblyMetadata))]
    [DataContract(IsReference = true)]
   [Serializable]
    public class AssemblyMetadata
    {
        public AssemblyMetadata()
        {

        }
        public AssemblyMetadata(Assembly assembly)
        {
            m_Name = assembly.ManifestModule.Name;
            m_Namespaces = from Type _type in assembly.GetTypes()
                           where _type.GetVisible()
                           group _type by _type.GetNamespace() into _group
                           orderby _group.Key
                           select new NamespaceMeta(_group.Key, _group);
            
        }
        [DataMember]
        string m_Name;
        [System.ComponentModel.DataAnnotations.Key]
       public int ID { get; set; }
        public string MName
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
       [DataMember]
        public IEnumerable<NamespaceMeta> m_Namespaces;
        public virtual IEnumerable<NamespaceMeta> M_Namespaces
        {
            get { return m_Namespaces; }
            set { m_Namespaces = value; }
        }
      







    }
}
