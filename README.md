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
public class RunCommand : Operand<Person, bool>
{
    public override bool Process(Person element)
    {
        element.Element.Run();
        return true;
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

These commands will call method ``` Run() ``` and ``` Walk() ``` on a ```Person``` object.

## Creating rule
Now we are going to create the rule, we are going to use the commands ```RunCommand``` and ```WalkCommand
```, with this will call method ```Process()``` on the object person ```perspn``` using the created rule ```rule```.

There are different expressions that can be used, in this case ```rule``` represents a batch operation, all commands will be executed in order given, ```Process()``` returns true if it matches or false if it does not match or there has been some error, in this case the rule only executes methods so ```res``` only indicates the success about the process.
```C#
public void RunAndWalk()
{
    RunCommand rc = new RunCommand();
    WalkCommand wc = new WalkCommand();
    Person person = new Person();
    
    var rule = rc / wc;

    var res = rule.Process(p);

    if(res==3)
    {
      Console.WriteLine("It runs!");
    }
    else
    {
      Console.WriteLine("It does not run!");
    }
}
```

## Operators
This table shows all operators available:
- a, b, c ... n -> Commands
- \# -> Number
- op -> operation

|OP|Name|Description|
|---|---|---|
|a + b + c ... + n| Batch | Executes all commands in given order.
|a > #| GreaterThan | Checks if result from command is greater than #
|a > #| GreaterThanOrEquals | Checks if result from command is greater or equals than #
|a < #| LessThan | Checks if result from command is less than #
|a <= #| LessThanOrEquals | Checks if result from command is less or equals than #
|(a)| Check | Executes command and returns command value that must be true or false
| (a - (b - (c - (n))) | Function | Executes funcion a using b as parameter for a and c as parameter for b ...
| !a | Not | Performs not operation on command result
| a & b | AndAlso | Processes and without short-circuit evaluation
| a && b | And | Processes and
| a \| b | OrAlso | Processes or without short-circuit evaluation
| a \|\| b | Or | Processes or
| (op) % a | IfElse | Executes op and if result is true executes command a
| (op) % a / b | IfElse | Executes op and if result is true executes command a otherwise executes b
| (op1) * (op2) | Loop | Executes op2 while op1 be true
| (op) >> # | Bitwise operator add | Shifts # based on op, if it's true it's shifted added
| (op) << # | Bitwise operator remove | Shifts # based on op, if it's true it's shifted removed
| (op1) % ~(op2) | Break | Stops execution if condition is met


