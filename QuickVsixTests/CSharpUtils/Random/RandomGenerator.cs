using System;
using System.Collections.ObjectModel;

namespace CSharpUtils
{
   public class RandomGenerator
   {
      public static readonly RandomGenerator Instance = new RandomGenerator();

      public virtual bool Bool()
      {
         return TestRandom.Bool();
      }

      public virtual int Int()
      {
         return TestRandom.Int();
      }

      public virtual string String()
      {
         return TestRandom.String();
      }

      public virtual EnumType Enum<EnumType>()
      {
         return TestRandom.Enum<EnumType>();
      }

      public virtual DateTime DateTime()
      {
         return TestRandom.DateTime();
      }

      public virtual ReadOnlyCollection<T> ReadOnlyCollection<T>() where T : new()
      {
         return TestRandom.ReadOnlyCollection<T>();
      }

      public virtual ReadOnlyCollection<string> ReadOnlyStringCollection()
      {
         return TestRandom.ReadOnlyStringCollection();
      }
   }
}
