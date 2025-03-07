namespace ASiNet.Extensions.DevTools.Exceptions;
public class CreateInstanceException(Type type) : Exception($"[Failed to create an instance of the object{type.FullName ?? type.Name}]")
{
}
