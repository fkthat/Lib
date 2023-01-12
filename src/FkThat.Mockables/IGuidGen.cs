namespace FkThat.Mockables;

/// <summary>
/// GUID generator.
/// </summary>
public interface IGuidGen
{
    /// <summary>
    /// Initializes a new instance of the System.Guid structure.
    /// </summary>
    /// <returns>A new GUID object.</returns>
    Guid NewGuid();
}
