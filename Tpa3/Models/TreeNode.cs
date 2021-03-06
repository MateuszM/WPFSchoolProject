﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Tpa3.Models
{
    [DataContract(IsReference = true)]
    public class TreeNode
    {
        public TreeNode()
        {

        }
        public TreeNode(String value,String typeName)
        {
            this.Childs = new List<TreeNode>();
            this.Value = value;
            this.TypeName = typeName;
        }
        [DataMember]
        public String _TypeName;
        public String TypeName
        {
            get { return _TypeName; }
            set
            {
                _TypeName = value;
            }
        }
        [DataMember]
        public String _Value;
        public String Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
            }
        }
        [DataMember]
        public List<TreeNode> _Childs;
        public List<TreeNode> Childs
        {
            get { return _Childs; }
            set { _Childs = value; }
        }
        
       
    }

}
