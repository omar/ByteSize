# ByteSize 

`ByteSize` is a utility class that makes byte size representation in code easier 
by removing ambiguity of the value being represented.

`ByteSize` is to bytes what `System.TimeSpan` is to time.

[![](https://travis-ci.org/omar/ByteSize.svg?branch=master)](https://travis-ci.org/omar/ByteSize)
[![Stable nuget](https://img.shields.io/nuget/v/ByteSize.svg)](https://www.nuget.org/packages/ByteSize/)

#### Development

- Install [.NET Core SDK](https://dotnet.microsoft.com/download)
- Build: `make build`
- Test: `make test`

## v2 Breaking Changes

### Ratio Changes (HUGE BREAKING CHANGE)

By default `ByteSize` now assumes `1 KB == 1000 B` and `1 KiB == 1024 B` to 
adhere to the IEC and NIST standards (https://en.wikipedia.org/wiki/Binary_prefix). 
In version 1 `ByteSize` assumed `1 KB == 1024 B`, that means if you're upgrading
from v1, you'll see differences in values.

When you upgrade an existing application to v2 your existing code will be using
the decimal representation of bytes (i.e. `1 KB == 1000 B`). If the difference
in calculation is not material to your application, you don't need to change anything.

However, if you want to use `1 KiB == 1024 B`, then you'll need to change all
`ByteSize` calls to the respective method. For example, calls to
`ByteSize.FromKiloByte` need to be changed to `ByteSize.FromKibiByte`.

Lastly, `ByteSize` no longer supports the ratio of `1 KB == 1024 B`. Note this 
is ***kilo***_bytes_ to _bytes_. The only ratio of `1 == 1024` is ***kibi***_bytes_ 
to _bytes_.

### Other Breaking Changes

- Renamed property `LargestWholeNumberSymbol` and `LargestWholeNumberValue` to `LargestWholeNumberDecimalSymbol` and `LargestWholeNumberDecimalValue` respectively.
- Drop support for all platforms _except_ `netstandard1.0` and `net45`.

## Usage

`ByteSize` adheres to the IEC standard, see this [Wikipedia article](https://en.wikipedia.org/wiki/Kilobyte#Definitions_and_usage).
That means `ByteSize` assumes:

- `1 kilobyte` = `1000 bytes` with 2 letter abbrevations `b`, `B`,`KB`, `MB`, `GB`, `TB`, `PB`.
- `1 kibibyte` = `1024 bytes` with 3 letter abbrevations `b`, `B`,`KiB`, `MiB`, `GiB`, `TiB`, `PiB`.

`ByteSize` manages conversion of the values internally and provides methods to create and retrieve the values as needed. See the examples below.

### Example 

Without `ByteSize`:

```c#
double maxFileSizeMBs = 1.5;

// I need it in KBs and KiBs!
var kilobytes = maxFileSizeMBs * 1000; // 1500
var kibibytes = maxFileSizeMBs * 1024; // 1536
```

With `ByteSize`:

```c#
var maxFileSize = ByteSize.FromMegaBytes(1.5);

// I have it in KBs and KiBs!!
maxFileSize.KiloBytes;  // 1500
maxFileSize.KibiBytes;  // 1464.84376
```

`ByteSize` behaves like any other struct backed by a numerical value allowing arithmetic operations between two objects.

```c#
// Add
var monthlyUsage = ByteSize.FromGigaBytes(10);
var currentUsage = ByteSize.FromMegaBytes(512);
ByteSize total = monthlyUsage + currentUsage;

total.Add(ByteSize.FromKiloBytes(10));
total.AddGigaBytes(10);

// Subtract
var delta = total.Subtract(ByteSize.FromKiloBytes(10));
delta = delta - ByteSize.FromGigaBytes(100);
delta = delta.AddMegaBytes(-100);
```

### Constructors

You can create a `ByteSize` object from `bits`, `bytes`, `kilobytes`, `megabytes`, `gigabytes`, and `terabytes`.

```c#
new ByteSize(15);            // Constructor takes in bits (long)
new ByteSize(1.5);           // ... or bytes (double)

// Static Constructors
ByteSize.FromBits(10);       // Same as constructor
ByteSize.FromBytes(1.5);     // Same as constructor

// Decimal: 1 KB = 1000 B
ByteSize.FromKiloBytes(1.5);
ByteSize.FromMegaBytes(1.5);
ByteSize.FromGigaBytes(1.5);
ByteSize.FromTeraBytes(1.5);

// Binary: 1 KiB = 1024 B
ByteSize.FromKibiBytes(1.5);
ByteSize.FromMebiBytes(1.5);
ByteSize.FromGibiBytes(1.5);
ByteSize.FromTebiBytes(1.5);
```

### Properties

A `ByteSize` object contains representations in:

- `bits`, `bytes`
- `kilobytes`, `megabytes`, `gigabytes`, and `terabytes`
- `kibibytes`, `mebibytes`, `gibibytes`, and `tebibytes`

```c#
var maxFileSize = ByteSize.FromKiloBytes(10);

maxFileSize.Bits;      // 80000
maxFileSize.Bytes;     // 10000

// Decimal
maxFileSize.KiloBytes; // 10
maxFileSize.MegaBytes; // 0.01
maxFileSize.GigaBytes; // 1E-05
maxFileSize.TeraBytes; // 1E-08

// Binary
maxFileSize.KibiBytes; // 9.765625
maxFileSize.MebiBytes; // 0.0095367431640625
maxFileSize.GibiBytes; // 9.31322574615479E-06
maxFileSize.TebiBytes; // 9.09494701772928E-09
```

A `ByteSize` object also contains four properties that represent the largest whole number symbol and value.

```c#
var maxFileSize = ByteSize.FromKiloBytes(10);

maxFileSize.LargestWholeNumberDecimalSymbol; // "KB"
maxFileSize.LargestWholeNumberDecimalValue;  // 10
maxFileSize.LargestWholeNumberBinarySymbol;  // "KiB"
maxFileSize.LargestWholeNumberBinaryValue;   // 9.765625
```

### String Representation

By default a `ByteSize` object uses the decimal value for string representation.

All string operations are localized to use the number decimal separator of the culture set in `Thread.CurrentThread.CurrentCulture`.

#### ToString

`ByteSize` comes with a handy `ToString` method that uses the largest metric prefix whose value is greater than or equal to 1.

```c#
// By default the decimal values are used
ByteSize.FromBits(7).ToString();         // 7 b
ByteSize.FromBits(8).ToString();         // 1 B
ByteSize.FromKiloBytes(.5).ToString();   // 500 B
ByteSize.FromKiloBytes(999).ToString();  // 999 KB
ByteSize.FromKiloBytes(1000).ToString(); // 1 MB
ByteSize.FromGigabytes(.5).ToString();   // 500 MB
ByteSize.FromGigabytes(1000).ToString(); // 1 TB

// Binary
ByteSize.Parse("1.55 kb").ToString("kib"); // 1.51 kib
```

#### Formatting

The `ToString` method accepts a single `string` parameter to format the output.
The formatter can contain the symbol of the value to display.

- Base: `b`, `B`
- Decimal: `KB`, `MB`, `GB`, `TB`
- Binary: `KiB`, `MiB`, `GiB`, `TiB`

The formatter uses the built in [`double.ToString` method](http://msdn.microsoft.com/en-us/library/kfsatb94\(v=vs.110\).aspx). 

The default number format is `0.##` which rounds the number to two decimal 
places and outputs only `0` if the value is `0`.

You can include symbol and number formats.

```c#
var b = ByteSize.FromKiloBytes(10.505);

// Default number format is 0.##
b.ToString("KB");         // 10.52 KB
b.ToString("MB");         // .01 MB
b.ToString("b");          // 86057 b

// Default symbol is the largest metric prefix value >= 1
b.ToString("#.#");        // 10.5 KB

// All valid values of double.ToString(string format) are acceptable
b.ToString("0.0000");     // 10.5050 KB
b.ToString("000.00");     // 010.51 KB

// You can include number format and symbols
b.ToString("#.#### MB");  // .0103 MB
b.ToString("0.00 GB");    // 0 GB
b.ToString("#.## B");     // 10757.12 B

// ByteSize object of value 0
var zeroBytes = ByteSize.FromKiloBytes(0); 
zeroBytes.ToString();           // 0 b
zeroBytes.ToString("0 kb");     // 0 kb
zeroBytes.ToString("0.## mb");  // 0 mb
```

#### Parsing

`ByteSize` has a `Parse` and `TryParse` method similar to other base classes.

Like other `TryParse` methods, `ByteSize.TryParse` returns `boolean` 
value indicating whether or not the parsing was successful. If the value is 
parsed it is output to the `out` parameter supplied.

```c#
ByteSize output;
ByteSize.TryParse("1.5mb", out output);
ByteSize.TryParse("1.5mib", out output);

// Invalid
ByteSize.Parse("1.5 b");   // Can't have partial bits

// Valid
ByteSize.Parse("5b");
ByteSize.Parse("1.55B");
ByteSize.Parse("1.55KB");
ByteSize.Parse("1.55 kB "); // Spaces are trimmed
ByteSize.Parse("1.55 kb");
ByteSize.Parse("1.55 MB");
ByteSize.Parse("1.55 mB");
ByteSize.Parse("1.55 mb");
ByteSize.Parse("1.55 GB");
ByteSize.Parse("1.55 gB");
ByteSize.Parse("1.55 gib");
ByteSize.Parse("1.55 TiB");
ByteSize.Parse("1.55 tiB");
ByteSize.Parse("1.55 tib");
ByteSize.Parse("1,55 kib"); // de-DE culture
```

#### Author and License

Omar Khudeira ([http://omar.io](http://omar.io))

Copyright (c) 2013-2021 Omar Khudeira. All rights reserved.

Released under MIT License (see LICENSE file).
