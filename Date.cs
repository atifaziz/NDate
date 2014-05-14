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

using System;

/// <summary>
/// Represents a single date without any time component or regard for 
/// any time zone.
/// </summary>

[Serializable]
// ReSharper disable once PartialTypeWithSinglePart
partial struct Date : 
    IEquatable<Date>, IComparable, IComparable<Date>, IFormattable, IConvertible
{
    static readonly string ThisTypeName = typeof(Date).Name;
    static readonly DateTime Epoch = DateTime.MinValue;
    const int MaxDays = 3652058;

    readonly int _days;

    public Date(DateTime date) : this((int)(date - Epoch).TotalDays) { }
    public Date(Date date) : this(date._days) { }
    public Date(int year, int month, int day) : this(new DateTime(year, month, day)) { }

    Date(int days)
    {
        if (days < 0 || days > MaxDays) throw new ArgumentOutOfRangeException();
        _days = days;
    }

    public static Date Today { get { return new Date(DateTime.Today); } }

    public DateTime ToDateTime() { return Epoch.AddDays(_days); }
    public DateTime ToDateTime(TimeSpan time) { return ToDateTime(time, DateTimeKind.Unspecified); }
    public DateTime ToDateTime(TimeSpan time, DateTimeKind kind) { return DateTime.SpecifyKind(ToDateTime() + time, kind); }

    public DateTimeOffset ToDateTimeOffset() { return new DateTimeOffset(ToDateTime()); }
    public DateTimeOffset ToDateTimeOffset(TimeSpan time) { return new DateTimeOffset(ToDateTime(time)); }
    public DateTimeOffset ToDateTimeOffset(TimeSpan time, TimeSpan offset) { return new DateTimeOffset(ToDateTime(time), offset); }
    
    public int       Day       { get { return ToDateTime().Day;         } }
    public int       Month     { get { return ToDateTime().Month;       } }
    public int       Year      { get { return ToDateTime().Year;        } }
    public int       DayOfYear { get { return ToDateTime().DayOfYear;   } }
    public DayOfWeek DayOfWeek { get { return ToDateTime().DayOfWeek;   } }

    public override int GetHashCode() { return _days; }
    public override bool Equals(object obj) { return obj is Date && Equals((Date) obj); }
    public bool Equals(Date other) { return _days == other._days; }

    public override string ToString() { return ToString((string) null); }
    public string ToString(IFormatProvider formatProvider) { return ToString(null, formatProvider); }
    public string ToString(string format) { return ToString(format, null); }
    public string ToString(string format, IFormatProvider formatProvider)
    {
        return string.IsNullOrEmpty(format) 
            || (format.Length == 1 && (format[0] == 'g' || format[0] == 'G')) 
            ? ToDateTime().ToShortDateString()
            : ToDateTime().ToString(format, formatProvider);
    }

    public int CompareTo(object obj)
    {
        if (!(obj is Date)) throw new ArgumentException(string.Format("Object must be of type {0}.", ThisTypeName), "obj");
        return CompareTo((Date) obj);
    }

    public int CompareTo(Date other) { return _days.CompareTo(other._days); }

    public static bool operator ==(Date a, Date b) { return a.Equals(b); }
    public static bool operator !=(Date a, Date b) { return !(a == b); }
    public static bool operator < (Date a, Date b) { return a.CompareTo(b) <  0; }
    public static bool operator <=(Date a, Date b) { return a.CompareTo(b) <= 0; }
    public static bool operator > (Date a, Date b) { return a.CompareTo(b) >  0; }
    public static bool operator >=(Date a, Date b) { return a.CompareTo(b) >= 0; }
    
    public static Date operator +(Date date, int days) { return new Date(date._days + days); }
    public static Date operator -(Date date, int days) { return date + -days; }
    public static int  operator -(Date a, Date b) { return a._days - b._days; }

    public static explicit operator Date(DateTime value) { return new Date(value); }
    public static implicit operator DateTime(Date value) { return value.ToDateTime(); }

    public Date AddDays(int days)     { return new Date(_days + days);                   }
    public Date AddMonths(int months) { return new Date(ToDateTime().AddMonths(months)); }
    public Date AddYears(int years)   { return new Date(ToDateTime().AddYears(years));   }

    TypeCode IConvertible.GetTypeCode() { return TypeCode.Object; }

    DateTime IConvertible.ToDateTime(IFormatProvider provider) { return ToDateTime(); }

    object IConvertible.ToType(Type conversionType, IFormatProvider provider)
    {
        var result =
            conversionType == typeof(string) ? ToString(provider)
            : conversionType == typeof(DateTime) ? ToDateTime()
            : conversionType == typeof(DateTimeOffset) ? ToDateTimeOffset()
            : (object)null;

        if (result == null)
            throw InvalidCastError(conversionType);

        return result;
    }

    static InvalidCastException InvalidCastError(Type targetType) { throw new InvalidCastException(string.Format("Invalid case from '{0}' to '{1}'.", ThisTypeName, targetType.Name)); }
    
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
