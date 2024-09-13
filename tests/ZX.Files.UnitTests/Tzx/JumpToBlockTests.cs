﻿using OldBit.ZX.Files.IO;
using OldBit.ZX.Files.Serialization;
using OldBit.ZX.Files.Tzx.Blocks;

namespace OldBit.ZX.Files.UnitTests.Tzx;

public class JumpToBlockTests
{
    [Fact]
    public void JumpToBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new JumpToBlock();

        block.Jump.Should().Be(0);
    }

    [Fact]
    public void JumpToBlock_ShouldDeserializeFromStream()
    {
        using var stream = new MemoryStream([0xE7, 0xFF]);
        var block = new JumpToBlock(new ByteStreamReader(stream));

        block.Jump.Should().Be(-25);
    }

    [Fact]
    public void JumpToBlock_ShouldSerializeToBytes()
    {
        var block = new JumpToBlock { Jump = -1 };

        var result = FileDataSerializer.Serialize(block);

        result.Should().Equal(0x23, 0xFF, 0xFF);
    }
}