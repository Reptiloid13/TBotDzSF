using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBotDZ.Models;
using TBotDZ.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TBotDZ.Controllers;

public class InlineKeyboardController
{
    private readonly IStorage _memoryStorage;
    private readonly ITelegramBotClient _telegramClient;


    public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
    {
        _telegramClient = telegramBotClient;
        _memoryStorage = memoryStorage;
    }

    public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
    {

        if (callbackQuery?.Data == null)
            return;

        _memoryStorage.GetSession(callbackQuery.From.Id).TextCode = callbackQuery.Data;

        string code = callbackQuery.Data switch
        {
            Datas.TextLength => "Calculate text length",
            Datas.Sum => "Calculate sum of digits",
            _ => String.Empty
        };


        await _telegramClient.SendMessage(callbackQuery.From.Id, $"<b> You choose - {code}. {Environment.NewLine}</b>" + $"{Environment.NewLine} You can change in the main menu.", cancellationToken: ct, parseMode: ParseMode.Html);

        await Enter.SendInstruction(_telegramClient, callbackQuery.From.Id, _memoryStorage.GetSession(callbackQuery.From.Id), ct);

    }

}
