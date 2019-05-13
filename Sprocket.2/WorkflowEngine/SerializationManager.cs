using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Parts.Elements.Operators;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Workflows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        #region ·   Constructor ·
        internal SerializationManager()
        {
            //CommonTypes = new List<Type>();
            //CommonTypes.AddRange(STATIC_TYPES);
        }
        #endregion

        #region ·   Methods   ·
        /// <summary>
        /// Filter types removing generics, datacontracts, interfaces and abstract classes.
        /// </summary>
        /// <param name="types">Types to filter.</param>
        /// <returns>A List with types filtered.</returns>
        private List<Type> Filter(Assembly assembly)
        {
            var types = assembly.GetTypes();
            var filteredTypes = new List<Type>();
            Action<Type, Type> createType = (t1, t2) =>
            {
                if(t1.GetInterfaces().Where(t => t.GetTypeInfo().IsGenericType).Any(t => t.GetGenericTypeDefinition() == t2.GetGenericTypeDefinition()))
                {
                    filteredTypes.Add(t1);
                }
                /*
                var type = t1.GetInterfaces().Where(t => t.GetTypeInfo().IsGenericType).FirstOrDefault(t => t.GetGenericTypeDefinition() == t2.GetGenericTypeDefinition());
                if (type != null)
                {
                    var genericTypes = type.GetGenericArguments();
                    type.GetGenericTypeDefinition().MakeGenericType(new Type[] { genericTypes[0], genericTypes[1] });
                    if (!filteredTypes.Contains(type))
                        filteredTypes.Add(type);
                }
                */
            };
            foreach (var type in types)
            {
                createType(type, typeof(IOperate<,>));
                createType(type, typeof(IOperator<>));
            }
            return filteredTypes;
        }
        /// <summary>
        /// Load types required to serialize using given assemblies.
        /// </summary>
        /// <param name="assemblyNames">Assemblies with types.</param>
        /// <returns>An array with assemblies.</returns>
        /*internal Type[] Load(List<ActivityAssembly> assemblies)
        {
            List<Type> typesResult = new List<Type>(CommonTypes);
            foreach (var ass in assemblies)
            {
                if (assemblyTypes.ContainsKey(ass.AssemblyName))
                {
                    typesResult.AddRange(assemblyTypes[ass.AssemblyName]);
                }
                else
                {
                    var executingAssemblyName = Assembly.GetEntryAssembly();
                    var assembly = AssemblyLoadContext.GetAssemblyName(ass.AssemblyPath);
                    var assemblyLoaded = AssemblyLoadContext.GetLoadContext(executingAssemblyName).LoadFromAssemblyName(assembly);

                    var types = Filter(assemblyLoaded);

                    assemblyTypes.TryAdd(ass.AssemblyName, types);

                    typesResult.AddRange(types);
                }
            }
            return typesResult.ToArray();
        }*/

        private DataContractSerializer GetSerializer(Stage stage)
        {
            DataContractSerializerSettings dcss = new DataContractSerializerSettings();
            dcss.MaxItemsInObjectGraph = int.MaxValue;
            dcss.PreserveObjectReferences = true;
            //dcss.KnownTypes = Load(stage.ActivitiesAssemblyNames);
            dcss.DataContractResolver = new RuleTypeResolver<T>(stage);
            return new DataContractSerializer(typeof(ExpressionOperator<T>), dcss);
        }

        /// <summary>
        /// Deserialize a stage recovering the logical operator.
        /// </summary>
        /// <param name="stage">Stage to deserialize.</param>
        /// <returns>A logical operator</returns>
        internal IOperator<T> Deserialize(Stage stage)
        {
            DataContractSerializer serializer = GetSerializer(stage);

            byte[] byteArray = Encoding.UTF8.GetBytes(stage.XMLStage);

            using (var ms = new MemoryStream(byteArray))
            {
                Operator<T> logicalOperator = (Operator<T>)serializer.ReadObject(ms);
                return logicalOperator;
            }
        }
        /// <summary>
        /// Serializes a stage logical operator.
        /// </summary>
        /// <param name="rule">A logicalOperator.</param>
        /// <param name="stage">A stage to deserialize.</param>
        /// <returns>A string containing the Xml.</returns>
        internal string Serialize(Operator<T> rule, Stage stage)
        {
            DataContractSerializer serializer = GetSerializer(stage);

            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, rule);
                byte[] read = new byte[ms.Length];
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(read, 0, (int)ms.Length);
                return Encoding.UTF8.GetString(read);
            }
        }
        #endregion
    }
}
