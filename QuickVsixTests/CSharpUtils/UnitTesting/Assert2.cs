using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CSharpUtils
{
   public static class Assert2
   {
      public static void IsNotNullOrEmpty(string value)
      {
         Assert.IsNotNull(value);
         Assert.IsNotEmpty(value);
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
         string programName = Reflect.Get<string>(processRunner, "_programName");
         Assert.AreEqual(expectedProgramName, programName);
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

      public static void Throws<ExpectedExceptionType>(Action action, string expectedExceptionMessage)
          where ExpectedExceptionType : Exception
      {
         using (new TestExecutionContext.IsolatedContext())
         {
            try
            {
               action();
            }
            catch (ExpectedExceptionType e)
            {
               Type actualExceptionType = e.GetType();
               Type expectedExceptionType = typeof(ExpectedExceptionType);
               if (actualExceptionType != expectedExceptionType)
               {
                  string notExactExpectedExceptionMessage =
                     "Action threw " + e.GetType().FullName + ". Exact expected exception: " + typeof(ExpectedExceptionType).FullName;
                  throw new AssertionException(notExactExpectedExceptionMessage);
               }
               string actualMessage = e.Message;
               if (expectedExceptionMessage != actualMessage)
               {
                  string nonMatchingMessageMessage = "Action threw exactly " + expectedExceptionType.FullName + " as expected but with an unexpected Message." + Environment.NewLine +
  "Expected message: \"" + expectedExceptionMessage + "\"" + Environment.NewLine +
  "  Actual message: \"" + actualMessage + "\"";
                  throw new AssertionException(nonMatchingMessageMessage);
               }
               return;
            }
            catch (Exception e)
            {
               string unrelatedExceptionTypeMessage =
                  "Action threw " + e.GetType().FullName + ". Exact expected exception: " + typeof(ExpectedExceptionType).FullName;
               throw new AssertionException(unrelatedExceptionTypeMessage);
            }
            string didNotThrowMessage = "Action did not throw. Exact expected exception: " +
               typeof(ExpectedExceptionType).FullName;
            throw new AssertionException(didNotThrowMessage);
         }
      }

      public static void EqualsThrowsInvalidCastExceptionWhenComparedWithANewObject(object expectedClassInstance)
      {
         string expectedClassInstanceFullName = expectedClassInstance.GetType().FullName;
         string expectedExceptionMessage =
            $"Unable to cast object of type 'System.Object' to type '{expectedClassInstanceFullName}'.";
         Assert2.Throws<InvalidCastException>(() => expectedClassInstance.Equals(new object()), expectedExceptionMessage);
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
   }
}
