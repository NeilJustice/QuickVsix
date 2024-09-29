using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpUtils
{
   public class StringSplitter
   {
      private readonly MethodCaller _methodCaller = new MethodCaller();

      public virtual ReadOnlyCollection<string> Split(string str, string separator)
      {
         ReadOnlyCollection<string> splitString = str.Split(new string[] { separator }, StringSplitOptions.None).ToReadOnlyCollection();
         return splitString;
      }

      public virtual ReadOnlyCollection<string> SplitLineWithRequiredNumberOfFields(string str, string separator, int requiredNumberOfFields)
      {
         if (requiredNumberOfFields <= 1)
         {
            throw new ArgumentException($"StringSplitter.SplitLineWithRequiredNumberOfFields(string str, string separator, int requiredNumberOfFields) called with requiredNumberOfFields <= 1: {requiredNumberOfFields}");
         }
         ReadOnlyCollection<string> splitString = _methodCaller.CallFunction(Split, str, separator);
         if (splitString.Count != requiredNumberOfFields)
         {
            string exceptionMessage = $"StringSplitter.SplitLineWithRequiredNumberOfFields(string str, string separator, int requiredNumberOfFields) called with str when split does not have required requiredNumberOfFields number of fields. str=\"{str}\". separator=\"{separator}\". requiredNumberOfFields={requiredNumberOfFields}. splitString.Count={splitString.Count}";
            throw new ArgumentException(exceptionMessage);
         }
         return splitString;
      }

      public virtual ReadOnlyCollection<string> SplitThenSkipFirstElement(string input, string separator)
      {
         ReadOnlyCollection<string> splitString = _methodCaller.CallFunction(Split, input, separator);
         ReadOnlyCollection<string> splitStringWithFirstElementSkipped = splitString.Skip(1).ToReadOnlyCollection();
         return splitStringWithFirstElementSkipped;
      }
   }
}
