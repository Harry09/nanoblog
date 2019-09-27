using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Nanoblog.AppCore.Extensions
{
    public static class EnumerableExtensions
    {
        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> list)
        {
            return new ObservableCollection<T>(list);
        }
    }
}
