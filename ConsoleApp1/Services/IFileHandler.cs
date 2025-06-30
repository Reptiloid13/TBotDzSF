using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TBotDZ.Services
{
    public class IFileHandler
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public IFileHandler(ITelegramBotClient telegramClient, IStorage memoryStorage)
        {
            _telegramClient = telegramClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (_memoryStorage.GetSession(message.Chat.Id).TextCode)
            {
                case Datas.TextLength:
                    await _telegramClient.SendMessage(message.Chat.Id, $"The length is {GetLength(message.Text)} chars.", cancellationToken: ct);
                    break;
            }
        }

        public string GetLength(string text)
        {
            return text.Length.ToString();
        }

        public async Task GetSum(Message message, CancellationToken ct)
        {
            var numbers = message.Text.Split(" ");
            var sum = 0;
            try
            {
                foreach (var num in numbers)
                {
                    sum += Convert.ToInt32(num);
                }
                await _telegramClient.SendMessage(message.Chat.Id, $"Sum of digits: {sum.ToString()}", cancellationToken: ct);
            }
            catch (Exception)
            {
                await _telegramClient.SendMessage(message.Chat.Id, $"Wrong format.", cancellationToken: ct);
            }
        }
    }
}
