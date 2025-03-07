namespace ASiNet.Extensions.DevTools.Exceptions;
public class TypeNotFoundException(Type type) : Exception($"Couldn't find the type[{type.FullName ?? type.Name}]")
{
}
