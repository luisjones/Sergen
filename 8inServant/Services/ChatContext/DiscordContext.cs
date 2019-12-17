﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using _8inServant.Services.Containers;
using _8inServant.Services.Processor;

namespace _8inServant.Services
{
    public class DiscordContext : IChatContext
    {
        private readonly DiscordSocketClient _discord;
        private readonly IConfiguration _config;

        // DiscordSocketClient, CommandService, and IConfigurationRoot are injected automatically from the IServiceProvider
        public DiscordContext(
            DiscordSocketClient discord,
            IConfiguration config)
        {
            _discord = discord;
            _config = config;
        }

        public async void Connect()
        {
            string discordToken = _config["Tokens:Discord"];     // Get the discord token from the config file
            if (string.IsNullOrWhiteSpace(discordToken))
                throw new Exception("Please enter your bot's token into the `appsettings.json` file found in the applications root directory.");

            await _discord.LoginAsync(TokenType.Bot, discordToken);     // Login to discord
            await _discord.StartAsync();                                // Connect to the websocket
        }

        public string GetUsername(ulong userID)
        {
            return _discord.GetUser(userID).Username;
        }
    }
}
