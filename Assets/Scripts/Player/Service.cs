using System;
using System.Collections.Generic;

public class Service
{
    private static Service _instance;
    public static Service Instance => _instance ?? (_instance = new Service());

    private Dictionary<Type, object> _services = new Dictionary<Type, object>();
    private Service() { }

    public void Register<T>(T service) where T : class
    {
        if (_services.ContainsKey(typeof(T)))
            return;

        _services.Add(typeof(T), service);
    }

    public T? Get<T>() where T : class
    {
        if (_services.ContainsKey(typeof(T)) == false)
            return null;

        return _services[typeof(T)] as T;
    }
}
