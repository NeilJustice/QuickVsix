using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.RegularExpressions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace CSharpUtils
{
   public static class Assert2
   {
      [ExcludeFromCodeCoverage]
      public static void AssertConsoleWriterHasProgramName<T>(string expectedProgramName, T component, string consoleWriterFieldName)
      {
         ConsoleWriter consoleWriter = Reflect.Get<ConsoleWriter>(component, consoleWriterFieldName);
         string programName = Reflect.Get<string>(consoleWriter, "_programName");
         Assert.AreEqual(expectedProgramName, programName);
      }

      [ExcludeFromCodeCoverage]
      public static void AssertProcessRunnerHasProgramName<T>(string expectedProgramName, T component, string processRunnerFieldName)
      {
         ProcessRunner processRunner = Reflect.Get<ProcessRunner>(component, processRunnerFieldName);
         string programName = processRunner.GetConsoleWriterProgramName();
         Assert.AreEqual(expectedProgramName, programName);
      }

      public static void EqualsThrowsWhenFieldNotEqual<FieldType>(
         object expectedObject, object actualObject,
         ref FieldType expectedField, ref FieldType actualField,
         FieldType nonDefaultFieldValue, string expectedAssertionNotEqualMessage)
      {
         expectedField = nonDefaultFieldValue;
         Throws<AssertionException>(() => expectedObject.Equals(actualObject), expectedAssertionNotEqualMessage);
         actualField = nonDefaultFieldValue;
         Assert.IsTrue(expectedObject.Equals(actualObject));
      }

      public static void EqualsThrowsWhenStringFieldNotEqual(
          object expectedObject, object actualObject, ref string expectedField, ref string actualField)
      {
         string nonDefaultFieldValue = TestRandom.String();
         string expectedAssertionNotEqualMessage = $@"  Assert.That(actual, Is.EqualTo(expected))
  Expected: ""{nonDefaultFieldValue}""
  But was:  null
";
         EqualsThrowsWhenFieldNotEqual(
            expectedObject, actualObject, ref expectedField, ref actualField,
            nonDefaultFieldValue, expectedAssertionNotEqualMessage);
      }

      public static void EqualsThrowsWhenBoolFieldNotEqual(
          object expectedObject, object actualObject, ref bool expectedField, ref bool actualField)
      {
         string expectedAssertionNotEqualMessage = $@"  Assert.That(actual, Is.EqualTo(expected))
  Expected: True
  But was:  False
";
         EqualsThrowsWhenFieldNotEqual(
            expectedObject, actualObject, ref expectedField, ref actualField,
            true, expectedAssertionNotEqualMessage);
      }

      public static void EqualsThrowsWhenIntFieldNotEqual(
          object expectedObject, object actualObject, ref int expectedField, ref int actualField)
      {
         int nonDefaultFieldValue = 1 + Math.Abs(TestRandom.Int()); // Math.Abs ensures nonDefaultFieldValue is not 0
         string expectedAssertionNotEqualMessage = $@"  Assert.That(actual, Is.EqualTo(expected))
  Expected: {nonDefaultFieldValue}
  But was:  0
";
         EqualsThrowsWhenFieldNotEqual(
            expectedObject, actualObject, ref expectedField, ref actualField,
            nonDefaultFieldValue, expectedAssertionNotEqualMessage);
      }

      public static void EqualsThrowsWhenEnumFieldNotEqual<EnumType>(
         object expectedObject, object actualObject, ref EnumType expectedField, ref EnumType actualField,
         EnumType zeroEnumValue, EnumType nonDefaultEnumValue)
      {
         string expectedAssertionNotEqualMessage = $@"  Assert.That(actual, Is.EqualTo(expected))
  Expected: {nonDefaultEnumValue}
  But was:  {zeroEnumValue}
";
         EqualsThrowsWhenFieldNotEqual(
            expectedObject, actualObject, ref expectedField, ref actualField,
            nonDefaultEnumValue, expectedAssertionNotEqualMessage);
      }

      public static void EqualsThrowsInvalidCastExceptionWhenComparedWithANewObject(object expectedClassInstance)
      {
         string expectedClassInstanceFullName = expectedClassInstance.GetType().FullName;
         string expectedExceptionMessage =
            $"Unable to cast object of type 'System.Object' to type '{expectedClassInstanceFullName}'.";
         Assert2.Throws<InvalidCastException>(() => expectedClassInstance.Equals(new object()), expectedExceptionMessage);
      }

      public static void EqualsThrowsInvalidCastExceptionWhenComparedWithANewObject_NonFullNameVersion(object expectedClassInstance)
      {
         string expectedClassInstanceName = expectedClassInstance.GetType().Name;
         string expectedExceptionMessage =
            $"Unable to cast object of type 'System.Object' to type '{expectedClassInstanceName}'.";
         Assert2.Throws<InvalidCastException>(() => expectedClassInstance.Equals(new object()), expectedExceptionMessage);
      }

      public static void FieldIsNull(object obj, string fieldName)
      {
         object field = Reflect.Get(obj, fieldName);
         Assert.IsNull(field, "Regarding field \"" + fieldName + "\"");
      }

      public static void FieldIsNonNullAndExactType<ExpectedFieldType>(object classInstance, string fieldName)
      {
         FieldInfo fieldInfo = classInstance.GetType().GetField(fieldName,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
         if (fieldInfo == null)
         {
            throw new AssertionException($"Field name not found on classInstance: {fieldName}");
         }
         Type expectedFieldType = typeof(ExpectedFieldType);
         Type actualDeclaredFieldType = fieldInfo.FieldType;
         object fieldValue = fieldInfo.GetValue(classInstance);
         if (fieldValue == null)
         {
            throw new AssertionException($"Field is null: {fieldName}");
         }
         if (expectedFieldType != actualDeclaredFieldType)
         {
            Type actualRuntimeFieldType = fieldValue.GetType();
            Assert.AreEqual(expectedFieldType, actualRuntimeFieldType, $"Field \"{fieldName}\"");
         }
      }

      public static void FieldIsNullAndExactType<ExpectedFieldType>(object classInstance, string fieldName)
      {
         FieldInfo fieldInfo = classInstance.GetType().GetField(fieldName,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
         if (fieldInfo == null)
         {
            throw new AssertionException($"Field name not found on classInstance: {fieldName}");
         }
         object fieldValue = fieldInfo.GetValue(classInstance);
         Assert.IsNull(fieldValue, $"Field: {fieldName}");
         Type expectedFieldType = typeof(ExpectedFieldType);
         Type actualDeclaredFieldType = fieldInfo.FieldType;
         Assert.AreEqual(expectedFieldType, actualDeclaredFieldType);
      }

      public static void IsNotNullOrEmpty(string value)
      {
         Assert.IsNotNull(value);
         Assert.IsNotEmpty(value);
      }

      public static void StringMatchesRegex(string expectedPattern, string str)
      {
         string message = $"Regex.IsMatch(\"{expectedPattern}\", \"{str}\")";
         Assert.IsTrue(Regex.IsMatch(str, expectedPattern), message);
      }

      public static void Throws<ExpectedExceptionType>(Action action, string expectedExceptionMessage)
         where ExpectedExceptionType : Exception
      {
         using (new TestExecutionContext.IsolatedContext())
         {
            try
            {
               action();
            }
            catch (ExpectedExceptionType ex)
            {
               Type actualExceptionType = ex.GetType();
               Type expectedExceptionType = typeof(ExpectedExceptionType);
               if (actualExceptionType != expectedExceptionType)
               {
                  string notExactExpectedExceptionMessage =
                     "Action threw " + ex.GetType().FullName + ". Exact expected exception: " + typeof(ExpectedExceptionType).FullName;
                  throw new AssertionException(notExactExpectedExceptionMessage);
               }
               string actualExceptionMessage = ex.Message;
               string expectedExceptionMessage_slashRRemoved = expectedExceptionMessage.Replace("\r", "");
               string actualExceptionMessage_slashRRemoved = actualExceptionMessage.Replace("\r", "");
               if (expectedExceptionMessage_slashRRemoved != actualExceptionMessage_slashRRemoved)
               {
                  string nonMatchingMessageMessage = "Action threw exactly " + expectedExceptionType.FullName + " as expected but with an unexpected Message." + Environment.NewLine +
  "Expected message: \"" + expectedExceptionMessage + "\"" + Environment.NewLine +
  "  Actual message: \"" + actualExceptionMessage + "\"";
                  throw new AssertionException(nonMatchingMessageMessage);
               }
               return;
            }
            catch (Exception ex)
            {
               string unrelatedExceptionTypeMessage =
                  "Action threw " + ex.GetType().FullName + ". Exact expected exception: " + typeof(ExpectedExceptionType).FullName;
               throw new AssertionException(unrelatedExceptionTypeMessage);
            }
            string didNotThrowMessage = "Action did not throw. Exact expected exception: " +
               typeof(ExpectedExceptionType).FullName;
            throw new AssertionException(didNotThrowMessage);
         }
      }

      public static void ThrowsNotSupportedException(Action action)
      {
         Throws<NotSupportedException>(() => action(),
            "Specified method is not supported.");
      }

      public static void PrivateFieldSameAs(object expectedObject, object obj, string fieldName)
      {
         FieldInfo fieldInfo = Reflect.GetFieldInfo(obj.GetType(), fieldName);
         object fieldValue = fieldInfo.GetValue(obj);
         if (fieldValue != expectedObject)
         {
            throw new AssertionException($"Field \"{fieldName}\" found but with a value not the same as the expected object.");
         }
      }

      // Private Public Methods

      public static string MakeExpectedButWas(string fieldValue)
      {
         return $@"  Expected: ""{fieldValue}""
  But was:  null
";
      }
   }
}
