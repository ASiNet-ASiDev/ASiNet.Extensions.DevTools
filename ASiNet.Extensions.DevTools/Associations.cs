using ASiNet.Extensions.DevTools.Exceptions;
using ASiNet.Extensions.DevTools.Interfaces;

namespace ASiNet.Extensions.DevTools;
public static class Associations
{

    private static Dictionary<string, IAssociation> _associations = [];


    /// <summary>
    /// Get
    /// </summary>
    /// <param name="key"> Key </param>
    /// <returns> Association </returns>
    /// <exception cref="NotFoundException"></exception>
    public static IAssociation Get(string key)
    {
        if(_associations.TryGetValue(key, out var value))
            return value;
        throw new NotFoundException(key);
    }


    public static AssociationBuilder CreateNew(string key) =>
        new(key);


    internal static void Add(IAssociation association)
    {
        if (!_associations.TryAdd(association.Key, association))
        {
            throw new ArgumentException($"An association with such a key already exists[{association.Key}]");
        }
    }
}
