using CSharpUtils;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

[TestFixture]
public class LinqHelperTests
{
   LinqHelper _linqHelper;

   [SetUp]
   public void SetUp()
   {
      _linqHelper = new LinqHelper();
   }

   [Test]
   public void TransformSum_ElementsAreEmpty_Returns0__IntsTestCase()
   {
      ReadOnlyCollection<int> emptyInts = Array.Empty<int>().ToReadOnlyCollection();
      double transformer(int i)
      {
         return i + 1;
      };
      //
      double sum = _linqHelper.TransformSum(emptyInts, transformer);
      //
      Assert.Zero(sum);
      transformer(TestRandom.Int()); // Code coverage
   }

   [Test]
   public void TransformSum_ElementsAreNonEmpty_CallsTransformerOnEachElement_ReturnsSumOfTransformedElements__IntsTestCase()
   {
      ReadOnlyCollection<int> emptyInts = new int[]
      {
         TestRandom.Int(),
         TestRandom.Int(),
         TestRandom.Int()
      }.ToReadOnlyCollection();
      double transformer(int i)
      {
         double transformedElement = i + 1;
         return transformedElement;
      };
      //
      double sum = _linqHelper.TransformSum(emptyInts, transformer);
      //
      double expectedSum = emptyInts[0] + 1 + emptyInts[1] + 1 + emptyInts[2] + 1;
      Assert.AreEqual(expectedSum, sum);
   }

   [Test]
   public void Count_ReturnsNumberOfMatchingElements()
   {
      int[] elements = new[] { 1, 2, 3, 4, 5 };
      //
      int numberOfMatchingElements = _linqHelper.Count(elements, (int i) => i % 2 == 0);
      //
      Assert.AreEqual(2, numberOfMatchingElements);
   }

   [Test]
   public void ForEach_ZeroElements_CallsAction0Times()
   {
      void throwIfCalled(int element)
      {
         throw new Exception();
      };
      Assert.Throws<Exception>(() => throwIfCalled(0));
      _linqHelper.ForEach(Array.Empty<int>(), throwIfCalled);
   }

   [Test]
   public void ForEach_OneElement_CallsActionOneTime()
   {
      int numberOfCalls = 0;
      int elementArg = 0;
      int[] elements = new int[] { TestRandom.Int() };
      //
      _linqHelper.ForEach(elements, (int element) =>
      {
         ++numberOfCalls;
         elementArg = element;
      });
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(elements[0], elementArg);
   }

   [Test]
   public void ForEach_TwoElements_CallsActionTwice()
   {
      int numberOfCalls = 0;
      var elementArgs = new Collection<int>();
      int[] elements = new int[] { TestRandom.Int(), TestRandom.Int() };
      //
      _linqHelper.ForEach(elements, (int element) =>
      {
         ++numberOfCalls;
         elementArgs.Add(element);
      });
      //
      Assert.AreEqual(2, numberOfCalls);
      Assert.AreEqual(new Collection<int>() { elements[0], elements[1] }, elementArgs);
   }

   [Test]
   public void IndexOfFirstMatchingElement_ElementsEmpty_ThrowsInvalidOperationException()
   {
      int value = TestRandom.Int();
      Assert2.Throws<InvalidOperationException>(() => _linqHelper.IndexOfFirstMatchingElement(Array.Empty<int>(), value),
         $"Zero elements found that equal value: {value}");
   }

   [Test]
   public void IndexOfFirstMatchingElement_ElementsNonEmptyAndDoesNotContainAnElementEqualToValue_ThrowsInvalidOperationException()
   {
      int[] elements = new int[]
      {
         TestRandom.Int(),
         TestRandom.Int()
      };
      //
      int value = Math.Max(elements[0], elements[1]) + 123;
      Assert2.Throws<InvalidOperationException>(() => _linqHelper.IndexOfFirstMatchingElement(elements, value),
         $"Zero elements found that equal value: {value}");
   }

   [TestCase(0)]
   [TestCase(1)]
   [TestCase(2)]
   public void IndexOfFirstMatchingElement_ElementsNonEmptyAndContainsTwoElementsEqualToValue_ReturnsIndexOfFirstOne(
      int indexOfMatchingElementAndExpectedReturnValue)
   {
      int element0 = TestRandom.Int();
      int element1 = element0 + 1;
      int element2 = element0 + 2;
      int[] elements = new int[]
      {
         element0,
         element1,
         element2
      };
      int value = elements[indexOfMatchingElementAndExpectedReturnValue];
      //
      int indexOfFirstMatchingElement = _linqHelper.IndexOfFirstMatchingElement(elements, value);
      //
      Assert.AreEqual(indexOfMatchingElementAndExpectedReturnValue, indexOfFirstMatchingElement);
   }

   [Test]
   public void IndexOfFirstElementThatStartsWithValue_ElementsEmpty_ThrowsInvalidOperationException()
   {
      string value = TestRandom.String();
      Assert2.Throws<InvalidOperationException>(() => _linqHelper.IndexOfFirstElementThatStartsWithValue(Array.Empty<string>(), value),
         $"Zero elements found that start with value: {value}");
   }

   [Test]
   public void IndexOfFirstElementThatStartsWithValue_ElementsNotEmpty_NoneOfTheStringsStartWithValue_ThrowsInvalidOperationException()
   {
      string value = "abc" + TestRandom.String();
      Assert2.Throws<InvalidOperationException>(() => _linqHelper.IndexOfFirstElementThatStartsWithValue(
         new[] { TestRandom.String(), TestRandom.String() }, value),
         $"Zero elements found that start with value: {value}");
   }

   [Test]
   public void IndexOfFirstElementThatStartsWithValue_ElementsNotEmpty_OneOfTheStringsStartWithValue_ReturnsItsIndex()
   {
      string[] elements = new string[]
      {
         "",
         "abc def ghi",
         " abc def",
         "abc DEF",
         "abc def",
         "abc def"
      };
      string value = "abc def 123";
      //
      int indexOfFirstElementThatStartsWithValue = _linqHelper.IndexOfFirstElementThatStartsWithValue(elements, value);
      //
      Assert.AreEqual(4, indexOfFirstElementThatStartsWithValue);
   }

   [Test]
   public void TwoArgSelectNonNullWithIndex_ZeroElements_CallsSelector0Times_ReturnsEmpty()
   {
      string[] elements = Array.Empty<string>();
      int numberOfSelectorCalls = 0;
      string arg2Value = TestRandom.String();
      var selector = new Func<string, int, string, string>((string element, int index, string arg2) =>
      {
         ++numberOfSelectorCalls;
         return "";
      });
      //
      ReadOnlyCollection<string> results = _linqHelper.TwoArgSelectNonNullWithIndex(elements, selector, arg2Value);
      //
      Assert.AreEqual(0, numberOfSelectorCalls);
      Assert.IsEmpty(results);

      // 100% code coverage
      Assert.AreEqual("", selector(null, 0, null));
   }

