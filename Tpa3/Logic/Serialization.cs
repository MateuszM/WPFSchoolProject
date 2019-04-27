﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization;
using Tpa3.Models;

namespace Tpa3.Logic
{
    [Export(typeof(ISerialize))]
    public class Serialization :ISerialize
    {
        public Serialization()
        {
            //  Serialize(file, Tr);
           // ListOfTypes = AssemblyMetadata;
        }
        
        IEnumerable<Type> ListOfTypes;
        
        public void Serialize(String filename, TreeNode Tree)
        { 
        var serializer = new DataContractSerializer(typeof(TreeNode));
            using (FileStream stream = File.Create(filename))
            {
                serializer.WriteObject(stream, Tree);
            }
}
        public void Serialize(String filename, AssemblyMetadata Adata)
        {
            var settings = new DataContractSerializerSettings();
            settings.PreserveObjectReferences = true;
            
            var serializer = new DataContractSerializer(typeof(AssemblyMetadata),settings);

            using (FileStream stream = File.Create(filename))
            {

                serializer.WriteObject(stream,Adata);

            }
        }
        public TreeNode Deserialization(String filename)
        {
            var serializer = new DataContractSerializer(typeof(TreeNode));
            using (FileStream stream = File.OpenRead(filename))
            {
                TreeNode data = (TreeNode)serializer.ReadObject(stream);
                return data;
            }
        }
        public AssemblyMetadata DeserializationAmeta(string filename)
        {
            var serializer = new DataContractSerializer(typeof(AssemblyMetadata));
            using (FileStream stream = File.OpenRead(filename))
            {
                AssemblyMetadata data = (AssemblyMetadata)serializer.ReadObject(stream);
                return data;
            }
        }

    }
}
