using System.Collections.Frozen;
using ASiNet.Extensions.DevTools.Base;
using ASiNet.Extensions.DevTools.Enums;
using ASiNet.Extensions.DevTools.Exceptions;
using ASiNet.Extensions.DevTools.Interfaces;

namespace ASiNet.Extensions.DevTools;
public class TwoWayAssociation : IAssociation
{
    internal TwoWayAssociation(string key, AssociationMode mode, List<AssociationContainerBase> containers)
    {
        Key = key;
        Mode = mode;
        Count = containers.Count * 2;
        _containers = Build(containers);
    }

    public string Key { get; }

    public AssociationMode Mode { get; }

    public int Count { get; }

    private FrozenDictionary<string, AssociationContainerBase> _containers;

    public TResult GetAssociation<TResult, TInput>() where TResult : class where TInput : class
    {
        if (_containers.TryGetValue(typeof(TInput).FullName ?? typeof(TInput).Name, out var container))
        {
            if (container.ContainerType is AssociationContainerType.GenericContainer)
            {
                var genericContainer = container as AssociationContainer<TInput, TResult> ??
                    throw new TypeMismatchException(typeof(AssociationContainer<TInput, TResult>), container.GetType());
                var result = genericContainer.MakeGenericInstance() ??
                    throw new CreateInstanceException(container.SecondType);
                return result; 
            }
            else
            {
                var obj = container.MakeInstance() ??
                    throw new CreateInstanceException(container.SecondType);
                var instance = obj as TResult ??
                    throw new TypeMismatchException(typeof(TResult), container.SecondType);
                return instance;
            }
        }
        throw new TypeNotFoundException(typeof(TInput));
    }

    public object GetAssociation(Type input)
    {
        if (_containers.TryGetValue(input.FullName ?? input.Name, out var container))
        {
            var obj = container.MakeInstance() ??
                    throw new CreateInstanceException(container.SecondType);
            return obj;
        }
        throw new TypeNotFoundException(input);
    }

    public TResult GetAssociation<TResult>(Type input) where TResult : class
    {
        if (_containers.TryGetValue(input.FullName ?? input.Name, out var container))
        {
            var obj = container.MakeInstance() ??
                    throw new CreateInstanceException(container.SecondType);
            return obj as TResult ??
                    throw new TypeMismatchException(typeof(TResult), container.SecondType);
        }
        throw new TypeNotFoundException(input);
    }

    public object GetAssociation<TInput>() where TInput : class
    {
        if (_containers.TryGetValue(typeof(TInput).FullName ?? typeof(TInput).Name, out var container))
        {
            var obj = container.MakeInstance() ??
                    throw new CreateInstanceException(container.SecondType);
            return obj;
        }
        throw new TypeNotFoundException(typeof(TInput));
    }


    private static FrozenDictionary<string, AssociationContainerBase> Build(List<AssociationContainerBase> containers)
    {
        var result = new Dictionary<string, AssociationContainerBase>(containers.Count * 2);

        foreach (var container in containers)
        {
            result.Add(container.FirstType.FullName ?? container.FirstType.Name, container);
            var backContainer = container.ContainerType is AssociationContainerType.GenericContainer ?
                AssociationHelper.MakeBackGenericContainer(container) :
                AssociationHelper.MakeBackContainer(container);
            result.Add(backContainer.FirstType.FullName ?? backContainer.FirstType.Name, backContainer);
        }

        return result.ToFrozenDictionary();
    }
}
