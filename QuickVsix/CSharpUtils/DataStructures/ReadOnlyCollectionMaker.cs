using System.Collections.ObjectModel;

namespace CSharpUtils
{
   public class ReadOnlyCollectionMaker
   {
      public virtual ReadOnlyCollection<T> MakeReadOnlyCollection<T>(T[] elements)
      {
         ReadOnlyCollection<T> readOnlyCollection = elements.ToReadOnlyCollection();
         return readOnlyCollection;
      }
   }
}
