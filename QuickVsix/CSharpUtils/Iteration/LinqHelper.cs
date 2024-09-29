using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpUtils
{
   public class LinqHelper
   {
      // ForEach

      public virtual void ForEach<T>(IEnumerable<T> elements, Action<T> action)
      {
         foreach (T element in elements)
         {
            action(element);
         }
      }

      public virtual void TwoArgForEach<T, Arg2Type>(IList<T> elements, Action<T, Arg2Type> action, Arg2Type arg2)
      {
         foreach (T element in elements)
         {
            action(element, arg2);
         }
      }

      public virtual void TwoArgParallelForEach<T, Arg2Type>(IList<T> elements, Action<T, Arg2Type> action, Arg2Type arg2)
      {
         void boundAction(T element) => action(element, arg2);
         elements.AsParallel().ForAll(boundAction);
      }

      public virtual void TwoArgParallelForEachWithIndex<T, Arg2Type>(
         IList<T> elements,
         Action<T, Arg2Type, int> action,
         Arg2Type arg2)
      {
         var elementsWithIndexes = new Collection<Tuple<T, int>>();
         for (int i = 0; i < elements.Count; ++i)
         {
            T ithElement = elements[i];
            elementsWithIndexes.Add(Tuple.Create(ithElement, i));
         }
         void boundAction(Tuple<T, int> elementAndIndex)
         {
            action(elementAndIndex.Item1, arg2, elementAndIndex.Item2);
         }
         elementsWithIndexes.AsParallel().ForAll(boundAction);
      }

      public virtual void TwoArgForEachWithIndex<T, Arg2Type>(IList<T> elements, Action<T, Arg2Type, int> action, Arg2Type arg2)
      {
         for (int i = 0; i < elements.Count; ++i)
         {
            T ithElement = elements[i];
            action(ithElement, arg2, i);
         }
      }

      public virtual void ThreeArgParallelForEachWithIndex<T, Arg2Type, Arg3Type>(
         IList<T> elements, Action<T, Arg2Type, Arg3Type, int> action, Arg2Type arg2, Arg3Type arg3)
      {
         var elementsWithIndexes = new Collection<Tuple<T, int>>();
         for (int i = 0; i < elements.Count; ++i)
         {
            T ithElement = elements[i];
            elementsWithIndexes.Add(Tuple.Create(ithElement, i));
         }
         void boundAction(Tuple<T, int> elementAndIndex)
         {
            action(elementAndIndex.Item1, arg2, arg3, elementAndIndex.Item2);
         }
         elementsWithIndexes.AsParallel().ForAll(boundAction);
      }

      public virtual void ThreeArgForEachWithIndex<T, Arg2Type, Arg3Type>(
         IList<T> elements,
         Action<T, Arg2Type, Arg3Type, int> action,
         Arg2Type arg2,
         Arg3Type arg3)
      {
         for (int i = 0; i < elements.Count; ++i)
         {
            T ithElement = elements[i];
            action(ithElement, arg2, arg3, i);
         }
      }

      public virtual void ThreeArgForEach<T, Arg2Type, Arg3Type>(
         IEnumerable<T> elements, Action<T, Arg2Type, Arg3Type> action, Arg2Type arg2, Arg3Type arg3)
      {
         foreach (T element in elements)
         {
            action(element, arg2, arg3);
         }
      }

      public virtual void ThreeArgParallelForEach<T, Arg2Type, Arg3Type>(
         IEnumerable<T> elements,
         Action<T, Arg2Type, Arg3Type> action,
         Arg2Type arg2,
         Arg3Type arg3)
      {
         void boundAction(T element) => action(element, arg2, arg3);
         elements.AsParallel().ForAll(boundAction);
      }

      public virtual void FourArgForEach<T, Arg2Type, Arg3Type, Arg4Type>(
         IEnumerable<T> elements,
         Action<T, Arg2Type, Arg3Type, Arg4Type> action,
         Arg2Type arg2,
         Arg3Type arg3,
         Arg4Type arg4)
      {
         foreach (T element in elements)
         {
            action(element, arg2, arg3, arg4);
         }
      }

      public virtual void FourArgParallelForEach<T, Arg2Type, Arg3Type, Arg4Type>(
         IEnumerable<T> elements,
         Action<T, Arg2Type, Arg3Type, Arg4Type> action,
         Arg2Type arg2,
         Arg3Type arg3,
         Arg4Type arg4)
      {
         void boundAction(T element) => action(element, arg2, arg3, arg4);
         elements.AsParallel().ForAll(boundAction);
      }

      // Transform

      public virtual ReadOnlyCollection<ResultType> TwoArgSelectNonNullWithIndex<T, Arg2Type, ResultType>(
         IEnumerable<T> elements, Func<T, int, Arg2Type, ResultType> selector, Arg2Type arg2)
      {
         int index = 0;
         IEnumerable<ResultType> transformedElements = elements.Select((T element) =>
         {
            ResultType transformedElement = selector(element, index++, arg2);
            return transformedElement;
         });
         ReadOnlyCollection<ResultType> nonNullTransformedElements = transformedElements.Where(result => result != null).ToReadOnlyCollection();
         return nonNullTransformedElements;
      }

      public virtual ReadOnlyCollection<TransformedElementType> TwoArgTransformWithIndex<T, Arg2Type, TransformedElementType>(
         IList<T> elements,
         Func<T, Arg2Type, int, TransformedElementType> func,
         Arg2Type arg2)
      {
         TransformedElementType[] transformedElements = new TransformedElementType[elements.Count];
         for (int i = 0; i < elements.Count; ++i)
         {
            T ithElement = elements[i];
            TransformedElementType ithTransformedElement = func(ithElement, arg2, i);
            transformedElements[i] = ithTransformedElement;
         }
         return transformedElements.ToReadOnlyCollection();
      }

      public virtual ReadOnlyCollection<TransformedElementType> TwoArgParallelTransformWithIndex<ElementType, Arg2Type, TransformedElementType>(
         IList<ElementType> elements,
         Func<ElementType, Arg2Type, int, TransformedElementType> func,
         Arg2Type arg2)
      {
         var elementsWithIndices = new Tuple<ElementType, int>[elements.Count];
         for (int i = 0; i < elements.Count; ++i)
         {
            ElementType ithElement = elements[i];
            elementsWithIndices[i] = new Tuple<ElementType, int>(ithElement, i);
         }
         TransformedElementType boundFunction(Tuple<ElementType, int> elementAndIndex)
         {
            TransformedElementType transformedElement = func(elementAndIndex.Item1, arg2, elementAndIndex.Item2);
            return transformedElement;
         }
         ReadOnlyCollection<TransformedElementType> transformedElements = elementsWithIndices.AsParallel().Select(boundFunction).ToReadOnlyCollection();
         return transformedElements;
      }

      public virtual double TransformSum<T>(ReadOnlyCollection<T> elements, Func<T, double> transformer)
      {
         double sum = 0.0;
         foreach (T element in elements)
         {
            double value = transformer(element);
            sum += value;
         }
         return sum;
      }

      public virtual ReadOnlyCollection<ResultType> Transform<T, ResultType>(
         IEnumerable<T> elements, Func<T, ResultType> selector)
      {
         ReadOnlyCollection<ResultType> transformedElements = elements.Select(selector).ToReadOnlyCollection();
         return transformedElements;
      }

      public virtual ReadOnlyCollection<TransformedElementType> TransformWithIndex<T, TransformedElementType>(
         IList<T> elements, Func<T, int, TransformedElementType> func)
      {
         TransformedElementType[] transformedElements = new TransformedElementType[elements.Count];
         for (int i = 0; i < elements.Count; ++i)
         {
            T ithElement = elements[i];
            TransformedElementType ithTransformedElement = func(ithElement, i);
            transformedElements[i] = ithTransformedElement;
         }
         return transformedElements.ToReadOnlyCollection();
      }

      public virtual ReadOnlyCollection<ResultType> TwoArgTransform<T, Arg2Type, ResultType>(
         ICollection<T> elements, Func<T, Arg2Type, ResultType> selector, Arg2Type arg2)
      {
         ReadOnlyCollection<ResultType> readonlyTransformedElements = elements.Select((T element) =>
         {
            ResultType transformedElement = selector(element, arg2);
            return transformedElement;
         }).ToReadOnlyCollection();
         return readonlyTransformedElements;
      }

      public virtual ReadOnlyCollection<ResultType> ThreeArgTransform<T, FirstExtraArgType, SecondExtraArgType, ResultType>(
         ICollection<T> elements,
         Func<T, FirstExtraArgType, SecondExtraArgType, ResultType> selector,
         FirstExtraArgType firstExtraArg,
         SecondExtraArgType secondExtraArg)
      {
         ReadOnlyCollection<ResultType> readonlyTransformedElements = elements.Select((T element) =>
         {
            ResultType transformedElement = selector(element, firstExtraArg, secondExtraArg);
            return transformedElement;
         }).ToReadOnlyCollection();
         return readonlyTransformedElements;
      }

      public virtual ReadOnlyCollection<ResultType> FourArgTransform<T, FirstExtraArgType, SecondExtraArgType, ThirdExtraArgType, ResultType>(
         ICollection<T> elements,
         Func<T, FirstExtraArgType, SecondExtraArgType, ThirdExtraArgType, ResultType> selector,
         FirstExtraArgType firstExtraArg,
         SecondExtraArgType secondExtraArg,
         ThirdExtraArgType thirdExtraArg)
      {
         ReadOnlyCollection<ResultType> readonlyTransformedElements = elements.Select((T element) =>
         {
            ResultType transformedElement = selector(element, firstExtraArg, secondExtraArg, thirdExtraArg);
            return transformedElement;
         }).ToReadOnlyCollection();
         return readonlyTransformedElements;
      }

      public virtual ReadOnlyCollection<ResultType> TransformNonNull<T, ResultType>(
         IEnumerable<T> elements, Func<T, ResultType> selector)
      {
         ReadOnlyCollection<ResultType> nonNullTransformedElements = elements.Select(selector).Where(ElementIsNotNull).ToReadOnlyCollection();
         return nonNullTransformedElements;
      }

      // Where

      public virtual int Count<T>(IEnumerable<T> elements, Func<T, bool> predicate)
      {
         return elements.Count(predicate);
      }

      public static bool ElementIsNotNull<T>(T element)
      {
         bool elementIsNotNull = element != null;
         return elementIsNotNull;
      }

      public virtual ReadOnlyCollection<T> Where<T>(IEnumerable<T> elements, Func<T, bool> predicate)
      {
         ReadOnlyCollection<T> matchingElements = elements.Where(predicate).ToReadOnlyCollection();
         return matchingElements;
      }

      public virtual ReadOnlyCollection<T> WhereNot<T>(IEnumerable<T> elements, Func<T, bool> predicate)
      {
         bool notPredicate(T arg) => !predicate(arg);
         ReadOnlyCollection<T> nonMatchingElements = elements.Where(notPredicate).ToReadOnlyCollection();
         return nonMatchingElements;
      }

      public virtual ReadOnlyCollection<T> TwoArgWhere<T, Arg2Type>(
         IEnumerable<T> elements, Func<T, Arg2Type, bool> predicate, Arg2Type arg2)
      {
         ReadOnlyCollection<T> matchingElements = elements.Where((T element) =>
         {
            bool doIncludeElement = predicate(element, arg2);
            return doIncludeElement;
         }).ToReadOnlyCollection();
         return matchingElements;
      }

      public virtual ReadOnlyCollection<T> TwoArgWhereNot<T, Arg2Type>(
         IEnumerable<T> elements, Func<T, Arg2Type, bool> predicate, Arg2Type arg2)
      {
         ReadOnlyCollection<T> nonMatchingElements = elements.Where((T element) =>
         {
            bool doIncludeElement = !predicate(element, arg2);
            return doIncludeElement;
         }).ToReadOnlyCollection();
         return nonMatchingElements;
      }

      public virtual int IndexOfFirstElementThatStartsWithValue(IEnumerable<string> strings, string value)
      {
         int index = 0;
         foreach (string str in strings)
         {
            if (str != "" && value.StartsWith(str, StringComparison.Ordinal))
            {
               return index;
            }
            ++index;
         }
         throw new InvalidOperationException($"Zero elements found that start with value: {value}");
      }

      public virtual int IndexOfFirstMatchingElement<T>(IEnumerable<T> elements, T value)
      {
         int index = 0;
         foreach (T element in elements)
         {
            if (element.Equals(value))
            {
               return index;
            }
            ++index;
         }
         throw new InvalidOperationException($"Zero elements found that equal value: {value}");
      }

      public virtual bool All<T>(IEnumerable<T> elements, Func<T, bool> predicate)
      {
         return elements.All((T element) =>
         {
            bool elementMatchesPredicate = predicate(element);
            return elementMatchesPredicate;
         });
      }

      public virtual bool Any<T>(ICollection<T> elements, Func<T, bool> predicate)
      {
         bool anyElementMatchesPredicate = elements.Any(predicate);
         return anyElementMatchesPredicate;
      }

      public virtual bool EmptyOrAny<T>(ICollection<T> elements, Func<T, bool> predicate)
      {
         if (elements.Count == 0)
         {
            return true;
         }
         bool anyElementMatchesPredicate = elements.Any(predicate);
         return anyElementMatchesPredicate;
      }

      public virtual bool TwoArgAny<T, Arg2Type>(IEnumerable<T> elements, Func<T, Arg2Type, bool> predicate, Arg2Type arg2)
      {
         return elements.Any((T element) =>
         {
            bool elementMatchesPredicate = predicate(element, arg2);
            return elementMatchesPredicate;
         });
      }
   }
}
