using CSharpUtils;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using System.Linq;

[TestFixture]
public class StringSplitterTests
{
   StringSplitter _stringSplitter;
   MethodCaller _methodCallerMock;

   [SetUp]
   public void SetUp()
   {
      _stringSplitter = new StringSplitter();
      _methodCallerMock = Mock.Component<MethodCaller>(_stringSplitter, "_methodCaller");
   }

   [Test]
   public void Split_EmptyString_ReturnsEmptyCollection()
   {
      ReadOnlyCollection<string> splitString = _stringSplitter.Split("", "");
      var expectedSplitString = new ReadOnlyCollection<string>(new[] { "" });
      Assert.AreEqual(expectedSplitString, splitString);
   }

   [Test]
   public void Split_NonEmptyString_InputDoesNotContainSeparator_ReturnsCollectionWithJustInputString()
   {
      ReadOnlyCollection<string> splitString = _stringSplitter.Split("abc", ",");
      var expectedSplitString = new ReadOnlyCollection<string>(new[] { "abc" });
      Assert.AreEqual(expectedSplitString, splitString);
   }

   [Test]
   public void Split_NonEmptyString_InputEqualsSeparator_ReturnsCollectionWithTwoEmptyStrings()
   {
      ReadOnlyCollection<string> splitString = _stringSplitter.Split(",", ",");
      var expectedSplitString = new ReadOnlyCollection<string>(new[] { "", "" });
      Assert.AreEqual(expectedSplitString, splitString);
   }

   [Test]
   public void Split_NonEmptyString_InputContainsSeparator_ReturnsCollectionWithSplitStringOnSeparator()
   {
      ReadOnlyCollection<string> splitString = _stringSplitter.Split("|a||b|c|", "|");
      var expectedSplitString = new ReadOnlyCollection<string>(new[] { "", "a", "", "b", "c", "" });
      Assert.AreEqual(expectedSplitString, splitString);
   }

   [TestCase(-1)]
   [TestCase(0)]
   [TestCase(1)]
   public void SplitLineWithRequiredNumberOfFields_NIs0Or1OrNegative_ThrowsArgumentException(int requiredNumberOfFields)
   {
      string str = TestRandom.String();
      string separator = TestRandom.String();
      Assert2.Throws<ArgumentException>(() => _stringSplitter.SplitLineWithRequiredNumberOfFields(str, separator, requiredNumberOfFields),
         "StringSplitter.SplitLineWithRequiredNumberOfFields(string str, string separator, int requiredNumberOfFields) called with requiredNumberOfFields <= 1: " + requiredNumberOfFields);
   }

   [TestCase(2)]
   [TestCase(3)]
   public void SplitLineWithRequiredNumberOfFields_NIs2OrGreater_SplitLineHasNNumberOfFields_ReturnsSplitLine(int requiredNumberOfFields)
   {
      ReadOnlyCollection<string> splitString = TestRandom.ReadOnlyStringCollectionWithCount(requiredNumberOfFields);
      Mock.Return(() => _methodCallerMock.CallFunction(default(Func<string, string, ReadOnlyCollection<string>>), null, null), splitString);

      string str = TestRandom.String();
      string separator = TestRandom.String();
      //
      ReadOnlyCollection<string> returnedSplitString = _stringSplitter.SplitLineWithRequiredNumberOfFields(str, separator, requiredNumberOfFields);
      //
      Called.Once(() => _methodCallerMock.CallFunction(_stringSplitter.Split, str, separator));
      Assert.AreEqual(splitString, returnedSplitString);
   }

   [TestCase(2, 1)]
   [TestCase(2, 3)]
   [TestCase(3, 1)]
   [TestCase(3, 2)]
   [TestCase(3, 4)]
   public void SplitLineWithRequiredNumberOfFields_NIs2OrGreater_SplitLineDoesNotHaveNNumberOfFields_ThrowsArgumentException(
      int requiredNumberOfFields, int splitNumberOfFields)
   {
      ReadOnlyCollection<string> splitString = TestRandom.ReadOnlyStringCollectionWithCount(splitNumberOfFields);
      Mock.Return(() => _methodCallerMock.CallFunction(default(Func<string, string, ReadOnlyCollection<string>>), null, null), splitString);

      string str = TestRandom.String();
      string separator = TestRandom.String();
      //
      string expectedExceptionMessage = $"StringSplitter.SplitLineWithRequiredNumberOfFields(string str, string separator, int requiredNumberOfFields) called with str when split does not have required requiredNumberOfFields number of fields. str=\"{str}\". separator=\"{separator}\". requiredNumberOfFields={requiredNumberOfFields}. splitString.Count={splitNumberOfFields}";
      Assert2.Throws<ArgumentException>(() => _stringSplitter.SplitLineWithRequiredNumberOfFields(str, separator, requiredNumberOfFields), expectedExceptionMessage);
      //
      Called.Once(() => _methodCallerMock.CallFunction(_stringSplitter.Split, str, separator));
   }

   [Test]
   public void SplitThenSkipFirstElement_CallsSplit_ReturnsSplitElementsWithFirstElementSkipped()
   {
      ReadOnlyCollection<string> splitString = Mock.ReturnRandomReadOnlyStringCollection(
         () => _methodCallerMock.CallFunction(default(Func<string, string, ReadOnlyCollection<string>>), default, default));

      string input = TestRandom.String();
      string separator = TestRandom.String();
      //
      ReadOnlyCollection<string> splitStringWithFirstElementSkipped = _stringSplitter.SplitThenSkipFirstElement(input, separator);
      //
      Called.Once(() => _methodCallerMock.CallFunction(_stringSplitter.Split, input, separator));
      ReadOnlyCollection<string> expectedSplitStringWithFirstElementSkipped = splitString.Skip(1).ToReadOnlyCollection();
      Assert.AreEqual(expectedSplitStringWithFirstElementSkipped, splitStringWithFirstElementSkipped);
   }
}
