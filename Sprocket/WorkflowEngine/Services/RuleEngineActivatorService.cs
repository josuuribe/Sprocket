using Microsoft.Extensions.Configuration;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Collections.Generic;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.IO;
using System.Runtime.Loader;

namespace RaraAvis.Sprocket.WorkflowEngine.Services
{
    /// <summary>
    /// Service that manage RuleManager lifecycle.
    /// </summary>
    /// <typeparam name="T">Element to process.</typeparam>
    public static class RuleEngineActivatorService<T>
        where T : IElement
    {
        #region ·   Fields  ·
        public static SprocketConfiguration Configuration { get; set; }
        #endregion

        static RuleEngineActivatorService()
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
            .Build();
            Configuration = configuration.GetSection("Sprocket").Get<SprocketConfiguration>();
            Configuration.Paths.Add(Path.Combine(AppContext.BaseDirectory, "RaraAvis.Sprocket.dll"));
        }

        #region ·   Methods ·
        /// <summary>
        /// Gets a rule engine given assembly path.
        /// </summary>
        /// <param name="assemblyRuleEngine">Assembly with <see cref="IRuleEngineService<T>"/> path.</param>
        /// <returns>A rule manager.</returns>
        public static IRuleEngineService<T> GetRuleEngine()
        {
            IRuleEngineService<T> engine = null;
            var conventions = new ConventionBuilder();
            conventions.ForTypesDerivedFrom<IRuleEngineService<T>>().Export<IRuleEngineService<T>>().Shared();

            var configuration = new ContainerConfiguration();
            foreach (var assemblyRuleEngine in Configuration.Paths)
            {
                var assemblyName = AssemblyLoadContext.GetAssemblyName(assemblyRuleEngine);
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(assemblyName);
                configuration = configuration.WithAssembly(assembly, conventions);
            }

            using (var container = configuration.CreateContainer())
            {
                engine = container.GetExport<IRuleEngineService<T>>();
            }

            return engine;
        }
        #endregion
    }
}
