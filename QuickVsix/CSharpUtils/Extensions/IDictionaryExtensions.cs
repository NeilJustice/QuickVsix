using System.Collections.ObjectModel;

namespace CSharpUtils
{
   public static class IDictionaryExtensions
   {
      public static ReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TKey, TValue>(
         this IDictionary<TKey, TValue> mutableDictionary) where TKey : notnull
      {
         var readonlyDictionary = new ReadOnlyDictionary<TKey, TValue>(mutableDictionary);
         return readonlyDictionary;
      }
   }
}
