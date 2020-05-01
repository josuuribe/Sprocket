using RaraAvis.Sprocket.Parts.Interfaces;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Operators
{
    /// <summary>
    /// A base class for all logical operators.
    /// </summary>
    /// <typeparam name="TElement">An IElement object.</typeparam>
    //[KnownType("GetTypes")]
    [DataContract]
    public abstract class ExpressionOperator<TElement> : Operator<TElement>
        where TElement : IElement
    {
        //public static Batch<TElement> operator +(ExpressionOperator<TElement> operatorLeft, ExpressionOperator<TElement> operatorRight)
        //{
        //    Batch<TElement> batch = new Batch<TElement>();
        //    BooleanCommandWrapper<TElement> booleanMethodWrapperLeft = new BooleanCommandWrapper<TElement>(operatorLeft);
        //    batch.Add(booleanMethodWrapperLeft);
        //    BooleanCommandWrapper<TElement> booleanMethodWrapperRight = new BooleanCommandWrapper<TElement>(operatorRight);
        //    batch.Add(booleanMethodWrapperRight);
        //    return batch;
        //}

        //public static ExpressionOperator<TElement> operator %(ExpressionOperator<TElement> operatorIf, IfThenElse<TElement> operatorIfThenElse)
        //{
        //    operatorIfThenElse.If = operatorIf;
        //    return operatorIfThenElse;
        //}

        //public static Operator<TElement> operator ^(ExpressionOperator<TElement> operatorLeft, int stageId)
        //{
        //    IfThen<TElement> ifThen = new IfThen<TElement>();
        //    ifThen.If = operatorLeft;
        //    JMP<TElement> jmp = new JMP<TElement>();
        //    jmp.Parameters = stageId;
        //    FunctionWrapper<TElement, int, bool> function = new FunctionWrapper<TElement, int, bool>(jmp);
        //    ifThen.Then = function;
        //    return function;
        //}

        //public static ExpressionOperator<TElement> operator ~(ExpressionOperator<TElement> expressionIf)
        //{
        //    IfThen<TElement> ifThen = new IfThen<TElement>();
        //    ifThen.If = expressionIf;
        //    Break<TElement> brk = new Break<TElement>();
        //    OperateWrapper<TElement> wrapperBrk = new OperateWrapper<TElement>(brk);
        //    ifThen.Then = wrapperBrk;
        //    return ifThen;
        //}

        //private static Type[] GetTypes()
        //{
        //    Type[] t = new Type[0];
        //    return t;
        //}
    }
}
