
using System;
using System.Collections.Generic;

namespace BattleOfHeroes.Showcase.Managers
{
    public static class ServiceLocator
    {
       private static Dictionary<Type,IService>  _services = new  Dictionary<Type,IService>();

       public static void AddService<T>(T service) where T : IService
       {
           _services.Add(typeof(T),service);
       }

       public static T GetService<T>() where T : IService
       {
           return (T)_services[typeof(T)];
       }

       public static bool HasService<T>()  where T : IService
       {
            return _services.ContainsKey(typeof(T));
       }
    }
}