using System;
using System.Text.RegularExpressions;

namespace CSharpUtils
{
   public class Regexer
   {
      public virtual string MatchAndGetGroup(string input, string pattern, string groupName)
      {
         Match match = Regex.Match(input, pattern);
         if (!match.Success)
         {
            throw new ArgumentException($"Regexer.MatchAndGetGroup(input: \"{input}\", pattern: \"{pattern}\", groupName: \"{groupName}\") did not match.");
         }
         GroupCollection groups = match.Groups;
         Group group = groups[groupName];
         if (!group.Success)
         {
            throw new ArgumentException($"Regexer.MatchAndGetGroup(input: \"{input}\", pattern: \"{pattern}\", groupName: \"{groupName}\") called with unrecognized group name.");
         }
         string groupMatchValue = group.Value;
         return groupMatchValue;
      }
   }
}
