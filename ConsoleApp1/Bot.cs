using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBotDZ.Controllers;
using TBotDZ.Services;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TBotDZ;

public class Bot : BackgroundService
{
    private ITelegramBotClient _telegramClient;

    private InlineKeyboardController _inlineKeyboardController;
    private TextMessageController _textMessageController;
    private DefaultMessageController _defaultMessageController;
    private IFileHandler _fileHandler;

    public Bot(ITelegramBotClient telegramClient, TextMessageController textMessageController, InlineKeyboardController inlineKeyboardController, DefaultMessageController defaultMessageController, IFileHandler fileHandler)
    {
        _telegramClient = telegramClient;
        _textMessageController = textMessageController;
        _inlineKeyboardController = inlineKeyboardController;
        _defaultMessageController = defaultMessageController;
        _fileHandler = fileHandler;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _telegramClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, new ReceiverOptions() { AllowedUpdates = { } }, cancellationToken: stoppingToken);

        Console.WriteLine("Bot started ");
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClietnt, Update update, CancellationToken cancellationToken)
    {
        // Обрабатываем нажатие на кнопки из Telegram Bot 
        if (update.Type == UpdateType.CallbackQuery)
        {
            await _inlineKeyboardController.Handle(update.CallbackQuery, cancellationToken);
            return;
        }

        if (update.Type == UpdateType.Message)
        {
            switch (update.Message!.Type)
            {
                case MessageType.Text:
                    await _textMessageController.Handle(update.Message, cancellationToken);
                    return;
                default: //unsupported message

                    await _defaultMessageController.Handle(update.Message, cancellationToken);
                    return;
            }

        }


    }

    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        {
            //Задаем сообщение об ошибке в зависимости от того, какая именно ошибка произошла
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error: \n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            // Выводим в консоль информацию об ошибке
            Console.WriteLine(errorMessage);


            //Задержка перед повторным подключением 

            Console.WriteLine("Waiting 10 seconds before retry ");
            Thread.Sleep(10000);

            return Task.CompletedTask;
        }
    }
}

