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
            private readonly TKey key;
            private readonly IEnumerable<TElement> values;

            public MyGrouping(TKey key, IEnumerable<TElement> values)
            {
                if (values == null)
                {
                    throw new ArgumentNullException("List of elements cannot be null");
                }
                this.key = key;
                this.values = values;
                Key = key;
            }

            public TKey Key { get; }

            public IEnumerator<TElement> GetEnumerator()
            {
                return values.GetEnumerator();
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
                IComparer<TKey> comparer, bool descending)
            {
                return SortedLst?.MyOrderBy(keySelector, comparer, descending);
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
        #endregion
        public static void ThrowIfEmpty<T>(IEnumerable<T> lst)
        {
            if (lst.Count() == 0)
                throw new InvalidOperationException("Sequence contains no elements");
        }
    }
}
