using System;
using System.Runtime.Serialization;
using System.Xml;

namespace RaraAvis.Sprocket.WorkflowEngine
{
    internal class RuleTypeResolver : DataContractResolver
    {
        internal RuleTypeResolver()
        { }
        public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
        {
            return Type.GetType(typeName);
        }
        public override bool TryResolveType(Type type, Type declaredType, DataContractResolver knownTypeResolver, out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
        {
            XmlDictionary dictionary = new XmlDictionary();
            typeName = dictionary.Add($"{type.FullName},{type.Assembly.FullName}");
            typeNamespace = dictionary.Add("http://tempuri.org");
            return true;
        }
    }
}