   [Test]
   public void TwoArgSelectNonNullWithIndex_OneElement_SelectorReturnsNull_ReturnsEmpty()
   {
      string[] elements = new string[] { TestRandom.String() };
      var elementArgs = new Collection<string>();
      var indexArgs = new Collection<int>();
      string arg2Value = TestRandom.String();
      //
      ReadOnlyCollection<string> results = _linqHelper.TwoArgSelectNonNullWithIndex(
         elements, (string element, int index, string arg2) =>
         {
            elementArgs.Add(element);
            indexArgs.Add(index);
            return (string)null;
         }, arg2Value);
      //
      Assert.AreEqual(new Collection<string> { elements[0] }, elementArgs);
      Assert.AreEqual(new Collection<int> { 0 }, indexArgs);
      Assert.AreEqual(0, results.Count);
   }

   [Test]
   public void TwoArgSelectNonNullWithIndex_OneElement_SelectorReturnsNonNull_ReturnsTransformedElement()
   {
      string[] elements = new string[] { TestRandom.String() };
      var elementArgs = new Collection<string>();
      var indexArgs = new Collection<int>();
      string arg2Value = TestRandom.String();
      //
      ReadOnlyCollection<string> results = _linqHelper.TwoArgSelectNonNullWithIndex(
         elements, (string element, int lineNumber, string arg2) =>
         {
            elementArgs.Add(element);
            indexArgs.Add(lineNumber);
            return "NonNullTransformedElement";
         }, arg2Value);
      //
      Assert.AreEqual(new Collection<string> { elements[0] }, elementArgs);
      Assert.AreEqual(new Collection<int> { 0 }, indexArgs);
      Assert.AreEqual(new string[] { "NonNullTransformedElement" }, results);
   }

   [Test]
   public void TwoArgSelectNonNullWithIndex_ThreeElements_SelectorReturnsNonNullThenNullThenNonNull_ReturnsFirstAndThirdTransformedElements()
   {
      string[] elements = new string[] { TestRandom.String(), TestRandom.String(), TestRandom.String() };
      var elementArgs = new Collection<string>();
      var indexArgs = new Collection<int>();
      string arg2Value = TestRandom.String();
      //
      ReadOnlyCollection<string> results = _linqHelper.TwoArgSelectNonNullWithIndex(
         elements, (string element, int index, string arg2) =>
         {
            elementArgs.Add(element);
            indexArgs.Add(index);
            if (index == 0)
            {
               return "NonNullTransformedElement1";
            }
            else if (index == 1)
            {
               return null;
            }
            else
            {
               Trace.Assert(index == 2);
               return "NonNullTransformedElement3";
            }
         }, arg2Value);
      //
      Assert.AreEqual(new Collection<string> { elements[0], elements[1], elements[2] }, elementArgs);
      Assert.AreEqual(new Collection<int> { 0, 1, 2 }, indexArgs);
      Assert.AreEqual(new string[] { "NonNullTransformedElement1", "NonNullTransformedElement3" }, results);
   }

   public static int PlusExtraArgPlusIndex(int x, int arg2, int index)
   {
      return x + arg2 + index;
   }

   [Test]
   public void TwoArgTransformWithIndex_CallsTransformFunctionWithTwoArgsAndIndex_ReturnsTransformedElements()
   {
      ReadOnlyCollection<int> elements = new int[] { TestRandom.Int(), TestRandom.Int(), TestRandom.Int() }.ToReadOnlyCollection();
      int arg2 = TestRandom.IntBetween(1, 3);
      //
      ReadOnlyCollection<int> transformedElements = _linqHelper.TwoArgTransformWithIndex(elements, PlusExtraArgPlusIndex, arg2);
      //
      ReadOnlyCollection<int> expectedTransformedElements = new int[]
      {
         elements[0] + arg2 + 0,
         elements[1] + arg2 + 1,
         elements[2] + arg2 + 2
      }.ToReadOnlyCollection();
      Assert.AreEqual(expectedTransformedElements, transformedElements);
   }

   [Test]
   public void TwoArgParallelTransformWithIndex_ParallelCallsTransformFunctionWithExtraArgAndIndex_ReturnsTransformedElements()
   {
      ReadOnlyCollection<int> elements = new int[] { TestRandom.Int(), TestRandom.Int(), TestRandom.Int() }.ToReadOnlyCollection();
      int arg2 = TestRandom.IntBetween(1, 3);
      //
      ReadOnlyCollection<int> transformedElements = _linqHelper.TwoArgParallelTransformWithIndex(elements, PlusExtraArgPlusIndex, arg2);
      //
      ReadOnlyCollection<int> expectedTransformedElements = new int[]
      {
         elements[0] + arg2 + 0,
         elements[1] + arg2 + 1,
         elements[2] + arg2 + 2
      }.ToReadOnlyCollection();
      CollectionAssert.AreEquivalent(expectedTransformedElements, transformedElements);
   }

   [Test]
   public void TwoArgForEach_ZeroElements_CallsAction0Times()
   {
      void throwIfCalled(int element, int arg2)
      {
         throw new Exception();
      };
      Assert.Throws<Exception>(() => throwIfCalled(0, 0));
      _linqHelper.TwoArgForEach(Array.Empty<int>(), throwIfCalled, 0);
   }

   [Test]
   public void TwoArgForEach_OneElement_CallsActionOneTime()
   {
      int numberOfCalls = 0;
      int elementArg = 0;
      string arg2Arg = null;
      int[] elements = new int[] { TestRandom.Int() };
      string randomArg2 = TestRandom.String();
      //
      _linqHelper.TwoArgForEach(elements, (int element, string arg2) =>
      {
         ++numberOfCalls;
         elementArg = element;
         arg2Arg = arg2;
      }, randomArg2);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(elements[0], elementArg);
      Assert.AreEqual(randomArg2, arg2Arg);
   }

   [Test]
   public void TwoArgForEach_TwoElements_CallsActionTwice()
   {
      int numberOfCalls = 0;
      var elementArgs = new Collection<int>();
      var extraArgArgs = new Collection<string>();
      int[] elements = new int[] { TestRandom.Int(), TestRandom.Int() };
      string randomArg2 = TestRandom.String();
      //
      _linqHelper.TwoArgForEach(elements, (int element, string arg2) =>
      {
         ++numberOfCalls;
         elementArgs.Add(element);
         extraArgArgs.Add(arg2);
      }, randomArg2);
      //
      Assert.AreEqual(2, numberOfCalls);
      Assert.AreEqual(new Collection<int> { elements[0], elements[1] }, elementArgs);
      Assert.AreEqual(new Collection<string> { randomArg2, randomArg2 }, extraArgArgs);
   }

