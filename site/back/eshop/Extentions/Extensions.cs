using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace eshop.Extensions
{
    public static class Extensions
    {
        public static void UpdateProperties<T>(this T target, T source)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                       .Where(p => p.CanRead && p.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source);
                if (value != null && !IsDefaultValue(value))
                {
                    prop.SetValue(target, value);
                }
            }
        }

        private static bool IsDefaultValue<T>(T value)
        {
            return EqualityComparer<T>.Default.Equals(value, default(T));
        }
    }


}
