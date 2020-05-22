using Newtonsoft.Json;
using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Elements.Casts;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using RaraAvis.Sprocket.WorkflowEngine.Serialization;
using RaraAvis.Sprocket.WorkflowEngine.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace RaraAvis.Sprocket.WorkflowEngine
{
    internal class SerializationManager<T>
        where T : IElement
    {
        //private DataContractSerializer dataContractSerializer;
        //private JsonSerializerSettings jsonSerializerSettings;

        private readonly ISerializer<T> serializer;

        #region ·   Constructor ·
        internal SerializationManager(SprocketConfiguration sprocketConfiguration)
        {
            var assemblyTypes = new List<Type>();
            foreach (var path in sprocketConfiguration.Paths)
            {
                var executingAssemblyName = Assembly.GetEntryAssembly();
                var assembly = AssemblyLoadContext.GetAssemblyName(path);
                var assemblyLoaded = AssemblyLoadContext.GetLoadContext(executingAssemblyName).LoadFromAssemblyName(assembly);
                assemblyTypes.AddRange(assemblyLoaded.GetTypes());
            }

            switch (sprocketConfiguration.SerializationFormat.ToLower())
            {
                case "xml":
                    serializer = new XmlOperatorSerializer<T>(assemblyTypes);
                    break;
                case "json":
                    serializer = new JsonOperatorSerializer<T>(assemblyTypes);
                    break;
            }

            //switch (sprocketConfiguration.SerializationFormat.ToLower())
            //{
            //    case "xml":
            //        DataContractSerializerSettings dcss = new DataContractSerializerSettings();
            //        dcss.MaxItemsInObjectGraph = int.MaxValue;
            //        dcss.PreserveObjectReferences = true;
            //        dcss.KnownTypes = AssemblyTypes;
            //        dcss.DataContractResolver = new RuleTypeResolver<T>();
            //        dataContractSerializer = new DataContractSerializer(typeof(Operator<T>), dcss);
            //        serialize = XmlSerialize;
            //        deserialize = XmlDeserialize;
            //        break;
            //    case "json":
            //        jsonSerializerSettings = new JsonSerializerSettings();
            //        jsonSerializerSettings.TypeNameHandling = TypeNameHandling.All;
            //        jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            //        jsonSerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;
            //        AssemblyTypes.Add(typeof(ValueTuple<,>));//Required by Jump
            //        var typesBinder = new KnownTypesBinder();
            //        typesBinder.KnownTypes = AssemblyTypes;
            //        jsonSerializerSettings.SerializationBinder = typesBinder;
            //        serialize = JsonSerialize;
            //        deserialize = JsonDeserialize;
            //        break;
            //}
        }
        #endregion

        #region ·   Methods   ·
        internal string Serialize(IOperator<T> op)
        {
            return serializer.Serialize(op);
        }

        internal IOperator<T> Deserialize(string text)
        {
            return serializer.Deserialize(text);
        }

        ///// <summary>
        ///// Deserialize a stage recovering the logical operator.
        ///// </summary>
        ///// <param name="stage">Stage to deserialize.</param>
        ///// <returns>A logical operator</returns>
        //private IOperator<T> XmlDeserialize(string xml)
        //{
        //    //byte[] byteArray = Encoding.UTF8.GetBytes(xml);

        //    //using (var ms = new MemoryStream(byteArray))
        //    //{
        //    //    IOperator<T> logicalOperator = (IOperator<T>)dataContractSerializer.ReadObject(ms);
        //    //    return logicalOperator;
        //    //}
        //}
        ///// <summary>
        ///// Serializes a stage logical operator.
        ///// </summary>
        ///// <param name="rule">A logicalOperator.</param>
        ///// <returns>A string containing the Xml.</returns>
        //private string XmlSerialize(IOperator<T> rule)
        //{
        //    //using (var ms = new MemoryStream())
        //    //{
        //    //    dataContractSerializer.WriteObject(ms, rule);
        //    //    byte[] read = new byte[ms.Length];
        //    //    ms.Seek(0, SeekOrigin.Begin);
        //    //    ms.Read(read, 0, (int)ms.Length);
        //    //    return Encoding.UTF8.GetString(read);
        //    //}
        //}

        //private IOperator<T> JsonDeserialize(string json)
        //{
        //    var o = JsonConvert.DeserializeObject(json, jsonSerializerSettings);
        //    return (IOperator<T>)o;
        //}

        //private string JsonSerialize(IOperator<T> op)
        //{
        //    //ValueAsOperate<T, bool> valueAsOperate = new ValueAsOperate<T, bool>(true);
        //    //OperateAsOperator<T, bool> operateAsOperator = new OperateAsOperator<T, bool>(valueAsOperate);
        //    //string s = JsonConvert.SerializeObject(operateAsOperator, typeof(OperateAsOperator<T, bool>), null);
        //    //jsonSerializerSettings.Converters.Add(new RuleConverter<T>());
        //    return JsonConvert.SerializeObject(op, op.GetType(), jsonSerializerSettings);
        //    //return JsonConvert.SerializeObject(op, typeof(OperateAsOperator<T, bool>), jsonSerializerSettings);
        //}
    }
    #endregion
}
