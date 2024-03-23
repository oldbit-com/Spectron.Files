﻿using OldBit.ZXTape.IO;
using OldBit.ZXTape.Serialization;
using OldBit.ZXTape.Tzx.Blocks;

namespace OldBit.ZXTape.UnitTests.Tzx;

public class GlueBlockTests
{
    [Fact]
    public void GlueBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new GlueBlock();

        block.Data.Count.Should().Be(9);
    }

    [Fact]
    public void GlueBlock_ShouldDeserializeFromStream()
    {
        using var stream = new MemoryStream([
            0x58, 0x54, 0x61, 0x70, 0x65, 0x21, 0x1A, 0x01, 0x02
        ]);
        var block = new GlueBlock(new ByteStreamReader(stream));

        block.Data.Count.Should().Be(9);
    }

    [Fact]
    public void GlueBlock_ShouldSerializeToBytes()
    {
        var block = new GlueBlock();

        var result = FileDataSerializer.Serialize(block);

        result.Should().Equal(0x5A, 0x58, 0x54, 0x61, 0x70, 0x65, 0x21, 0x1A, 0x01, 0x14);
    }
}