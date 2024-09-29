namespace CSharpUtils
{
   public class Pluralizer
   {
      public virtual string PluralizeIfNot1(int amount, string singularForm, string pluralForm)
      {
         if (amount == 1)
         {
            return singularForm;
         }
         return pluralForm;
      }
   }
}
