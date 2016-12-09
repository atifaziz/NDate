#r "NDate/bin/Debug/netstandard1.3/NDate.dll"

using System.Globalization;
using static System.Console;
using NDate;

CultureInfo.CurrentCulture = new CultureInfo("en-GB");

// Create a Date given a year, month and day

var date = new Date(1995, 8, 15);
WriteLine(date); // 15/08/1995

// Create a Date from DateTime; the time component is lost

WriteLine(new Date(DateTime.Today)); // 09/12/2016

// Get today's date

var today = Date.Today;
WriteLine(today);

// Convert to DateTime

var time = new TimeSpan(12, 34, 56);
WriteLine(date.ToDateTime());        // 15/08/1995 00:00:00
WriteLine(date.ToDateTime(time));    // 15/08/1995 12:34:56
WriteLine(date + time);              // 15/08/1995 12:34:56

// Implicit conversion to DateTime

var tz = TimeZone.CurrentTimeZone;
WriteLine(tz.GetUtcOffset(date)); // 02:00:00

// Convert to DateTimeOffset

WriteLine(date.ToDateTimeOffset());     // 15/08/1995 00:00:00 +02:00
WriteLine(date.ToDateTimeOffset(time)); // 15/08/1995 12:34:56 +02:00

var offset = new TimeSpan(1, 0, 0);
WriteLine(date.ToDateTimeOffset(time, offset)); // 15/08/1995 12:34:56 +01:00

// Get different parts of date

WriteLine(date.Day);        // 15
WriteLine(date.Month);      // 8
WriteLine(date.Year);       // 1995
WriteLine(date.DayOfYear);  // 227
WriteLine(date.DayOfWeek);  // Tuesday

// Date math

var tomorrow = Date.Today + 1;
WriteLine(tomorrow);             // 10/12/2016

var yesterday = Date.Today - 1;  // 08/12/2016
WriteLine(yesterday);

WriteLine(date - Date.MinValue); // 728519
WriteLine(date.AddDays(10));     // 25/08/1995
WriteLine(date.AddMonths(6));    // 15/02/1996
WriteLine(date.AddYears(2));     // 15/08/1997

// Compare dates

WriteLine(today == new Date(today));    // True; same
WriteLine(today != tomorrow);           // True; different
WriteLine(today < tomorrow);            // True; smaller
WriteLine(tomorrow > Date.Today);       // True; bigger

// Format

WriteLine(date.ToString("D"));          // 15 August 1995
WriteLine(date.ToString("yyyyMMdd"));   // 19950815
WriteLine(date.ToIso8601String());      // 1995-08-15

// Miscellaneous

WriteLine(Date.FirstOfMonth(1995, 8));  // 01/08/1995
WriteLine(Date.EndOfMonth(1995, 8));    // 31/08/1995

// Range checking

WriteLine(Date.Today.IsInRange(Date.Today, Date.Today)); // True
WriteLine(Date.Today.IsBetween(Date.Today, Date.Today)); // False; end is exclusive
