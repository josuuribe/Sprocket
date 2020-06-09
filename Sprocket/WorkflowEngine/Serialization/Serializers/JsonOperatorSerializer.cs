using Newtonsoft.Json;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using System;
using System.Composition;
using System.Diagnostics.CodeAnalysis;

namespace RaraAvis.Sprocket.WorkflowEngine.Serialization.Serializers
{
    [Export(typeof(ISerializer<>))]
    [ExportMetadata("serializationFormat", "json")]
    internal class JsonOperatorSerializer<TTarget> : Serializer<TTarget>
        where TTarget : notnull
    {

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
        public override IOperator<TTarget> Deserialize(string text)
        {
            return (IOperator<TTarget>)(JsonConvert.DeserializeObject(text, jsonSerializerSettings))!;
        }
        [return: NotNull]
        public override string Serialize(IOperator<TTarget> @operator)
        {
            return JsonConvert.SerializeObject(@operator, @operator.GetType(), jsonSerializerSettings);
        }
    }
}
