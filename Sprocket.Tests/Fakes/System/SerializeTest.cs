using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using RaraAvis.Sprocket.WorkflowEngine.Services;
using System;
using System.Linq;

namespace RaraAvis.Sprocket.Tests.Fakes.System
{
    public class WorflowEngineTest
    {
        private readonly IRuleEngineService<Person> ruleEngineServiceXml;
        private readonly IRuleEngineService<Person> ruleEngineServiceJson;

        public WorflowEngineTest()
        {
            RuleEngineActivatorService<Person>.Configuration.SerializationFormat = "xml";
            ruleEngineServiceXml = RuleEngineActivatorService<Person>.GetRuleEngine();
            RuleEngineActivatorService<Person>.Configuration.SerializationFormat = "json";
            ruleEngineServiceJson = RuleEngineActivatorService<Person>.GetRuleEngine();
        }

        public Rule<Person> Start(IOperator<Person> @operator, Person p)
        {
            Person personXml = p;
            Person personJson = (Person)p.Clone();

            var xml = ruleEngineServiceXml.Serialize(@operator);
            var opXml = ruleEngineServiceXml.Deserialize(xml);

            var json = ruleEngineServiceJson.Serialize(@operator);
            var opJson = ruleEngineServiceJson.Deserialize(json);

            var resJson = ruleEngineServiceXml.Init(opXml, personJson);
            var resXml = ruleEngineServiceXml.Init(opJson, personXml);

            bool equals = true;
            equals &= resJson.ExecutionResult == resXml.ExecutionResult;
            equals &= resJson.UserStatus == resXml.UserStatus;

            return equals ? resXml : throw new Exception("Mismatch json/xml");
        }

        public bool Match(IOperator<Person> @operator, Person p)
        {
            Person personXml = p;
            Person personJson = (Person)p.Clone();

            var xml = ruleEngineServiceXml.Serialize(@operator);
            var opXml = ruleEngineServiceXml.Deserialize(xml);

            var json = ruleEngineServiceJson.Serialize(@operator);
            var opJson = ruleEngineServiceJson.Deserialize(json);

            var resJson = opXml.Process(personJson);
            var resXml = opJson.Process(personXml);

            bool equals = true;
            equals &= resJson == resXml;

            return equals ? resXml : throw new Exception("Mismatch json/xml");
        }
    }
}
