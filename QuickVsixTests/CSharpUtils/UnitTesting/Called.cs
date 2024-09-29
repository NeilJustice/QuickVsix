using FakeItEasy;
using FakeItEasy.Configuration;
using System;
using System.Linq.Expressions;

namespace CSharpUtils
{
   public static class Called
   {
      public static UnorderedCallAssertion Once(Expression<Action> callSpecification)
      {
         UnorderedCallAssertion unorderedCallAssertion = A.CallTo(callSpecification).MustHaveHappened(1, Times.Exactly);
         return unorderedCallAssertion;
      }

      public static UnorderedCallAssertion Once<ReturnType>(Expression<Func<ReturnType>> callSpecification)
      {
         UnorderedCallAssertion unorderedCallAssertion = A.CallTo(callSpecification).MustHaveHappened(1, Times.Exactly);
         return unorderedCallAssertion;
      }

      public static void ThreeTimes(Expression<Action> expectedExactArgumentsCall)
      {
         A.CallTo(expectedExactArgumentsCall).MustHaveHappened(3, Times.Exactly);
      }

      public static void NumberOfTimes(int expectedNumberOfCalls, Expression<Action> expectedCall)
      {
         A.CallTo(expectedCall).WithAnyArguments().MustHaveHappened(expectedNumberOfCalls, Times.Exactly);
      }

      public static UnorderedCallAssertion WasCalled(Expression<Action> callSpecification)
      {
         UnorderedCallAssertion unorderedCallAssertion = A.CallTo(callSpecification).MustHaveHappened();
         return unorderedCallAssertion;
      }

      public static UnorderedCallAssertion PropertySetOnce<T>(Expression<Func<T>> propertySpecification, T expectedValue)
      {
         UnorderedCallAssertion unorderedCallAssertion = A.CallToSet(propertySpecification).To(expectedValue).MustHaveHappened(1, Times.Exactly);
         return unorderedCallAssertion;
      }
   }
}
