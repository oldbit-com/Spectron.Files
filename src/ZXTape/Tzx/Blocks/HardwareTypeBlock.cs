﻿using OldBit.ZXTape.IO;
using OldBit.ZXTape.Tzx.Serialization;

namespace OldBit.ZXTape.Tzx.Blocks;

/// <summary>
/// Represents the 'Hardware Type' block.
/// </summary>
public class HardwareTypeBlock : IBlock
{
    /// <summary>
    /// Gets the block ID.
    /// </summary>
    [BlockProperty(Order = 0)]
    public byte BlockId => BlockCode.HardwareType;

    /// <summary>
    /// Helper property needed by the serialization.
    /// Gets the number of machines and hardware types for which info is supplied
    /// </summary>
    [BlockProperty(Order = 1)]
    private byte Count => (byte)Infos.Count;

    /// <summary>
    /// Gets or sets the array of machines and hardware.
    /// </summary>
    [BlockProperty(Order = 2)]
    public List<HardwareInfo> Infos { get; set; } = [];

    /// <summary>
    /// Creates a new instance of the 'Hardware Type' block.
    /// </summary>
    public HardwareTypeBlock()
    {
    }

    /// <summary>
    /// Creates a new instance of the 'Hardware Type' block using the byte reader.
    /// </summary>
    /// <param name="reader">A byte reader.</param>
    internal HardwareTypeBlock(ByteStreamReader reader)
    {
        var count = reader.ReadByte();
        for (var i = 0; i < count; i++)
        {
            Infos.Add(new HardwareInfo(reader));
        }
    }

    /// <summary>
    /// Represents the HWINFO structure.
    /// </summary>
    public class HardwareInfo
    {
        /// <summary>
        /// Gets or sets the hardware type.
        /// </summary>
        [BlockProperty(Order = 0)]
        public byte Type { get; set; }

        /// <summary>
        /// Gets or sets the hardware id.
        /// </summary>
        [BlockProperty(Order = 1)]
        public byte Id { get; set; }

        /// <summary>
        /// Gets or sets the hardware information.
        /// </summary>
        [BlockProperty(Order = 2)]
        public byte Info { get; set; }

        /// <summary>
        /// Creates a new instance of the 'HWINFO' structure.
        /// </summary>
        public HardwareInfo()
        {
        }

        /// <summary>
        /// Creates a new instance of the 'HWINFO' structure using the byte reader..
        /// </summary>
        /// <param name="reader">A byte reader.</param>
        internal HardwareInfo(ByteStreamReader reader)
        {
            Type = reader.ReadByte();
            Id = reader.ReadByte();
            Info = reader.ReadByte();
        }
    }
}