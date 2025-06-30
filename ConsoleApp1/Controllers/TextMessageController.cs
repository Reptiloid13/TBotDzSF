using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBotDZ.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TBotDZ.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IFileHandler _fileHandler;

        public TextMessageController(ITelegramBotClient telegramBotClient, IFileHandler fileHandler)
        {
            _telegramBotClient = telegramBotClient;
            _fileHandler = fileHandler;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":
                    //Объект представляющий кнопки

                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[] {InlineKeyboardButton.WithCallbackData($"Calculate the length", Datas.TextLength),
                    InlineKeyboardButton.WithCallbackData($"Calculate the sum "), Datas.Sum});

                    //передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramBotClient.SendMessage(message.Chat.Id, $"<b> Our bot can 1) Calculate text length.</b>{Environment.NewLine}" + $"{Environment.NewLine} <b> 2) And calculate the sum of digits..</b>",
                        cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;

                default:
                    await _fileHandler.Handle(message, ct);
                    break;

            }
        }
    }


}
