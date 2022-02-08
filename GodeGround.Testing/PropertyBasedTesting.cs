using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace GodeGround.Testing
{
    public static class IntArrayExtensions
    {
        public static IEnumerable<int> Insert(this IEnumerable<int> cs, int x)
        {
            var result = new List<int>(cs);
            foreach (var c in cs)
            {
                if (x <= c)
                {
                    result.Insert(result.IndexOf(c), x);
                    return result;
                }
            }
            result.Add(x);
            return result;
        }

        ///<returns> true iff source is strictly decreasing </returns>
        public static bool IsOrdered<T>(this IEnumerable<T> source)
        {
            var comparer = Comparer<T>.Default;
            return source.Zip(source.Skip(1), (a, b) => comparer.Compare(a, b) > 0)
            .Aggregate((a, b) => a && b);
        }
    }

    public class PropertyBasedTesting
    {
        class Eq : IEqualityComparer<double>
        {
            public bool Equals(double x, double y)
            {
                return x == y;
            }

            public int GetHashCode(double obj)
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void TestSpecialSequenceEqual()
        {
            Configuration config = Configuration.VerboseThrowOnFailure;
            config.QuietOnSuccess = false;
            config.MaxNbOfTest = 50;
            //A simple example

            Prop.ForAll<double[]>(xs => xs.Reverse().Reverse().SequenceEqual(xs, new Eq()))
                .Check(config);
            //Grouping properties : not yet implemented
        }

        [Fact]
        private void TestDefaultSequenceEqual()
        {
            Prop.ForAll<double[]>(xs => xs.Reverse().Reverse().SequenceEqual(xs))
                .VerboseCheckThrowOnFailure();
        }

        [Fact]
        private void TestWithCondition()
        {
            Prop.ForAll<int, int[]>((x, xs) =>
                    xs.Insert(x).IsOrdered()
                    .When(xs.Length > 0 && xs.IsOrdered())
                .VerboseCheckThrowOnFailure());
        }

        public void Rest() {

            Prop.ForAll<int[]>(xs => xs.Reverse().SequenceEqual(xs))
                .QuickCheck("RevId");

            //--------Properties--------------
            //Default Comparison compares NaN properly
            Prop.ForAll<double[]>(xs => xs.Reverse().Reverse().SequenceEqual(xs))
                .QuickCheck("RevRevFloat");

            //conditional properties
            Prop.ForAll<int, int[]>((x, xs) => xs.Insert(x).IsOrdered().When(xs.IsOrdered()))
                .QuickCheck("Insert");

            Prop.ForAll<int>(a => Prop.When(a != 0, () => 1 / a == 1 / a))
                .QuickCheck("DivByZero");

            Prop.ForAll<short[]>(xs => Prop.When(condition: xs.Length > 0, assertion: () => xs.Length > 0))
                .QuickCheck("When non-empty array ==> Assert non-empty array");

            //counting trivial cases
            Prop.ForAll<int, int[]>((x, xs) =>
                    xs.Insert(x).IsOrdered()
                    .When(xs.IsOrdered())
                    .Classify(xs.Length == 0, "trivial"))
                .QuickCheck("InsertTrivial");

            //classifying test values
            Prop.ForAll<int, int[]>((x, xs) =>
                    xs.Insert(x).IsOrdered()
                    .When(xs.IsOrdered())
                    .Classify(new[] { x }.Concat(xs).IsOrdered(), "at-head")
                    .Classify(xs.Concat(new int[] { x }).IsOrdered(), "at-tail"))
                .QuickCheck("InsertClassify");

            //collecting data values
            Prop.ForAll<int, int[]>((x, xs) =>
                    xs.Insert(x).IsOrdered()
                    .When(xs.IsOrdered())
                    .Collect("length " + xs.Count().ToString()))
                .QuickCheck("InsertCollect");

            //combining observations
            Prop.ForAll<int, int[]>((x, xs) =>
                    xs.Insert(x).IsOrdered()
                    .When(xs.IsOrdered())
                    .Classify(new[] { x }.Concat(xs).IsOrdered(), "at-head")
                    .Classify(xs.Concat(new[] { x }).IsOrdered(), "at-tail")
                    .Collect("length " + xs.Count().ToString()))
                .QuickCheck("InsertCombined");

            //---labelling sub properties-----
            Prop.ForAll<int, int>((n, m) => {
                var res = n * m;
                return (new Func<bool>(() => res / m == n)).When(m != 0.0).Label("div1")
                   .And((new Func<bool>(() => res / n == m)).When(n != 0.0).Label("div2"))
                   .And((res > m).Label("lt1"))
                   .And((res > n).Label("lt2"))
                   .Label(string.Format("evidence = {0}", res));
            }).QuickCheck("Multiple labels");


            Prop.ForAll<int, int>((m, n) => {
                var result = m + n;
                return (result >= m).Label("result > #1")
                   .And(result >= n).Label("result > #2")
                   .And(result < m + n).Label("result not sum");
            }).QuickCheck("ComplexProp");

            Prop.ForAll<int>(x => false.Label("Always false")
                            .And(Math.Abs(x) - x == 0))
                .QuickCheck("Label");


            //-------Test data generators-----------
            //can't be made generic, only in separate method?
            Func<int[], Gen<int>> chooseFromList = xs =>
                 from i in Gen.Choose(0, xs.Length - 1)
                 select xs[i];

            var chooseBool = Gen.OneOf(Gen.Constant(true), Gen.Constant(false));

            var chooseBool2 = Gen.Frequency(
                new WeightAndValue<Gen<bool>>(2, Gen.Constant(true)),
                new WeightAndValue<Gen<bool>>(1, Gen.Constant(false)));

            //the size of test data : see matrix method

            //generating recursive data types: not so common in C#?

            // generating functions:
            Prop.ForAll((Func<int, int> f, Func<int, int> g, ICollection<int> a) => {
                var l1 = a.Select(x => f(g(x)));
                var l2 = a.Select(g).Select(f);
                return l1.SequenceEqual(l2);
            }).QuickCheck();

            //generators support select, selectmany and where
            var gen = from x in Arb.Generate<int>()
                      from int y in Gen.Choose(5, 10)
                      where x > 5
                      select new { Fst = x, Snd = y };

            //registering default arbitrary instances
            Arb.Register<MyArbitraries>();

            Prop.ForAll<long>(l => l + 1 > l)
                .QuickCheck();

            Prop.ForAll<string>(s => true)
                .Check(new Configuration { Name = "Configuration Demo", MaxNbOfTest = 500 });

            // discard

            Prop.ForAll<int>(s => s >= 4 || Prop.Discard<bool>()).QuickCheck();

            Console.WriteLine();

            // replay
            //Prop.ForAll<string>(s => s == null)
            //    .Label("Replay")
            //    .Check(new Configuration { MaxNbOfTest = 1, Replay = FsCheck.Random.StdGen.NewStdGen(1, 1) });

            Console.WriteLine();

            Prop.ForAll((IEnumerable<int> a, IEnumerable<int> b) =>
                            a.Except(b).Count() <= a.Count())
                .QuickCheck();

        }

        public static Gen<T> Matrix<T>(Gen<T> gen)
        {
            return Gen.Sized(s => gen.Resize(Convert.ToInt32(Math.Sqrt(s))));
        }

        public class ArbitraryLong : Arbitrary<long>
        {
            public override Gen<long> Generator
            {
                get
                {
                    return Gen.Sized(s => Gen.Choose(-s, s))
                        .Select(i => Convert.ToInt64(i));
                }
            }
        }


        public class MyArbitraries
        {
            public static Arbitrary<long> Long() { return new ArbitraryLong(); }

            public static Arbitrary<IEnumerable<T>> Enumerable<T>()
            {
                return Arb.Default.Array<T>().Convert(x => (IEnumerable<T>)x, x => (T[])x);
            }

            public static Arbitrary<StringBuilder> StringBuilder()
            {
                return Arb.Generate<string>().Select(x => new StringBuilder(x)).ToArbitrary();
            }
        }

    }


namespace FsCheck.XUnit.CSharpExamples
    {
        public class ReverseFixture
        {
            [Property(QuietOnSuccess = true)]
            public bool Bcl(int[] xs)
            {
                return xs.Reverse().Reverse().SequenceEqual(xs);
            }

            [Property(QuietOnSuccess = true)]
            public void Bcl2(int[] xs)
            {
                if (true == xs.Reverse().Reverse().SequenceEqual(xs))
                {
                    // all ok
                }
                else
                {
                    throw new Exception("Fail at life");
                }
            }

            [Property]
            public bool ShouldFail_1(int[] xs)
            {
                return xs.BadReverse1().SequenceEqual(xs.Reverse());
            }

            [Property]
            public bool ShouldFail_2(int[] xs)
            {
                return xs.BadReverse2().SequenceEqual(xs.Reverse());
            }

            [Property(MaxTest = 1000, EndSize = 5)]
            public bool ShouldFail_3(int[] xs)
            {
                return xs.BadReverse3().SequenceEqual(xs.Reverse());
            }

            [Property]
            public bool ShouldFail_Exception(int[] xs)
            {
                throw new InvalidOperationException("Test failed!");
            }
        }

        public static class ExtensionMethods
        {
            /// <summary> Never reverts the <paramref name="list"/> </summary>
            public static IEnumerable<TSource> BadReverse1<TSource>(this IEnumerable<TSource> list)
            {
                return list;
            }

            /// <summary> When the <paramref name="list"/> is longer than 10, doesn't revert it </summary>
            public static IEnumerable<TSource> BadReverse2<TSource>(this IEnumerable<TSource> list)
            {
                var badReverse2 = list as IList<TSource> ?? list.ToList();
                return badReverse2.Count > 10 ? badReverse2 : badReverse2.Reverse();
            }

            /// <summary> When the <paramref name="list"/> is accidently sorted, doesn't revert it! </summary>
            public static IEnumerable<TSource> BadReverse3<TSource>(this IEnumerable<TSource> list) where TSource : IEquatable<TSource>
            {
                var copy = list.ToList();
                var isOrdered = copy.SequenceEqual(copy.AsEnumerable().OrderBy(_ => _)); //O(nLog(n)) instead of O(n)
                return copy.Count >= 3 && isOrdered ? copy : copy.AsEnumerable().Reverse();
            }


        }
    }

}
