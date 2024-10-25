﻿using OldBit.ZX.Files.IO;
using OldBit.ZX.Files.Serialization;
using OldBit.ZX.Files.Tzx.Blocks;

namespace OldBit.ZX.Files.Tests.Tzx;

public class StandardSpeedDataBlockTests
{
    [Fact]
    public void StandardSpeedDataBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new StandardSpeedDataBlock();

        block.PauseDuration.Should().Be(0);
        block.Data.Count().Should().Be(0);
    }

    [Fact]
    public void StandardSpeedDataBlock_ShouldDeserializeFromStream()
    {
        var data = new byte[]
        {
            0x01, 0x02, 0x13, 0x00, 0x00, 0x03, 0x52, 0x4F,
            0x4D, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
            0x02, 0x00, 0x00, 0x00, 0x00, 0x80, 0xF1
        };
        using var stream = new MemoryStream(data);
        var block = new StandardSpeedDataBlock(new ByteStreamReader(stream));

        block.PauseDuration.Should().Be(0x0201);
        block.Data.Should().Equal(data[4..]);
    }

    [Fact]
    public void StandardSpeedDataBlock_ShouldSerializeToBytes()
    {
        var block = new StandardSpeedDataBlock
        {
            PauseDuration = 1000,
            Data = [0x01, 0x02, 0x03, 0x04]
        };

        var result = FileDataSerializer.Serialize(block);

        result.Should().Equal(0x10, 0xE8, 0x03, 0x04, 0x00, 0x01, 0x02, 0x03, 0x04);
    }
}