using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using RaraAvis.Sprocket.WorkflowEngine.Services;
using System;

namespace RaraAvis.Sprocket.Tests.Fakes.SyworflowEngineTestem
{
    public class WorkflowEngineTest
    {
        private readonly IRuleEngineService<Person> ruleEngineServiceXml;
        private readonly IRuleEngineService<Person> ruleEngineServiceJson;

        public WorkflowEngineTest()
        {
            RuleEngineActivatorService<Person>.Configuration.SerializationFormat = "xml";
            ruleEngineServiceXml = RuleEngineActivatorService<Person>.RuleEngine;
            RuleEngineActivatorService<Person>.Configuration.SerializationFormat = "json";
            ruleEngineServiceJson = RuleEngineActivatorService<Person>.RuleEngine;
        }

        public Rule<Person> Start(IOperator<Person> @operator, Person p)
        {
            Person personXml = p;
            Person personJson = (Person)p.Clone();

            var xml = ruleEngineServiceXml.Serializer.Serialize(@operator);
            var opXml = ruleEngineServiceXml.Serializer.Deserialize(xml);

            var json = ruleEngineServiceJson.Serializer.Serialize(@operator);
            var opJson = ruleEngineServiceJson.Serializer.Deserialize(json);

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

            var xml = ruleEngineServiceXml.Serializer.Serialize(@operator);
            var opXml = ruleEngineServiceXml.Serializer.Deserialize(xml);

            var json = ruleEngineServiceJson.Serializer.Serialize(@operator);
            var opJson = ruleEngineServiceJson.Serializer.Deserialize(json);

            var resJson = opXml.Process(personJson);
            var resXml = opJson.Process(personXml);

            bool equals = true;
            equals &= resJson == resXml;

            return equals ? resXml : throw new Exception("Mismatch json/xml");
        }

        public bool MatchNullSerializer(IOperator<Person> @operator, Person p)
        {
            RuleEngineActivatorService<Person>.Configuration.SerializationFormat = "wrong";
            var ruleEngineService = RuleEngineActivatorService<Person>.RuleEngine;
            var serialized = ruleEngineService.Serializer.Serialize(@operator);
            var op = ruleEngineService.Serializer.Deserialize(serialized);
            return op.Process(p);
        }
    }
}
