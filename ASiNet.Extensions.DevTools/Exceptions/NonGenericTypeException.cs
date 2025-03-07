namespace ASiNet.Extensions.DevTools.Exceptions;
public class NonGenericTypeException(Type type) : Exception($"{type.FullName ?? type.Name}")
{
}
