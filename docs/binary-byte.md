# Binary Byte

- Unit of Measurement

## Unit of Measurement

ByteSize started off with:

- 1 kilobyte = 1000 bytes
- 2 letter units (KB, GB etc.)

## Research

| Vendor             | Abbreviation   | Ratio    | [IEC Standard] | Source
| ---                | ---            | ---      | ---            | ---
| GCP                | KB, GB, etc.   | 1 = 1024 | No             | https://cloud.google.com/storage/pricing
| AWS                | KB, GB, etc.   | 1 = 1024 | No             | https://aws.amazon.com/ebs/features/
| AWS Glossary       | KB, GB, etc.   | 1 = 1000 | Yes            | https://docs.aws.amazon.com/general/latest/gr/glos-chap.html
| AWS Glossary       | KiB, GiB, etc. | 1 = 1024 | Yes            | https://docs.aws.amazon.com/general/latest/gr/glos-chap.html
| Azure (RAM)        | KiB, GiB, etc. | 1 = 1024 | Yes            | https://azure.microsoft.com/en-gb/blog/largest-vm-in-the-cloud/
| Azure (Disk)       | KB, GB, etc.   | 1 = 1000 | Yes            | https://azure.microsoft.com/en-gb/blog/largest-vm-in-the-cloud/
| Mac OS X Leopard + | KB, GB, etc.   | 1 = 1000 | Yes            | https://support.apple.com/en-us/HT201402
| iOS 11 +           | KB, GB, etc.   | 1 = 1000 | Yes            | https://support.apple.com/en-us/HT201402

In summary, developers are attempting to adhere to the IEC standard by either:

- Changing the abbreviation and ratio to match the IEC binary standard, or;
- Changing the ratio they've used in the past to match the decimal values


[IEC Standard]: https://en.wikipedia.org/wiki/Binary_prefix#kibi

## Conclusion

Based on the limited research I've done, there seems to be a trend of adhereing
to the IEC standards. Given that, I'm choosing to also adhere to those standards
and will implement the binary byte by introducing a new `BinaryByteSize` class
that implements the IEC standard. 

The `ByteSize` class will remain and represent the decimal representation.