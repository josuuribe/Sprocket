using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace RaraAvis.Sprocket.WorkflowEngine.Serialization
{
    internal class KnownTypesBinder : ISerializationBinder
    {
        
        public IList<Type> KnownTypes { get; set; } = new List<Type>();
        public KnownTypesBinder(IList<Type> knownTypes)
        {
            this.KnownTypes = knownTypes;
        }
        public Type BindToType(string? assemblyName,  string typeName)
        {
            var type = KnownTypes.SingleOrDefault(t => t.Name == typeName);
            return (type?.ContainsGenericParameters ?? false) ? Type.GetType(assemblyName) : type!;
        }
        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = serializedType.AssemblyQualifiedName;
            typeName = serializedType.Name;
        }
    }
}
