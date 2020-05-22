using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;

namespace RaraAvis.Sprocket.WorkflowEngine.Serialization.Serializers
{
    internal class XmlOperatorSerializer<TElement> : ISerializer<TElement>
        where TElement : IElement
    {
        private DataContractSerializer dataContractSerializer;

        public XmlOperatorSerializer(List<Type> knownTypes)
        {
            DataContractSerializerSettings dcss = new DataContractSerializerSettings();
            dcss.MaxItemsInObjectGraph = int.MaxValue;
            dcss.PreserveObjectReferences = true;
            knownTypes.Add(typeof(Expression<>));
            knownTypes.Add(typeof(Func<>));
            dcss.KnownTypes = knownTypes;
            dcss.DataContractResolver = new RuleTypeResolver<TElement>();
            dataContractSerializer = new DataContractSerializer(typeof(IOperator<TElement>), dcss);
        }

        public IOperator<TElement> Deserialize(string xml)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(xml);

            using (var ms = new MemoryStream(byteArray))
            {
                IOperator<TElement> logicalOperator = (IOperator<TElement>)dataContractSerializer.ReadObject(ms);
                return logicalOperator;
            }
        }

        public string Serialize(IOperator<TElement> @operator)
        {
            using (var ms = new MemoryStream())
            {
                dataContractSerializer.WriteObject(ms, @operator);
                byte[] read = new byte[ms.Length];
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(read, 0, (int)ms.Length);
                return Encoding.UTF8.GetString(read);
            }
        }
    }
}
