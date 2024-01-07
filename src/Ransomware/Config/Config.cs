// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Config.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The config class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ransomware.Config;

/// <summary>
/// The config class.
/// </summary>
[Serializable]
public sealed record class Config
{
    /// <summary>
    /// Gets or sets the messages.
    /// </summary>
    public List<Message> Messages { get; set; } = new();
}