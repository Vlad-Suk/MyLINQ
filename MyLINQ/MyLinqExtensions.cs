//The main purpose of this project is work with logic of methods.
//For this reason I didn't do all overloads.
using System;
using System.Linq;
using System.Collections.Generic;

namespace MyLINQ
{
    public static class MyLinqExtensions
    {
        public static void ThrowIfEmpty<T>(IEnumerable<T> lst)
        {
            if (lst.Count() == 0)
                throw new InvalidOperationException("Sequence contains no elements");
        }
        public static IEnumerable<T> MyPrepend<T>(this IEnumerable<T> lst, T arg)
        {
            yield return arg;
            foreach (var el in lst)
            {
                yield return el;
            }
        }
        public static IEnumerable<T> MyAppend<T>(this IEnumerable<T> lst, T arg)
        {
            foreach (var el in lst)
            {
                yield return el;
            }
            yield return arg;
        }
        public static IEnumerable<T> MyConcat<T>(this IEnumerable<T> lst1, IEnumerable<T> lst2)
        {
            foreach (var el in lst1)
            {
                yield return el;
            }
            foreach (var el in lst2)
            {
                yield return el;
            }
        }
        public static int MyMax(this IEnumerable<int> lst)
        {
            ThrowIfEmpty(lst);
            var max = lst.First();
            foreach (var el in lst)
            {
                if (max < el)
                    max = el;
            }
            return max;
        }
        public static int MyMin(this IEnumerable<int> lst)
        {
            ThrowIfEmpty(lst);
            var min = lst.First();
            foreach (var el in lst)
            {
                if (min > el)
                    min = el;
            }
            return min;
        }
        public static bool MyAll<T>(this IEnumerable<T> lst, Func<T, bool> predicate)
        {
            foreach (var el in lst)
            {
                if (!predicate(el))
                    return false;
            }
            return true;
        }
        public static bool MyAny<T>(this IEnumerable<T> lst, Func<T, bool> predicate)
        {
            foreach (var el in lst)
            {
                if (predicate(el))
                    return true;
            }
            return false;
        }
        public static T MyAggregate<T>(this IEnumerable<T> lst, Func<T, T, T> func)
        {
            ThrowIfEmpty(lst);
            T result = default;
            foreach (var el in lst)
            {
                result = func(result, el);
            }
            return result;
        }
        public static double MyAverage(this IEnumerable<int> lst)
        {
            ThrowIfEmpty(lst);
            double sum = 0;
            foreach (var el in lst)
            {
                sum += el;
            }
            return sum / lst.Count();
        }
        public static IEnumerable<T> MyDistinct<T>(this IEnumerable<T> lst)
        {
            var result = new List<T>();
            foreach (var el in lst)
            {
                if (!result.Contains(el))
                    result.Add(el);
            }
            return result;
        }
        public static T MyFirst<T>(this IEnumerable<T> lst, Func<T, bool> predicate)
        {
            foreach (var el in lst)
            {
                if (predicate(el))
                    return el;
            }
            throw new InvalidOperationException("Sequence contains no matching element");
        }
        public static T MyLast<T>(this IEnumerable<T> lst, Func<T, bool> predicate)
        {
            foreach (var el in lst.Reverse())
            {
                if (predicate(el))
                    return el;
            }
            throw new InvalidOperationException("Sequence contains no matching element");
        }
        public static IEnumerable<Tout> MySelect<Tin, Tout>(this IEnumerable<Tin> lst, Func<Tin, Tout> lambda)
        {
            foreach (var el in lst)
            {
                yield return lambda(el);
            }
        }
    }
}
