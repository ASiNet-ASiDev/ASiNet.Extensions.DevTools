using ASiNet.Extensions.DevTools.Associations.Base;
using ASiNet.Extensions.DevTools.Associations.Enums;

namespace ASiNet.Extensions.DevTools.Associations;
public class AssociationBuilder(string key) : IAssociationBuilder
{

    public string Key { get; } = key;

    private AssociationMode _mode = AssociationMode.TwoWay;

    private List<AssociationContainerBase> _containers = [];

    public IAssociationBuilder Mode(AssociationMode mode)
    {
        _mode = mode;
        return this;
    }

    public IAssociationBuilder AddTransient<First, Second>() where First : class, new() where Second : class, new()
    {
        var newContainer = new AssociationContainer<First, Second>(() => new(), () => new());
        _containers.Add(newContainer);
        return this;
    }

    public IAssociationBuilder AddTransient<First, Second>(Func<First> firstConstructor, Func<Second> secondConstructor)
    {
        var newContainer = new AssociationContainer<First, Second>(firstConstructor, secondConstructor);
        _containers.Add(newContainer);
        return this;
    }

    public IAssociationBuilder AddTransient(Type firstType, Type secondType, Func<object> firstConstructor, Func<object> secondConstructor)
    {
        var newContainer = new AssociationContainer(firstType, secondType, firstConstructor, secondConstructor);
        _containers.Add(newContainer);
        return this;
    }

    public IAssociationBuilder AddTransient(Type firstType, Type secondType)
    {
        var newContainer = new AssociationContainer(firstType, secondType,
            () => AssociationHelper.GetDefaultConstructor(firstType),
            () => AssociationHelper.GetDefaultConstructor(secondType));
        _containers.Add(newContainer);
        return this;
    }

    //public IAssociationBuilder AddSingleton<First, Second>(First firstInstance)
    //{
    //    throw new NotImplementedException();
    //}

    //public IAssociationBuilder AddSingleton<First, Second>(Second secondInstance)
    //{
    //    throw new NotImplementedException();
    //}

    public IAssociation Build()
    {
        switch (_mode)
        {
            case AssociationMode.TwoWay:
                var twoWauAssociation = new TwoWayAssociation(Key, _mode, _containers);
                Association.Add(twoWauAssociation);
                return twoWauAssociation;
            case AssociationMode.OneWay:
                throw new NotImplementedException();
            case AssociationMode.OneWayBack:
                throw new NotImplementedException();
        }
        throw new NotImplementedException();
    }
}
