using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLINQ
{
    public static partial class MyLinqExtensions
    {
        #region GroupBy
        private class MyGrouping<TKey, TElement> : IGrouping<TKey, TElement>
        {
            IEnumerable<TElement> Values { get; }

            public MyGrouping(TKey key, IEnumerable<TElement> values)
            {
                Values = values ?? throw new ArgumentNullException(nameof(values), "List of elements cannot be null.");
                Key = key;
            }

            public TKey Key { get; }
            public IEnumerator<TElement> GetEnumerator()
            {
                return Values.GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

        }
        #endregion

        #region OrderBy
        private static IEnumerable<T> QuickSort<T>(IEnumerable<T> lst, Func<T, T, int> compare)
        {
            if (lst.Count() < 2) return lst;

            var val = lst.Last();
            var left = lst.SkipLast(1).Where(el => compare(el, val) <= 0);
            var right = lst.SkipLast(1).Where(el => compare(el, val) > 0);

            return QuickSort(left, compare).Append(val).Concat(QuickSort(right, compare));
        }

        private class MyOrder<TElement> : IOrderedEnumerable<TElement>
        {
            private IEnumerable<TElement> SortedLst { get; }
            public MyOrder(IEnumerable<TElement> lst)
                => SortedLst = lst;

            public IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(
                Func<TElement, TKey> keySelector,
                IComparer<TKey>? comparer, bool descending)
            {
                IComparer<TKey> nonNullComparer = comparer ?? Comparer<TKey>.Default;
                return SortedLst.MyOrderBy(keySelector, nonNullComparer, descending);
            }

            public IEnumerator<TElement> GetEnumerator()
            {
                return SortedLst.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        // Alternative MyOrderBy version
        public static IOrderedEnumerable<TElement> MyOrderBy<TElement>(
            this IEnumerable<TElement> lst,
            IComparer<TElement>? comparer, bool descending = false)
        {
            IComparer<TElement> nonNullComparer = comparer ?? Comparer<TElement>.Default;
            Func<TElement, TElement, int> compare = (el1, el2) =>
            {
                if (!descending)
                {
                    return nonNullComparer.Compare(el1, el2);
                }
                else
                {
                    return nonNullComparer.Compare(el2, el1);
                }
            };

            return new MyOrder<TElement>(QuickSort(lst, compare));
        }
        #endregion
        public static void ThrowIfEmpty<T>(IEnumerable<T> lst)
        {
            if (!lst.Any())
                throw new InvalidOperationException("Sequence contains no elements.");
        }

    }
}
