using System.Text;

namespace CSharpUtils
{
   public static class StringExtensions
   {
      public static string RemoveCarriageReturns(this string str)
      {
         string stringWithoutCarriageReturns = str.Replace("\r", "");
         return stringWithoutCarriageReturns;
      }

      public static string Repeat(this string str, int n)
      {
         return new StringBuilder(str.Length * n).Insert(0, str, n).ToString();
      }
   }
}
