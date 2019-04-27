using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tpa3.Models
{
    [DataContract(IsReference = true)]
    public class MethodMetadata
    {
        
        
        public static IEnumerable<MethodMetadata> EmitMethods(IEnumerable<MethodBase> methods)
        {
            return from MethodBase _currentMethod in methods
                   where _currentMethod.GetVisible()
                   select new MethodMetadata(_currentMethod);
        }

        #region private
        //vars
       [ DataMemberAttribute]
        [Key]
       public int ID { get; set; }
        string m_Name;
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        [DataMemberAttribute]
        public IEnumerable<TypeMetadata> m_GenericArguments;
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> m_Modifiers;
      [DataMember]
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Tm_Modifiers
        {
            get { return m_Modifiers; }
            set { m_Modifiers = value; }
        }
        public string GetModifiersString
        {
            get { return m_Modifiers.ToString(); }
            
        }
        [DataMember]
        public TypeMetadata m_ReturnType;
        [DataMember]
        public bool m_Extension;
        public bool M_Extension
        {
            get { return m_Extension; }
        }
        public int ParentID { get; set; }
        public string ParentType { get; set; }
        
       [DataMember]
        public IEnumerable<ParameterMetadata> m_Parameters;
        //constructor
        private MethodMetadata(MethodBase method)
        {
            m_Name = method.Name;
            m_GenericArguments = !method.IsGenericMethodDefinition ? null : TypeMetadata.EmitGenericArguments(method.GetGenericArguments());
            m_ReturnType = EmitReturnType(method);
            m_Parameters = EmitParameters(method.GetParameters());
            m_Modifiers = EmitModifiers(method);
            m_Extension = EmitExtension(method);
        }
        //methods
        private static IEnumerable<ParameterMetadata> EmitParameters(IEnumerable<ParameterInfo> parms)
        {
            return from parm in parms
                   select new ParameterMetadata(parm.Name, TypeMetadata.EmitReference(parm.ParameterType));
        }
        private static TypeMetadata EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            return TypeMetadata.EmitReference(methodInfo.ReturnType);
        }
        private static bool EmitExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }
        private static Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> EmitModifiers(MethodBase method)
        {
            AccessLevel _access = AccessLevel.IsPrivate;
            if (method.IsPublic)
                _access = AccessLevel.IsPublic;
            else if (method.IsFamily)
                _access = AccessLevel.IsProtected;
            else if (method.IsFamilyAndAssembly)
                _access = AccessLevel.IsProtectedInternal;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if (method.IsAbstract)
                _abstract = AbstractEnum.IsAbstract;
            StaticEnum _static = StaticEnum.NotStatic;
            if (method.IsStatic)
                _static = StaticEnum.IsStatic;
            VirtualEnum _virtual = VirtualEnum.NotVirtual;
            if (method.IsVirtual)
                _virtual = VirtualEnum.IsVirtual;
            return new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>(_access, _abstract, _static, _virtual);
        }
        #endregion

    }
}
