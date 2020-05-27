using RaraAvis.Sprocket.RuleEngine.Interfaces;
using System;
using System.Composition;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;

namespace RaraAvis.Sprocket.WorkflowEngine.Serialization.Serializers
{
    [Export("serializer", typeof(ISerializer<>))]
    [ExportMetadata("serializationFormat", "xml")]
    internal class XmlOperatorSerializer<TTarget> : Serializer<TTarget>, ISerializer<TTarget>
        where TTarget : notnull
    {
        [DisallowNull]
        private readonly DataContractSerializer dataContractSerializer;
        [ImportingConstructor]
        public XmlOperatorSerializer():base()
        {
            DataContractSerializerSettings dcss = new DataContractSerializerSettings
            {
                MaxItemsInObjectGraph = int.MaxValue,
                PreserveObjectReferences = true
            };
            knownTypes.Add(typeof(Expression<>));
            knownTypes.Add(typeof(Func<>));
            dcss.KnownTypes = knownTypes;
            dcss.DataContractResolver = new RuleTypeResolver();
            dataContractSerializer = new DataContractSerializer(typeof(IOperator<TTarget>), dcss);
        }
        public override IOperator<TTarget> Deserialize(string text)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(text);

            using (var ms = new MemoryStream(byteArray))
            {
                IOperator<TTarget> logicalOperator = (IOperator<TTarget>)dataContractSerializer.ReadObject(ms);
                return logicalOperator;
            }
        }
        public override string Serialize(IOperator<TTarget> @operator)
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