   [Test]
   public void TwoArgForEachWithIndex_ZeroElements_CallsAction0Times()
   {
      void throwIfCalled(int element, int arg2, int elementIndex)
      {
         throw new Exception();
      };
      Assert.Throws<Exception>(() => throwIfCalled(0, 0, 0));
      _linqHelper.TwoArgForEachWithIndex(Array.Empty<int>(), throwIfCalled, 0);
   }

   [Test]
   public void TwoArgForEachWithIndex_OneElement_CallsActionOneTime()
   {
      int numberOfCalls = 0;
      int elementArg = 0;
      string arg2Arg = null;
      int[] elements = new int[] { TestRandom.Int() };
      string randomArg2 = TestRandom.String();
      int elementIndexArg = -1;
      //
      _linqHelper.TwoArgForEachWithIndex(elements, (int element, string arg2, int elementIndex) =>
      {
         ++numberOfCalls;
         elementArg = element;
         arg2Arg = arg2;
         elementIndexArg = elementIndex;
      }, randomArg2);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(elements[0], elementArg);
      Assert.AreEqual(randomArg2, arg2Arg);
      Assert.AreEqual(0, elementIndexArg);
   }

   [Test]
   public void TwoArgForEachWithIndex_TwoElements_CallsActionTwice()
   {
      int numberOfCalls = 0;
      var elementArgs = new Collection<int>();
      var extraArgArgs = new Collection<string>();
      int[] elements = new int[] { TestRandom.Int(), TestRandom.Int() };
      string randomArg2 = TestRandom.String();
      var elementIndexArgs = new Collection<int>();
      //
      _linqHelper.TwoArgForEachWithIndex(elements, (int element, string arg2, int elementIndex) =>
      {
         ++numberOfCalls;
         elementArgs.Add(element);
         extraArgArgs.Add(arg2);
         elementIndexArgs.Add(elementIndex);
      }, randomArg2);
      //
      Assert.AreEqual(2, numberOfCalls);
      Assert.AreEqual(new Collection<int> { elements[0], elements[1] }, elementArgs);
      Assert.AreEqual(new Collection<string> { randomArg2, randomArg2 }, extraArgArgs);
      Assert.AreEqual(new Collection<int> { 0, 1 }, elementIndexArgs);
   }

   [Test]
   public void TwoArgParallelForEach_ZeroElements_CallsAction0Times()
   {
      void throwIfCalled(int element, int arg2)
      {
         throw new Exception();
      };
      Assert.Throws<Exception>(() => throwIfCalled(0, 0));
      _linqHelper.TwoArgParallelForEach(Array.Empty<int>(), throwIfCalled, 0);
   }

   [Test]
   public void TwoArgParallelForEach_OneElement_CallsActionOneTime()
   {
      int numberOfCalls = 0;
      int elementArg = 0;
      string arg2Arg = null;
      int[] elements = new int[] { TestRandom.Int() };
      string randomArg2 = TestRandom.String();
      //
      _linqHelper.TwoArgParallelForEach(elements, (int element, string arg2) =>
      {
         ++numberOfCalls;
         elementArg = element;
         arg2Arg = arg2;
      }, randomArg2);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(elements[0], elementArg);
      Assert.AreEqual(randomArg2, arg2Arg);
   }

   [Test]
   public void TwoArgParallelForEach_TwoElements_CallsActionTwice()
   {
      var functionCallArguments = new System.Collections.Concurrent.ConcurrentBag<Tuple<int, string>>();
      int[] elements = new int[] { TestRandom.Int(), TestRandom.Int() };
      string extraArg1 = TestRandom.String();
      //
      _linqHelper.TwoArgParallelForEach(elements,
         (int element, string extraArg1Arg) =>
         {
            functionCallArguments.Add(new Tuple<int, string>(element, extraArg1Arg));
         }, extraArg1);
      //
      var expectedFunctionCallArguments = new System.Collections.Concurrent.ConcurrentBag<Tuple<int, string>>()
      {
         new Tuple<int, string>(elements[0], extraArg1),
         new Tuple<int, string>(elements[1], extraArg1)
      };
      CollectionAssert.AreEquivalent(expectedFunctionCallArguments, functionCallArguments);
   }

   [Test]
   public void TwoArgParallelForEachWithIndex_ZeroElements_CallsAction0Times()
   {
      void throwIfCalled(int element, int arg2, int elementIndex)
      {
         throw new Exception();
      };
      Assert.Throws<Exception>(() => throwIfCalled(0, 0, 0));
      _linqHelper.TwoArgParallelForEachWithIndex(Array.Empty<int>(), throwIfCalled, 0);
   }

   [Test]
   public void TwoArgParallelForEachWithIndex_OneElement_CallsActionOneTime()
   {
      int numberOfCalls = 0;
      int elementArg = 0;
      string arg2Arg = null;
      int[] elements = new int[] { TestRandom.Int() };
      string randomArg2 = TestRandom.String();
      int elementIndexArg = -1;
      //
      _linqHelper.TwoArgParallelForEachWithIndex(elements, (int element, string arg2, int elementIndex) =>
      {
         ++numberOfCalls;
         elementArg = element;
         arg2Arg = arg2;
         elementIndexArg = elementIndex;
      }, randomArg2);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(elements[0], elementArg);
      Assert.AreEqual(randomArg2, arg2Arg);
      Assert.AreEqual(0, elementIndexArg);
   }

   [Test]
   public void TwoArgParallelForEachWithIndex_TwoElements_CallsActionTwice()
   {
      var functionCallArguments = new System.Collections.Concurrent.ConcurrentBag<Tuple<int, string, int>>();
      int[] elements = new int[] { TestRandom.Int(), TestRandom.Int() };
      string extraArg1 = TestRandom.String();
      //
      _linqHelper.TwoArgParallelForEachWithIndex(elements,
         (int element, string extraArg1Arg, int elementIndex) =>
         {
            functionCallArguments.Add(new Tuple<int, string, int>(element, extraArg1Arg, elementIndex));
         }, extraArg1);
      //
      var expectedFunctionCallArguments = new System.Collections.Concurrent.ConcurrentBag<Tuple<int, string, int>>()
      {
         new Tuple<int, string, int>(elements[0], extraArg1, 0),
         new Tuple<int, string, int>(elements[1], extraArg1, 1)
      };
      CollectionAssert.AreEquivalent(expectedFunctionCallArguments, functionCallArguments);
   }

   [Test]
   public void ThreeArgParallelForEachWithIndex_ZeroElements_CallsAction0Times()
   {
      void throwIfCalled(int element, int arg1, int arg3, int elementIndex)
      {
         throw new Exception();
      };
      Assert.Throws<Exception>(() => throwIfCalled(0, 0, 0, 0));
      _linqHelper.ThreeArgParallelForEachWithIndex(Array.Empty<int>(), throwIfCalled, 0, 0);
   }

