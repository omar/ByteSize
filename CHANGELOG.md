### 2.1.2 (2024-01-14)
- Add strong name support

### 2.1.1 (2021-11-06)
- Add documentation
- Fix potential System.NullReferenceException in the ToString() methods

### 2.1.0 (2021-09-03)
- Support multiply (*) and divide (/) operators of ByteSize object
- Add ToString IFormattable parameter overload
- Add target for .NET 5

### 2.0.0 (2020-01-14)
**HUGE BREAKING CHANGE**:

By default `ByteSize` now assumes `1 KB == 1000 B` and `1 KiB == 1024 B` to
adhere to the IEC and NIST standards (https://en.wikipedia.org/wiki/Binary_prefix).
In the past `ByteSize` assumed `1 KB == 1024 B`, that means if you're upgrading
from v1, you'll see differences in values.

Other Breaking Changes:

- Renamed property `LargestWholeNumberSymbol` and `LargestWholeNumberValue` to `LargestWholeNumberDecimalSymbol` and `LargestWholeNumberDecimalValue` respectively.
- Drop support for all platforms _except_ `netstandard1.0` and `net45`.

New Features:

- Support for binary and decimal values (e.g. `ByteSize.FromKibiByte` and `ByteSize.FromKiloByte`).
- New constructor that takes a `long` value as the number of bits.
- Support for culture on Parse and TryParse

### 1.3.0 (2017-01-22)
- Add support for subtracting two ByteSize objects.

### 1.2.5 (2017-01-21)
- Support .NET Standard 1.0 and .NET Framework 3.5.

### 1.2.4 (2016-11-25)
- Fix TryParse to not throw exceptions.

### 1.2.3 (2016-11-10)
- Fix ToString result for ByteSize of zero value.

### 1.2.2 (2016-10-28)
- Improve memory footprint of a ByteSize object.

### 1.2.1 (2016-07-23)
- Fix parsing of numbers with a group separator like '1,200.56 mb'.

### 1.2.0 (2016-06-18)
- Localize number decimal separator for Parse and TryParse methods.

### 1.1.3 (2016-06-18)
- Remove extra dependencies when targeting full framework or portable class library.

### 1.1.2 (2016-03-22)
- Use 0 as ByteSize.MinValue and fix unit tests for locals.

### 1.1.1 (2015-12-13)
- Support Core CLR.

### 1.1.0 (2015-08-20)
- Add localization to ToString method.

### 1.0.0 (2015-05-30)
- Change namespace to ByteSizeLib.

### 0.3.0 (2014-10-16)
- Add PetaByte support.

### 0.2.0 (2014-09-27)
- Add Parse/TryParse, fix arithmitic operations, convert to PCL.

### 0.1.0 (2013-11-17)
- Initial Release
