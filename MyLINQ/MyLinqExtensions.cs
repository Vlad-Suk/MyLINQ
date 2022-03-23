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
        /// <summary>
        /// Place <paramref name="arg"/> as the first element of the sequence.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable<T></param>
        /// <param name="arg">Argument to put as first element of the sequence</param>
        /// <returns>IEnumerable<T> with <paramref name="arg"/> prepended</returns>
        public static IEnumerable<T> MyPrepend<T>(this IEnumerable<T> lst, T arg)
        {
            yield return arg;
            foreach (var el in lst)
            {
                yield return el;
            }
        }

        /// <summary>
        /// Place <paramref name="arg"/> as the last element of the sequence.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable<T></param>
        /// <param name="arg">Argument to put as last element of the sequence</param>
        /// <returns>IEnumerable<T> with <paramref name="arg"/> prepended</returns>
        public static IEnumerable<T> MyAppend<T>(this IEnumerable<T> lst, T arg)
        {
            foreach (var el in lst)
            {
                yield return el;
            }
            yield return arg;
        }

        /// <summary>
        ///  Concatenates two sequences.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst1">First input IEnumerable<T></param>
        /// <param name="lst2">Second input IEnumerable<T></param>
        /// <returns>IEnumerable<T> Concatenated two sequences/</returns>
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
        /// <summary>
        /// Method returns the maximum value in a sequence of <see cref="int">.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable<int></param>
        /// <returns>The maximum value in a sequence.</returns>
        /// <exeption cref = "InvalidOperationException">
        /// Sequence contains no elements.
        /// </exeption>
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
        /// <summary>
        /// Method returns the minimum value in a sequence of <see cref="int">.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable<int></param>
        /// <returns>The minimum value in a sequence.</returns>
        /// <exeption cref = "InvalidOperationException">
        /// Sequence contains no elements.
        /// </exeption>
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
        /// <summary>
        /// Determines whether all elements of a sequence satisfy a condition.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable<T></param>
        /// <param name="predicate">Input lambda expression.</param>
        /// <returns><see cref="true"> if every elements of the source sequence passes the test 
        /// in the specified predicate, or if the sequence is empty; otherwise <see cref="false">.</returns>

        public static bool MyAll<T>(this IEnumerable<T> lst, Func<T, bool> predicate)
        {
            foreach (var el in lst)
            {
                if (!predicate(el))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Determines whether any element of a sequence satisfy a condition.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable<T></param>
        /// <param name="predicate">Input lambda expression.</param>
        /// <returns><see cref="true"> if least of its elements of the source sequence passes the test 
        /// in the specified predicate, or if the sequence is not empty; otherwise <see cref="false">.</returns>

        public static bool MyAny<T>(this IEnumerable<T> lst, Func<T, bool> predicate)
        {
            foreach (var el in lst)
            {
                if (predicate(el))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Applies an accumulator function over the sequence.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable<T></param>
        /// <param name="func">Input lambda expression.</param>
        /// <returns>The final accumulator value.</returns>
        /// <exeption cref = "InvalidOperationException">
        /// Sequence contains no elements.
        /// </exeption>
        public static T? MyAggregate<T>(this IEnumerable<T> lst, Func<T?, T, T> func)
        {
            ThrowIfEmpty(lst);
            T? result = default;
            foreach (var el in lst)
            {
                result = func(result, el);
            }
            return result;
        }
        /// <summary>
        /// Computes the average of a sequence of <see cref="int"/>.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable<int></param>
        /// <returns>The average of the sequence of values.</returns>
        /// <exeption cref = "InvalidOperationException">
        /// Sequence contains no elements.
        /// </exeption>
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
        /// <summary>
        /// Returns distinct elements from a sequence by using the default equality comparer to compare velues.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable<T></param>
        /// <returns>An IEnumerable<T> that contains distinct elements from the source sequence.</returns>
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
        /// <summary>
        /// Returns the first element in a sequence that satisfies a specified condition.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable<T></param>
        /// <param name="predicate">Input lambda expression.</param>
        /// <returns>The first element in the sequence that passes the test in the specified predicate function.</returns>
        /// <exeption cref = "InvalidOperationException">
        /// Sequence contains no matching element.
        /// </exeption>
        public static T MyFirst<T>(this IEnumerable<T> lst, Func<T, bool> predicate)
        {
            foreach (var el in lst)
            {
                if (predicate(el))
                    return el;
            }
            throw new InvalidOperationException("Sequence contains no matching element");
        }
        /// <summary>
        /// Returns the last element in a sequence that satisfies a specified condition.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable<T></param>
        /// <param name="predicate">Input lambda expression.</param>
        /// <returns>The last element in the sequence that passes the test in the specified predicate function.</returns>
        /// <exeption cref = "InvalidOperationException">
        /// Sequence contains no matching element.
        /// </exeption>
        public static T MyLast<T>(this IEnumerable<T> lst, Func<T, bool> predicate)
        {
            foreach (var el in lst.Reverse())
            {
                if (predicate(el))
                    return el;
            }
            throw new InvalidOperationException("Sequence contains no matching element");
        }
        /// <summary>
        /// Projects each element of a sequence into a new form.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable<Tin></param>
        /// <param name="lambda">Input lambda expression.</param>
        /// <returns>An IEnumerable<T> whose elements are the result of invoking the transform function on each element of source.</returns>

        public static IEnumerable<Tout> MySelect<Tin, Tout>(this IEnumerable<Tin> lst, Func<Tin, Tout> lambda)
        {
            foreach (var el in lst)
            {
                yield return lambda(el);
            }
        }
        /// <summary>
        /// Projects each element of a sequence to <see cref="IEnumerable{TOut}"/> and flattens the resulting sequences in one sequence.
        ///
        /// Takes O(n*n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable<Tin></param>
        /// <param name="inElToTempOutLst">Input lambda expression.</param>
        /// <returns>An IEnumerable<T> whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.</returns>
        public static IEnumerable<Tout> MySelectMany<Tin, Tout>(this IEnumerable<Tin> lst,
            Func<Tin, IEnumerable<Tout>> inElToTempOutLst)
        {
            foreach (var el in lst)
            {
                var tempOutList = inElToTempOutLst(el);
                foreach (var outEl in tempOutList)
                    yield return outEl;
            }
        }
        /// <summary>
        /// Bypasses a specified number of elements in a sequence and than return the remaining elements.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable<T></param>
        /// <param name="countInput">Input number of values to skip.</param>
        /// <returns>An IEnumerable<T> that contains the elements that occur after the specified index in the input sequence.</returns>
        public static IEnumerable<T> MySkip<T>(this IEnumerable<T> lst, int countInput)
        {
            var count = 0;
            foreach (var el in lst)
            {
                count++;
                if (count <= countInput)
                {
                    continue;
                }
                yield return el;
            }
        }
        /// <summary>
        /// Returns a new enumerable collection that contains the elements from source with the last count elements of the source collection omitted.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable<T></param>
        /// <param name="countInput">Input number of values to skip.</param>
        /// <returns>An IEnumerable<T> that contains the elements from source minus count elements from the end of the collection.</returns>
        public static IEnumerable<T> MySkipLast<T>(this IEnumerable<T> lst, int countInput)
        {
            var i = 0;
            var length = lst.Count();
            foreach (var el in lst)
            {
                i++;
                if (i > length - countInput)
                {
                    yield break;
                }
                yield return el;
            }
        }
        /// <summary>
        /// Returns a specified numder of contiguous elements from the start of a sequence.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable<T></param>
        /// <param name="num">Input number of values to take.</param>
        /// <returns>An IEnumerable<T> that contains the specified number of elements from the start of the input sequence.</returns>
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
        /// <summary>
        /// Returns a new enumerable collection that contains the last count elements from source.
        ///
        /// Takes O(n) in time and O(n) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable<T></param>
        /// <param name="num">Input number of values to take.</param>
        /// <returns>An IEnumerable<T> that contains the last count elements from source.</returns>
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
        /// <summary>
        /// Correlates the elements of two sequences based on matching keys.The default equality comparer is used to compare keys.
        ///
        /// Takes O(n*n) in time and O(n) in memory.
        /// </summary>
        /// <param name="outer">Input outer IEnumerable<TOut></param>
        /// <param name="inner">Input inner IEnumerable<TOut>.</param>
        /// <param name="makeKeyOut">Input lambda to make key from IEnumerable<TOut></param>
        /// <param name="makeKeyIn">Input lambda to make key from IEnumerable<TIn></param>
        /// <param name="makeRes">Input lambda to make result from IEnumerable<TOut> and IEnumerable<TIn> using redy-made keys</param>
        /// <returns>An IEnumerable<TRes> that has elements of type TRes that are obtained by performing an inner join or two sequences.</returns>
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

            foreach (var (keyOut, valueOut) in outKeyValueList)
            {
                foreach (var (keyIn, valueIn) in inKeyValueList)
                {
                    if (Equals(keyOut, keyIn))
                    {
                        yield return makeRes(valueOut, valueIn);
                    }
                }
            }
        }
        /// <summary>
        /// Applies a specified function to the corresponding elements of two sequences producing a sequence of the result.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="firstLst">Input first IEnumerable<T></param>
        /// <param name="secondLst">Input second IEnumerable<T></param>
        /// <param name="makeRes">Lamda to make result TFirst and TSecond.</param>
        /// <returns>An IEnumerable<TResult> that contains merged elements of two input sequences.</returns>
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
        /// <summary>
        /// Groups elements of two sequences according to a specify key selector function.
        ///
        /// Takes O(n*n) in time and O(n) in memory.
        /// </summary>
        /// <param name="source">Input source IEnumerable<T></param>
        /// <param name="keyMaker">Lamda to make TKey from TList.</param>
        /// <returns>An IEnumerable<IGrouping<TKey, TList>> where each <IGrouping<TKey, TList>> object contains a sequence of objects and a key.</returns>
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
                    if (Equals(key, keyMaker(el)))
                        listValue.Add(el);
                }
                yield return new MyGrouping<TKey, TList>(key, listValue);
            }
        }
        /// <summary>
        /// Sorts elements of a sequence in ascending order according to a key.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable<TElement></param>
        /// <param name="keySelector">Lamda to make TKey from TElement.</param>
        /// <returns>An IOrderedEnumerable<TElement> whose elements sorted according to a key.</returns>
        public static IOrderedEnumerable<TElement> MyOrderBy<TElement, TKey>(
            this IEnumerable<TElement> lst, Func<TElement, TKey> keySelector)
            where TKey : IComparable<TKey>
        {
            Func<TElement, TElement, int> compare = (el1, el2) =>
            {
                return keySelector(el1).CompareTo(keySelector(el2));
            };

            return new MyOrder<TElement>(QuickSort(lst, compare));
        }
        /// <summary>
        /// Sorts elements of a sequence in ascending order according to a key.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable<TElement></param>
        /// <param name="keySelector">Lamda to make TKey from TElement.</param>
        /// <returns>An IOrderedEnumerable<TElement> whose elements sorted according to a key.</returns>
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
        /// <summary>
        /// Determines whether a sequence contains a specified element by using the default equality comparer.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable<TElement></param>
        /// <returns><see cref="true"/> if sequence contains an element that has specified element; otherwise <see cref="false"/>.</returns>
        public static bool MyContains<T>(this IEnumerable<T> lst, T val)
        {
            foreach (var el in lst)
            {
                if (Equals(el, val)) return true;
            }
            return false;
        }
        /// <summary>
        /// Returns the element at a specified index in a sequence.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="lst">Input IEnumerable.<TElement></param>
        /// <param name="index">Index in a sequence.</param>
        /// <returns>The element at the specified position in the soruce sequence.</returns>
        public static T? MyElementAt<T>(this IEnumerable<T> lst, int index)
        {
            if (index < 0 || index >= lst.Count())
                throw new ArgumentOutOfRangeException(nameof(index), "Index was out of range. Must be non-negative and less than the size of the collection.");
            var current = 0;
            foreach (var el in lst)
            {
                if (current == index)
                    return el;
                current++;
            }
            return default;
        }
        /// <summary>
        /// Applies a specified function to the corresponding elements of two sequences producing a sequence of the result.
        ///
        /// Takes O(n) in time and O(1) in memory.
        /// </summary>
        /// <param name="firstLst">Input first IEnumerable<T></param>
        /// <param name="secondLst">Input second IEnumerable<T></param>
        /// <param name="makeRes">Lamda to make result TFirst and TSecond.</param>
        /// <returns>An IEnumerable<TResult> that contains merged elements of two input sequences.</returns>
        public static IEnumerable<T> MyUnion<T>(this IEnumerable<T> lst1, IEnumerable<T> lst2)
        {
            var result = lst1.Concat(lst2);
            return result.Distinct().ToList();
        }
        public static IEnumerable<T> MyIntersect<T>(this IEnumerable<T> lst1, IEnumerable<T> lst2)
        {
            var result = new List<T>();
            foreach (var el1 in lst1)
            {
                foreach (var el2 in lst2)
                {
                    if (Equals(el1, el2) && (!result.Contains(el1)))
                        result.Add(el1);
                }
            }
            return result;
        }
        public static IEnumerable<T> MyExcept<T>(this IEnumerable<T> lst1, IEnumerable<T> lst2)
        {
            var distinct = lst1.Distinct();
            var result = distinct.ToList();
            lst1 = distinct.ToList();

            foreach (var el1 in lst1)
            {
                foreach (var el2 in lst2)
                {
                    if (Equals(el1, el2))
                    {
                        result.Remove(el1);
                    }
                }
            }
            return result;
        }
        public static IEnumerable<TRes> MyCast<TRes>(this IEnumerable source)
        {
            foreach (var el in source)
            {
                yield return (TRes)el;
            }
        }
        public static IEnumerable<TRes> MyOfType<TRes>(this IEnumerable source)
        {
            foreach (object el in source)
            {
                if (el is TRes result)
                    yield return result;
            }
        }
        public static IEnumerable<T> MyAsEnumerable<T>(this IEnumerable<T> source) => source;
        public static bool MySequenceEqual<T>(this IEnumerable<T> lst1, IEnumerable<T> lst2)
        {
            if (lst1.Count() != lst2.Count())
                return false;
            var em1 = lst1.GetEnumerator();
            var em2 = lst2.GetEnumerator();
            while (em1.MoveNext() && em2.MoveNext())
            {
                if (!Equals(em1.Current, em2.Current))
                    return false;
            }
            return true;
        }
        public static IEnumerable<T> MyReverse<T>(this IEnumerable<T> lst)
        {
            var stackLst = new Stack<T>(lst);
            foreach (var el in stackLst)
                yield return el;
        }
    }
}
