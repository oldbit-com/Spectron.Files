namespace OldBit.Spectron.Files.Tzx;

public static class ArchiveInfo
{
    public const byte Title = 0x00;
    public const byte Publisher = 0x01;
    public const byte Author = 0x02;
    public const byte Year = 0x03;
    public const byte Language = 0x04;
    public const byte Type = 0x05;
    public const byte Price = 0x06;
    public const byte Protection = 0x07;
    public const byte Origin = 0x08;
    public const byte Comments = 0xFF;

    /// <summary>
    /// Gets the display name of the specified archive info property.
    /// </summary>
    /// <param name="id">The id of archive info property.</param>
    /// <returns>The display name of the archive info property.</returns>
    public static string GetName(byte id) => id switch
    {
        Title => "Title",
        Publisher => "Publisher",
        Author => "Author",
        Year => "Year",
        Language => "Language",
        Type => "Type",
        Price => "Price",
        Protection => "Protection",
        Origin => "Origin",
        Comments => "Comments",
        _ => string.Empty
    };
}
