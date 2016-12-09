#region Copyright (c) 2014 Atif Aziz. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

namespace NDate.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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

        const int TestYear = 1995;
        const int TestMonth = 8;
        const int TestDay = 15;
        static readonly Date TestDate = new Date(TestYear, TestMonth, TestDay);

        [Fact]
        public void DateIsInitialized()
        {
            var date = new Date(TestYear, TestMonth, TestDay);
            Assert.Equal(TestYear, date.Year);
            Assert.Equal(TestMonth, date.Month);
            Assert.Equal(TestDay, date.Day);
        }

        [Fact]
        public void DateIsInitializedWithDate()
        {
            var date = new Date(TestDate);
            Assert.Equal(TestDate.Year, date.Year);
            Assert.Equal(TestDate.Month, date.Month);
            Assert.Equal(TestDate.Day, date.Day);
        }

        [Fact]
        public void DateIsInitializedWithDateTime()
        {
            var dt = new DateTime(TestYear, TestMonth, TestDay);
            var date = new Date(dt);
            Assert.Equal(dt.Year, date.Year);
            Assert.Equal(dt.Month, date.Month);
            Assert.Equal(dt.Day, date.Day);
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
        public void FirstOfMonth(int year, int month)
        {
            var date = Date.FirstOfMonth(year, month);
            Assert.Equal(year, date.Year);
            Assert.Equal(month, date.Month);
            Assert.Equal(1, date.Day);
        }

        [Theory]
        [InlineData(true,  TestYear + 0, TestMonth + 0, TestDay + 0, TestYear + 0, TestMonth + 0, TestDay + 0)]
        [InlineData(true,  TestYear + 0, TestMonth + 0, TestDay - 1, TestYear + 0, TestMonth + 0, TestDay + 0)]
        [InlineData(true,  TestYear + 0, TestMonth + 0, TestDay + 0, TestYear + 0, TestMonth + 0, TestDay + 1)]
        [InlineData(false, TestYear + 0, TestMonth + 0, TestDay + 1, TestYear + 0, TestMonth + 0, TestDay + 1)]
        [InlineData(true,  TestYear + 0, TestMonth - 1, TestDay + 0, TestYear + 0, TestMonth + 0, TestDay + 0)]
        [InlineData(true,  TestYear + 0, TestMonth + 0, TestDay + 0, TestYear + 0, TestMonth + 1, TestDay + 0)]
        [InlineData(false, TestYear + 0, TestMonth + 1, TestDay + 0, TestYear + 0, TestMonth + 1, TestDay + 0)]
        [InlineData(true,  TestYear - 1, TestMonth + 0, TestDay + 0, TestYear + 0, TestMonth + 0, TestDay + 0)]
        [InlineData(true,  TestYear + 0, TestMonth + 0, TestDay + 0, TestYear + 0, TestMonth + 0, TestDay + 1)]
        [InlineData(false, TestYear + 1, TestMonth + 0, TestDay + 0, TestYear + 0, TestMonth + 0, TestDay + 1)]
        public void IsInRange(bool expected, int y1, int m1, int d1, int y2, int m2, int d2)
        {
            var first = new Date(y1, m1, d1);
            var last = new Date(y2, m2, d2);
            Assert.Equal(expected, TestDate.IsInRange(first, last));
        }

        [Theory]
        [InlineData(false, TestYear + 0, TestMonth + 0, TestDay + 0, TestYear + 0, TestMonth + 0, TestDay + 0)]
        [InlineData(false, TestYear + 0, TestMonth + 0, TestDay - 1, TestYear + 0, TestMonth + 0, TestDay + 0)]
        [InlineData(true,  TestYear + 0, TestMonth + 0, TestDay + 0, TestYear + 0, TestMonth + 0, TestDay + 1)]
        [InlineData(false, TestYear + 0, TestMonth + 0, TestDay + 1, TestYear + 0, TestMonth + 0, TestDay + 1)]
        [InlineData(false, TestYear + 0, TestMonth - 1, TestDay + 0, TestYear + 0, TestMonth + 0, TestDay + 0)]
        [InlineData(true,  TestYear + 0, TestMonth + 0, TestDay + 0, TestYear + 0, TestMonth + 1, TestDay + 0)]
        [InlineData(false, TestYear + 0, TestMonth + 1, TestDay + 0, TestYear + 0, TestMonth + 1, TestDay + 0)]
        [InlineData(false, TestYear - 1, TestMonth + 0, TestDay + 0, TestYear + 0, TestMonth + 0, TestDay + 0)]
        [InlineData(true,  TestYear + 0, TestMonth + 0, TestDay + 0, TestYear + 0, TestMonth + 0, TestDay + 1)]
        [InlineData(false, TestYear + 1, TestMonth + 0, TestDay + 0, TestYear + 0, TestMonth + 0, TestDay + 1)]
        public void IsBetween(bool expected, int y1, int m1, int d1, int y2, int m2, int d2)
        {
            var first = new Date(y1, m1, d1);
            var end = new Date(y2, m2, d2);
            Assert.Equal(expected, TestDate.IsBetween(first, end));
        }

        [Theory]
        [InlineData("1995-08-15 12:30:00", 12.5)]
        [InlineData("1995-08-16 00:00:00", 24.0)]
        [InlineData("1995-08-16 01:00:00", 25.0)]
        public void AddTimeSpan(string expected, double hours)
        {
            var dt = TestDate + TimeSpan.FromHours(hours);
            Assert.Equal(expected, dt.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
        }

        [Fact] // ReSharper disable once InconsistentNaming
        public void ToIso8601StringReturnsYYYYMMDD()
        {
            Assert.Equal("1995-08-15", TestDate.ToIso8601String());
        }
    }
}
