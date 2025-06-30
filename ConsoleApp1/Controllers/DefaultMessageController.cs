using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TBotDZ.Controllers;

public class DefaultMessageController
{
    private readonly ITelegramBotClient _telegramBotClient;

    public DefaultMessageController(ITelegramBotClient telegramBotClient)
    {
        _telegramBotClient = telegramBotClient;
    }

    public async Task Handle(Message message, CancellationToken ct)
    {
        Console.WriteLine($"Controller {GetType().Name} has received a message");
        await _telegramBotClient.SendMessage(message.Chat.Id, $"The message unsupported format has been received", cancellationToken:ct);
    }
}
