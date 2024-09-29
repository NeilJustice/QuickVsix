using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpUtils
{
   public static class IEnumerableExtensions
   {
      public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> elements)
      {
         T[] elementsArray = elements.ToArray();
         var readonlyElements = new ReadOnlyCollection<T>(elementsArray);
         return readonlyElements;
      }
   }
}
