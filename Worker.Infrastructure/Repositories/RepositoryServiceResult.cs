using System.Diagnostics.CodeAnalysis;

namespace Worker.Infrastructure.Repositories;

/// <summary>
/// Represents the result of a repository service operation.
/// </summary>
/// <typeparam name="T">Type of the result.</typeparam>
public class RepositoryServiceResult<T>
{
    [MemberNotNullWhen(true, nameof(Result))]
    public bool IsSuccess { get; }
    public T? Result { get; }
    public string Message { get; }
    public Exception? Exception { get; }

    /// <summary>
    /// Creates a successful result for the repository service.
    /// </summary>
    /// <param name="result">Result of the expected Type.</param>
    /// <param name="message">Message related to this success</param>
    public RepositoryServiceResult(T? result, string message)
    {
        IsSuccess = true;
        Result = result;
        Message = message;
    }

    /// <summary>
    /// Creates a failure result for the repository service.
    /// </summary>
    /// <param name="message">Message with a reason of the failure.</param>
    /// <param name="exception">Potential related exception for this failure.</param>
    public RepositoryServiceResult(string message, Exception? exception = null)
    {
        IsSuccess = false;
        Result = default;
        Message = message;
        Exception = exception;
    }
}
