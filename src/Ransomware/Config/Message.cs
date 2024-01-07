// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Message.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The message class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ransomware.Config;

/// <summary>
/// The message class.
/// </summary>
[Serializable]
public sealed record class Message
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the caption.
    /// </summary>
    public string Caption { get; init; } = string.Empty;
}