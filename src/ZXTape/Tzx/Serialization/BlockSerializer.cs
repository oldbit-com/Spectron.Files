using System.Collections;
using System.Reflection;
using OldBit.ZXTape.Extensions;

namespace OldBit.ZXTape.Tzx.Serialization;

/// <summary>
/// Implements generic block data serializer.
/// </summary>
internal class BlockSerializer
{
    /// <summary>
    /// Serializes the block as an array of bytes.
    /// </summary>
    /// <param name="block">The block to serialize.</param>
    /// <returns>An array of bytes representing the serialized block.</returns>
    internal IEnumerable<byte> Serialize(object block)
    {
        var propsAndAttrs =
            block.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Select(p => (Property: p, Atttribute: p.GetCustomAttributes(typeof(BlockPropertyAttribute)).FirstOrDefault() as BlockPropertyAttribute))
            .Where(p => p.Atttribute != null)
            .OrderBy(p => p.Atttribute!.Order);

        var result = new List<byte>();
        foreach (var propAttr in propsAndAttrs)
        {
            if (propAttr.Property.PropertyType == typeof(byte))
            {
                var value = (byte)propAttr.Property.GetValue(block)!;
                result.Add(value);
            }
            else if (propAttr.Property.PropertyType == typeof(Word))
            {
                var value = (Word)propAttr.Property.GetValue(block)!;
                result.Add((byte)value);
                result.Add((byte)(value >> 8));
            }
            else if (propAttr.Property.PropertyType == typeof(short))
            {
                var value = (short)propAttr.Property.GetValue(block)!;
                result.Add((byte)value);
                result.Add((byte)(value >> 8));
            }
            else if (propAttr.Property.PropertyType == typeof(string))
            {
                var value = (string)propAttr.Property.GetValue(block)!;
                if (propAttr.Atttribute!.Size != 0)
                {
                    if (value.Length > propAttr.Atttribute.Size)
                    {
                        value = value[..propAttr.Atttribute.Size];
                    }
                    else if (value.Length < propAttr.Atttribute.Size)
                    {
                        value = value.PadRight(propAttr.Atttribute.Size);
                    }
                }

                result.AddRange(value.ToAsciiBytes());
            }
            else if (propAttr.Property.PropertyType == typeof(int))
            {
                var value = (int)propAttr.Property.GetValue(block)!;
                for (var i = 0; i < 4; i++)
                {
                    result.Add((byte)(value >> (i * 8)));
                    if (propAttr.Atttribute!.Size != 0 && i == propAttr.Atttribute!.Size - 1)
                    {
                        break;
                    }
                }
            }
            else if (propAttr.Property.PropertyType == typeof(DWord))
            {
                var value = (DWord)propAttr.Property.GetValue(block)!;
                for (var i = 0; i < 4; i++)
                {
                    result.Add((byte)(value >> (i * 8)));
                    if (propAttr.Atttribute!.Size != 0 && i == propAttr.Atttribute!.Size - 1)
                    {
                        break;
                    }
                }
            }
            else if (propAttr.Property.PropertyType.IsEnum)
            {
                var value = (Enum)propAttr.Property.GetValue(block)!;
                result.Add(Convert.ToByte(value));
            }
            else if (propAttr.Property.PropertyType.IsEnumerable())
            {
                var values = (IEnumerable)propAttr.Property.GetValue(block)!;
                foreach (var value in values)
                {
                    result.AddRange(value.GetType().IsPrimitive ?
                        SerializePrimitiveType(value) :
                        Serialize(value));
                }
            }
            else
            {
                throw new NotSupportedException($"The type '{propAttr.Property.PropertyType.Name}' is not supported by the serializer.");
            }
        }

        return result.ToArray();
    }

    private static IEnumerable<byte> SerializePrimitiveType(object value)
    {
        var type = value.GetType();
        if (type == typeof(byte))
        {
            yield return Convert.ToByte(value);
        }
        else if (type == typeof(short))
        {
            yield return (byte)(short)value;
            yield return (byte)((short)value >> 8);
        }
        else if (type == typeof(ushort))
        {
            yield return (byte)(ushort)value;
            yield return (byte)((ushort)value >> 8);
        }
        else
        {
            throw new NotSupportedException($"The type '{type.Name}' is not supported by the serializer.");
        }
    }
}