   [Test]
   public void ThreeArgParallelForEachWithIndex_OneElement_CallsActionOneTime()
   {
      int numberOfCalls = 0;
      int elementArg = 0;
      string arg1Arg = null;
      string arg2Arg = null;
      int[] elements = new int[] { TestRandom.Int() };
      string randomArg1 = TestRandom.String();
      string randomArg2 = TestRandom.String();
      int elementIndexArg = -1;
      //
      _linqHelper.ThreeArgParallelForEachWithIndex(elements, (int element, string arg1, string arg2, int elementIndex) =>
      {
         ++numberOfCalls;
         elementArg = element;
         arg1Arg = arg1;
         arg2Arg = arg2;
         elementIndexArg = elementIndex;
      }, randomArg1, randomArg2);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(elements[0], elementArg);
      Assert.AreEqual(randomArg1, arg1Arg);
      Assert.AreEqual(randomArg2, arg2Arg);
      Assert.AreEqual(0, elementIndexArg);
   }

   [Test]
   public void ThreeArgParallelForEachWithIndex_TwoElements_CallsActionTwice()
   {
      var functionCallArguments = new System.Collections.Concurrent.ConcurrentBag<Tuple<int, string, string, int>>();
      int[] elements = new int[] { TestRandom.Int(), TestRandom.Int() };
      string arg1 = TestRandom.String();
      string arg2 = TestRandom.String();
      //
      _linqHelper.ThreeArgParallelForEachWithIndex(elements,
         (int element, string arg1Arg, string arg2Arg, int elementIndex) =>
         {
            functionCallArguments.Add(new Tuple<int, string, string, int>(element, arg1Arg, arg2Arg, elementIndex));
         }, arg1, arg2);
      //
      var expectedFunctionCallArguments = new System.Collections.Concurrent.ConcurrentBag<Tuple<int, string, string, int>>()
      {
         new Tuple<int, string, string, int>(elements[0], arg1, arg2, 0),
         new Tuple<int, string, string, int>(elements[1], arg1, arg2, 1)
      };
      CollectionAssert.AreEquivalent(expectedFunctionCallArguments, functionCallArguments);
   }

   [Test]
   public void ThreeArgForEachWithIndex_ZeroElements_CallsAction0Times()
   {
      void throwIfCalled(int element, int arg1, int arg2, int elementIndex)
      {
         throw new Exception();
      };
      Assert.Throws<Exception>(() => throwIfCalled(0, 0, 0, 0));
      _linqHelper.ThreeArgForEachWithIndex(Array.Empty<int>(), throwIfCalled, 0, 0);
   }

   [Test]
   public void ThreeArgForEachWithIndex_OneElement_CallsActionOneTime()
   {
      int numberOfCalls = 0;
      int elementArg = 0;
      string arg1Arg = null;
      string arg2Arg = null;
      int[] elements = new int[] { TestRandom.Int() };
      string randomArg1 = TestRandom.String();
      string randomArg2 = TestRandom.String();
      int elementIndexArg = -1;
      //
      _linqHelper.ThreeArgForEachWithIndex(elements, (int element, string arg1, string arg2, int elementIndex) =>
      {
         ++numberOfCalls;
         elementArg = element;
         arg1Arg = arg1;
         arg2Arg = arg2;
         elementIndexArg = elementIndex;
      }, randomArg1, randomArg2);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(elements[0], elementArg);
      Assert.AreEqual(randomArg1, arg1Arg);
      Assert.AreEqual(randomArg2, arg2Arg);
      Assert.AreEqual(0, elementIndexArg);
   }

   [Test]
   public void ThreeArgForEachWithIndex_TwoElements_CallsActionTwice()
   {
      int numberOfCalls = 0;
      var elementArgs = new Collection<int>();
      var arg1Args = new Collection<string>();
      var arg2Args = new Collection<string>();
      int[] elements = new int[] { TestRandom.Int(), TestRandom.Int() };
      string randomArg1 = TestRandom.String();
      string randomArg2 = TestRandom.String();
      var elementIndexArgs = new Collection<int>();
      //
      _linqHelper.ThreeArgForEachWithIndex(elements, (int element, string arg1, string arg2, int elementIndex) =>
      {
         ++numberOfCalls;
         elementArgs.Add(element);
         arg1Args.Add(arg1);
         arg2Args.Add(arg2);
         elementIndexArgs.Add(elementIndex);
      }, randomArg1, randomArg2);
      //
      Assert.AreEqual(2, numberOfCalls);
      Assert.AreEqual(new Collection<int> { elements[0], elements[1] }, elementArgs);
      Assert.AreEqual(new Collection<string> { randomArg1, randomArg1 }, arg1Args);
      Assert.AreEqual(new Collection<string> { randomArg2, randomArg2 }, arg2Args);
      Assert.AreEqual(new Collection<int> { 0, 1 }, elementIndexArgs);
   }

   [Test]
   public void ThreeArgForEach_ZeroElements_CallsAction0Times()
   {
      void throwIfCalled(int element, int extraArg1, int extraArg2)
      {
         throw new Exception();
      };
      Assert.Throws<Exception>(() => throwIfCalled(0, 0, 0));
      _linqHelper.ThreeArgForEach(Array.Empty<int>(), throwIfCalled, 0, 0);
   }

   [Test]
   public void ThreeArgForEach_OneElement_CallsActionOneTime()
   {
      int numberOfCalls = 0;
      int elementArg = 0;
      string extraArg1Arg = null;
      string extraArg2Arg = null;
      int[] elements = new int[] { TestRandom.Int() };
      string randomExtraArg1 = TestRandom.String();
      string randomExtraArg2 = TestRandom.String();
      //
      _linqHelper.ThreeArgForEach(elements,
      (int element, string extraArg1, string extraArg2) =>
      {
         ++numberOfCalls;
         elementArg = element;
         extraArg1Arg = extraArg1;
         extraArg2Arg = extraArg2;
      }, randomExtraArg1, randomExtraArg2);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(elements[0], elementArg);
      Assert.AreEqual(randomExtraArg1, extraArg1Arg);
      Assert.AreEqual(randomExtraArg2, extraArg2Arg);
   }

