using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;

namespace Tpa3.Models
{
    [DataContract(IsReference = true), KnownType(typeof(Attribute))]
    
    public class TypeMetadata
    {
        public TypeMetadata()
        {

        }

        #region constructors
        public TypeMetadata(Type type)
        {
            m_typeName = type.Name;
            m_DeclaringType = EmitDeclaringType(type.DeclaringType);
            m_Constructors = MethodMetadata.EmitMethods(type.GetConstructors());
            m_Methods = MethodMetadata.EmitMethods(type.GetMethods());
            m_NestedTypes = EmitNestedTypes(type.GetNestedTypes());
            m_ImplementedInterfaces = EmitImplements(type.GetInterfaces());
            m_GenericArguments = !type.IsGenericTypeDefinition ? null : TypeMetadata.EmitGenericArguments(type.GetGenericArguments());
            m_Modifiers = EmitModifiers(type);
            m_BaseType = EmitExtends(type);
            m_Properties = PropertyMetadata.EmitProperties(type.GetProperties());
            m_TypeKind = GetTypeKind(type);
            m_Attributes = type.GetCustomAttributes(false).Cast<Attribute>();
        }
        #endregion
        
        
        #region API
        
        public enum TypeKind
        {
            EnumType, StructType, InterfaceType, ClassType
        }
        internal static TypeMetadata EmitReference(Type type)
        {
            if (!type.IsGenericType)
                return new TypeMetadata(type.Name, type.GetNamespace());
            else
                return new TypeMetadata(type.Name, type.GetNamespace(), EmitGenericArguments(type.GetGenericArguments()));
        }
        internal static IEnumerable<TypeMetadata> EmitGenericArguments(IEnumerable<Type> arguments)
        {
            return from Type _argument in arguments select EmitReference(_argument);
        }
        #endregion

        #region private
        //vars
        private string m_typeName;
        [DataMember]
        [Key]
    public int ID { get; set; }
        public String TypeName
        {
            get { return m_typeName; }
            set { m_typeName = value; }
        }
        private string m_NamespaceName;
        [DataMember]
        public String NamespaceName
        {
            get { return m_NamespaceName; }
            set { m_NamespaceName = value; }
        }
        [DataMember]
        public TypeMetadata m_BaseType;
        [DataMember]
        public IEnumerable<TypeMetadata> m_GenericArguments;
        
        public Tuple<AccessLevel, SealedEnum, AbstractEnum> m_Modifiers;
        [DataMember]
        public Tuple<AccessLevel, SealedEnum, AbstractEnum> Tm_Modifiers
        {
            get { return m_Modifiers; }
            set { m_Modifiers = value; }
        }
        private TypeKind m_TypeKind;
        [DataMember]
        public TypeKind TypeKinds
        {
            get { return m_TypeKind; }
            set { m_TypeKind = value; }
        }
        
        public IEnumerable<Attribute> m_Attributes;
        [IgnoreDataMember]
        public IEnumerable<Attribute> M_Attributes
        {
            get { return m_Attributes; }
            set { value = m_Attributes; }
        }
        [DataMember]
        public IEnumerable<TypeMetadata> m_ImplementedInterfaces;
        [DataMember]
        public IEnumerable<TypeMetadata> m_NestedTypes;
        [DataMember]
        public IEnumerable<PropertyMetadata> m_Properties;
        [DataMember]
        public TypeMetadata m_DeclaringType;
        public TypeMetadata DeclaringType
        {
            get { return m_DeclaringType; }
            set { m_DeclaringType = value; }
        }
        public int ParentID { get; set; }
        public string ParentType { get; set; }
        [DataMember]
        public IEnumerable<MethodMetadata> m_Methods;
        [DataMember]
        public IEnumerable<MethodMetadata> m_Constructors;
        //constructors
        private TypeMetadata(string typeName, string namespaceName)
        {
            m_typeName = typeName;
            m_NamespaceName = namespaceName;
        }
        private TypeMetadata(string typeName, string namespaceName, IEnumerable<TypeMetadata> genericArguments) : this(typeName, namespaceName)
        {
            m_GenericArguments = genericArguments;
        }
        //methods
        private TypeMetadata EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            return EmitReference(declaringType);
        }
        private IEnumerable<TypeMetadata> EmitNestedTypes(IEnumerable<Type> nestedTypes)
        {
            return from _type in nestedTypes
                   where _type.GetVisible()
                   select new TypeMetadata(_type);
        }
        private IEnumerable<TypeMetadata> EmitImplements(IEnumerable<Type> interfaces)
        {
            return from currentInterface in interfaces
                   select EmitReference(currentInterface);
        }
        private static TypeKind GetTypeKind(Type type)
        {
            return type.IsEnum ? TypeKind.EnumType :
                   type.IsValueType ? TypeKind.StructType :
                   type.IsInterface ? TypeKind.InterfaceType :
                   TypeKind.ClassType;
        }
        static Tuple<AccessLevel, SealedEnum, AbstractEnum> EmitModifiers(Type type)
        {
            AccessLevel _access = AccessLevel.IsPrivate;
            if (type.IsPublic)
                _access = AccessLevel.IsPublic;
            else if (type.IsNestedPublic)
                _access = AccessLevel.IsPublic;
            else if (type.IsNestedFamily)
                _access = AccessLevel.IsProtected;
            else if (type.IsNestedFamANDAssem)
                _access = AccessLevel.IsProtectedInternal;
            SealedEnum _sealed = SealedEnum.NotSealed;
            if (type.IsSealed) _sealed = SealedEnum.IsSealed;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if (type.IsAbstract)
                _abstract = AbstractEnum.IsAbstract;
            return new Tuple<AccessLevel, SealedEnum, AbstractEnum>(_access, _sealed, _abstract);
        }
        private static TypeMetadata EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;
            return EmitReference(baseType);
        }
        #endregion

    }
}
