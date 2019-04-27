using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tpa3.Models
{
 [DataContract]
    public class PropertyMetadata
    {
        public PropertyMetadata()
        {

        }
       public static IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props)
        {
            return from prop in props
                   where prop.GetGetMethod().GetVisible() || prop.GetSetMethod().GetVisible()
                   select new PropertyMetadata(prop.Name, TypeMetadata.EmitReference(prop.PropertyType));
        }

        #region private
        private string m_Name;
        [DataMember]
        [Key]
        public int ID { get; set; }
        public String Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        [DataMember]
        public TypeMetadata m_TypeMetadata;
        private PropertyMetadata(string propertyName, TypeMetadata propertyType)
        {
            m_Name = propertyName;
            m_TypeMetadata = propertyType;
        }
        public int ParentID { get; set; }
        public string ParentType { get; set; }
        #endregion

    }
}