   [Test]
   public void ThreeArgForEach_TwoElements_CallsActionTwice()
   {
      int numberOfCalls = 0;
      var elementArgs = new Collection<int>();
      var extraArg1Args = new Collection<string>();
      var extraArg2Args = new Collection<string>();
      int[] elements = new int[] { TestRandom.Int(), TestRandom.Int() };
      string randomExtraArg1 = TestRandom.String();
      string randomExtraArg2 = TestRandom.String();
      //
      _linqHelper.ThreeArgForEach(elements,
      (int element, string extraArg1, string extraArg2) =>
      {
         ++numberOfCalls;
         elementArgs.Add(element);
         extraArg1Args.Add(extraArg1);
         extraArg2Args.Add(extraArg2);
      }, randomExtraArg1, randomExtraArg2);
      //
      Assert.AreEqual(2, numberOfCalls);
      Assert.AreEqual(new Collection<int>() { elements[0], elements[1] }, elementArgs);
      Assert.AreEqual(new Collection<string>() { randomExtraArg1, randomExtraArg1 }, extraArg1Args);
      Assert.AreEqual(new Collection<string>() { randomExtraArg2, randomExtraArg2 }, extraArg2Args);
   }

   [Test]
   public void ThreeArgParallelForEach_ZeroElements_CallsAction0Times()
   {
      void throwIfCalled(int element, int extraArg1, int extraArg2)
      {
         throw new Exception();
      };
      Assert.Throws<Exception>(() => throwIfCalled(0, 0, 0));
      _linqHelper.ThreeArgParallelForEach(Array.Empty<int>(), throwIfCalled, 0, 0);
   }

   [Test]
   public void ThreeArgParallelForEach_OneElement_CallsActionOneTime()
   {
      int numberOfCalls = 0;
      int elementArg = 0;
      string extraArg1Arg = null;
      string extraArg2Arg = null;
      int[] elements = new int[] { TestRandom.Int() };
      string randomExtraArg1 = TestRandom.String();
      string randomExtraArg2 = TestRandom.String();
      //
      _linqHelper.ThreeArgParallelForEach(elements,
      (int element, string extraArg1, string extraArg2) =>
      {
         ++numberOfCalls;
         elementArg = element;
         extraArg1Arg = extraArg1;
         extraArg2Arg = extraArg2;
      }, randomExtraArg1, randomExtraArg2);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(elements[0], elementArg);
      Assert.AreEqual(randomExtraArg1, extraArg1Arg);
      Assert.AreEqual(randomExtraArg2, extraArg2Arg);
   }

   [Test]
   public void ThreeArgParallelForEach_TwoElements_CallsActionTwice()
   {
      var functionCallArguments = new System.Collections.Concurrent.ConcurrentBag<Tuple<int, string, string>>();
      int[] elements = new int[] { TestRandom.Int(), TestRandom.Int() };
      string extraArg1 = TestRandom.String();
      string extraArg2 = TestRandom.String();
      //
      _linqHelper.ThreeArgParallelForEach(elements,
         (int element, string extraArg1Arg, string extraArg2Arg) =>
         {
            functionCallArguments.Add(new Tuple<int, string, string>(element, extraArg1Arg, extraArg2Arg));
         }, extraArg1, extraArg2);
      //
      var expectedFunctionCallArguments = new System.Collections.Concurrent.ConcurrentBag<Tuple<int, string, string>>()
      {
         new Tuple<int, string, string>(elements[0], extraArg1, extraArg2),
         new Tuple<int, string, string>(elements[1], extraArg1, extraArg2)
      };
      CollectionAssert.AreEquivalent(expectedFunctionCallArguments, functionCallArguments);
   }

   [Test]
   public void FourArgForEach_ZeroElements_CallsAction0Times()
   {
      void throwIfCalled(int element, int extraArg1, int extraArg2, int extraArg3)
      {
         throw new Exception();
      };
      Assert.Throws<Exception>(() => throwIfCalled(0, 0, 0, 0));
      _linqHelper.FourArgForEach(Array.Empty<int>(), throwIfCalled, 0, 0, 0);
   }

   [Test]
   public void FourArgForEach_OneElement_CallsActionOneTime()
   {
      int numberOfCalls = 0;
      int elementArg = 0;
      string extraArg1Arg = null;
      string extraArg2Arg = null;
      string extraArg3Arg = null;
      int[] elements = new int[] { TestRandom.Int() };
      string randomExtraArg1 = TestRandom.String();
      string randomExtraArg2 = TestRandom.String();
      string randomExtraArg3 = TestRandom.String();
      //
      _linqHelper.FourArgForEach(elements,
      (int element, string extraArg1, string extraArg2, string extraArg3) =>
      {
         ++numberOfCalls;
         elementArg = element;
         extraArg1Arg = extraArg1;
         extraArg2Arg = extraArg2;
         extraArg3Arg = extraArg3;
      }, randomExtraArg1, randomExtraArg2, randomExtraArg3);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(elements[0], elementArg);
      Assert.AreEqual(randomExtraArg1, extraArg1Arg);
      Assert.AreEqual(randomExtraArg2, extraArg2Arg);
      Assert.AreEqual(randomExtraArg3, extraArg3Arg);
   }

   [Test]
   public void FourArgForEach_TwoElements_CallsActionTwice()
   {
      int numberOfCalls = 0;
      var elementArgs = new Collection<int>();
      var extraArg1Args = new Collection<string>();
      var extraArg2Args = new Collection<string>();
      var extraArg3Args = new Collection<string>();
      int[] elements = new int[] { TestRandom.Int(), TestRandom.Int() };
      string randomExtraArg1 = TestRandom.String();
      string randomExtraArg2 = TestRandom.String();
      string randomExtraArg3 = TestRandom.String();
      //
      _linqHelper.FourArgForEach(elements,
      (int element, string extraArg1, string extraArg2, string extraArg3) =>
      {
         ++numberOfCalls;
         elementArgs.Add(element);
         extraArg1Args.Add(extraArg1);
         extraArg2Args.Add(extraArg2);
         extraArg3Args.Add(extraArg3);
      }, randomExtraArg1, randomExtraArg2, randomExtraArg3);
      //
      Assert.AreEqual(2, numberOfCalls);
      Assert.AreEqual(new Collection<int>() { elements[0], elements[1] }, elementArgs);
      Assert.AreEqual(new Collection<string>() { randomExtraArg1, randomExtraArg1 }, extraArg1Args);
      Assert.AreEqual(new Collection<string>() { randomExtraArg2, randomExtraArg2 }, extraArg2Args);
      Assert.AreEqual(new Collection<string>() { randomExtraArg3, randomExtraArg3 }, extraArg3Args);
   }

   [Test]
   public void FourArgParallelForEach_ZeroElements_CallsAction0Times()
   {
      void throwIfCalled(int element, int extraArg1, int extraArg2, int extraArg3)
      {
         throw new Exception();
      };
      Assert.Throws<Exception>(() => throwIfCalled(0, 0, 0, 0));
      _linqHelper.FourArgParallelForEach(Array.Empty<int>(), throwIfCalled, 0, 0, 0);
   }

