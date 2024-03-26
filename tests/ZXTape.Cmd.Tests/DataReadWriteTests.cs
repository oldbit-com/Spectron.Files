using FluentAssertions;
using OldBit.ZXTape.Sna;
using OldBit.ZXTape.Tap;
using OldBit.ZXTape.Tzx;
using OldBit.ZXTape.Z80;
using Xunit.Abstractions;

namespace ZXTape.Cmd.Tests;

public class DataReadWriteTests(ITestOutputHelper output)
{
    [Fact(Skip = "Only for manual testing")]
    public void CompareTzxLoadedFileWithSavedFile()
    {
        var count = 0;
        var files = Directory.EnumerateFiles(TestFilesPath, "*.tzx", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            var sourceFileBytes = File.ReadAllBytes(file);
            var sourceStream = new MemoryStream(sourceFileBytes);

            var txzFile = TzxFile.Load(sourceStream);
            var memoryStream = new MemoryStream();
            txzFile.Save(memoryStream);

            var savedFileBytes = memoryStream.ToArray();

            savedFileBytes.Should().Equal(sourceFileBytes, file);

            count += 1;
        }

        output.WriteLine("Total files: " + count);
    }

    [Fact(Skip = "Only for manual testing")]
    public void CompareTapLoadedFileWithSavedFile()
    {
        var count = 0;
        var files = Directory.EnumerateFiles(TestFilesPath, "*.tap", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            var sourceFileBytes = File.ReadAllBytes(file);
            var sourceStream = new MemoryStream(sourceFileBytes);

            var tapFile = TapFile.Load(sourceStream);
            var memoryStream = new MemoryStream();
            tapFile.Save(memoryStream);

            var savedFileBytes = memoryStream.ToArray();

            savedFileBytes.Should().Equal(sourceFileBytes, file);

            count += 1;
        }

        output.WriteLine("Total files: " + count);
    }

    [Fact(Skip = "Only for manual testing")]
    public void CompareSnaLoadedFileWithSavedFile()
    {
        var count = 0;
        var files = Directory.EnumerateFiles(TestFilesPath, "*.sna", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            var sourceFileBytes = File.ReadAllBytes(file);
            var sourceStream = new MemoryStream(sourceFileBytes);

            var snaFile = SnaFile.Load(sourceStream);
            var memoryStream = new MemoryStream();
            snaFile.Save(memoryStream);

            var savedFileBytes = memoryStream.ToArray();

            savedFileBytes.Should().Equal(sourceFileBytes, file);

            count += 1;
        }

        output.WriteLine("Total files: " + count);
    }

    //[Fact(Skip = "Only for manual testing")]
    [Fact]
    public void CompareZ80LoadedFileWithSavedFile()
    {
        var count = 0;
        var files = Directory.EnumerateFiles(TestFilesPath, "*.z80", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            var sourceFileBytes = File.ReadAllBytes(file);
            var sourceStream = new MemoryStream(sourceFileBytes);

            var z80File = Z80File.Load(sourceStream);
            var memoryStream = new MemoryStream();
            z80File.Save(memoryStream);

            var savedFileBytes = memoryStream.ToArray();

            savedFileBytes.Should().Equal(sourceFileBytes, file);

            count += 1;
        }

        output.WriteLine("Total files: " + count);
    }

    private static string TestFilesPath =>
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "Games/ZX/ZX Spectrum Archive/");
}