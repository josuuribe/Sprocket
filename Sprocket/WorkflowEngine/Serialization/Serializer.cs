using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Serialization;
using RaraAvis.Sprocket.WorkflowEngine.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.Loader;

namespace RaraAvis.Sprocket.WorkflowEngine
{
    internal abstract class Serializer<TTarget>: ISerializer<TTarget>
        where TTarget : notnull
    {
        protected List<Type> knownTypes = new List<Type>();

        #region ·   Constructor ·
        protected Serializer()
        {
            foreach (var path in RuleEngineActivatorService<TTarget>.Configuration.Paths)
            {
                var executingAssemblyName = Assembly.GetEntryAssembly();
                var assembly = AssemblyLoadContext.GetAssemblyName(path);
                var assemblyLoaded = AssemblyLoadContext.GetLoadContext(executingAssemblyName).LoadFromAssemblyName(assembly);
                knownTypes.AddRange(assemblyLoaded.GetTypes());
            }
        }
        #endregion
        #region ·   Methods   ·
        [return: NotNull]
        public abstract string Serialize(IOperator<TTarget> @operator);
        [return: MaybeNull]
        public abstract IOperator<TTarget> Deserialize(string text);
        #endregion
    }
}
