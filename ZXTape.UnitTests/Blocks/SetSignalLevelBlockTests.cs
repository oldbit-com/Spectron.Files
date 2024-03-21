﻿using OldBit.SpeccyLib.Blocks;
using OldBit.ZXTape.Reader;
using OldBit.ZXTape.Tzx.Blocks;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.UnitTests.Blocks;

public class SetSignalLevelBlockTests
{
    [Fact]
    public void SetSignalLevelBlock_NewInstance_ShouldCreateWithDefaultValues()
    {
        var block = new SetSignalLevelBlock();

        block.Level.Should().Be(SignalLevel.Low);
    }

    [Fact]
    public void SetSignalLevelBlock_ShouldDeserializeFromStream()
    {
        using var stream = new MemoryStream([0x01, 0x00, 0x00, 0x00, 0x01]);
        var block = new SetSignalLevelBlock(new ByteStreamReader(stream));

        block.Level.Should().Be(SignalLevel.High);
    }

    [Fact]
    public void SetSignalLevelBlock_ShouldSerializeToBytes()
    {
        var block = new SetSignalLevelBlock{ Level = SignalLevel.High };

        var serializer = new BlockSerializer();
        var result = serializer.Serialize(block);

        result.Should().Equal(0x01, 0x00, 0x00, 0x00, 0x01);
    }
}