using Microsoft.Extensions.Configuration;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using RaraAvis.Sprocket.WorkflowEngine.Serialization;
using RaraAvis.Sprocket.WorkflowEngine.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.IO;
using System.Runtime.Loader;

namespace RaraAvis.Sprocket.WorkflowEngine.Services
{
    /// <summary>
    /// Bootstrap for Rule engine.
    /// </summary>
    /// <typeparam name="TTarget">Target type to use.</typeparam>
    public static class RuleEngineActivatorService<TTarget>
        where TTarget : notnull
    {
        #region ·   Fields  ·
        internal static SprocketConfiguration Configuration { get; set; }
        private static readonly ContainerConfiguration containerConfiguration = null!;
        #endregion

#pragma warning disable CA1810 // Initialize reference type static fields inline
        static RuleEngineActivatorService()
#pragma warning restore CA1810 // Initialize reference type static fields inline
        {
            var config = new ConfigurationBuilder()
                                        .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
                                        .Build();
            Configuration = config.GetSection("Sprocket").Get<SprocketConfiguration>();
            Configuration.Paths.Add(Path.Combine(AppContext.BaseDirectory, Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location)));

            var configuration = new ContainerConfiguration();
            foreach (var assemblyRuleEngine in Configuration.Paths)
            {
                var path = Path.IsPathFullyQualified(assemblyRuleEngine) ? assemblyRuleEngine : Path.Combine(AppContext.BaseDirectory, assemblyRuleEngine);
                var assemblyName = AssemblyLoadContext.GetAssemblyName(path);
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(assemblyName);
                containerConfiguration = configuration.WithAssembly(assembly);
            }
        }

        #region ·   Methods ·
        /// <summary>
        /// Gets a new rule engine given assembly path.
        /// </summary>
        /// <returns>The rule engine service.</returns>
        public static IRuleEngineService<TTarget> RuleEngine
        {
            get
            {
                ISerializer<TTarget> serializer = null!;
                using (var container = containerConfiguration.CreateContainer())
                {
                    var constraints = new Dictionary<string, object>() { { "serializationFormat", Configuration.SerializationFormat.ToLowerInvariant() } };
                    var cc = new System.Composition.Hosting.Core.CompositionContract(typeof(ISerializer<TTarget>), null, constraints);
                    container.TryGetExport(cc, out object serializerService);
                    serializer = serializerService as ISerializer<TTarget> ?? new NullSerializer<TTarget>();
                }
                return new RuleEngineService<TTarget>(serializer);
            }
        }
        #endregion
    }
}
