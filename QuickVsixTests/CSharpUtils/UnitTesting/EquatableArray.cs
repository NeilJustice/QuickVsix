using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpUtils
{
   // EquatableArray<T> exists because asserting that a FakeItEasy-mocked function
   // was called with an expected array performs a reference address equality comparision
   // between the expected array and the actual array, whereas what is usually wanted
   // is an element-by-element equality comparison between an expected array and an actual array.
   // EquatableArray<T>.Equals() performs an element-by-element equality assertion by way of
   // NUnit.Framework.Assert.AreEqual(T[], T[]).
   public class EquatableArray<T> : IEnumerable<T>
   {
      private readonly T[] elements;

      public EquatableArray(T[] elements)
      {
         this.elements = elements;
      }

      public override bool Equals(object obj)
      {
         if (obj is EquatableArray<T> actualEquatableArray)
         {
            Assert.AreEqual(elements, actualEquatableArray.elements);
            return true;
         }
         if (obj is T[] actualElements)
         {
            Assert.AreEqual(elements, actualElements);
            return true;
         }
         throw new InvalidCastException("EquatableArray<T>.Equals(actualObject) passed a non-EquatableArray<T> non-T[] actualObject.");
      }

      public override int GetHashCode()
      {
         throw new NotSupportedException();
      }

      IEnumerator<T> IEnumerable<T>.GetEnumerator()
      {
         foreach (T element in elements)
         {
            yield return element;
         }
      }

      public IEnumerator GetEnumerator()
      {
         foreach (T element in elements)
         {
            yield return element;
         }
      }
   }
}
