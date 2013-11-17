# ByteSize 

`ByteSize` is a utility class that makes byte size representation in code easier by removing ambiguity of the value being represented.

`ByteSize` is to bytes what `System.TimeSpan` is to time.

## Usage 

Without `ByteSize`:

```
public static readonly double MaxFileSizeMBs = 1.5;

// I need it in KBs!
var kilobytes = MaxFileSizeMBs * 1024;
```

With `ByteSize`:

```
public static readonly MaxFileSize = ByteSize.FromMegaBytes(1.5);

// I have it in KBs!
MaxFileSize.KiloBytes;
```

`ByeSize` behaves like any other struct backed by a numerical value.

```
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

```
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

```
var maxFileSize = ByteSize.FromKiloBytes(10);

maxFileSize.Bits;      // 81920
maxFileSize.Bytes;     // 10240
maxFileSize.KiloBytes; // 10
maxFileSize.MegaBytes; // 0.009765625
maxFileSize.GigaBytes; // 9.53674316e-6
maxFileSize.TeraBytes; // 9.31322575e-9
```

A `ByteSize` object also contains two properties that represent the largest metric prefix symbol and value.

```
var maxFileSize = ByteSize.FromKiloBytes(10);

maxFileSize.LargestWholeNumberSymbol;  // "KB"
maxFileSize.LargestWholeNumberValue;   // 10
```

### String Representation

`ByteSize` comes with a handy `ToString` method that uses the largest metric prefix whose value is greater than or equal to 1.

```
ByteSize.FromBits(7).ToString();         // 7 b
ByteSize.FromBits(8).ToString();         // 1 B
ByteSize.FromKiloBytes(.5).ToString();   // 512 B
ByteSize.FromKiloBytes(1000).ToString(); // 1000 KB
ByteSize.FromKiloBytes(1024).ToString(); // 1 MB
ByteSize.FromGigabytes(.5).ToString();   // 512 MB
ByteSize.FromGigabytes(1024).ToString(); // 1 TB
```

#### String Formatting

The `ToString` method accepts a single `string` parameter to format the output. The formatter can contain the symbol of the value to display: `b`, `B`, `KB`, `MB`, `GB`, `TB`. The formatter uses the built in [`double.ToString` method](http://msdn.microsoft.com/en-us/library/kfsatb94\(v=vs.110\).aspx). The default number format is `#.##` which rounds the number to two decimal places.

You can include symbol and number formats.

```
var b = ByteSize.FromKiloBytes(10.505);

// Default number format is #.##
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
b.ToString("#.## B");     // 10757.12 B```

#### TODO

- Add `Parse` and `TryParse` methods

#### Author and License

Omar Khudeira ([http://omar.io](http://omar.io))

Copyright (c) 2013 Omar Khudeira. All rights reserved.

Released under [Apache License 2.0](http://www.apache.org/licenses/LICENSE-2.0).

