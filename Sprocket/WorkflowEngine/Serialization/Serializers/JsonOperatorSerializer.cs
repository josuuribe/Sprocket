using Newtonsoft.Json;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaraAvis.Sprocket.WorkflowEngine.Serialization.Serializers
{
    internal class JsonOperatorSerializer<TElement> : ISerializer<TElement>
        where TElement : IElement
    {
        JsonSerializerSettings jsonSerializerSettings = null;

        public JsonOperatorSerializer(List<Type> knownTypes)
        {
            jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.TypeNameHandling = TypeNameHandling.All;
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            jsonSerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;
            jsonSerializerSettings.Context = new System.Runtime.Serialization.StreamingContext(System.Runtime.Serialization.StreamingContextStates.Other, "newtonsoft");

            knownTypes.Add(typeof(ValueTuple<,>));//Required by Jump
            var typesBinder = new KnownTypesBinder();
            typesBinder.KnownTypes = knownTypes;

            jsonSerializerSettings.SerializationBinder = typesBinder;
        }

        public IOperator<TElement> Deserialize(string json)
        {
            return (IOperator<TElement>)JsonConvert.DeserializeObject(json, jsonSerializerSettings);
        }

        public string Serialize(IOperator<TElement> @operator)
        {
            return JsonConvert.SerializeObject(@operator, @operator.GetType(), jsonSerializerSettings);
        }
    }
}
