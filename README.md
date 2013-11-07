# ByteSize 

`ByteSize` is a utility class that makes byte size representation in code easier by removing ambiguity of the value being represented.

`ByteSize` is to bytes what `System.TimeSpan` is to time.

#### Usage 

Without `ByteSize`:

```
public static readonly double MaxFileSizeMBs = 1.5;

// I need it in KBs!
var kilobytes = MaxFileSizeMBs / 1024;
```

With `ByteSize`:

```
public static readonly MaxFileSize = ByteSize.FromMegaBytes(1.5);

// I have it in KBs!
MaxFileSize.KiloBytes;
```

You can create a `ByteSize` object from `bits`, `bytes`, `kilobytes`, `megabytes`, and `gigabytes`.

```
ByteSize.FromBits(10);
ByteSize.FromBytes(1.5);
ByteSize.FromKiloBytes(1.5);
ByteSize.FromMegaBytes(1.5);
ByteSize.FromGigaBytes(1.5);
```

A `ByteSize` object contains byte representation in `bits`, `bytes`, `kilobytes`, `megabytes`, and `gigabytes`.

```
var maxFileSize = ByteSize.FromKiloBytes(10);

maxFileSize.Bits;      // 81920
maxFileSize.Bytes;     // 10240
maxFileSize.KiloBytes; // 10
maxFileSize.MegaBytes; // 0.009765625
maxFileSize.GigaBytes; // 9.53674316e-6
```

`ByteSize` comes with a handy `ToString` method that uses the largest metric prefix whose value is greater than or equal to 1.

```
ByteSize.FromBits(7).ToString();         // 7 b
ByteSize.FromBits(8).ToString();         // 1 B
ByteSize.FromKiloBytes(.5).ToString();   // 512 B
ByteSize.FromKiloBytes(1000).ToString(); // 1000 KB
ByteSize.FromKiloBytes(1024).ToString(); // 1 MB
ByteSize.FromGigabytes(.5).ToString();   // 512 MB
```

#### Author and License

Omar Khudeira ([http://omar.io](http://omar.io))

Copyright (c) 2013 Omar Khudeira. All rights reserved.

Released under [Apache License 2.0](http://www.apache.org/licenses/LICENSE-2.0).

