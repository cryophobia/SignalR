using System;
using Newtonsoft.Json;

namespace SignalR.Client
{
    public static class ConnectionExtensions
    {
        public static T GetValue<T>(this IConnection connection, string key)
        {
            object value;

#if MONOTOUCH
			lock(connection.Items) 
			{
#endif
				if (connection.Items.TryGetValue(key, out value))
	            {
	                return (T)value;
	            }
#if MONOTOUCH
			}
#endif
            
            return default(T);
        }

#if !WINDOWS_PHONE && !SILVERLIGHT && !NET35 && !__ANDROID__ && !MONOTOUCH
        public static IObservable<string> AsObservable(this Connection connection)
        {
            return connection.AsObservable(value => value);
        }

        public static IObservable<T> AsObservable<T>(this Connection connection)
        {
            return connection.AsObservable(value => JsonConvert.DeserializeObject<T>(value));
        }

        public static IObservable<T> AsObservable<T>(this Connection connection, Func<string, T> selector)
        {
            return new ObservableConnection<T>(connection, selector);
        }
#endif
    }
}
