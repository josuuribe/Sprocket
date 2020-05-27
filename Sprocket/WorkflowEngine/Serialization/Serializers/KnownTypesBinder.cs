using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace RaraAvis.Sprocket.WorkflowEngine.Serialization
{
    internal class KnownTypesBinder : ISerializationBinder
    {
        [DisallowNull]
        public IList<Type> KnownTypes { get; set; } = new List<Type>();
        public KnownTypesBinder(IList<Type> knownTypes)
        {
            this.KnownTypes = knownTypes;
        }
        [return: MaybeNull]
        public Type BindToType(string? assemblyName, [DisallowNull] string typeName)
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
