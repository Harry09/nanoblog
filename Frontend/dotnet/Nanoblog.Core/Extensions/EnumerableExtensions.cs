using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Nanoblog.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> list)
        {
            return new ObservableCollection<T>(list);
        }
    }
}
