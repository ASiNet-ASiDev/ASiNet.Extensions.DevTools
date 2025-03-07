using ASiNet.Extensions.DevTools.Enums;

namespace ASiNet.Extensions.DevTools.Base;
internal abstract class AssociationContainerBase
{
    public AssociationContainerBase(Type first, Type second, object firstConstructor, object secondConstructor, AssociationContainerType containerType)
    {
        ContainerType = containerType;
        FirstType = first;
        SecondType = second;
        FirstConstructorObj = firstConstructor;
        SecondConstructorObj = secondConstructor;
    }

    public AssociationContainerType ContainerType { get; }

    public object FirstConstructorObj { get; protected set; } = null!;
    public object SecondConstructorObj { get; protected set; } = null!;

    public Type FirstType { get; }

    public Type SecondType { get; }

    public abstract object? MakeInstance();
}

internal class AssociationContainer : AssociationContainerBase
{
    public AssociationContainer(Type first, Type second, Func<object> firstConstructor, Func<object> secondConstructor)
        : base(first, second, firstConstructor, secondConstructor, AssociationContainerType.NonGenericContainer)
    {
        FirstConstructor = firstConstructor;
        SecondConstructor = secondConstructor;
    }

    public Func<object> FirstConstructor { get; }
    public Func<object> SecondConstructor { get; }

    public override object? MakeInstance()
    {
        return SecondConstructor.Invoke();
    }
}

internal class AssociationContainer<TFirst, TSecond> : AssociationContainerBase
{
    public AssociationContainer(Func<TFirst> firstConstructor, Func<TSecond> secondConstructor)
        : base(typeof(TFirst), typeof(TSecond), firstConstructor, secondConstructor, AssociationContainerType.GenericContainer)
    {
        FirstConstructor = firstConstructor;
        SecondConstructor = secondConstructor;
    }

    public Func<TFirst> FirstConstructor { get; }
    public Func<TSecond> SecondConstructor { get; }


    public override object? MakeInstance() =>
        MakeGenericInstance();

    public TSecond MakeGenericInstance()
    {
        return SecondConstructor.Invoke();
    }
}
