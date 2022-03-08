//The main purpose of this project is work with logic of methods.
//For this reason I didn't do all overloads.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace MyLINQ
{
    public static partial class MyLinqExtensions
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
        public static IEnumerable<Tout> MyManySelect<Tin, Tout>(this IEnumerable<Tin> lst,
            Func<Tin, IEnumerable<Tout>> inElToTempOutLst)
        {
            foreach (var el in lst)
            {
                var tempOutList = inElToTempOutLst(el);
                foreach (var outEl in tempOutList)
                    yield return outEl;
            }
        }
        public static IEnumerable<T> MySkip<T>(this IEnumerable<T> lst, int arg)
        {
            var count = 0;
            foreach (var el in lst)
            {
                count++;
                if (count <= arg)
                {
                    continue;
                }
                yield return el;
            }
        }
        public static IEnumerable<T> MySkipLast<T>(this IEnumerable<T> lst, int arg)
        {
            var i = 0;
            var length = lst.Count();
            foreach (var el in lst)
            {
                i++;
                if (i > length - arg)
                {
                    yield break;
                }
                yield return el;
            }
        }
        public static IEnumerable<T> MyTake<T>(this IEnumerable<T> lst, int num)
        {
            var counter = 0;
            foreach (var el in lst)
            {
                counter++;
                if (counter <= num)
                {
                    yield return el;
                }
                else
                {
                    yield break;
                }
            }
        }
        public static IEnumerable<T> MyTakeLast<T>(this IEnumerable<T> lst, int num)
        {
            var count = 0;
            var stack1 = new Stack<T>(lst);
            var stack2 = new Stack<T>();

            foreach (var el in stack1)
            {
                count++;
                if (count > num) break;
                stack2.Push(el);
            }

            foreach (var el in stack2)
                yield return el;
        }
        public static IEnumerable<TRes> MyJoin<TOut, TIn, TKey, TRes>(this IEnumerable<TOut> outer,
           IEnumerable<TIn> inner, Func<TOut, TKey> makeKeyOut, Func<TIn, TKey> makeKeyIn, Func<TOut, TIn, TRes> makeRes)
        {
            var outKeyValueList = new List<(TKey keyOut, TOut valueOut)>();
            var inKeyValueList = new List<(TKey keyIn, TIn valueIn)>();

            foreach (var el in outer)
            {
                outKeyValueList.Add((makeKeyOut(el), el));
            }
            foreach (var el in inner)
            {
                inKeyValueList.Add((makeKeyIn(el), el));
            }

            foreach (var el1 in outKeyValueList)
            {
                foreach (var el2 in inKeyValueList)
                {
                    if (el1.keyOut.Equals(el2.keyIn))
                    {
                        yield return makeRes(el1.valueOut, el2.valueIn);
                    }
                }
            }
        }
        public static IEnumerable<TResult> MyZip<TFirst, TSecond, TResult>(
        this IEnumerable<TFirst> firstLst, IEnumerable<TSecond> secondLst, Func<TFirst, TSecond, TResult> makeRes)
        {
            var firstEnumerator = firstLst.GetEnumerator();
            var secondEnumerator = secondLst.GetEnumerator();

            while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
            {
                yield return makeRes(firstEnumerator.Current, secondEnumerator.Current);
            }
        }
        public static IEnumerable<IGrouping<TKey, TList>> MyGroupBy<TKey, TList>
            (this IEnumerable<TList> source, Func<TList, TKey> keyMaker)
        {
            var keys = new List<TKey>();

            foreach (var el in source)
            {
                keys.Add(keyMaker(el));
            }
            foreach (var key in keys.Distinct())
            {
                var listValue = new List<TList>();
                foreach (var el in source)
                {
                    if (key.Equals(keyMaker(el)))
                        listValue.Add(el);
                }
                yield return new MyGrouping<TKey, TList>(key, listValue);
            }
        }

        public static IOrderedEnumerable<TElement> MyOrderBy<TElement, TKey>(
            this IEnumerable<TElement> lst, Func<TElement, TKey> ketSelector)
            where TKey : IComparable<TKey>
        {
            Func<TElement, TElement, int> compare = (el1, el2) =>
            {
                return ketSelector(el1).CompareTo(ketSelector(el2));
            };

            return new MyOrder<TElement>(QuickSort(lst, compare));
        }

        public static IOrderedEnumerable<TElement> MyOrderBy<TElement>(
            this IEnumerable<TElement> lst,
            IComparer<TElement> comparer, bool descending = false)
        {
            Func<TElement, TElement, int> compare = (el1, el2) =>
            {
                if (!descending)
                {
                    return comparer.Compare(el1, el2);
                }
                else
                {
                    return comparer.Compare(el2, el1);
                }
            };

            return new MyOrder<TElement>(QuickSort(lst, compare));
        }

        public static IOrderedEnumerable<TElement> MyOrderBy<TElement, TKey>(
            this IEnumerable<TElement> lst, Func<TElement, TKey> keySelector,
            IComparer<TKey> comparer, bool descending = false)
        {
            Func<TElement, TElement, int> compare = (el1, el2) =>
            {
                if (!descending)
                {
                    return comparer.Compare(keySelector(el1), keySelector(el2));
                }
                else
                {
                    return comparer.Compare(keySelector(el2), keySelector(el1));
                }
            };

            return new MyOrder<TElement>(QuickSort(lst, compare));
        }

        public static bool MyContains<T>(this IEnumerable<T> lst, T val)
        {
            foreach (var el in lst)
            {
                if (el.Equals(val)) return true;
            }
            return false;
        }
        public static T MyElementAt<T>(this IEnumerable<T> lst, int index)
        {
            if (index < 0 || index >= lst.Count())
                throw new ArgumentOutOfRangeException("Index was out of range. Must be non-negative and less than the size of the collection.");
            var current = 0;
            foreach (var el in lst)
            {
                if (current == index)
                    return el;
                current++;
            }
            return default;
        }


    }
}
