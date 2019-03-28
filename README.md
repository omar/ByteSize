# ByteSize 

`ByteSize` is a utility class that makes byte size representation in code easier 
by removing ambiguity of the value being represented.

`ByteSize` is to bytes what `System.TimeSpan` is to time.

[![](https://travis-ci.org/omar/DecimalByteSize.svg?branch=master)](https://travis-ci.org/omar/ByteSize)
[![Stable nuget](https://img.shields.io/nuget/v/DecimalByteSize.svg)](https://www.nuget.org/packages/ByteSize/)

#### Development

- Install [.NET Core SDK](https://dotnet.microsoft.com/download)
- Build: `make build`
- Test: `make test`

## Usage

`ByteSize` comes with 3 different ways to represent bytes:

- `DecimalByteSize` which assumes `1 kilobyte` = `1000 bytes` with 2 letter abbrevations `b`, `B`,`KB`, `MB`, `GB`, `TB`, `PB`.
- `BinaryByteSize` which assumes `1 kibibyte` = `1024 bytes` with 3 letter abbrevations `b`, `B`,`KiB`, `MiB`, `GiB`, `TiB`, `PiB`.
- `NonStandardByteSize` which assumes `1 kilobyte` = `1024 bytes` with 2 letter abbrevations `b`, `B`,`KB`, `MiB`, `GB`, `TB`, `PB`.

The first two adhere to the IEC standard, see this [Wikipedia article](https://en.wikipedia.org/wiki/Kilobyte#Definitions_and_usage).


### Example 

Without `ByteSize`:

```c#
static double MaxFileSizeMBs = 1.5;

// I need it in KBs!
var kilobytes = MaxFileSizeMBs * 1000; // 1500
```

With `ByteSize`:

```c#
static MaxFileSize = DecimalByteSize.FromMegaBytes(1.5);

// I have it in KBs!
MaxFileSize.KiloBytes;  // 1500
```

`ByeSize` behaves like any other struct backed by a numerical value.

```c#
// Add
var monthlyUsage = DecimalByteSize.FromGigaBytes(10);
var currentUsage = DecimalByteSize.FromMegaBytes(512);
ByteSize total = monthlyUsage + currentUsage;

total.Add(DecimalByteSize.FromKiloBytes(10));
total.AddGigaBytes(10);

// Subtract
var delta = total.Subtract(DecimalByteSize.FromKiloBytes(10));
delta = delta - DecimalByteSize.FromGigaBytes(100);
delta = delta.AddMegaBytes(-100);
```

### Constructors

You can create a `ByteSize` object from `bits`, `bytes`, `kilobytes`, `megabytes`, `gigabytes`, and `terabytes`.

```c#
new DecimalByteSize(1.5);           // Constructor takes in bytes

// Static Constructors
DecimalByteSize.FromBits(10);       // Bits are whole numbers only
DecimalByteSize.FromBytes(1.5);     // Same as constructor
DecimalByteSize.FromKiloBytes(1.5);
DecimalByteSize.FromMegaBytes(1.5);
DecimalByteSize.FromGigaBytes(1.5);
DecimalByteSize.FromTeraBytes(1.5);
```

### Properties

A `ByteSize` object contains representations in `bits`, `bytes`, `kilobytes`, `megabytes`, `gigabytes`, and `terabytes`.

```c#
var maxFileSize = DecimalByteSize.FromKiloBytes(10);

maxFileSize.Bits;      // 80000
maxFileSize.Bytes;     // 10000
maxFileSize.KiloBytes; // 10
maxFileSize.MegaBytes; // 0.01
maxFileSize.GigaBytes; // 1E-05
maxFileSize.TeraBytes; // 1E-08
```

A `ByteSize` object also contains two properties that represent the largest metric prefix symbol and value.

```c#
var maxFileSize = DecimalByteSize.FromKiloBytes(10);

maxFileSize.LargestWholeNumberSymbol; // "KB"
maxFileSize.LargestWholeNumberValue;  // 10
```

### String Representation

All string operations are localized to use the number decimal separator of the culture set in `Thread.CurrentThread.CurrentCulture`.

#### ToString

`ByteSize` comes with a handy `ToString` method that uses the largest metric prefix whose value is greater than or equal to 1.

```c#
DecimalByteSize.FromBits(7).ToString();         // 7 b
DecimalByteSize.FromBits(8).ToString();         // 1 B
DecimalByteSize.FromKiloBytes(.5).ToString();   // 500 B
DecimalByteSize.FromKiloBytes(999).ToString();  // 999 KB
DecimalByteSize.FromKiloBytes(1000).ToString(); // 1 MB
DecimalByteSize.FromGigabytes(.5).ToString();   // 500 MB
DecimalByteSize.FromGigabytes(1000).ToString(); // 1 TB
```

#### Formatting

The `ToString` method accepts a single `string` parameter to format the output.
The formatter can contain the symbol of the value to display depending on the object:

- `NonStandardByteSize` and `DecimalByteSize`: `b`, `B`, `KB`, `MB`, `GB`, `TB`.
- `BinaryByteSize`: `b`, `B`,`KiB`, `MiB`, `GiB`, `TiB`

The formatter uses the built in [`double.ToString` method](http://msdn.microsoft.com/en-us/library/kfsatb94\(v=vs.110\).aspx). 

The default number format is `0.##` which rounds the number to two decimal 
places and outputs only `0` if the value is `0`.

You can include symbol and number formats.

```c#
var b = DecimalByteSize.FromKiloBytes(10.505);

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
var zeroBytes = DecimalByteSize.FromKiloBytes(0); 
zeroBytes.ToString();           // 0 b
zeroBytes.ToString("0 kb");     // 0 kb
zeroBytes.ToString("0.## mb");  // 0 mb
```

#### Parsing

`ByteSize` has a `Parse` and `TryParse` method similar to other base classes.

Like other `TryParse` methods, `DecimalByteSize.TryParse` returns `boolean` 
value indicating whether or not the parsing was successful. If the value is 
parsed it is output to the `out` parameter supplied.

```c#
ByteSize output;
DecimalByteSize.TryParse("1.5mb", out output);

// Invalid
DecimalByteSize.Parse("1.5 b");   // Can't have partial bits

// Valid
DecimalByteSize.Parse("5b");
DecimalByteSize.Parse("1.55B");
DecimalByteSize.Parse("1.55KB");
DecimalByteSize.Parse("1.55 kB "); // Spaces are trimmed
DecimalByteSize.Parse("1.55 kb");
DecimalByteSize.Parse("1.55 MB");
DecimalByteSize.Parse("1.55 mB");
DecimalByteSize.Parse("1.55 mb");
DecimalByteSize.Parse("1.55 GB");
DecimalByteSize.Parse("1.55 gB");
DecimalByteSize.Parse("1.55 gb");
DecimalByteSize.Parse("1.55 TB");
DecimalByteSize.Parse("1.55 tB");
DecimalByteSize.Parse("1.55 tb");
DecimalByteSize.Parse("1,55 kb"); // de-DE culture
```

#### Author and License

Omar Khudeira ([http://omar.io](http://omar.io))

Copyright (c) 2013-2019 Omar Khudeira. All rights reserved.

Released under MIT License (see LICENSE file).

