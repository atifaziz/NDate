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

namespace NDate
{
    using System;
    #if NDATE_CONTRACTS
    using System.Diagnostics.Contracts;
    #endif

    /// <summary>
    /// Represents a single date without any time component or regard for
    /// any time zone.
    /// </summary>

    // ReSharper disable once PartialTypeWithSinglePart
    partial struct Date :
        IEquatable<Date>, IComparable, IComparable<Date>, IFormattable
    {
        static readonly DateTime Epoch = DateTime.MinValue;
        const int MaxDays = 3652058;

        public static readonly Date MinValue = new Date();
        public static readonly Date MaxValue = new Date(MaxDays);

        readonly int _days;

        public Date(DateTime date) : this((int)(date - Epoch).TotalDays) { }
        public Date(Date date) : this(date._days) { }
        public Date(int year, int month, int day) : this(new DateTime(year, month, day)) { }

        Date(int days)
        {
            if (days < 0 || days > MaxDays) throw new ArgumentOutOfRangeException();
            _days = days;
        }

        public static Date Today => new Date(DateTime.Today);

        public DateTime ToDateTime() => Epoch.AddDays(_days);
        public DateTime ToDateTime(TimeSpan time) => ToDateTime(time, DateTimeKind.Unspecified);
        public DateTime ToDateTime(TimeSpan time, DateTimeKind kind) => DateTime.SpecifyKind(ToDateTime() + time, kind);

        public DateTimeOffset ToDateTimeOffset() => new DateTimeOffset(ToDateTime());
        public DateTimeOffset ToDateTimeOffset(TimeSpan time) => new DateTimeOffset(ToDateTime(time));
        public DateTimeOffset ToDateTimeOffset(TimeSpan time, TimeSpan offset) => new DateTimeOffset(ToDateTime(time), offset);

        public int       Day       => ToDateTime().Day;
        public int       Month     => ToDateTime().Month;
        public int       Year      => ToDateTime().Year;
        public int       DayOfYear => ToDateTime().DayOfYear;
        public DayOfWeek DayOfWeek => ToDateTime().DayOfWeek;

        public override int GetHashCode() => _days;
        public override bool Equals(object obj) => obj is Date && Equals((Date) obj);
        public bool Equals(Date other) => _days == other._days;

        public override string ToString() => ToString((string) null);
        public string ToString(IFormatProvider formatProvider) => ToString(null, formatProvider);
        public string ToString(string format) => ToString(format, null);
        public string ToString(string format, IFormatProvider formatProvider) =>
            string.IsNullOrEmpty(format)
            || (format.Length == 1 && (format[0] == 'g' || format[0] == 'G'))
            ? ToDateTime().ToString("d")
            : ToDateTime().ToString(format, formatProvider);

        public int CompareTo(object obj)
        {
            if (!(obj is Date)) throw new ArgumentException($"Object must be of type {nameof(Date)}.", nameof(obj));
            return CompareTo((Date) obj);
        }

        public int CompareTo(Date other) => _days.CompareTo(other._days);

        public static bool operator ==(Date a, Date b) => a.Equals(b);
        public static bool operator !=(Date a, Date b) => !(a == b);
        public static bool operator < (Date a, Date b) => a.CompareTo(b) <  0;
        public static bool operator <=(Date a, Date b) => a.CompareTo(b) <= 0;
        public static bool operator > (Date a, Date b) => a.CompareTo(b) >  0;
        public static bool operator >=(Date a, Date b) => a.CompareTo(b) >= 0;

        public static Date operator +(Date date, int days) => new Date(date._days + days);
        public static Date operator -(Date date, int days) => date + -days;
        public static int  operator -(Date a, Date b) => a._days - b._days;

        public static explicit operator Date(DateTime value) => new Date(value);
        public static implicit operator DateTime(Date value) => value.ToDateTime();

        public Date AddDays(int days)     => new Date(_days + days);
        public Date AddMonths(int months) => new Date(ToDateTime().AddMonths(months));
        public Date AddYears(int years)   => new Date(ToDateTime().AddYears(years));

        public static Date FirstOfMonth(int year, int month) =>
            new Date(year, month, 1);

        public static Date EndOfMonth(int year, int month) =>
            new Date(year, month, DateTime.DaysInMonth(year, month));

        #if NDATE_CONTRACTS
        [Pure]
        #endif
        public bool IsInRange(Date first, Date last) => this >= first && this <= last;

        #if NDATE_CONTRACTS
        [Pure]
        #endif
        public bool IsBetween(Date first, Date end) => this >= first && this < end;
    }

    #if NDATE_CONVERTIBLE

    partial struct Date : IConvertible
    {
        TypeCode IConvertible.GetTypeCode() => TypeCode.Object;

        DateTime IConvertible.ToDateTime(IFormatProvider provider) => ToDateTime();

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            var result
                = conversionType == typeof(string) ? ToString(provider)
                : conversionType == typeof(DateTime) ? ToDateTime()
                : conversionType == typeof(DateTimeOffset) ? ToDateTimeOffset()
                : (object)null;

            if (result == null)
                throw InvalidCastError(conversionType);

            return result;
        }

        static InvalidCastException InvalidCastError(Type targetType) { throw new InvalidCastException($"Invalid case from '{nameof(Date)}' to '{targetType.Name}'."); }

        bool     IConvertible.ToBoolean(IFormatProvider provider)  { throw InvalidCastError(typeof(bool));    }
        char     IConvertible.ToChar(IFormatProvider provider)     { throw InvalidCastError(typeof(char));    }
        sbyte    IConvertible.ToSByte(IFormatProvider provider)    { throw InvalidCastError(typeof(sbyte));   }
        byte     IConvertible.ToByte(IFormatProvider provider)     { throw InvalidCastError(typeof(byte));    }
        short    IConvertible.ToInt16(IFormatProvider provider)    { throw InvalidCastError(typeof(short));   }
        ushort   IConvertible.ToUInt16(IFormatProvider provider)   { throw InvalidCastError(typeof(ushort));  }
        int      IConvertible.ToInt32(IFormatProvider provider)    { throw InvalidCastError(typeof(int));     }
        uint     IConvertible.ToUInt32(IFormatProvider provider)   { throw InvalidCastError(typeof(uint));    }
        long     IConvertible.ToInt64(IFormatProvider provider)    { throw InvalidCastError(typeof(long));    }
        ulong    IConvertible.ToUInt64(IFormatProvider provider)   { throw InvalidCastError(typeof(ulong));   }
        float    IConvertible.ToSingle(IFormatProvider provider)   { throw InvalidCastError(typeof(float));   }
        double   IConvertible.ToDouble(IFormatProvider provider)   { throw InvalidCastError(typeof(double));  }
        decimal  IConvertible.ToDecimal(IFormatProvider provider)  { throw InvalidCastError(typeof(decimal)); }
    }

    #endif

    #if NDATE_SERIALIZABLE
    [Serializable] partial struct Date {}
    #endif

    #if NDATE_PUBLIC
    public partial struct Date { }
    #endif
}