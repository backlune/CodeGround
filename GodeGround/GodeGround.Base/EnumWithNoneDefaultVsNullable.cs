using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GodeGround.Base
{
    public enum FruitsWithDefault
    {
        None = 0,
        Apple = 1,
        Pears = 2,
    }

    public enum Fruits
    {
        Apple = 0,
        Pears = 1,
    }

    public class Tree
    {
        public string Name { get; set; }

        public Nullable<Fruits> FruitType { get; set; }

        public FruitsWithDefault FruitType2 { get; set; }
    }

    
    public class EnumWithNoneDefault
    {

        [Fact]
        private void TestIfValueNotSet()
        {
            var tree = new Tree();
            var result = tree.FruitType2 == FruitsWithDefault.None;

            Assert.True(result);
        }

        [Fact]
        private void CheckSpecificValue()
        {
            var tree = new Tree() { FruitType2 = FruitsWithDefault.Apple };
            var result = tree.FruitType2 == FruitsWithDefault.Apple;

            Assert.True(result);
        }

    }

    public class EnumWithNullable
    {
        [Fact]
        private void TestIfValueNotSet()
        {
            var tree = new Tree();
            var result = !tree.FruitType.HasValue;

            Assert.True(result);

            var def = tree.FruitType.GetValueOrDefault();
            Assert.Equal(Fruits.Apple, def);
        }

        [Fact]
        private void CheckSpecificValue()
        {
            var tree = new Tree() { FruitType = Fruits.Apple };
            var result = tree.FruitType == Fruits.Apple;

            Assert.True(result);
        }
    }
}
