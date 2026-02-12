// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics;
using System.Reflection;

namespace Gardener.Core.Impl.DependencyInjection
{
    /// <summary>
    /// 通过扫描特性注册服务
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// 通过扫描特性注册服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceCollection"></param>
        /// <param name="assemblys"></param>
        public static void AddServicesFromAttribute(this IServiceCollection serviceCollection, params Assembly[] assemblys)
        {
            Stopwatch stopwatch= Stopwatch.StartNew();
            if (!assemblys.Any())
            {
                throw new ArgumentException($"The {assemblys} is empty.", nameof(assemblys));
            }
            List<(Type, bool, bool, bool)> servicesToBeRegistered = assemblys
                .SelectMany(assembly => assembly.GetTypes())
                .Select(x =>
                {

                    return (x, x.IsDefined(typeof(TransientServiceAttribute), false), x.IsDefined(typeof(ScopedServiceAttribute), false), x.IsDefined(typeof(SingletonServiceAttribute), false));
                })
                .Where(p => p.Item2 || p.Item3 || p.Item4)
                .ToList();
            List<ServiceDescriptor> serviceDescriptors = new List<ServiceDescriptor>();
            foreach ((Type, bool, bool, bool) item in servicesToBeRegistered)
            {
                Type serviceType = item.Item1;
                List<Type> implementations = new List<Type>();
                if (serviceType.IsGenericType && serviceType.IsGenericTypeDefinition)
                {
                    implementations = assemblys.SelectMany(a => a.GetTypes())
                    .Where(type => type.IsGenericType && type.IsClass && type.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == serviceType.GetGenericTypeDefinition()))
                    .ToList();
                }
                else
                {
                    //implementations = assemblys.SelectMany(a => a.GetTypes())
                    //.Where(type => serviceType.IsAssignableFrom(type) && type.IsClass).ToList();
                    implementations.Add(serviceType);
                }
                List<(Type, Type)> impls = new List<(Type, Type)>();
                if (implementations.Any())
                {
                    foreach (Type implementation in implementations)
                    {
                        Type[] ts = implementation.GetInterfaces();
                        if (ts?.Length > 0)
                        {
                            foreach (Type type in ts)
                            {
                                impls.Add((type, implementation));
                            }
                        }

                        Type? baseType = implementation.BaseType;
                        if (baseType != null && !baseType.Equals(typeof(object)))
                        {
                            impls.Add((baseType, implementation));
                        }
                        else
                        {
                            impls.Add((implementation, implementation));
                        }
                    }
                }
                else
                {
                    if (serviceType.IsClass)
                    {
                        impls.Add((serviceType, serviceType));
                    }
                }
                foreach (var impl in impls)
                {
                    //Console.WriteLine($"register service：{impl.Item1.Name} -> {impl.Item2.Name}[{item.Item2}-{item.Item3}-{item.Item4}]");
                    if (item.Item2)
                    {
                        serviceDescriptors.Add(new ServiceDescriptor(impl.Item1, impl.Item2, ServiceLifetime.Transient));
                    }
                    if (item.Item3)
                    {
                        serviceDescriptors.Add(new ServiceDescriptor(impl.Item1, impl.Item2, ServiceLifetime.Scoped));
                    }
                    if (item.Item4)
                    {
                        serviceDescriptors.Add(new ServiceDescriptor(impl.Item1, impl.Item2, ServiceLifetime.Singleton));
                    }
                }
            }
            if (serviceDescriptors.Any())
            {
                serviceCollection.Add(serviceDescriptors);
            }
            Console.WriteLine($"注入耗时{stopwatch.ElapsedMilliseconds}");
        }
    }
}
