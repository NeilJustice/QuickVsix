using System;

namespace CSharpUtils
{
   public class Asserter
   {
      public virtual void ThrowIfEqual<T>(T unexpectedValue, T realValue, string realValueArgumentText, string additionalMessage)
      {
         StaticThrowIfEqual(unexpectedValue, realValue, realValueArgumentText, additionalMessage);
      }

      public virtual void ThrowIfNotEqual<T>(T expectedValue, T realValue, string realValueArgumentText, string additionalMessage)
      {
         StaticThrowIfNotEqual(expectedValue, realValue, realValueArgumentText, additionalMessage);
      }

      public virtual void ThrowIfNull<T>(T value, string variableName)
      {
         if (value == null)
         {
            throw new InvalidOperationException($"ThrowIfNull failed for variableName=\"{variableName}\"");
         }
      }

      public static void StaticThrowIfEqual<T>(T unexpectedValue, T realValue, string realValueArgumentText, string additionalMessage)
      {
         bool realValueEqualsExpectedValue = realValue.Equals(unexpectedValue);
         if (realValueEqualsExpectedValue)
         {
            string realValueToString = realValue.ToString();
            string unexpectedValueToString = unexpectedValue.ToString();
            throw new InvalidOperationException(
$@"CSharpUtils.Asserter.ThrowIfEqual() failed: realValue.Equals(unexpectedValue) returned true.
unexpectedValue.ToString()=[{unexpectedValueToString}]
      realValue.ToString()=[{realValueToString}]
     realValueArgumentText=[{realValueArgumentText}]
         additionalMessage=[{additionalMessage}]
");
         }
      }

      public static void StaticThrowIfNotEqual<T>(T expectedValue, T realValue, string realValueArgumentText, string additionalMessage)
      {
         bool realValueEqualsExpectedValue = realValue.Equals(expectedValue);
         if (!realValueEqualsExpectedValue)
         {
            string realValueToString = realValue.ToString();
            string expectedValueToString = expectedValue.ToString();
            throw new InvalidOperationException(
$@"CSharpUtils.Asserter.ThrowIfNotEqual() failed: realValue.Equals(expectedValue) returned false.
expectedValue.ToString()=[{expectedValueToString}]
    realValue.ToString()=[{realValueToString}]
   realValueArgumentText=[{realValueArgumentText}]
       additionalMessage=[{additionalMessage}]
");
         }
      }
   }
}
