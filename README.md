# ByteSize 

`ByteSize` is a utility class that makes byte size representation in code easier by removing ambiguity of the value being represented.

`ByteSize` is to bytes what `System.TimeSpan` is to time.

[![](https://travis-ci.org/omar/ByteSize.svg?branch=master)](https://travis-ci.org/omar/ByteSize)
[![Stable nuget](https://img.shields.io/nuget/v/ByteSize.svg)](https://www.nuget.org/packages/ByteSize/)

#### Building

* Windows: use Visual Studio
* Mac OS X
   * Install [Mono](http://www.mono-project.com/download/).
     * NOTE: using `brew install mono` will not install the PCL libraries required to build the PCL compatible DLLs. The PCL libraries can be installed by running the installer downloaded from http://www.mono-project.com/download/. 
   * Run `make build` in terminal.
* Linux
   * Install [Mono](http://www.mono-project.com/docs/getting-started/install/linux/) and the reference assemblies (`sudo apt-get referenceassemblies-pcl`).
   * Run `make build` in terminal.

## Usage 

`ByteSize` assumes `1 kilobyte` = `1024 bytes`. See [why here](http://omar.io/2017/01/16/when-technically-right-is-wrong-kilobytes.html).

Without `ByteSize`:

```c#
static double MaxFileSizeMBs = 1.5;

// I need it in KBs!
var kilobytes = MaxFileSizeMBs * 1024; // 1536
```

With `ByteSize`:

```c#
static MaxFileSize = ByteSize.FromMegaBytes(1.5);

// I have it in KBs!
MaxFileSize.KiloBytes;  // 1536
```

`ByeSize` behaves like any other struct backed by a numerical value.

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
new ByteSize(1.5);           // Constructor takes in bytes

// Static Constructors
ByteSize.FromBits(10);       // Bits are whole numbers only
ByteSize.FromBytes(1.5);     // Same as constructor
ByteSize.FromKiloBytes(1.5);
ByteSize.FromMegaBytes(1.5);
ByteSize.FromGigaBytes(1.5);
ByteSize.FromTeraBytes(1.5);
```

### Properties

A `ByteSize` object contains representations in `bits`, `bytes`, `kilobytes`, `megabytes`, `gigabytes`, and `terabytes`.

```c#
var maxFileSize = ByteSize.FromKiloBytes(10);

maxFileSize.Bits;      // 81920
maxFileSize.Bytes;     // 10240
maxFileSize.KiloBytes; // 10
maxFileSize.MegaBytes; // 0.009765625
maxFileSize.GigaBytes; // 9.53674316e-6
maxFileSize.TeraBytes; // 9.31322575e-9
```

A `ByteSize` object also contains two properties that represent the largest metric prefix symbol and value.

```c#
var maxFileSize = ByteSize.FromKiloBytes(10);

maxFileSize.LargestWholeNumberSymbol;  // "KB"
maxFileSize.LargestWholeNumberValue;   // 10
```

### String Representation

All string operations are localized to use the number decimal separator of the culture set in `Thread.CurrentThread.CurrentCulture`.

#### ToString

`ByteSize` comes with a handy `ToString` method that uses the largest metric prefix whose value is greater than or equal to 1.

```c#
ByteSize.FromBits(7).ToString();         // 7 b
ByteSize.FromBits(8).ToString();         // 1 B
ByteSize.FromKiloBytes(.5).ToString();   // 512 B
ByteSize.FromKiloBytes(1000).ToString(); // 1000 KB
ByteSize.FromKiloBytes(1024).ToString(); // 1 MB
ByteSize.FromGigabytes(.5).ToString();   // 512 MB
ByteSize.FromGigabytes(1024).ToString(); // 1 TB
```

#### Formatting

The `ToString` method accepts a single `string` parameter to format the output. The formatter can contain the symbol of the value to display: `b`, `B`, `KB`, `MB`, `GB`, `TB`. The formatter uses the built in [`double.ToString` method](http://msdn.microsoft.com/en-us/library/kfsatb94\(v=vs.110\).aspx). 

The default number format is `0.##` which rounds the number to two decimal places and outputs only `0` if the value is `0`.

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

Like other `TryParse` methods, `ByteSize.TryParse` returns `boolean` value indicating whether or not the parsing was successful. If the value is parsed it is output to the `out` parameter supplied.

```c#
ByteSize output;
ByteSize.TryParse("1.5mb", out output);

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
ByteSize.Parse("1.55 gb");
ByteSize.Parse("1.55 TB");
ByteSize.Parse("1.55 tB");
ByteSize.Parse("1.55 tb");
ByteSize.Parse("1,55 kb"); // de-DE culture
```

#### Author and License

Omar Khudeira ([http://omar.io](http://omar.io))

Copyright (c) 2013-2016 Omar Khudeira. All rights reserved.

Released under MIT License (see LICENSE file).