   [Test]
   public void FourArgParallelForEach_OneElement_CallsActionOneTime()
   {
      int numberOfCalls = 0;
      int elementArg = 0;
      string extraArg1Arg = null;
      string extraArg2Arg = null;
      string extraArg3Arg = null;
      int[] elements = new int[] { TestRandom.Int() };
      string randomExtraArg1 = TestRandom.String();
      string randomExtraArg2 = TestRandom.String();
      string randomExtraArg3 = TestRandom.String();
      //
      _linqHelper.FourArgParallelForEach(elements,
      (int element, string extraArg1, string extraArg2, string extraArg3) =>
      {
         ++numberOfCalls;
         elementArg = element;
         extraArg1Arg = extraArg1;
         extraArg2Arg = extraArg2;
         extraArg3Arg = extraArg3;
      }, randomExtraArg1, randomExtraArg2, randomExtraArg3);
      //
      Assert.AreEqual(1, numberOfCalls);
      Assert.AreEqual(elements[0], elementArg);
      Assert.AreEqual(randomExtraArg1, extraArg1Arg);
      Assert.AreEqual(randomExtraArg2, extraArg2Arg);
      Assert.AreEqual(randomExtraArg3, extraArg3Arg);
   }

   [Test]
   public void FourArgParallelForEach_TwoElements_CallsActionTwice()
   {
      var functionCallArguments = new System.Collections.Concurrent.ConcurrentBag<Tuple<int, string, string, string>>();
      int[] elements = new int[] { TestRandom.Int(), TestRandom.Int() };
      string extraArg1 = TestRandom.String();
      string extraArg2 = TestRandom.String();
      string extraArg3 = TestRandom.String();
      //
      _linqHelper.FourArgParallelForEach(elements,
         (int element, string extraArg1Arg, string extraArg2Arg, string extraArg3Arg) =>
         {
            functionCallArguments.Add(new Tuple<int, string, string, string>(element, extraArg1Arg, extraArg2Arg, extraArg3Arg));
         }, extraArg1, extraArg2, extraArg3);
      //
      Assert.AreEqual(2, functionCallArguments.Count);
      var expectedFunctionCallArguments = new System.Collections.Concurrent.ConcurrentBag<Tuple<int, string, string, string>>()
      {
         new Tuple<int, string, string, string>(elements[0], extraArg1, extraArg2, extraArg3),
         new Tuple<int, string, string, string>(elements[1], extraArg1, extraArg2, extraArg3)
      };
      CollectionAssert.AreEquivalent(expectedFunctionCallArguments, functionCallArguments);
   }

   // Transform

   [Test]
   public void Transform_ReturnsTransformedElementsAsReadOnlyCollection()
   {
      char[] elements = { 'a', 'b', 'c' };
      //
      ReadOnlyCollection<char> transformedElements = _linqHelper.Transform(elements, (char c) => char.ToUpper(c));
      //
      Assert.AreEqual(new char[] { 'A', 'B', 'C' }, transformedElements);
   }

   public static int PlusIndex(int x, int index)
   {
      return x + index;
   }

   [Test]
   public void TransformWithIndex_CallsTransformFunctionWithEachElementAndIndex_ReturnsTransformedElements()
   {
      ReadOnlyCollection<int> elements = new int[] { TestRandom.Int(), TestRandom.Int(), TestRandom.Int() }.ToReadOnlyCollection();
      //
      ReadOnlyCollection<int> transformedElements = _linqHelper.TransformWithIndex(elements, PlusIndex);
      //
      ReadOnlyCollection<int> expectedTransformedElements = new int[]
      {
         elements[0] + 0,
         elements[1] + 1,
         elements[2] + 2
      }.ToReadOnlyCollection();
      Assert.AreEqual(expectedTransformedElements, transformedElements);
   }

   [Test]
   public void TwoArgTransform_ReturnsTransformedElementsAsReadonlyCollection()
   {
      string receivedExtraArg = null;
      string arg2Value = TestRandom.String();
      int[] elements = { 1, 2, 3 };
      //
      ReadOnlyCollection<int> transformedElements = _linqHelper.TwoArgTransform(elements,
          (int element, string arg2) =>
          {
             receivedExtraArg = arg2;
             return element + 1;
          }, arg2Value);
      //
      Assert.AreEqual(receivedExtraArg, arg2Value);
      ReadOnlyCollection<int> expectedTransformedElements = new[] { 2, 3, 4 }.ToReadOnlyCollection();
      Assert.AreEqual(expectedTransformedElements, transformedElements);
   }

   [Test]
   public void ThreeArgTransform_ReturnsTransformedElementsAsReadOnlyCollection()
   {
      string receivedFirstExtraArg = null;
      string receivedSecondExtraArg = null;
      string firstExtraArgValue = TestRandom.String();
      string secondExtraArgValue = TestRandom.String();
      ReadOnlyCollection<int> elements = new[] { 1, 2, 3 }.ToReadOnlyCollection();
      //
      ReadOnlyCollection<int> transformedElements = _linqHelper.ThreeArgTransform(elements,
          (int element, string firstExtraArg, string secondExtraArg) =>
          {
             receivedFirstExtraArg = firstExtraArg;
             receivedSecondExtraArg = secondExtraArg;
             return element + 1;
          }, firstExtraArgValue, secondExtraArgValue);
      //
      Assert.AreEqual(receivedFirstExtraArg, firstExtraArgValue);
      Assert.AreEqual(receivedSecondExtraArg, secondExtraArgValue);
      ReadOnlyCollection<int> expectedTransformedElements = new[] { 2, 3, 4 }.ToReadOnlyCollection();
      Assert.AreEqual(expectedTransformedElements, transformedElements);
   }

   [Test]
   public void FourArgTransform_ReturnsTransformedElementsAsReadOnlyCollection()
   {
      string receivedFirstExtraArg = null;
      string receivedSecondExtraArg = null;
      string receivedThirdExtraArg = null;
      string firstExtraArgValue = TestRandom.String();
      string secondExtraArgValue = TestRandom.String();
      string thirdExtraArgValue = TestRandom.String();
      ReadOnlyCollection<int> elements = new[] { 1, 2, 3 }.ToReadOnlyCollection();
      //
      ReadOnlyCollection<int> transformedElements = _linqHelper.FourArgTransform(elements,
          (int element, string firstExtraArg, string secondExtraArg, string thirdExtraArg) =>
          {
             receivedFirstExtraArg = firstExtraArg;
             receivedSecondExtraArg = secondExtraArg;
             receivedThirdExtraArg = thirdExtraArg;
             return element + 1;
          }, firstExtraArgValue, secondExtraArgValue, thirdExtraArgValue);
      //
      Assert.AreEqual(receivedFirstExtraArg, firstExtraArgValue);
      Assert.AreEqual(receivedSecondExtraArg, secondExtraArgValue);
      Assert.AreEqual(receivedThirdExtraArg, thirdExtraArgValue);
      ReadOnlyCollection<int> expectedTransformedElements = new[] { 2, 3, 4 }.ToReadOnlyCollection();
      Assert.AreEqual(expectedTransformedElements, transformedElements);
   }

