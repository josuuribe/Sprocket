# Simple example

This section will provide a simple example about the main idea, wiki explain more features in detail like serialize rules. 

## Creating entity
First you need an entity, a class where rules will act on.
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
    
    public void Walk()
    {
        this.DistanceTravelled++;
    }
}
```

## Creating command
Now we need to create commands, commands are objects that will perform actions on the object, e.g:
```C#
public class RunCommand : BooleanCommand<Person>
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

This command will call method ``` Run() ``` on ```Person``` class, the class ```BooleanCommand``` is a command class that returns a boolean and execute some code, in this case calls ```Run()``` and ```Walk()```.

## Creating rule
Now we create the rule, we are going to use the commands ```RunCommand``` and ```WalkCommand
```, so we create the commands and invoke ```Match()``` on the object person ```p``` using the created expression ```op```.

There are different expressions that can be used, in this case ```op``` represents a batch operation, all commands will be executed in order given, ```Match``` returns true if it matches or false if it does not match or there has been some error, in this case the rule only executes methods so ```res``` only indicates the success about the process.
```C#
public void RunAndWalk()
{
    RunCommand rc = new RunCommand();
    WalkCommand wc = new WalkCommand();
    Person p = new Person();
    
    var op = (rc + wc);

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
