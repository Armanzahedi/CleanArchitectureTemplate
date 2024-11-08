using System.Reflection;

namespace CA.Presentation.Middlewares.GlobalExceptionHandler;

public static class ExceptionHandlerProvider
{
    private static readonly Dictionary<Type, object> Handlers = new();

    static ExceptionHandlerProvider()
    {
        var handlerTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.BaseType?.GetInterfaces().Any(i => i == typeof(IExceptionHandler)) == true).ToList();

        foreach (var type in handlerTypes)
        {
            var exceptionType = type.BaseType?.GetGenericArguments()[0];
            if (exceptionType != null)
            {
                var handlerInstance = Activator.CreateInstance(type);
                if (handlerInstance != null)
                {
                    Handlers[exceptionType] = handlerInstance;
                }
            }
        }
    }

    public static ExceptionHandler<TException>? GetHandler<TException>() where TException : Exception
    {
        if (Handlers.TryGetValue(typeof(TException), out var handler))
            return handler as ExceptionHandler<TException>;

        return null;
    }
    public static IExceptionHandler? GetHandler(Type exceptionType)
    {
        if (!typeof(Exception).IsAssignableFrom(exceptionType))
            throw new ArgumentException("Type must be a subclass of Exception", nameof(exceptionType));

        if (Handlers.TryGetValue(exceptionType, out var handler))
            return handler as IExceptionHandler;

        return null;
    }
}