   [Test]
   public void TransformNonNull_ReturnsTransformedElementsAsArrayMinusAnyNulls()
   {
      string[] elements = { "a", null, "b", "c", null };
      //
      ReadOnlyCollection<string> transformedElements = _linqHelper.TransformNonNull(
          elements, (string s) =>
          {
             if (s != null)
             {
                return s.ToUpper();
             }
             else
             {
                return null;
             }
          });
      //
      var expectedTransformedElements = new ReadOnlyCollection<string>(new string[] { "A", "B", "C" });
      Assert.AreEqual(expectedTransformedElements, transformedElements);
   }

   [Test]
   public static void ElementIsNotNull_ReturnsTrueeIfElementIsNotNull()
   {
      string nonNullString = TestRandom.String();
      Assert.IsTrue(LinqHelper.ElementIsNotNull(nonNullString));

      Exception nonNullException = new Exception();
      Assert.IsTrue(LinqHelper.ElementIsNotNull(nonNullException));

      string nullString = null;
      Assert.IsFalse(LinqHelper.ElementIsNotNull(nullString));

      Exception nullException = null;
      Assert.IsFalse(LinqHelper.ElementIsNotNull(nullException));
   }

   [Test]
   public void Where_ReturnsMatchingElements()
   {
      int[] elements = new[] { 1, 2, 3, 4, 5 };
      //
      ReadOnlyCollection<int> matchingElements = _linqHelper.Where(elements, (int i) => i % 2 == 0);
      //
      Assert.AreEqual(new[] { 2, 4 }.ToReadOnlyCollection(), matchingElements);
   }

   [Test]
   public void WhereNot_ReturnsNonMatchingElements()
   {
      int[] elements = new[] { 1, 2, 3, 4, 5 };
      //
      ReadOnlyCollection<int> nonMatchingElements = _linqHelper.WhereNot(elements, (int i) => i % 2 == 0);
      //
      Assert.AreEqual(new[] { 1, 3, 5 }.ToReadOnlyCollection(), nonMatchingElements);
   }

   [Test]
   public void TwoArgWhere_EmptyElements_CallsPredicateZeroTimes_ReturnsEmptyElements()
   {
      int[] elements = Array.Empty<int>();
      var predicate = new Func<int, string, bool>((int element, string arg2) =>
      {
         throw new InvalidOperationException();
      });
      //
      ReadOnlyCollection<int> matchingElements = _linqHelper.TwoArgWhere(elements, predicate, TestRandom.String());
      //
      Assert.AreEqual(0, matchingElements.Count);

      // 100% code coverage
      Assert2.Throws<InvalidOperationException>(() => predicate(0, null),
          "Operation is not valid due to the current state of the object.");
   }

   [Test]
   public void TwoArgWhere_OneElement_PredicateDoesNotMatch_ReturnsEmptyElements()
   {
      string[] elements = new[] { TestRandom.String() };
      int arg2Value = TestRandom.Int();
      var elementArgs = new Collection<string>();
      var extraArgArgs = new Collection<int>();
      //
      ReadOnlyCollection<string> matchingElements = _linqHelper.TwoArgWhere(
          elements, (string element, int arg2) =>
          {
             elementArgs.Add(element);
             extraArgArgs.Add(arg2);
             return false;
          }, arg2Value);
      //
      Assert.AreEqual(new Collection<string> { elements[0] }, elementArgs);
      Assert.AreEqual(new Collection<int> { arg2Value }, extraArgArgs);
      Assert.AreEqual(0, matchingElements.Count);
   }

   [Test]
   public void TwoArgWhere_OneElement_PredicateMatches_ReturnsTheElement()
   {
      string[] elements = new[] { TestRandom.String() };
      int arg2Value = TestRandom.Int();
      var elementArgs = new Collection<string>();
      var extraArgArgs = new Collection<int>();
      //
      ReadOnlyCollection<string> matchingElements = _linqHelper.TwoArgWhere(
         elements, (string element, int arg2) =>
         {
            elementArgs.Add(element);
            extraArgArgs.Add(arg2);
            return true;
         }, arg2Value);
      //
      Assert.AreEqual(elements, elementArgs);
      Assert.AreEqual(new Collection<int> { arg2Value }, extraArgArgs);
      Assert.AreEqual(new string[] { elements[0] }, matchingElements);
   }

   [Test]
   public void TwoArgWhere_FourElements_TwoMatch_ReturnsTheTwoMatchingElements()
   {
      int[] elements = new[] { 1, 2, 3, 4 };
      string arg2Value = TestRandom.String();
      var elementArgs = new Collection<int>();
      var extraArgArgs = new Collection<string>();
      //
      ReadOnlyCollection<int> matchingElements = _linqHelper.TwoArgWhere(
         elements, (int element, string arg2) =>
         {
            elementArgs.Add(element);
            extraArgArgs.Add(arg2);
            bool doIncludeElement = element % 2 == 0;
            return doIncludeElement;
         }, arg2Value);
      //
      Assert.AreEqual(elements, elementArgs);
      Assert.AreEqual(new Collection<string> {
            arg2Value, arg2Value, arg2Value, arg2Value}, extraArgArgs);
      Assert.AreEqual(new int[] { 2, 4 }, matchingElements);
   }

   [Test]
   public void TwoArgWhereNot_EmptyElements_CallsPredicateZeroTimes_ReturnsEmptyElements()
   {
      int[] elements = Array.Empty<int>();
      var predicate = new Func<int, string, bool>((int element, string arg2) =>
      {
         throw new InvalidOperationException();
      });
      //
      ReadOnlyCollection<int> nonMatchingElements = _linqHelper.TwoArgWhereNot(elements, predicate, TestRandom.String());
      //
      Assert.AreEqual(0, nonMatchingElements.Count);

      // 100% code coverage
      Assert2.Throws<InvalidOperationException>(() => predicate(0, null),
          "Operation is not valid due to the current state of the object.");
   }

   [Test]
   public void TwoArgWhereNot_OneElement_PredicateDoesNotMatch_ReturnsElement()
   {
      string[] elements = new[] { TestRandom.String() };
      int arg2Value = TestRandom.Int();
      var elementArgs = new Collection<string>();
      var extraArgArgs = new Collection<int>();
      //
      ReadOnlyCollection<string> nonMatchingElements = _linqHelper.TwoArgWhereNot(
          elements, (string element, int arg2) =>
          {
             elementArgs.Add(element);
             extraArgArgs.Add(arg2);
             return false;
          }, arg2Value);
      //
      Assert.AreEqual(new Collection<string> { elements[0] }, elementArgs);
      Assert.AreEqual(new Collection<int> { arg2Value }, extraArgArgs);
      Assert.AreEqual(new Collection<string> { elements[0] }, nonMatchingElements);
   }

