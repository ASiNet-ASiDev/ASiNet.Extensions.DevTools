using ASiNet.Extensions.DevTools.Enums;

namespace ASiNet.Extensions.DevTools.Interfaces;
public interface IAssociationBuilder
{
    public IAssociationBuilder Mode(AssociationMode mode);

    public IAssociationBuilder AddTransient<First, Second>() where First : class, new() where Second : class, new();

    public IAssociationBuilder AddTransient(Type firstType, Type secondType);

    public IAssociationBuilder AddTransient<First, Second>(Func<First> firstConstructor, Func<Second> secondConstructor);

    public IAssociationBuilder AddTransient(Type firstType, Type secondType, Func<object> firstConstructor, Func<object> secondConstructor);

    //public IAssociationBuilder AddSingleton<First, Second>(First firstInstance);

    //public IAssociationBuilder AddSingleton<First, Second>(Second secondInstance);


    public IAssociation Build();
}
