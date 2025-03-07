namespace ASiNet.Extensions.DevTools.Exceptions;
public class TypeMismatchException(Type expected, Type resulting)
    : Exception($"The resulting type[{resulting.FullName ?? resulting.Name}] does not match the expected[{expected.FullName ?? expected.Name}] one")
{
}
