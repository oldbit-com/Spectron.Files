using System.Reflection;
using OldBit.Spectron.Files.Rzx;

namespace OldBit.Spectron.Files.Tests.Rzx;

public class RzxFileTests
{
    [Fact]
    public void RzxFile_ShouldLoad()
    {
        using var file = LoadTestFile("manic.rzx");
        var rzxFile = RzxFile.Load(file);

        rzxFile.Creator?.CreatorName.ShouldBe("SPIN");
        rzxFile.Creator?.MajorVersion.ShouldBe(0);
        rzxFile.Creator?.MinorVersion.ShouldBe(3);

        rzxFile.Snapshots.ShouldHaveSingleItem();
        rzxFile.Snapshots[0].Extension.ShouldBe("Z80");
    }

    private static FileStream LoadTestFile(string fileName)
    {
        var location = typeof(RzxFileTests).GetTypeInfo().Assembly.Location;
        var dir = Path.GetDirectoryName(location) ?? throw new InvalidOperationException();
        var path =  Path.Combine(dir, "TestFiles", fileName);

        return File.OpenRead(path);
    }
}