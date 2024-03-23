using FluentAssertions;
using OldBit.ZXTape.Tzx;
using Xunit.Abstractions;

namespace ZXTape.Cmd.Tests;

public class TzxReadWriteTests(ITestOutputHelper output)
{
    [Fact(Skip = "Only for manual testing")]
    public void CompareLoadedFileWithSavedFile()
    {
        var count = 0;
        var files = Directory.EnumerateFiles("$HOME$/Games/ZX/ZX Spectrum Games Collection/", "*.tzx", SearchOption.AllDirectories);
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
                output.WriteLine($"Error in file {file}: {e.Message}");
               // throw;
            }
        }

        output.WriteLine("Total files: " + count);
    }
}