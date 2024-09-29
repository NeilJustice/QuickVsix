using CSharpUtils;
using NUnit.Framework;

[TestFixture]
public class PluralizerTests
{
   Pluralizer _pluralizer;

   [SetUp]
   public void SetUp()
   {
      _pluralizer = new Pluralizer();
   }

   [Test]
   public void PluralizeIfNot1_AmountIs1_ReturnsSingularForm()
   {
      string singularForm = TestRandom.String();
      string pluralForm = TestRandom.String();
      //
      string potentiallyPluralizedForm = _pluralizer.PluralizeIfNot1(1, singularForm, pluralForm);
      //
      Assert.AreEqual(singularForm, potentiallyPluralizedForm);
   }

   [TestCase(-1)]
   [TestCase(0)]
   [TestCase(2)]
   [TestCase(3)]
   public void PluralizeIfNot1_AmountIsNot1_ReturnsPluralForm(int amount)
   {
      string singularForm = TestRandom.String();
      string pluralForm = TestRandom.String();
      //
      string potentiallyPluralizedForm = _pluralizer.PluralizeIfNot1(amount, singularForm, pluralForm);
      //
      Assert.AreEqual(pluralForm, potentiallyPluralizedForm);
   }
}
