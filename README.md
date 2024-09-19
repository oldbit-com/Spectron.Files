# ZX Files

This a dotnet library that allows reading and writing different file formats used by the ZX Spectrum emulators.

I have created this library to be able to read and write different file formats used by the ZX Spectrum emulator
I am currently working on as a fun project.

## Currently supported formats:
- TAP
- TZX
- SNA
- Z80
- SZX (some obscure / least useful blocks are not supported yet)

## Usage

### Reading a TAP file

Using file path:
```csharp
var tapFile = TapFile.Load(filePath);
````

or from a stream:
```csharp
using var stream = File.OpenRead(fileName);
var tapFile = TapFile.Load(stream);
````

This is still work in progress, although the library is already usable, but some breaking changes may still happen.
