// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Import.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The import class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ransomware.Config;

/// <summary>
/// The import class.
/// </summary>
public static class Import
{
    /// <summary>
    /// Loads the configuration from a XML file.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <returns>The <see cref="Config"/> class.</returns>
    public static Config? LoadConfigFromXmlFile(string fileName)
    {
        var xDocument = XDocument.Load(fileName);
        return CreateObjectsFromString<Config?>(xDocument);
    }

    /// <summary>
    /// Creates an object from a string.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <param name="xDocument">The X document.</param>
    /// <returns>The new configuration objects.</returns>
    private static T? CreateObjectsFromString<T>(XDocument xDocument)
    {
        var xmlSerializer = new XmlSerializer(typeof(T));
        return (T?) xmlSerializer.Deserialize(new StringReader(xDocument.ToString()));
    }
}