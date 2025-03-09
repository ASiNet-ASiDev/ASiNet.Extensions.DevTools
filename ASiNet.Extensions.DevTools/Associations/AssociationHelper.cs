using System.Linq.Expressions;
using System.Reflection;
using ASiNet.Extensions.DevTools.Associations.Base;
using ASiNet.Extensions.DevTools.Exceptions;

namespace ASiNet.Extensions.DevTools.Associations;
internal static class AssociationHelper
{
    public static AssociationContainerBase MakeBackGenericContainer(AssociationContainerBase instance)
    {
        var containerType = instance.GetType();
        if (!containerType.IsGenericType)
            throw new NonGenericTypeException(containerType);

        var newContainerType = typeof(AssociationContainer<,>)
            .MakeGenericType(instance.SecondType, instance.FirstType);

        var newContainer = Activator.CreateInstance(newContainerType, [instance.SecondConstructorObj, instance.FirstConstructorObj]) as AssociationContainerBase
            ?? throw new CreateInstanceException(newContainerType);
        return newContainer;
    }

    public static AssociationContainerBase MakeBackContainer(AssociationContainerBase instance)
    {
        if(instance is not AssociationContainer associationContainer)
            throw new NullReferenceException();
        var backContainer = new AssociationContainer(
                    associationContainer.SecondType,
                    associationContainer.FirstType,
                    associationContainer.SecondConstructor,
                    associationContainer.FirstConstructor);
        return backContainer;
    }


    public static Func<object> GetDefaultConstructor(Type type)
    {
        var ctor = type.GetConstructor(BindingFlags.Public | BindingFlags.Instance, []) ??
            throw new DefaultConstructorNotFoundException(type);
        return Expression.Lambda<Func<object>>(Expression.Convert(Expression.New(ctor), typeof(object))).Compile();
    }

}
