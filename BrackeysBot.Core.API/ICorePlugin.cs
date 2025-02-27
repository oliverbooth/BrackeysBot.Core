﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BrackeysBot.API.Plugins;
using BrackeysBot.Core.API.Configuration;
using DSharpPlus.Entities;

[assembly: InternalsVisibleTo("BrackeysBot.Core")]

namespace BrackeysBot.Core.API;

/// <summary>
///     Represents the core plugin for BrackeysBot.
/// </summary>
public interface ICorePlugin : IPlugin
{
    /// <summary>
    ///     Gets the current instance of the core plugin.
    /// </summary>
    /// <value>The current instance of the core plugin.</value>
    static ICorePlugin Current { get; internal set; } = null!;

    /// <summary>
    ///     Gets the permission level of a user in a specified guild.
    /// </summary>
    /// <param name="user">The user whose permission level to retrieve.</param>
    /// <param name="guild">The guild whose roles to compare with.</param>
    /// <returns>
    ///     <para>The user's <see cref="PermissionLevel" />, or <see cref="PermissionLevel.Default" /> if:</para>
    ///     <ul>
    ///         <li>The guild has no configuration.</li>
    ///         <li>or the user is not currently in the guild.</li>
    ///     </ul>
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     <para><paramref name="user" /> is <see langword="null" />.</para>
    ///     -or-
    ///     <para><paramref name="guild" /> is <see langword="null" />.</para>
    /// </exception>
    PermissionLevel GetPermissionLevel(DiscordUser user, DiscordGuild guild);

    /// <summary>
    ///     Logs an embed to the staff log channel.
    /// </summary>
    /// <param name="guild">The guild in which to log.</param>
    /// <param name="embed">The embed to log.</param>
    /// <param name="notificationOptions">
    ///     Optional. The staff notification options. Defaults to <see cref="StaffNotificationOptions.None" />.
    /// </param>
    Task LogAsync(DiscordGuild guild, DiscordEmbed embed,
        StaffNotificationOptions notificationOptions = StaffNotificationOptions.None);

    /// <summary>
    ///     Determines if a user is a higher permission level than another user.
    /// </summary>
    /// <param name="user">The user whose hierarchy status to retrieve.</param>
    /// <param name="other">The user whose hierarchy status with which to compare.</param>
    /// <param name="guild">The guild whose roles to compare with.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="user" /> is considered a higher level than <paramref name="other" />;
    ///     otherwise, <see langword="false" />.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     <para><paramref name="user" /> is <see langword="null" />.</para>
    ///     -or-
    ///     <para><paramref name="other" /> is <see langword="null" />.</para>
    ///     -or-
    ///     <para><paramref name="guild" /> is <see langword="null" />.</para>
    /// </exception>
    bool IsHigherLevelThan(DiscordUser user, DiscordUser other, DiscordGuild guild);

    /// <summary>
    ///     Determines if the member is considered a staff member.
    /// </summary>
    /// <param name="user">The user whose permission level to retrieve.</param>
    /// <param name="guild">The guild whose roles to compare with.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="user" /> is considered a staff member; otherwise, <see langword="false" />.
    /// </returns>
    bool IsStaffMember(DiscordUser user, DiscordGuild guild);

    /// <summary>
    ///     Registers a field to be displayed with the <c>userinfo</c> command.
    /// </summary>
    /// <param name="builderEvaluator">The field builder.</param>
    void RegisterUserInfoField(Action<UserInfoFieldBuilder> builderEvaluator);

    /// <summary>
    ///     Registers a field to be displayed with the <c>userinfo</c> command.
    /// </summary>
    /// <param name="builder">The field builder.</param>
    void RegisterUserInfoField(UserInfoFieldBuilder builder);

    /// <summary>
    ///     Retrieves the configuration for a specified guild. A return value indicates whether the retrieval was successful.
    /// </summary>
    /// <param name="guild">The guild whose configuration to retrieve.</param>
    /// <param name="configuration">
    ///     When this method returns, contains the configuration for <paramref name="guild" />, or <see langword="null" /> if the
    ///     retrieval failed.
    /// </param>
    /// <returns>
    ///     <see langword="true" /> if the configuration for <paramref name="guild" /> was retrieved successfully; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    bool TryGetGuildConfiguration(DiscordGuild guild, [NotNullWhen(true)] out GuildConfiguration? configuration);
}
