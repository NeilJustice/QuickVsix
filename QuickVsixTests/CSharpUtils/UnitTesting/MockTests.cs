using System.Collections.ObjectModel;
using CSharpUtils;
using FakeItEasy;
using NUnit.Framework;

[TestFixture]
public class MockTests
{
   ReturnTestClass _mockObject;

   [SetUp]
   public void SetUp()
   {
      _mockObject = Mock.Strict<ReturnTestClass>();
   }

   public enum ReturnRandomEnum
   {
      A, B, C
   }

   public class ReturnTestClass
   {
      public virtual bool GetBool() { return false; }
      public virtual char GetChar() { return ' '; }
      public virtual int GetInt() { return 0; }
      public virtual long GetLong() { return 0L; }
      public virtual string GetString() { return null; }
      public virtual DateTime GetDateTime() { return DateTime.MinValue; }
      public virtual double GetDouble() { return 0.0; }
      public virtual decimal GetDecimal() { return 0.0m; }
      public virtual Uri GetUri() { return null; }
      public virtual Guid GetGuid() { return new Guid(); }
      public virtual ReturnRandomEnum GetEnum() { return ReturnRandomEnum.A; }
      public virtual ReadOnlyCollection<int> GetInts() { return null; }
      public virtual ReadOnlyCollection<decimal> GetDecimals() { return null; }
      public virtual ReadOnlyCollection<string> GetStrings() { return null; }
      public virtual Tuple<string, string> GetTupleStringString() { return null; }
      public virtual Tuple<DateTime, DateTime> GetTupleDateTimeDateTime() { return null; }
   }

   public class MockTestClass
   {
      public virtual int Method()
      {
         return 0;
      }

      public virtual int Property { get; set; }
   }

   public class Component
   {
   }

   class Class
   {
      private object _privateObjectField = new object();
      private Component _privateComponentField = new Component();
      private object _privateNullObjectField;
      private Component _privateNullComponentField;

      public void SuppressUnusedVariableWarning()
      {
         UnusedVariableWarning.Suppress(ref _privateObjectField);
         UnusedVariableWarning.Suppress(ref _privateComponentField);
         UnusedVariableWarning.Suppress(ref _privateNullObjectField);
         UnusedVariableWarning.Suppress(ref _privateNullComponentField);
      }
   }

