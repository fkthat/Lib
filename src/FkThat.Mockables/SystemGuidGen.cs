namespace FkThat.Mockables;

/// <summary>
/// Standard GUID generator.
/// </summary>
public class SystemGuidGen : IGuidGen
{
    ///<inheritdoc/>
    public Guid NewGuid() => Guid.NewGuid();
}
