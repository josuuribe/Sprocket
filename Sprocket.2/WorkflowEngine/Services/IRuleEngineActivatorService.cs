using RaraAvis.Sprocket.Parts.Interfaces;

namespace RaraAvis.Sprocket.Services
{
    /// <summary>
    /// Expose all requires functionality a rule engine must adopt.
    /// </summary>
    /// <typeparam name="T">Element to process.</typeparam>
    //[Export(typeof(IRuleEngineActivatorService<>))]
    public interface IRuleEngineActivatorService<T>
        where T : IElement
    {
        IRuleEngineService<T> GetRuleEngine();
        IRuleEngineService<T> GetRuleEngine(string assemblyRuleEngine);
    }
}
