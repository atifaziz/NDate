namespace NDate.Tests
{
    using System;
    using Xunit;

    public class DateTests
    {
        [Fact]
        public void DefaultValueIsJan01Y0001()
        {
            var date = new Date();
            Assert.Equal(1, date.Year);
            Assert.Equal(1, date.Month);
            Assert.Equal(1, date.Day);
        }

        [Fact]
        public void DateIsInitialized()
        {
            var date = new Date(1995, 8, 15);
            Assert.Equal(1995, date.Year);
            Assert.Equal(8, date.Month);
            Assert.Equal(15, date.Day);
        }

        [Fact]
        public void DateIsInitializedWithDate()
        {
            var date = new Date(new Date(1995, 8, 15));
            Assert.Equal(1995, date.Year);
            Assert.Equal(8, date.Month);
            Assert.Equal(15, date.Day);
        }

        [Fact]
        public void DateIsInitializedWithDateTime()
        {
            var date = new Date(new DateTime(1995, 8, 15));
            Assert.Equal(1995, date.Year);
            Assert.Equal(8, date.Month);
            Assert.Equal(15, date.Day);
        }

        [Fact]
        public void MinValueIsJan01Y0001()
        {
            var date = Date.MinValue;
            Assert.Equal(1, date.Year);
            Assert.Equal(1, date.Month);
            Assert.Equal(1, date.Day);
        }

        [Fact]
        public void MaxValueIsDec31Y9999()
        {
            var date = Date.MaxValue;
            Assert.Equal(9999, date.Year);
            Assert.Equal(12, date.Month);
            Assert.Equal(31, date.Day);
        }

        [Fact]
        public void MinValueIsSameAsDateTimeMinValue()
        {
            var date = Date.MinValue;
            Assert.Equal(DateTime.MinValue.Year , date.Year);
            Assert.Equal(DateTime.MinValue.Month, date.Month);
            Assert.Equal(DateTime.MinValue.Day  , date.Day);
        }

        [Fact]
        public void MaxValueIsSameAsDateTimeMaxValue()
        {
            var date = Date.MaxValue;
            Assert.Equal(DateTime.MaxValue.Year , date.Year);
            Assert.Equal(DateTime.MaxValue.Month, date.Month);
            Assert.Equal(DateTime.MaxValue.Day  , date.Day);
        }
    }
}
