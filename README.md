This library creates simple business rules that can be serialized and save in a datastore, this is useful for create dynamic rules and avoid create application logic too much, these rules can also be created dinamically. This library has the next characteristics:

- It does not use if
- Flow guided by design
- Anti-Null

# Simple example

This section will provide a simple example about the main idea, wiki explains more features in detail like rules serialization. 

## Creating entity
First you need an entity, a class where rules will act on.
```C#
public class Person
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public int Age { get; set; }

    public Person()
    {
        this.Family = new List<Person>();
        IsHungry = true;
    }

    public void Run()
    {
        this.DistanceTravelled+=2;
    }
    
    public void Walk()
    {
        this.DistanceTravelled++;
    }
}
```

## Creating command
Now we need to create commands, commands are objects that will perform actions on the object, the only requirement is inherit from *Operand<TTarget, TValue>* class where *TTarget* is the class that contains the methods to be executed by commands and *TValue* is the value returned by the command, e.g:
```C#
public class GetDistanceCommand : Operand<Person, int>
{
    public override int Process(Person element)
    {
        return element.DistanceTravelled;
    }
}

public class WalkCommand : Operand<Person, bool>
{
    public override bool Process(Person element)
    {
        element.Walk();
        return true;
    }
}
```

These commands will call property ``` DistanceTravelled ``` and method ``` Walk() ``` on a ```Person``` object.

## Creating rule
Now we are going to create the rule called ```rule```, we will do it using the previous commands, with these ones we will use method ```Process()``` in operator class using the object person ```person```.

There are different expressions that can be used, in this case ```rule``` represents a conditional operation, it checks if the distance travelled by person object is less than 10, if the condition is true the command will be executed otherwise nothing will happen and false will be returned.

```C#
public void Walk_If_True()
{
    var gdc = new GetDistanceCommand();
    var wc = new WalkCommand();
    Person person = new Person();
    
    var rule = (gdc < 10) % wc;

    var res = rule.Process(p);

    if(rule)
    {
      Console.WriteLine("It runs!");
    }
    else
    {
      Console.WriteLine("It does not run!");
    }
}
```

This is valid for simple rules, sometimes it's necessary save information in rule or get a more information, in that case we'll use RuleEngine, for it it's only necessary get one and use it, this is the same example with RuleEngine.

```C#
public void Walk_If_True()
{
    IRuleEngineService<Person> ruleEngineService = RuleEngineActivatorService<Person>.RuleEngine; // Get RuleEngine
    var gdc = new GetDistanceCommand();
    var wc = new WalkCommand();
    Person person = new Person();
    
    var rule = (gdc < 10) % wc;

    var res = ruleEngineService.Init(op, person); // Executes op in person 

    var text = ruleEngineService.Serializer.Serialize(op); // Serializes op

    if(res.ExecutionResult == ExecutionResult.Positive)
    {
      Console.WriteLine("It returns true and walk!");
    }
    else
    {
      Console.WriteLine("It returns false!");
    }
}
```

## Operators
This table shows all operators available:
- a, b, c ... n -> Commands
- \# -> Number
- op -> operation

|Operator|Name|Description|
|---|---|---|
|a / b / c ... / n| Batch | Executes all operands in given order|
|a > # | GreaterThan | Returns true if a is greater than #, otherwise false|
|a > # | GreaterThanOrEquals | Returns true if a is greater or equals than #, otherwise false|
|a < # | LessThan | Returns true if a is less than #, otherwise false|
|a <= # | LessThanOrEquals | Returns true if a is less or equals than #, otherwise false|
|a == b | Equals | Returns true if both operands are equals, otherwise false|
|a == b | NotEquals | Returns true if both operands are not equals, otherwise false|
| !a | Not | Performs not operation on operand result|
| a & b | AndAlso | Processes and without short-circuit evaluation|
| a && b | And | Processes and operation on operands a and b|
| a \| b | OrAlso | Processes or without short-circuit evaluation|
| a \|\| b | Or | Processes or on operands a and b|
| (op) % a | Jump (If-Then) | Executes op, if result is true executes operand a|
| (op) % (a,b) | Jump (If-Then-Else) | Executes op, if result is true executes operand a otherwise executes b|
| (op1) * (op2) | Loop | Executes op2 while op1 be true|
| (op) >> # | Bitwise operator add | Adds # to UserStatus variable in Rule object in a bitwise operation|
| (op) << # | Bitwise operator remove | Removes # to UserStatus variable in Rule object in a bitwise operation|
| (op1) % ~(op2) | Break | Stops execution if condition is met|
| +(op) | True | Executes op and returns true|
| -(op) | False | Executes op and returns false|


