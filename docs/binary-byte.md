# Binary Byte

ByteSize started off with:

- 1 kilobyte = 1024 bytes
- 2 letter abbreviation (KB, GB etc.)

See why [here](https://omar.io/2017/01/16/when-technically-right-is-wrong-kilobytes.html). 

Since then, there have been a few requests to support the [IEC binary prefix](https://en.wikipedia.org/wiki/Binary_prefix#kibi),
see [issue#1](https://github.com/omar/ByteSize/issues/1) and [Humanizr/Humanizer/issues/592](https://github.com/Humanizr/Humanizer/issues/592).
This document will include the rational for the design of implementing the IEC standard.

## Research

| Vendor             | Abbreviation   | Ratio    | [IEC Standard] | Source
| ---                | ---            | ---      | ---            | ---
| ByteSize           | KB, GB, etc.   | 1 = 1024 | No             |
| GCP                | KB, GB, etc.   | 1 = 1024 | No             | https://cloud.google.com/storage/pricing
| AWS                | KB, GB, etc.   | 1 = 1024 | No             | https://aws.amazon.com/ebs/features/
| AWS Glossary       | KB, GB, etc.   | 1 = 1000 | Yes            | https://docs.aws.amazon.com/general/latest/gr/glos-chap.html
| AWS Glossary       | KiB, GiB, etc. | 1 = 1024 | Yes            | https://docs.aws.amazon.com/general/latest/gr/glos-chap.html
| Azure (RAM)        | KiB, GiB, etc. | 1 = 1024 | Yes            | https://azure.microsoft.com/en-gb/blog/largest-vm-in-the-cloud/
| Azure (Disk)       | KB, GB, etc.   | 1 = 1000 | Yes            | https://azure.microsoft.com/en-gb/blog/largest-vm-in-the-cloud/
| Mac OS X Leopard + | KB, GB, etc.   | 1 = 1000 | Yes            | https://support.apple.com/en-us/HT201402
| iOS 11 +           | KB, GB, etc.   | 1 = 1000 | Yes            | https://support.apple.com/en-us/HT201402

In summary, developers are _attempting_ to adhere to the IEC standard by either:

- Changing the abbreviation and ratio to match the IEC binary standard, or;
- Changing the ratio they've used in the past to match the decimal values

[IEC Standard]: https://en.wikipedia.org/wiki/Binary_prefix#kibi

## Implementation Considerations

1. Two structs that represents the decimal (`DecimalByteSize`) and binary 
   values (`BinaryByteSize`).
    - Pros
        - Clear separation of what's being represented by the object.
        - Solve the namespace collision that forced the `ByteSize` namespace.
    - Cons
        - Swapping between binary and decimal representation requires two 
          objects. Maybe can provide a simple way to go from one to the other:
          `BinaryByteSize.FromDecimalByteSize` and
          `DecimalByteSize.FromBinaryByteSize`. No idea how valuable or needed
          this would be though.
        - Breaking change since `BinaryByteSize` has the same ratio as the
          original `ByteSize` class, but not the text representation.
          (See "Backwards Compatibility Note" below).
    - Backwards Compatibility Note
        - v1.x of ByteSize could ship with two additional structs 
          `DecimalByteSize` and `BinaryByteSize` but keep `ByteSize` with it's
          current implementation.
        - v2.0 of ByteSize could break compatiblity and remove the `ByteSize`
          struct in favor of `DecimalByteSize` and `BinaryByteSize`.

2. Single struct
    - Flat properties (e.g. `value.KiloByte` and `value.KibiByte`).
        - Pros
            - Single object that allows use of both standards.
        - Cons
            - Autocomplete lists will have twice as many methods and properties
    - Nested properties (e.g. `value.Decimal.KiloByte` and `value.Binary.KibiByte`).
        - Pros
            - Single object that allows use of both standards.
        - Cons
            - Autocomplete lists will have twice as many methods and properties
