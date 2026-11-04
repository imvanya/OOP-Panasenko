using System;
using System.Collections.Generic;
using System.Numerics;

namespace Lab13v18
{
    public static class FunctionalUtils
    {
        public static List<TResult> Map<T, TResult>(List<T> source, Func<T, TResult> mapper)
        {
            Validate(source, mapper);
            var result = new List<TResult>(source.Count);
            for (int i = 0; i < source.Count; i++)
            {
                result.Add(mapper(source[i]));
            }
            return result;
        }

        public static List<T> Filter<T>(List<T> source, Func<T, bool> predicate)
        {
            Validate(source, predicate);
            var result = new List<T>();
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public static TNum Sum<T, TNum>(List<T> source, Func<T, TNum> selector)
            where TNum : INumber<TNum>
        {
            Validate(source, selector);
            TNum sum = TNum.Zero;
            foreach (var item in source)
            {
                sum = sum + selector(item);
            }
            return sum;
        }

        public static int Count<T>(List<T> source, Predicate<T> match)
        {
            Validate(source, match);
            int counter = 0;
            foreach (var item in source)
            {
                if (match(item)) counter++;
            }
            return counter;
        }

        public static T FirstOrDefault<T>(List<T> source, Predicate<T> match)
        {
            Validate(source, match);
            for (int i = 0; i < source.Count; i++)
            {
                if (match(source[i]))
                {
                    return source[i];
                }
            }
            return default;
        }

        private static void Validate(object source, object action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Список не може бути null");
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "Делегат не може бути null");
            }
        }
    }
}