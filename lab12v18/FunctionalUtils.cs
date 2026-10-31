using System;
using System.Collections.Generic;
using System.Numerics;

namespace Lab12v18
{
    public static class FunctionalUtils
    {
        public static List<TResult> Map<T, TResult>(List<T> list, Func<T, TResult> mapper)
        {
            CheckArguments(list, mapper);
            List<TResult> result = new List<TResult>();
            foreach (T item in list)
            {
                result.Add(mapper(item));
            }
            return result;
        }

        public static List<T> Filter<T>(List<T> list, Func<T, bool> condition)
        {
            CheckArguments(list, condition);
            List<T> result = new List<T>();
            foreach (T item in list)
            {
                if (condition(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public static TNum Sum<T, TNum>(List<T> list, Func<T, TNum> selector)
            where TNum : INumber<TNum>
        {
            CheckArguments(list, selector);
            TNum total = TNum.Zero;
            foreach (T item in list)
            {
                total += selector(item);
            }
            return total;
        }

        public static int Count<T>(List<T> list, Predicate<T> condition)
        {
            CheckArguments(list, condition);
            int count = 0;
            foreach (T item in list)
            {
                if (condition(item))
                {
                    count++;
                }
            }
            return count;
        }

        public static T FirstOrDefault<T>(List<T> list, Predicate<T> condition)
        {
            CheckArguments(list, condition);
            foreach (T item in list)
            {
                if (condition(item))
                {
                    return item;
                }
            }
            return default(T);
        }

        private static void CheckArguments(object list, object function)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list", "Список не задано");
            }
            if (function == null)
            {
                throw new ArgumentNullException("function", "Делегат не задано");
            }
        }
    }
}