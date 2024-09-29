using FakeItEasy;
using FakeItEasy.Creation;
using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace CSharpUtils
{
   public static class Mock
   {
      public static T Strict<T>() where T : class
      {
         return A.Fake<T>((IFakeOptions<T> fakeOptions) => fakeOptions.Strict());
      }

      public static T Component<T>(object classInstance, string componentFieldName) where T : class
      {
         Assert2.FieldIsNonNullAndExactType<T>(classInstance, componentFieldName);
         T strictComponentMock = Strict<T>();
         Reflect.Set(classInstance, componentFieldName, strictComponentMock);
         return strictComponentMock;
      }

      public static void Component(object classInstanceUnderTest, string componentFieldName, object componentMock)
      {
         Reflect.Set(classInstanceUnderTest, componentFieldName, componentMock);
      }

      public static T NullComponent<T>(object classInstance, string componentFieldName) where T : class
      {
         Assert2.FieldIsNullAndExactType<T>(classInstance, componentFieldName);
         T componentMock = Strict<T>();
         Reflect.Set(classInstance, componentFieldName, componentMock);
         return componentMock;
      }

      public static void Throw<ExceptionType>(
          Expression<Action> actionCall, ExceptionType exception) where ExceptionType : Exception
      {
         A.CallTo(actionCall).WithAnyArguments().Throws(exception);
      }

      public static void Throw<ExceptionType, ReturnType>(
          Expression<Func<ReturnType>> functionCall, ExceptionType exception) where ExceptionType : Exception
      {
         A.CallTo(functionCall).WithAnyArguments().Throws(exception);
      }

      public static void Expect(Expression<Action> call)
      {
         A.CallTo(call).WithAnyArguments().DoesNothing();
      }

      public static void Return<ReturnType>(Expression<Func<ReturnType>> call, ReturnType returnValue)
      {
         A.CallTo(call).WithAnyArguments().Returns(returnValue);
      }

      public static MockedComponentType ReturnStrictMock<MockedComponentType>(Expression<Func<MockedComponentType>> call) where MockedComponentType : class
      {
         var strictMock = Strict<MockedComponentType>();
         A.CallTo(call).WithAnyArguments().Returns(strictMock);
         return strictMock;
      }

      public static void ReturnValues<ReturnType>(Expression<Func<ReturnType>> call, params ReturnType[] returnValues)
      {
         A.CallTo(call).WithAnyArguments().ReturnsNextFromSequence(returnValues);
      }

      public static void ExpectPropertySet<T>(Expression<Func<T>> propertySpecification)
      {
         A.CallToSet(propertySpecification).DoesNothing();
      }

      public static bool ReturnRandomBool(Expression<Func<bool>> call)
      {
         bool randomBool = TestRandom.Bool();
         A.CallTo(call).WithAnyArguments().Returns(randomBool);
         return randomBool;
      }

      public static int ReturnRandomInt(Expression<Func<int>> call)
      {
         int randomInt = TestRandom.Int();
         A.CallTo(call).WithAnyArguments().Returns(randomInt);
         return randomInt;
      }

      public static string ReturnRandomString(Expression<Func<string>> call)
      {
         string randomString = TestRandom.String();
         A.CallTo(call).WithAnyArguments().Returns(randomString);
         return randomString;
      }

      public static EnumType ReturnRandomEnum<EnumType>(Expression<Func<EnumType>> call)
      {
         EnumType randomEnum = TestRandom.Enum<EnumType>();
         A.CallTo(call).WithAnyArguments().Returns(randomEnum);
         return randomEnum;
      }

      public static DateTime ReturnRandomDateTime(Expression<Func<DateTime>> call)
      {
         DateTime randomDateTime = TestRandom.DateTime();
         A.CallTo(call).WithAnyArguments().Returns(randomDateTime);
         return randomDateTime;
      }

      public static ReadOnlyCollection<T> ReturnRandomReadOnlyCollection<T>(Expression<Func<ReadOnlyCollection<T>>> call) where T : new()
      {
         var randomReadOnlyCollection = TestRandom.ReadOnlyCollection<T>();
         A.CallTo(call).WithAnyArguments().Returns(randomReadOnlyCollection);
         return randomReadOnlyCollection;
      }

      public static ReadOnlyCollection<string> ReturnRandomReadOnlyStringCollection(Expression<Func<ReadOnlyCollection<string>>> call)
      {
         ReadOnlyCollection<string> randomReadOnlyStringCollection = TestRandom.ReadOnlyStringCollection();
         A.CallTo(call).WithAnyArguments().Returns(randomReadOnlyStringCollection);
         return randomReadOnlyStringCollection;
      }

      public static Tuple<string, string> ReturnRandomTupleStringString(Expression<Func<Tuple<string, string>>> call)
      {
         var randomTupleStringString = new Tuple<string, string>(TestRandom.String(), TestRandom.String());
         A.CallTo(call).WithAnyArguments().Returns(randomTupleStringString);
         return randomTupleStringString;
      }

      public static Tuple<DateTime, DateTime> ReturnRandomTupleDateTimeDateTime(Expression<Func<Tuple<DateTime, DateTime>>> call)
      {
         var randomTupleDateTimeDateTime = new Tuple<DateTime, DateTime>(TestRandom.DateTime(), TestRandom.DateTime());
         A.CallTo(call).WithAnyArguments().Returns(randomTupleDateTimeDateTime);
         return randomTupleDateTimeDateTime;
      }
   }
}
