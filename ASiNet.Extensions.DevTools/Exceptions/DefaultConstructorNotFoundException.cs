namespace ASiNet.Extensions.DevTools.Exceptions;
public class DefaultConstructorNotFoundException(Type type) : Exception($"The constructor without parameters was not found in type[{type.FullName ?? type.Name}]")
{
}
