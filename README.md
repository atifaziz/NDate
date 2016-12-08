# NDate

[![Build Status][build-badge]][builds]
[![NuGet][nuget-badge]][nuget-pkg]
[![MyGet][myget-badge]][edge-pkgs]

NDate is a [.NET Standard][netstd] library that provides a `Date` value type
for times when you need to strictly represent a date without any time
component.

## Examples

```c#
// Create a Date given a year, month and day

var da = new Date(1995, 8, 15);

// Create a Date from DateTime; the time component is lost

var db = new Date(DateTime.Today);

// Get today's date

var today = Date.Today;

// Convert to DateTime

var dta = Date.Today.ToDateTime();
var dtb = Date.Today.ToDateTime(new TimeSpan(12, 34, 56));
var dtc = Date.Today + new TimeSpan(12, 34, 56);

// Convert to DateTimeOffset

var dtoa = Date.Today.ToDateTimeOffset();
var dtob = Date.Today.ToDateTimeOffset(new TimeSpan(12, 34, 56));
var dtoc = Date.Today.ToDateTimeOffset(new TimeSpan(12, 34, 56), new TimeSpan(1, 0, 0));

// Get different parts of date

var d = Date.Today.Day;
var m = Date.Today.Month;
var y = Date.Today.Year;
var doy = Date.Today.DayOfYear;
var dow = Date.Today.DayOfWeek;

// Date math

var tomorrow = Date.Today + 1;
var yesterday = Date.Today - 1;
var days = Date.Today - Date.MinValue;
var dc = Date.Today.AddDays(10);
var dd = Date.Today.AddMonths(6);
var de = Date.Today.AddYears(2);


// Compare dates

var same = Date.Today == Date.Today;
var different = Date.Today != tomorrow;
var smaller = Date.Today < tomorrow;
var bigger = tomorrow > Date.Today;

// Format

var s1 = Date.Today.ToString("D");
var s2 = Date.Today.ToString("yyyy-MM-dd");

// Others

var fom = Date.FirstOfMonth(1995, 8); // Aug 01, 1995
var eom = Date.EndOfMonth(1995, 8);   // Aug 31, 1995

// Range checking

var yes = Date.Today.IsInRange(Date.Today, Date.Today); //
var no  = Date.Today.IsBetween(Date.Today, Date.Today); // end is exclusive
```


[netstd]: https://docs.microsoft.com/en-us/dotnet/articles/standard/library
[build-badge]: https://img.shields.io/appveyor/ci/raboof/ndate.svg
[myget-badge]: https://img.shields.io/myget/raboof/v/NDate.svg?label=myget
[edge-pkgs]: https://www.myget.org/feed/raboof/package/nuget/NDate
[nuget-badge]: https://img.shields.io/nuget/v/NDate.svg
[nuget-pkg]: https://www.nuget.org/packages/NDate
[builds]: https://ci.appveyor.com/project/raboof/ndate
