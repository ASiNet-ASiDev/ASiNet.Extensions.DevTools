using ASiNet.Extensions.DevTools.Enums;
using ASiNet.Extensions.DevTools.Exceptions;

namespace ASiNet.Extensions.DevTools.Interfaces;
public interface IAssociation
{

    public string Key { get; }

    public AssociationMode Mode { get; }
    /// <summary>
    /// 
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    /// <exception cref="TypeNotFoundException"></exception>
    /// <exception cref="TypeMismatchException"></exception>
    /// <exception cref="CreateInstanceException"></exception>
    /// <returns></returns>
    public TResult GetAssociation<TResult, TInput>() where TResult : class where TInput : class;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <exception cref="TypeNotFoundException"></exception>
    /// <exception cref="TypeMismatchException"></exception>
    /// <exception cref="CreateInstanceException"></exception>
    /// <returns></returns>
    public object GetAssociation(Type input);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="input"></param>
    /// <exception cref="TypeNotFoundException"></exception>
    /// <exception cref="TypeMismatchException"></exception>
    /// <exception cref="CreateInstanceException"></exception>
    /// <returns></returns>
    public TResult GetAssociation<TResult>(Type input) where TResult : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <exception cref="TypeNotFoundException"></exception>
    /// <exception cref="TypeMismatchException"></exception>
    /// <exception cref="CreateInstanceException"></exception>
    /// <returns></returns>
    public object GetAssociation<TInput>() where TInput : class;
}
