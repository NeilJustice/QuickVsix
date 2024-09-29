using CSharpUtils;
using NUnit.Framework;
using System;
using System.Reflection;

[TestFixture]
public static class ReflectTests
{
   class GetFieldInfoTester
   {
      public int x;
   };

   [OneTimeSetUp]
   public static void OneTimeSetUp()
   {
      UnusedVariableWarning.Suppress(ref new GetFieldInfoTester().x);
   }

   [Test]
   public static void GetFieldInfoGeneric_ReturnsFieldInfo()
   {
      FieldInfo fieldInfo = Reflect.GetFieldInfo<GetFieldInfoTester>("x");
      FieldInfo classicFieldInfo = typeof(GetFieldInfoTester).GetField("x");
      Assert.AreEqual(classicFieldInfo, fieldInfo);

      new GetFieldInfoTester().x = 10; // Suppress variable is never assigned to warning
   }

   [Test]
   public static void GetFieldInfoTypeArgument_ReturnsFieldInfo()
   {
      Type t = typeof(GetFieldInfoTester);
      FieldInfo fieldInfo = Reflect.GetFieldInfo(t, "x");
      FieldInfo classicFieldInfo = typeof(GetFieldInfoTester).GetField("x");
      Assert.AreEqual(classicFieldInfo, fieldInfo);
   }

   class X
   {
      public int x;
   };

   [Test]
   public static void GetAndSet_GetsAndSetInstanceFields_ThrowsIfFieldsNotFoundOrNotSettable()
   {
      var x = new X();

      Assert.AreEqual(0, Reflect.Get(x, "x"));
      Assert2.Throws<ArgumentException>(() => Reflect.Get(x, "x123"),
          "Field not found: ReflectTests+X.x123");

      Reflect.Set(x, "x", 10);
      Assert.AreEqual(10, Reflect.Get(x, "x"));
      Assert2.Throws<ArgumentException>(() => Reflect.Set(x, "x123", 10),
          "Field not found: ReflectTests+X.x123");
      Assert2.Throws<ArgumentException>(() => Reflect.Set(x, "x", "not_an_int"),
          "Field ReflectTests+X.x with type System.Int32 is not assignable from value [not_an_int] which is of type System.String");

      UnusedVariableWarning.Suppress(ref x.x);
   }

   class SetPropertyTestClass
   {
      public int PublicProperty { get; set; }
      public string PublicPrivateProperty { get; private set; }
   }

   [Test]
   public static void SetProperty_InvalidPropertyName_ThrowsWithExceptionTextIncludingInvalidPropertyName()
   {
      var obj = new SetPropertyTestClass();
      string invalidPropertyName = TestRandom.String();
      Assert2.Throws<ArgumentException>(() =>
          Reflect.SetProperty(obj, invalidPropertyName, TestRandom.Int()),
          $"Property not found on object: {invalidPropertyName}");
   }

   [Test]
   public static void SetProperty_ValidPropertyNames_SetsPublicAndNonPublicProperties()
   {
      var obj = new SetPropertyTestClass();
      Assert.AreEqual(0, obj.PublicProperty);
      int publicPropertyValue = TestRandom.Int();
      string publicPrivatePropertyValue = TestRandom.String();
      //
      Reflect.SetProperty(obj, "PublicProperty", publicPropertyValue);
      Reflect.SetProperty(obj, "PublicPrivateProperty", publicPrivatePropertyValue);
      //
      Assert.AreEqual(publicPropertyValue, obj.PublicProperty);
      Assert.AreEqual(publicPrivatePropertyValue, obj.PublicPrivateProperty);
   }

   [Test]
   public static void GenericGetOfTypeString_FieldIsTypeInt_ThrowsInvalidCastException()
   {
      Assert2.Throws<InvalidCastException>(() => Reflect.Get<string>(new X(), "x"),
          "Unable to cast object of type 'System.Int32' to type 'System.String'.");
   }

   [Test]
   public static void GenericGetOfTypeInt_FieldIsTypeInt_ReturnsFieldCastToInt()
   {
      Assert.AreEqual(0, Reflect.Get<int>(new X(), "x"));
   }

   struct ZeroFieldZeroPropertyStruct
   {
   }
}
