# Spectron Files

This a dotnet library that allows reading and writing different file formats used by the ZX Spectrum emulators.

I have created this library to be able to read and write different file formats used by the ZX Spectrum emulator
I am currently working on as a fun project.

## Currently supported formats:
- TAP
- TZX
- SNA
- Z80
- SZX (some obscure / least useful blocks are not implemented)

## Usage

### Reading/writing a TAP file

Using file path:
```csharp
var tap = TapFile.Load(filePath);
tap.Save(filePath);
````

or from a stream:
```csharp
using var stream = File.OpenRead(fileName);
var tap = TapFile.Load(stream);
tap.Save(filePath);
````

### Reading a TZX file