   [Test]
   public static void Component_OutParameter_TargetFieldIsNotExactType_ThrowsAssertionException()
   {
      var classInstance = new Class();
      //
      Assert2.Throws<AssertionException>(() =>
          Mock.Component<Component>(classInstance, "_privateObjectField"),
@"  Field ""_privateObjectField""
Assert.That(actual, Is.EqualTo(expected))
  Expected: <MockTests+Component>
  But was:  <System.Object>
");
   }

   [Test]
   public static void Component_TargetFieldIsExactType_SetsFieldToStrictMock()
   {
      var classInstance = new Class();
      //
      Component componentMock = Mock.Component<Component>(classInstance, "_privateComponentField");
      //
      Assert2.PrivateFieldSameAs(componentMock, classInstance, "_privateComponentField");
   }

   [Test]
   public static void Component_VoidReturnValue_SetsField()
   {
      var classInstance = new Class();
      var componentMock = Mock.Strict<Component>();
      //
      Mock.Component(classInstance, "_privateComponentField", componentMock);
      //
      Assert2.PrivateFieldSameAs(componentMock, classInstance, "_privateComponentField");
      classInstance.SuppressUnusedVariableWarning();
   }

   [Test]
   public static void NullComponent_TargetFieldIsNotNull_ThrowsAssertionException()
   {
      var classInstance = new Class();
      //
      Assert2.Throws<AssertionException>(() =>
         Mock.NullComponent<Component>(classInstance, "_privateComponentField"),
@"  Field: _privateComponentField
Assert.That(anObject, Is.Null)
  Expected: null
  But was:  <MockTests+Component>
");
   }

   [Test]
   public static void NullComponent_TargetFieldIsNullButNotExactType_ThrowsAssertionException()
   {
      var classInstance = new Class();
      //
      Assert2.Throws<AssertionException>(() =>
         Mock.NullComponent<Component>(classInstance, "_privateNullObjectField"),
@"  Assert.That(actual, Is.EqualTo(expected))
  Expected: <MockTests+Component>
  But was:  <System.Object>
");
   }

   [Test]
   public static void NullComponent_TargetFieldIsNullAndExactType_SetsFieldToStrictMock()
   {
      var classInstance = new Class();
      //
      Component componentMock = Mock.NullComponent<Component>(classInstance, "_privateNullComponentField");
      //
      Assert2.PrivateFieldSameAs(componentMock, classInstance, "_privateNullComponentField");
      classInstance.SuppressUnusedVariableWarning();
   }

   public class ThrowTestingClass
   {
      public virtual void VirtualAction()
      {
      }

      public virtual bool VirtualFunction()
      {
         return true;
      }
   }

   [Test]
   public static void Throw_Action_MakesMockActionThrowSpecifiedExceptionWhenCalled__ExceptionTestCase()
   {
      var throwingTestingClassMock = Mock.Strict<ThrowTestingClass>();
      string message = TestRandom.String();
      var ex = new Exception(message);
      //
      Mock.Throw(() => throwingTestingClassMock.VirtualAction(), ex);
      //
      Assert2.Throws<Exception>(() => throwingTestingClassMock.VirtualAction(), message);
      Assert2.Throws<Exception>(() => throwingTestingClassMock.VirtualAction(), message);

      new ThrowTestingClass().VirtualAction(); // 100% code coverage
   }

   [Test]
   public static void Throw_Action_MakesMockActionThrowSpecifiedExceptionWhenCalled__ArgumentExceptionTestCase()
   {
      var throwingTestingClassMock = Mock.Strict<ThrowTestingClass>();
      string message = TestRandom.String();
      var ex = new ArgumentException(message);
      //
      Mock.Throw(() => throwingTestingClassMock.VirtualAction(), ex);
      //
      Assert2.Throws<ArgumentException>(() => throwingTestingClassMock.VirtualAction(), message);
      Assert2.Throws<ArgumentException>(() => throwingTestingClassMock.VirtualAction(), message);
   }

   [Test]
   public static void Throw_Function_MakesMockFunctionThrowSpecifiedExceptionWhenCalled__ExceptionTestCase()
   {
      var throwingTestingClassMock = Mock.Strict<ThrowTestingClass>();
      string message = TestRandom.String();
      var ex = new Exception(message);
      //
      Mock.Throw(() => throwingTestingClassMock.VirtualFunction(), ex);
      //
      Assert2.Throws<Exception>(() => throwingTestingClassMock.VirtualFunction(), message);
      Assert2.Throws<Exception>(() => throwingTestingClassMock.VirtualFunction(), message);

      new ThrowTestingClass().VirtualFunction(); // 100% code coverage
   }

   [Test]
   public static void Throw_Funcion_MakesMockFunctionThrowSpecifiedExceptionWhenCalled__ArgumentExceptionTestCase()
   {
      var throwingTestingClassMock = Mock.Strict<ThrowTestingClass>();
      string message = TestRandom.String();
      var ex = new ArgumentException(message);
      //
      Mock.Throw(() => throwingTestingClassMock.VirtualFunction(), ex);
      //
      Assert2.Throws<ArgumentException>(() => throwingTestingClassMock.VirtualFunction(), message);
      Assert2.Throws<ArgumentException>(() => throwingTestingClassMock.VirtualFunction(), message);
   }

   [Test]
   public static void Return_MakesMockMethodNotThrowWhenCalledAndReturnValue()
   {
      var mock = Mock.Strict<MockTestClass>();
      Mock.Return(() => mock.Method(), 1);
      //
      int returnValueA = mock.Method();
      int returnValueB = mock.Method();
      int returnValueC = mock.Method();
      //
      Assert.AreEqual(1, returnValueA);
      Assert.AreEqual(1, returnValueB);
      Assert.AreEqual(1, returnValueC);
   }

   public class B
   {
   }

   public class A
   {
      public A()
      {
      }

      public virtual B GetA()
      {
         return null;
      }
   }

   [Test]
   public static void ReturnStrictMock_MakesMockMethodNotThrowWhenCalledAndReturnANewStrictMock()
   {
      var a = Mock.Strict<A>();
      //
      B strictMock1 = Mock.ReturnStrictMock(() => a.GetA());
      //
      B getAReturnValue1 = a.GetA();
      Assert.AreSame(strictMock1, getAReturnValue1);

      Assert.IsNull(new A().GetA()); // 100% CSharpUtilsTests code coverage
   }

   [Test]
   public static void ReturnValues_MakesMockMethodNotThrowWhenCalledAndReturnValues()
   {
      var mock = Mock.Strict<MockTestClass>();
      Mock.ReturnValues(() => mock.Method(), 1, 2, 3);
      //
      int returnValueA = mock.Method();
      int returnValueB = mock.Method();
      int returnValueC = mock.Method();
      //
      Assert.AreEqual(1, returnValueA);
      Assert.AreEqual(2, returnValueB);
      Assert.AreEqual(3, returnValueC);
   }

   [Test]
   public static void ExpectPropertySet_MakesSettingPropertyNotThrow()
   {
      var mock = Mock.Strict<MockTestClass>();
      Assert2.Throws<ExpectationException>(() => mock.Property = 1,
         "Call to unconfigured method of strict fake: MockTests+MockTestClass.Property = 1.");
      //
      Mock.ExpectPropertySet(() => mock.Property);
      //
      Assert.DoesNotThrow(() => mock.Property = 0);
      Assert.DoesNotThrow(() => mock.Property = 1);
   }

   [Test]
   public static void MockTestClass_MethodAndProperty_CodeCoverage()
   {
      var instance = new MockTestClass();
      instance.Method();
      instance.Property = 1;
      Assert.AreEqual(1, instance.Property);
   }

   [Test]
   public void ReturnRandomBool_ExpectsCallAndReturnsRandomBool()
   {
      bool randomBool = Mock.ReturnRandomBool(() => _mockObject.GetBool());
      bool returnedRandomBool = _mockObject.GetBool();
      Assert.AreEqual(randomBool, returnedRandomBool);
   }

   [Test]
   public void ReturnRandomInt_ExpectsCallAndReturnsRandomInt()
   {
      int randomInt = Mock.ReturnRandomInt(() => _mockObject.GetInt());
      int returnedRandomInt = _mockObject.GetInt();
      Assert.AreEqual(randomInt, returnedRandomInt);
   }

   [Test]
   public void ReturnRandomString_ExpectsCallAndReturnsRandomString()
   {
      string randomString = Mock.ReturnRandomString(() => _mockObject.GetString());
      string returnedRandomString = _mockObject.GetString();
      Assert.AreEqual(randomString, returnedRandomString);
   }

   [Test]
   public void ReturnRandomEnum_ExpectsCallAndReturnsRandomEnum()
   {
      ReturnRandomEnum randomEnum = Mock.ReturnRandomEnum(() => _mockObject.GetEnum());
      ReturnRandomEnum returnedRandomEnum = _mockObject.GetEnum();
      Assert.AreEqual(randomEnum, returnedRandomEnum);
   }

   [Test]
   public void ReturnRandomDateTime_ExpectsCallAndReturnsRandomDateTime()
   {
      DateTime randomDateTime = Mock.ReturnRandomDateTime(() => _mockObject.GetDateTime());
      DateTime returnedRandomDateTime = _mockObject.GetDateTime();
      Assert.AreEqual(randomDateTime, returnedRandomDateTime);
   }

   [Test]
   public void ReturnRandomReadOnlyCollection_ExpectsCallAndReturnsRandomReadOnlyCollectionOfT()
   {
      ReadOnlyCollection<int> randomInts = Mock.ReturnRandomReadOnlyCollection(() => _mockObject.GetInts());
      ReadOnlyCollection<int> returnedRandomInts = _mockObject.GetInts();
      Assert.AreEqual(randomInts, returnedRandomInts);
   }

   [Test]
   public void ReturnRandomReadOnlyStringCollection_ExpectsCallAndReturnsRandomReadOnlyStringCollection()
   {
      ReadOnlyCollection<string> randomStrings = Mock.ReturnRandomReadOnlyStringCollection(() => _mockObject.GetStrings());
      ReadOnlyCollection<string> returnedRandomStrings = _mockObject.GetStrings();
      Assert.AreEqual(randomStrings, returnedRandomStrings);
   }

   [Test]
   public void ReturnRandomTupleStringString_ExpectsCallAndReturnsRandomTupleStringString()
   {
      Tuple<string, string> tupleStringString = Mock.ReturnRandomTupleStringString(() => _mockObject.GetTupleStringString());
      Tuple<string, string> returnedTupleStringString = _mockObject.GetTupleStringString();
      Assert.AreEqual(tupleStringString, returnedTupleStringString);
   }

   [Test]
   public void ReturnRandomTupleDateTimeDateTime_ExpectsCallAndReturnsRandomTupleDateTimeDateTime()
   {
      Tuple<DateTime, DateTime> tupleDateTimeDateTime = Mock.ReturnRandomTupleDateTimeDateTime(() => _mockObject.GetTupleDateTimeDateTime());
      Tuple<DateTime, DateTime> returnedTupleDateTimeDateTime = _mockObject.GetTupleDateTimeDateTime();
      Assert.AreEqual(tupleDateTimeDateTime, returnedTupleDateTimeDateTime);
   }

   [Test]
   public static void ReturnTestClass_100PercentCodeCoverage()
   {
      var returnTestClass = new ReturnTestClass();
      Assert.IsFalse(returnTestClass.GetBool());
      Assert.AreEqual(' ', returnTestClass.GetChar());
      Assert.AreEqual(0, returnTestClass.GetInt());
      Assert.IsNull(returnTestClass.GetString());
      Assert.AreEqual(DateTime.MinValue, returnTestClass.GetDateTime());
      Assert.AreEqual(0, returnTestClass.GetLong());
      Assert.AreEqual(0.0, returnTestClass.GetDouble());
      Assert.AreEqual(0.0m, returnTestClass.GetDecimal());
      Assert.IsNull(returnTestClass.GetUri());
      Assert.AreEqual(new Guid(), returnTestClass.GetGuid());
      Assert.AreEqual(ReturnRandomEnum.A, returnTestClass.GetEnum());
      Assert.IsNull(returnTestClass.GetInts());
      Assert.IsNull(returnTestClass.GetDecimals());
      Assert.IsNull(returnTestClass.GetStrings());
      Assert.IsNull(returnTestClass.GetTupleStringString());
      Assert.IsNull(returnTestClass.GetTupleDateTimeDateTime());
   }
}
