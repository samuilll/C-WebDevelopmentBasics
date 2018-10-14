using System;
using System.Dynamic;

namespace SIS.MvcFramework.Services.Contracts
{
    public interface IServiceCollection
    {
        void AddService<TSource, TDestination>();

        T CreateInstance<T>();

        object CreateInstance(Type type);
    }
}