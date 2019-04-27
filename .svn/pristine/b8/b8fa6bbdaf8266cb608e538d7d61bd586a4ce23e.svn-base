using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tpa3.Models
{
    [KnownType(typeof(Tpa3.Models.NamespaceMeta))]
    [DataContract(IsReference = true)]
    public class NamespaceMeta
    {
       public NamespaceMeta()
        {

        }
        public NamespaceMeta(string name, IEnumerable<Type> types)
        {
            m_NamespaceName = name;
            m_Types = from type in types orderby type.Name select new TypeMetadata(type);
        }
        
        private string m_NamespaceName;
        [Key]
       public int ID { get; set; }
        [ DataMember]
        public string MNamespaceName
        {
            get { return m_NamespaceName; }
            set { m_NamespaceName = value; }
        }
        
       [DataMember]
      public IEnumerable<TypeMetadata> m_Types;

        public int ParentID { get; set; }
        public string ParentType { get; set; }
    }
}
