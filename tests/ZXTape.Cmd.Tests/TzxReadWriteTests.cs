using FluentAssertions;
using OldBit.ZXTape.Tzx;
using Xunit.Abstractions;

namespace ZXTape.Cmd.Tests;

public class TzxReadWriteTests(ITestOutputHelper output)
{
    private readonly ITestOutputHelper _output = output;

    [Fact]
    public void CompareLoadedFileWithSavedFile()
    {
        var files = Directory.EnumerateFiles("/Users/voytas/Games/ZX/ZX Spectrum Archive/", "*.tzx", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            var sourceFileBytes = File.ReadAllBytes(file);
            var sourceStream = new MemoryStream(sourceFileBytes);

            var txzFile = TzxFile.Load(sourceStream);
            var memoryStream = new MemoryStream();
            txzFile.Save(memoryStream);

            var savedFileBytes = memoryStream.ToArray();

            savedFileBytes.Should().Equal(sourceFileBytes, file);
        }
    }
}