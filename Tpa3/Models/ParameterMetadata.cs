﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tpa3.Models
{
    [DataContract(IsReference = true)]
    public class ParameterMetadata
    {
        public ParameterMetadata()
        {

        }
        public ParameterMetadata(string name, TypeMetadata typeMetadata)
        {
            this.m_Name = name;
            this.m_TypeMetadata = typeMetadata;
        }

        //private vars
      
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

        public int ParentID { get; set; }
        public string ParentType { get; set; }
    }
}
