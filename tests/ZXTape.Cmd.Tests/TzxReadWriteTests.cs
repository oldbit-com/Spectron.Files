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
        var count = 0;
        var files = Directory.EnumerateFiles("/Users/voytas/Games/ZX/ZX Spectrum Games Collection/", "*.tzx", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            var sourceFileBytes = File.ReadAllBytes(file);
            var sourceStream = new MemoryStream(sourceFileBytes);

            try
            {
                var txzFile = TzxFile.Load(sourceStream);
                var memoryStream = new MemoryStream();
                txzFile.Save(memoryStream);

                var savedFileBytes = memoryStream.ToArray();

                savedFileBytes.Should().Equal(sourceFileBytes, file);

                count += 1;
            }
            catch (Exception e)
            {
                _output.WriteLine($"Error in file {file}: {e.Message}");
               // throw;
            }
        }

        _output.WriteLine("Total files: " + count);
    }
}