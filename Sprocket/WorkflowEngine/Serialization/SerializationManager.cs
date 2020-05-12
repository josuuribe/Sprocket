using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Runtime.Serialization;
using System.Text;

namespace RaraAvis.Sprocket.WorkflowEngine
{
    internal class SerializationManager<T>
        where T : IElement
    {
        #region ·   Fields  ·
        //private const string SYSTEM = "SYSTEM";
        private RuleTypeResolver<T> typeResolver = null;


        //private List<Type> STATIC_TYPES = new List<Type>
        //{
        //    typeof(IfThen<T>), typeof(IfThenElse<T>),
        //    typeof(Not<T>),typeof(Yes<T>),
        //    typeof(And<T>),typeof(Or<T>),
        //    typeof(Batch<T>),typeof(Break<T>),typeof(JMP<T>),
        //    typeof(BoolWrapper<T>),typeof(Loop<T>),
        //    typeof(Equals<T,byte>),typeof(GreaterThan<T,byte>),typeof(GreaterThanOrEquals<T,byte>),typeof(LessThan<T,byte>),typeof(LessThanOrEquals<T,byte>),typeof(NotEquals<T,byte>),
        //    typeof(Equals<T,short>),typeof(GreaterThan<T,short>),typeof(GreaterThanOrEquals<T,short>),typeof(LessThan<T,short>),typeof(LessThanOrEquals<T,short>),typeof(NotEquals<T,short>),
        //    typeof(Equals<T,int>),typeof(GreaterThan<T,int>),typeof(GreaterThanOrEquals<T,int>),typeof(LessThan<T,int>),typeof(LessThanOrEquals<T,int>),typeof(NotEquals<T,int>),
        //    typeof(Equals<T,long>),typeof(GreaterThan<T,long>),typeof(GreaterThanOrEquals<T,long>),typeof(LessThan<T,long>),typeof(LessThanOrEquals<T,long>),typeof(NotEquals<T,long>),
        //    typeof(Equals<T,float>),typeof(GreaterThan<T,float>),typeof(GreaterThanOrEquals<T,float>),typeof(LessThan<T,float>),typeof(LessThanOrEquals<T,float>),typeof(NotEquals<T,float>),
        //    typeof(Equals<T,double>),typeof(GreaterThan<T,double>),typeof(GreaterThanOrEquals<T,double>),typeof(LessThan<T,double>),typeof(LessThanOrEquals<T,double>),typeof(NotEquals<T,double>),
        //    typeof(Equals<T,decimal>),typeof(GreaterThan<T,decimal>),typeof(GreaterThanOrEquals<T,decimal>),typeof(LessThan<T,decimal>),typeof(LessThanOrEquals<T,decimal>),typeof(NotEquals<T,decimal>),
        //    typeof(GenericWrapper<T,byte>),typeof(GenericWrapper<T,short>),typeof(GenericWrapper<T,int>),typeof(GenericWrapper<T,long>),typeof(GenericWrapper<T,float>),typeof(GenericWrapper<T,double>),typeof(GenericWrapper<T,decimal>),
        //    typeof(T),
        //};

        //internal List<Type> CommonTypes;
        #endregion

        private DataContractSerializer dataContractSerializer;

        #region ·   Constructor ·
        internal SerializationManager(SprocketConfiguration sprocketConfiguration)
        {
            //CommonTypes = new List<Type>();
            //CommonTypes.AddRange(STATIC_TYPES);
            List<Type> assemblyTypes = new List<Type>();
            foreach (var path in sprocketConfiguration.Paths)
            {
                var executingAssemblyName = Assembly.GetEntryAssembly();
                var assembly = AssemblyLoadContext.GetAssemblyName(path);
                var assemblyLoaded = AssemblyLoadContext.GetLoadContext(executingAssemblyName).LoadFromAssemblyName(assembly);
                assemblyTypes.AddRange(assemblyLoaded.GetTypes());
            }
            DataContractSerializerSettings dcss = new DataContractSerializerSettings();
            dcss.MaxItemsInObjectGraph = int.MaxValue;
            dcss.PreserveObjectReferences = true;
            dcss.KnownTypes = assemblyTypes;
            dcss.DataContractResolver = new RuleTypeResolver<T>();
            dataContractSerializer = new DataContractSerializer(typeof(Operator<T>), dcss);
        }
        #endregion

        #region ·   Methods   ·
        /// <summary>
        /// Deserialize a stage recovering the logical operator.
        /// </summary>
        /// <param name="stage">Stage to deserialize.</param>
        /// <returns>A logical operator</returns>
        internal IOperator<T> Deserialize(string xml)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(xml);

            using (var ms = new MemoryStream(byteArray))
            {
                Operator<T> logicalOperator = (Operator<T>)dataContractSerializer.ReadObject(ms);
                return logicalOperator;
            }
        }
        /// <summary>
        /// Serializes a stage logical operator.
        /// </summary>
        /// <param name="rule">A logicalOperator.</param>
        /// <param name="stage">A stage to deserialize.</param>
        /// <returns>A string containing the Xml.</returns>
        internal string Serialize(Operator<T> rule)
        {
            using (var ms = new MemoryStream())
            {
                dataContractSerializer.WriteObject(ms, rule);
                byte[] read = new byte[ms.Length];
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(read, 0, (int)ms.Length);
                return Encoding.UTF8.GetString(read);
            }
        }
        #endregion
    }
}
