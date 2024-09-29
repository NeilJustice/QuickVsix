using CSharpUtils;
using NUnit.Framework;

public class CalledClass
{
   public virtual void Action(int x)
   {
   }

   public virtual int Function(int x)
   {
      return 0;
   }

   public virtual int Property { get; set; }
}

[TestFixture]
public class CalledTests
{
   CalledClass _mock;

   [SetUp]
   public void SetUp()
   {
      _mock = FakeItEasy.A.Fake<CalledClass>();
   }

   [Test]
   public void Once_ActionOverload_DoesNotThrowIfCallCalledOnce()
   {
      int arg = TestRandom.Int();
      Assert.Throws<FakeItEasy.ExpectationException>(() => Called.Once(() => _mock.Action(arg)));
      _mock.Action(arg);
      Called.Once(() => _mock.Action(arg));
      _mock.Action(arg);
      Assert.Throws<FakeItEasy.ExpectationException>(() => Called.Once(() => _mock.Action(arg)));
   }

   [Test]
   public void Once_FuncOverload_DoesNotThrowIfCallCalledOnce()
   {
      int arg = TestRandom.Int();
      Assert.Throws<FakeItEasy.ExpectationException>(() => Called.Once(() => _mock.Function(arg)));
      _mock.Function(arg);
      Called.Once(() => _mock.Function(arg));
      _mock.Function(arg);
      Assert.Throws<FakeItEasy.ExpectationException>(() => Called.Once(() => _mock.Function(arg)));
   }

   [Test]
   public void ThreeTimes_DoesNotThrowIfCallCalledExactlyThreeTimes()
   {
      int arg = TestRandom.Int();
      Assert.Throws<FakeItEasy.ExpectationException>(() => Called.ThreeTimes(() => _mock.Function(arg)));
      _mock.Function(arg);
      Assert.Throws<FakeItEasy.ExpectationException>(() => Called.ThreeTimes(() => _mock.Function(arg)));
      _mock.Function(arg);
      Assert.Throws<FakeItEasy.ExpectationException>(() => Called.ThreeTimes(() => _mock.Function(arg)));
      _mock.Function(arg);
      Called.ThreeTimes(() => _mock.Function(arg));
      Assert.Throws<FakeItEasy.ExpectationException>(() => Called.ThreeTimes(() => _mock.Function(arg + 1)));
      _mock.Function(arg);
      Assert.Throws<FakeItEasy.ExpectationException>(() => Called.ThreeTimes(() => _mock.Function(arg)));
   }

   [Test]
   public void NumberOfTimes_ThrowsIfMethodNotCalledNumberOfTimesWithAnyArguments()
   {
      Called.NumberOfTimes(0, () => _mock.Action(0));
      Called.NumberOfTimes(0, () => _mock.Action(1));
      Called.NumberOfTimes(0, () => _mock.Function(0));
      Called.NumberOfTimes(0, () => _mock.Function(1));

      _mock.Action(10);
      _mock.Function(20);

      Called.NumberOfTimes(1, () => _mock.Action(0));
      Called.NumberOfTimes(1, () => _mock.Action(10));
      Called.NumberOfTimes(1, () => _mock.Function(0));
      Called.NumberOfTimes(1, () => _mock.Function(20));

      _mock.Action(30);
      _mock.Function(40);

      Called.NumberOfTimes(2, () => _mock.Action(0));
      Called.NumberOfTimes(2, () => _mock.Action(0));
      Called.NumberOfTimes(2, () => _mock.Function(0));
      Called.NumberOfTimes(2, () => _mock.Function(0));

      Assert2.Throws<FakeItEasy.ExpectationException>(() =>
          Called.NumberOfTimes(3, () => _mock.Action(0)),
@"

  Assertion failed for the following call:
    CalledClass.Action(x: <Predicated>)
  Expected to find it 3 times exactly but found it twice among the calls:
    1: CalledClass.Action(x: 10)
    2: CalledClass.Function(x: 20)
    3: CalledClass.Action(x: 30)
    4: CalledClass.Function(x: 40)

");
      Assert2.Throws<FakeItEasy.ExpectationException>(() =>
          Called.NumberOfTimes(3, () => _mock.Function(0)),
@"

  Assertion failed for the following call:
    CalledClass.Function(x: <Predicated>)
  Expected to find it 3 times exactly but found it twice among the calls:
    1: CalledClass.Action(x: 10)
    2: CalledClass.Function(x: 20)
    3: CalledClass.Action(x: 30)
    4: CalledClass.Function(x: 40)

");
   }

   [Test]
   public void WasCalled_ThrowsIfCallNotCalled_OtherwiseDoesNotThrowIfCalledAnyNumberOfTimesWithExactArguments()
   {
      Assert2.Throws<FakeItEasy.ExpectationException>(() => Called.WasCalled(() => _mock.Action(0)),
          @"

  Assertion failed for the following call:
    CalledClass.Action(x: 0)
  Expected to find it once or more but no calls were made to the fake object.

");
      _mock.Action(1);
      Assert2.Throws<FakeItEasy.ExpectationException>(() => Called.WasCalled(() => _mock.Action(0)),
          @"

  Assertion failed for the following call:
    CalledClass.Action(x: 0)
  Expected to find it once or more but didn't find it among the calls:
    1: CalledClass.Action(x: 1)

");
      Called.WasCalled(() => _mock.Action(1));
      _mock.Action(2);
      Called.WasCalled(() => _mock.Action(1));
      Called.WasCalled(() => _mock.Action(2));
   }

   [Test]
   public void PropertySetOnce_DoesNotThrowIfPropertySetExactlyOnce()
   {
      int value = TestRandom.Int();
      Assert.Throws<FakeItEasy.ExpectationException>(() => Called.PropertySetOnce(() => _mock.Property, value));
      _mock.Property = value;
      Called.PropertySetOnce(() => _mock.Property, value);
      Assert.Throws<FakeItEasy.ExpectationException>(() => Called.PropertySetOnce(() => _mock.Property, value + 1));
      _mock.Property = value;
      Assert.Throws<FakeItEasy.ExpectationException>(() => Called.PropertySetOnce(() => _mock.Property, value));
   }

   [Test]
   public static void CalledClassCodeCoverage()
   {
      var c = new CalledClass();
      c.Action(0);
      c.Function(0);
      c.Property = 1;
      Assert.AreEqual(1, c.Property);
   }
}
