using System.Reflection;

namespace RoleRollsPocketEdition.Core;

public static class ServiceCollectionExtensions
{
    public static void AddTransientServices(this IServiceCollection services, Assembly assembly)
    {
        var interfaceBType = typeof(ITransientDependency);

        // Encontra todas as classes que implementam InterfaceB
        var implementations = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && interfaceBType.IsAssignableFrom(t));

        foreach (var implementation in implementations)
        {
            // Encontra todas as interfaces que a classe implementa além de InterfaceB
            var otherInterfaces = implementation.GetInterfaces()
                .Where(i => i != interfaceBType);

            // Registra a classe usando cada uma das interfaces adicionais
            foreach (var otherInterface in otherInterfaces)
            {
                services.Add(new ServiceDescriptor(otherInterface, implementation, ServiceLifetime.Transient));
            }
        }
    }
    public static void AddScopedServices(this IServiceCollection services, Assembly assembly)
    {
        var interfaceBType = typeof(IScopedDepedency);

        // Encontra todas as classes que implementam InterfaceB
        var implementations = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && interfaceBType.IsAssignableFrom(t));

        foreach (var implementation in implementations)
        {
            // Encontra todas as interfaces que a classe implementa além de InterfaceB
            var otherInterfaces = implementation.GetInterfaces()
                .Where(i => i != interfaceBType);

            // Registra a classe usando cada uma das interfaces adicionais
            foreach (var otherInterface in otherInterfaces)
            {
                services.Add(new ServiceDescriptor(otherInterface, implementation, ServiceLifetime.Scoped));
            }
        }
    }
}