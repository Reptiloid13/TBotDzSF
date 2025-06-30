using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TBotDZ.Models;

public class Enter
{
    public static async Task SendInstruction(ITelegramBotClient telegramBotClient, long id, Session session, CancellationToken ct)
    {
        switch (session.TextCode)
        {
            case Datas.TextLength:
                await telegramBotClient.SendMessage(id, "Enter the text message: ", cancellationToken: ct);
                break;
            case Datas.Sum:
                await telegramBotClient.SendMessage(id, "Enter digits: ", cancellationToken: ct);
                break;
            default:
                await telegramBotClient.SendMessage(id, "", cancellationToken: ct);
                break;
        }
    }
}
