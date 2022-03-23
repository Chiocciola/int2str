using System;
using Xunit;

namespace Int2Str.Test
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(0)]
        [InlineData(12)]
        [InlineData(123)]
        [InlineData(1234)]
        [InlineData(12345)]
        [InlineData(123456)]
        [InlineData(int.MaxValue)]
        public void Test1(int n)
        {
            Assert.Equal(n.ToString(), Int2Str.Int2Str_Naive(n));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(12)]
        [InlineData(123)]
        [InlineData(1234)]
        [InlineData(12345)]
        [InlineData(123456)]
        [InlineData(111_222_333_444_555_666)]
        [InlineData(long.MaxValue)]
        public void Test_Int2Str_BinSearch_Div10(long n)
        {
            Assert.Equal(n.ToString(), Int2Str.Int2Str_BinSearch_Div10(n));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(12)]
        [InlineData(123)]
        [InlineData(1234)]
        [InlineData(12345)]
        [InlineData(123456)]
        [InlineData(long.MaxValue)]
        public void Test_Int2Str_BinSearch_Div100(long n)
        {
            Assert.Equal(n.ToString(), Int2Str.Int2Str_BinSearch_Div100(n));
        }

        [Fact]
        public void Test_FindCharCount()
        {
            var p = (int)Math.Log10(long.MaxValue);

            Assert.Equal(18, p);

            for (int i = 0; i <= p; i++)
            {

                Assert.Equal(i+1, Int2Str.FindCharCount((long)Math.Pow(10, i)));
            }
        }

        [Fact]
        public void Test_FindCharCount1()
        {
            var p = (int)Math.Log10(ulong.MaxValue);

            Assert.Equal(19, p);

            for (int i = 0; i <= p; i++)
            {
                Assert.Equal(i+1, Int2Str.FindCharCount1((ulong)Math.Pow(10, i)));
            }
        }

        [Fact]
        public void Test_FastAllocateString()
        {
            Assert.NotNull(Int2Str.FastAllocateString);
        }

        [Fact]
        public unsafe void Test_ToStringHack()
        {
            Assert.Equal("0", 0.ToString());

            fixed (char* p = 0.ToString())
            {
                *p = '1';
            }

            Assert.Equal("1", 0.ToString());
        }

        [Fact]
        public unsafe void Test_ToStringHack2()
        {
            fixed (char* p = "2")
            {
                *p = '3';
            }

            Assert.Equal("3", "2");
        }


        [Theory]
        [InlineData(4)]        
        public unsafe void Test_ToStringHack3(int value)
        {
            fixed (char* p = "4")
            {
                *p = '5';
            }

            Assert.Equal("5", value.ToString());
        }

    }
}