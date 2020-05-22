using RaraAvis.Sprocket.RuleEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;

namespace RaraAvis.Sprocket.WorkflowEngine
{
    internal class RuleTypeResolver<T> : DataContractResolver
        where T : IElement
    {
        private List<string> assemblyTypes = new List<string>();

        internal RuleTypeResolver()
        {
            //foreach (var ass in stage.ActivitiesAssemblyNames)
            //{
            //    if (!assemblyTypes.Contains(ass.AssemblyName))
            //    {
            //        var executingAssemblyName = Assembly.GetEntryAssembly();
            //        var assembly = AssemblyLoadContext.GetAssemblyName(ass.AssemblyPath);
            //        var assemblyLoaded = AssemblyLoadContext.GetLoadContext(executingAssemblyName).LoadFromAssemblyName(assembly);
            //        assemblyTypes.Add(ass.AssemblyName);
            //    }
            //}
        }

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
