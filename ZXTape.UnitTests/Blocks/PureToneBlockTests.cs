﻿using OldBit.ZXTape.Reader;
using OldBit.ZXTape.Tzx.Blocks;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.UnitTests.Blocks;

public class PureToneBlockTests
{

    [Fact]
    public void PureToneBlock_NewInstance_IsCreatedWithDefaults()
    {
        var block = new PureToneBlock();

        block.PulseLength.Should().Be(0);
        block.PulseCount.Should().Be(0);
    }

    [Fact]
    public void PureToneBlock_IsDeserializedFromStream()
    {
        using var stream = new MemoryStream([0x01, 0x02, 0x02, 0x04]);
        var block = new PureToneBlock(new ByteStreamReader(stream));

        block.PulseLength.Should().Be(0x0201);
        block.PulseCount.Should().Be(0x0402);
    }

    [Fact]
    public void PureToneBlock_IsSerializedToBytes()
    {
        var block = new PureToneBlock { PulseCount = 10, PulseLength = 1923 };

        var serializer = new BlockSerializer();
        var result = serializer.Serialize(block);

        result.Should().Equal(0x83, 0x07, 0x0A, 0x00);
    }
}
