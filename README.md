# Simple example

We create first the entity we want to work with like, we'll need to implement ```IElement```.

## Creating entity

```C#
public class Person : IElement
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
}
```

## Creating command
Now we need to create commands, commands are objects that will perform actions on the object, e.g:
```C#
public class RunCommand : BooleanCommand<bool>
{
    public override bool Value(RuleElement<Person> element)
    {
        element.Element.Run();
        return true;
    }
}

public class WalkCommand : BooleanCommand<Person>
{
    public override bool Value(RuleElement<Person> element)
    {
        element.Element.Walk();
        return true;
    }
}
```

This command will call method ``` Run() ``` on ```Person``` class, the class ```BooleanCommand``` inherits from ``` Command<Entity, Value> ``` the first type is the entity this command will act in and the second is the return parameter, boolean in this case.

## Creating rule
Now we create the rule, we are going to use the commands for walk and run, so we create the commands and we call Match using the expression and the person object to execute with, ```Match(x,y)``` returns true if it matches or false if it does not match or there has been some error, in this case the rule only executes methods so ```res``` only indicates the success about the process.
```C#
public void RunAndWalk()
{
    RunCommand rc = new RunCommand();
    WalkCommand wc = new WalkCommand();
    Person p = new Person();
    
    Operator<Person> op = (rc + wc);

    var res = st.Match(op, p);

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