   [Test]
   public void TwoArgWhereNot_OneElement_PredicateMatches_ReturnsEmptyElements()
   {
      string[] elements = new[] { TestRandom.String() };
      int arg2Value = TestRandom.Int();
      var elementArgs = new Collection<string>();
      var extraArgArgs = new Collection<int>();
      //
      ReadOnlyCollection<string> nonMatchingElements = _linqHelper.TwoArgWhereNot(
         elements, (string element, int arg2) =>
         {
            elementArgs.Add(element);
            extraArgArgs.Add(arg2);
            return true;
         }, arg2Value);
      //
      Assert.AreEqual(elements, elementArgs);
      Assert.IsEmpty(nonMatchingElements);
   }

   [Test]
   public void TwoArgWhereNot_FourElements_TwoMatch_ReturnsTheTwoNonMatchingElements()
   {
      int[] elements = new[] { 1, 2, 3, 4 };
      string arg2Value = TestRandom.String();
      var elementArgs = new Collection<int>();
      var extraArgArgs = new Collection<string>();
      //
      ReadOnlyCollection<int> nonMatchingElements = _linqHelper.TwoArgWhereNot(
         elements, (int element, string arg2) =>
         {
            elementArgs.Add(element);
            extraArgArgs.Add(arg2);
            bool doIncludeElement = element % 2 == 0;
            return doIncludeElement;
         }, arg2Value);
      //
      Assert.AreEqual(elements, elementArgs);
      Assert.AreEqual(new Collection<string> { arg2Value, arg2Value, arg2Value, arg2Value }, extraArgArgs);
      Assert.AreEqual(new int[] { 1, 3 }, nonMatchingElements);
   }

   [Test]
   public void All_ReturnsTrueIfAllElementsMatchPredicate__AllDoNotMatchTestCase()
   {
      int[] elements = { 2, 4, 6, 8 };
      //
      bool allElementsMatch = _linqHelper.All(elements, (int i) =>
      {
         return i % 2 != 0;
      });
      //
      Assert.IsFalse(allElementsMatch);
   }

   [Test]
   public void All_ReturnsTrueIfAllElementsMatchPredicate__AllDoMatchTestCase()
   {
      int[] elements = { 2, 4, 6, 8 };
      //
      bool allElementsMatch = _linqHelper.All(elements, (int i) =>
      {
         return i % 2 == 0;
      });
      //
      Assert.IsTrue(allElementsMatch);
   }

   [Test]
   public void Any_ReturnsTrueIfAnyElementMatchesPredicate__TestCase1()
   {
      int[] elements = new[] { 1, 1, 1 };
      //
      bool anyElementIsEven = _linqHelper.Any(elements, x => x % 2 == 0);
      //
      Assert.IsFalse(anyElementIsEven);
   }

   [Test]
   public void Any_ReturnsTrueIfAnyElementMatchesPredicate__TestCase2()
   {
      int[] elements = new[] { 1, 1, 0 };
      //
      bool anyElementIsEven = _linqHelper.Any(elements, x => x % 2 == 0);
      //
      Assert.IsTrue(anyElementIsEven);
   }

   [Test]
   public void EmptyOrAny_ElementsAreEmpty_ReturnsTrue()
   {
      int[] emptyElements = Array.Empty<int>();
      //
      bool emptyOrAny = _linqHelper.EmptyOrAny(emptyElements, x => x % 2 == 0);
      //
      Assert.IsTrue(emptyOrAny);
   }

   [Test]
   public void EmptyOrAny_ElementsAreNotEmpty_ZeroElementsMatchPredicate_ReturnsFalse()
   {
      int[] emptyElements = new int[] { 1 };
      //
      bool emptyOrAny = _linqHelper.EmptyOrAny(emptyElements, x => x % 2 == 0);
      //
      Assert.IsFalse(emptyOrAny);
   }

   [Test]
   public void EmptyOrAny_ElementsAreNotEmpty_OneElementMatchesPredicate_ReturnsTrue()
   {
      int[] emptyElements = new int[] { 0, 1 };
      //
      bool emptyOrAny = _linqHelper.EmptyOrAny(emptyElements, x => x % 2 == 1);
      //
      Assert.IsTrue(emptyOrAny);
   }

   [Test]
   public void TwoArgAny_EmptySequence_ReturnsFalse()
   {
      int[] emptySequence = Array.Empty<int>();
      var predicate = new Func<int, char, bool>((int element, char arg2) => throw new Exception(""));
      //
      bool anyElementMatched = _linqHelper.TwoArgAny(emptySequence, predicate, 'a');
      //
      Assert.IsFalse(anyElementMatched);

      // 100% code coverage
      Assert2.Throws<Exception>(() => predicate(0, '\0'), "");
   }

   [TestCase(false, false)]
   [TestCase(true, true)]
   public void TwoArgAny_OneElementSequence_ReturnsTrueIfPredicateMatches(bool predicateReturnValue, bool expectedTwoArgAnyReturnValue)
   {
      int[] oneElementSequence = new int[] { 1 };
      var elementArgs = new Collection<int>();
      var extraArgArgs = new Collection<string>();
      string arg2Value = TestRandom.String();
      //
      bool anyElementMatched = _linqHelper.TwoArgAny(oneElementSequence,
         (int element, string arg2) =>
         {
            elementArgs.Add(element);
            extraArgArgs.Add(arg2);
            return predicateReturnValue;
         }, arg2Value);
      //
      Assert.AreEqual(new Collection<int> { 1 }, elementArgs);
      Assert.AreEqual(new Collection<string> { arg2Value }, extraArgArgs);
      Assert.AreEqual(expectedTwoArgAnyReturnValue, anyElementMatched);
   }

   [Test]
   public void TwoArgAny_ThreeElementSequence_SecondElementMatches_ReturnsTrue_CallsPredicateTwice()
   {
      string[] threeElementSequence = new string[] { "Apple", "Orange", "Banana" };
      var elementArgs = new Collection<string>();
      var extraArgArgs = new Collection<int>();
      int arg2Value = TestRandom.Int();
      //
      bool anyElementMatched = _linqHelper.TwoArgAny(threeElementSequence,
         (string element, int arg2) =>
         {
            elementArgs.Add(element);
            extraArgArgs.Add(arg2);
            return element == "Orange";
         }, arg2Value);
      //
      Assert.AreEqual(new Collection<string> { "Apple", "Orange" }, elementArgs);
      Assert.AreEqual(new Collection<int> { arg2Value, arg2Value }, extraArgArgs);
      Assert.IsTrue(anyElementMatched);
   }
}
