﻿using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.Services;
using System;
using System.Collections.Generic;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.IO;
using System.Runtime.Loader;

namespace RaraAvis.Sprocket.WorkflowEngine
{
    /// <summary>
    /// Service that manage RuleManager lifecycle.
    /// </summary>
    /// <typeparam name="T">Element to process.</typeparam>
    public class RuleEngineActivatorService<T> : IRuleEngineActivatorService<T>
        where T : IElement
    {
        #region ·   Fields  ·
        private static Dictionary<string, IRuleEngineService<T>> ruleEngineCache = new Dictionary<string, IRuleEngineService<T>>();
        #endregion

        #region ·   Methods ·
        /// <summary>
        /// Gets default rule engine.
        /// </summary>
        /// <returns>Default rule engine.</returns>
        public IRuleEngineService<T> GetRuleEngine()
        {
            return GetRuleEngine(Path.Combine(AppContext.BaseDirectory, "RaraAvis.Sprocket.dll"));
        }
        /// <summary>
        /// Gets a rule engine given assembly path.
        /// </summary>
        /// <param name="assemblyRuleEngine">Assembly with <see cref="IRuleEngineService<T>"/> path.</param>
        /// <returns>A rule manager.</returns>
        public IRuleEngineService<T> GetRuleEngine(string assemblyRuleEngine)
        {
            IRuleEngineService<T> engine = null;
            try
            {
                engine = ruleEngineCache[assemblyRuleEngine];
            }
            catch (KeyNotFoundException)
            {
                var conventions = new ConventionBuilder();
                conventions.ForTypesDerivedFrom<IRuleEngineService<T>>().Export<IRuleEngineService<T>>().Shared();

                var configuration = new ContainerConfiguration();
                var assemblyName = AssemblyLoadContext.GetAssemblyName(assemblyRuleEngine);
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(assemblyName);

                configuration = configuration.WithAssembly(assembly, conventions);

                using (var container = configuration.CreateContainer())
                {
                    engine = container.GetExport<IRuleEngineService<T>>();
                }
                ruleEngineCache.Add(assemblyRuleEngine, engine);
            }
            return engine;
        }
        #endregion
    }
}
