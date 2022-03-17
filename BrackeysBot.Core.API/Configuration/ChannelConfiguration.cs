﻿using System.Text.Json.Serialization;

namespace BrackeysBot.Core.API.Configuration;

/// <summary>
///     Represents a channel configuration.
/// </summary>
public sealed class ChannelConfiguration
{
    /// <summary>
    ///     Gets or sets the ID of the staff log channel.
    /// </summary>
    /// <value>The ID of the staff log channel.</value>
    [JsonPropertyName("logChannelId")]
    public ulong LogChannelId { get; set; }
}
