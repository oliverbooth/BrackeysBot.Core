using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using BrackeysBot.API.Extensions;
using BrackeysBot.API.Plugins;
using BrackeysBot.Core.API;
using BrackeysBot.Core.API.Configuration;
using BrackeysBot.Core.API.Extensions;
using BrackeysBot.Core.Commands;
using BrackeysBot.Core.Data;
using BrackeysBot.Core.Services;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;
using PermissionLevel = BrackeysBot.Core.API.PermissionLevel;

namespace BrackeysBot.Core;

/// <summary>
///     Represents the core plugin for BrackeysBot.
/// </summary>
[Plugin("BrackeysBot.Core")]
[PluginDescription("The core plugin for BrackeysBot.")]
[PluginIntents(DiscordIntents.AllUnprivileged)]
internal sealed class CorePlugin : MonoPlugin, ICorePlugin
{
    private ConfigurationService _configurationService = null!;
    private DiscordLogService _discordLogService = null!;
    private UserInfoService _userInfoService = null!;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CorePlugin" /> class.
    /// </summary>
    public CorePlugin()
    {
        // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
        ICorePlugin.Current ??= this;
    }

    /// <inheritdoc />
    public PermissionLevel GetPermissionLevel(DiscordUser user, DiscordGuild guild)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));
        if (guild is null) throw new ArgumentNullException(nameof(guild));

        if (!TryGetGuildConfiguration(guild, out GuildConfiguration? configuration))
            return PermissionLevel.Default;

        if (!guild.Members.TryGetValue(user.Id, out DiscordMember? member))
            return PermissionLevel.Default;

        if ((member.Permissions & DSharpPlus.Permissions.Administrator) != 0)
            return PermissionLevel.Administrator;

        RoleConfiguration roleConfiguration = configuration.RoleConfiguration;
        List<ulong> roles = member.Roles.Select(r => r.Id).ToList();

        if (roles.Contains(roleConfiguration.AdministratorRoleId)) return PermissionLevel.Administrator;
        if (roles.Contains(roleConfiguration.ModeratorRoleId)) return PermissionLevel.Moderator;
        if (roles.Contains(roleConfiguration.GuruRoleId)) return PermissionLevel.Guru;

        return PermissionLevel.Default;
    }

    /// <inheritdoc />
    public Task LogAsync(DiscordGuild guild, DiscordEmbed embed,
        StaffNotificationOptions notificationOptions = StaffNotificationOptions.None)
    {
        return _discordLogService.LogAsync(guild, embed, notificationOptions);
    }

    /// <inheritdoc />
    public bool IsHigherLevelThan(DiscordUser user, DiscordUser other, DiscordGuild guild)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));
        if (other is null) throw new ArgumentNullException(nameof(other));
        if (guild is null) throw new ArgumentNullException(nameof(guild));

        return GetPermissionLevel(user, guild) > GetPermissionLevel(other, guild);
    }

    /// <inheritdoc />
    public bool IsStaffMember(DiscordUser user, DiscordGuild guild)
    {
        return GetPermissionLevel(user, guild) >= PermissionLevel.Moderator;
    }

    /// <inheritdoc />
    public void RegisterUserInfoField(Action<UserInfoFieldBuilder> builderEvaluator)
    {
        if (builderEvaluator is null) throw new ArgumentNullException(nameof(builderEvaluator));
        var builder = new UserInfoFieldBuilder();
        builderEvaluator(builder);
        RegisterUserInfoField(builder);
    }

    /// <inheritdoc />
    public void RegisterUserInfoField(UserInfoFieldBuilder builder)
    {
        _userInfoService.RegisterField(builder);
    }

    /// <inheritdoc />
    public bool TryGetGuildConfiguration(DiscordGuild guild, [NotNullWhen(true)] out GuildConfiguration? configuration)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (_configurationService is null)
        {
            configuration = null;
            return false;
        }

        configuration = _configurationService.GetGuildConfiguration(guild);
        return true;
    }

    /// <inheritdoc />
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ICorePlugin>(this);
        services.AddSingleton<ConfigurationService>();
        services.AddSingleton<UserInfoService>();

        services.AddHostedSingleton<BookmarkService>();
        services.AddHostedSingleton<BufferedLogService>();
        services.AddHostedSingleton<DiscordLogService>();
        services.AddHostedSingleton<UserReactionService>();

        services.AddDbContext<CoreContext>();
    }

    /// <inheritdoc />
    protected override Task OnLoad()
    {
        _configurationService = ServiceProvider.GetRequiredService<ConfigurationService>();
        _discordLogService = ServiceProvider.GetRequiredService<DiscordLogService>();
        _userInfoService = ServiceProvider.GetRequiredService<UserInfoService>();

        RegisterUserInfoFields();

        Logger.Info("Registering command modules");
        CommandsNextExtension commandsNext = DiscordClient.GetCommandsNext();
        commandsNext.RegisterCommands(typeof(CorePlugin).Assembly);

        SlashCommandsExtension slashCommands = DiscordClient.GetSlashCommands();
        slashCommands.RegisterCommands<ClearCommand>();
        slashCommands.RegisterCommands<SayCommand>();
        slashCommands.RegisterCommands<UserInfoCommand>();

        DiscordClient.GuildAvailable += DiscordClientOnGuildAvailable;

        return base.OnLoad();
    }

    private async Task DiscordClientOnGuildAvailable(DiscordClient sender, GuildCreateEventArgs e)
    {
        SlashCommandsExtension slashCommands = DiscordClient.GetSlashCommands();
        await slashCommands.RefreshCommands();
    }

    private void RegisterUserInfoFields()
    {
        RegisterUserInfoField(builder =>
        {
            builder.WithName("Username");
            builder.WithValue(context =>
            {
                string username = context.TargetUser.GetUsernameWithDiscriminator();
                return context.TargetUser.IsBot ? $"🤖 {username}" : username;
            });
        });

        RegisterUserInfoField(builder =>
        {
            builder.WithName("User ID");
            builder.WithValue(context => context.TargetUser.Id);
        });

        RegisterUserInfoField(builder =>
        {
            builder.WithName("User Created");
            builder.WithValue(context => Formatter.Timestamp(context.TargetUser.CreationTimestamp));
        });

        RegisterUserInfoField(builder =>
        {
            builder.WithName("Nickname");
            builder.WithValue(context => context.TargetMember!.Nickname);
            builder.WithExecutionFilter(context => !string.IsNullOrWhiteSpace(context.TargetMember?.Nickname));
        });

        RegisterUserInfoField(builder =>
        {
            builder.WithName("Join Date");
            builder.WithValue(context => Formatter.Timestamp(context.TargetMember!.JoinedAt));
            builder.WithExecutionFilter(context => context.TargetMember is not null);
        });

        RegisterUserInfoField(builder =>
        {
            builder.WithName("Permission Level");
            builder.WithValue(context => context.TargetMember!.GetPermissionLevel(context.Guild!).ToString("G"));
            builder.WithExecutionFilter(context => context.TargetMember is not null);
        });
    }
}
