﻿using OldBit.ZXTape.IO;
using OldBit.ZXTape.Serialization;
using OldBit.ZXTape.Tzx.Blocks;

namespace OldBit.ZXTape.UnitTests.Tzx;

public class LoopStartBlockTests
{
    [Fact]
    public void LoopStartBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new LoopStartBlock();

        block.Count.Should().Be(0);
    }

    [Fact]
    public void LoopStartBlock_ShouldDeserializeFromStream()
    {
        var bytes = new byte[] { 0x05, 0x00 };
        using var stream = new MemoryStream(bytes);
        var block = new LoopStartBlock(new ByteStreamReader(stream));

        block.Count.Should().Be(5);
    }

    [Fact]
    public void LoopStartBlock_ShouldSerializeToBytes()
    {
        var block = new LoopStartBlock{ Count = 2 };

        var result = FileDataSerializer.Serialize(block);

        result.Should().Equal(0x24, 0x02, 0x00);
    }
}