namespace NDate.Tests
{
    using System;
    using System.Collections.Generic;
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

        static readonly Date TestDate = new Date(1995, 8, 15);

        [Fact]
        public void EqualsReturnsTrueForSameDates()
        {
            var copy = new Date(TestDate);
            Assert.Equal(copy, TestDate);
            Assert.True(copy.Equals((object) TestDate));
        }

        static object[] Objects(params object[] values) => values;

        public static IEnumerable<object[]> DatesOtherThanTestDate => new[]
        {
            Objects(TestDate.Year + 1, TestDate.Month, TestDate.Day),
            Objects(TestDate.Year, TestDate.Month + 1, TestDate.Day),
            Objects(TestDate.Year, TestDate.Month, TestDate.Day + 1),
        };

        [Theory]
        [MemberData(nameof(DatesOtherThanTestDate))]
        public void EqualsReturnsFalseForDifferentDates(int year, int month, int day)
        {
            Assert.NotEqual(new Date(year, month, day), TestDate);
            Assert.False(new Date(year, month, day).Equals((object) TestDate));
        }

        [Fact]
        public void EqualityOperatorReturnsTrueForSameDates()
        {
            Assert.True(TestDate == new Date(TestDate));
        }

        [Theory]
        [MemberData(nameof(DatesOtherThanTestDate))]
        public void EqualityOperatorReturnsFalseForDifferentDates(int year, int month, int day)
        {
            Assert.False(new Date(year, month, day) == TestDate);
        }

        [Theory]
        [MemberData(nameof(DatesOtherThanTestDate))]
        public void InequalityOperatorReturnsTrueForDifferentDates(int year, int month, int day)
        {
            Assert.True(new Date(year, month, day) != TestDate);
        }

        public void InequalityOperatorReturnsFalseForSameDates()
        {
            Assert.False(TestDate != new Date(TestDate));
        }

        [Theory]
        [InlineData(2000, 01, 31)]
        [InlineData(2000, 02, 29)]
        [InlineData(2001, 02, 28)]
        [InlineData(2000, 03, 31)]
        [InlineData(2000, 04, 30)]
        [InlineData(2000, 05, 31)]
        [InlineData(2000, 06, 30)]
        [InlineData(2000, 07, 31)]
        [InlineData(2000, 08, 31)]
        [InlineData(2000, 09, 30)]
        [InlineData(2000, 10, 31)]
        [InlineData(2000, 11, 30)]
        [InlineData(2000, 12, 31)]
        public void EndOfMonth(int year, int month, int day)
        {
            var date = Date.EndOfMonth(year, month);
            Assert.Equal(year, date.Year);
            Assert.Equal(month, date.Month);
            Assert.Equal(day, date.Day);
        }

        [Theory]
        [InlineData(2000, 01)]
        [InlineData(2000, 02)]
        [InlineData(2000, 03)]
        [InlineData(2000, 04)]
        [InlineData(2000, 05)]
        [InlineData(2000, 06)]
        [InlineData(2000, 07)]
        [InlineData(2000, 08)]
        [InlineData(2000, 09)]
        [InlineData(2000, 10)]
        [InlineData(2000, 11)]
        [InlineData(2000, 12)]
        public void EndOfMonth(int year, int month)
        {
            var date = Date.FirstOfMonth(year, month);
            Assert.Equal(year, date.Year);
            Assert.Equal(month, date.Month);
            Assert.Equal(1, date.Day);
        }
    }
}
