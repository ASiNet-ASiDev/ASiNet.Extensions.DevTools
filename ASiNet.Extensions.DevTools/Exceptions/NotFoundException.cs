namespace ASiNet.Extensions.DevTools.Exceptions;
public class NotFoundException(string key) : Exception($"The element with the specified key was not found[{key}]")
{
}
