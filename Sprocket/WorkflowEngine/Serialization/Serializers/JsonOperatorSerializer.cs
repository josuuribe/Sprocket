using Newtonsoft.Json;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Diagnostics.CodeAnalysis;

namespace RaraAvis.Sprocket.WorkflowEngine.Serialization.Serializers
{
    [Export("serializer", typeof(ISerializer<>))]
    [ExportMetadata("serializationFormat", "json")]
    internal class JsonOperatorSerializer<TTarget> : Serializer<TTarget>, ISerializer<TTarget>
        where TTarget : notnull
    {
        [DisallowNull]
        readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
        [ImportingConstructor]
        public JsonOperatorSerializer()
        {
            jsonSerializerSettings.TypeNameHandling = TypeNameHandling.All;
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            jsonSerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;
            jsonSerializerSettings.Context = new System.Runtime.Serialization.StreamingContext(System.Runtime.Serialization.StreamingContextStates.Other, "newtonsoft");
            knownTypes.Add(typeof(ValueTuple<,>));//Required by Jump
            var typesBinder = new KnownTypesBinder(knownTypes);
            jsonSerializerSettings.SerializationBinder = typesBinder;
        }
        [return: NotNull]
        public override IOperator<TTarget> Deserialize([DisallowNull]string text)
        {
            return (IOperator<TTarget>)(JsonConvert.DeserializeObject(text, jsonSerializerSettings))!;
        }
        [return: NotNull]
        public override string Serialize([DisallowNull]IOperator<TTarget> @operator)
        {
            return JsonConvert.SerializeObject(@operator, @operator.GetType(), jsonSerializerSettings);
        }
    }
}
