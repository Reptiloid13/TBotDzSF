using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using TBotDZ.Controllers;
using Telegram.Bot;
using System.Threading.Tasks;
using System;
using TBotDZ.Services;
using TBotDZ.Configuration;

namespace TBotDZ;

public class Program
{
    public static async Task Main()
    {
        Console.OutputEncoding = Encoding.Unicode;

        //Объект отвечаю за постоянный железный цикл приложения 

        var host = new HostBuilder().ConfigureServices((hostContext, services) => ConfigureServices(services))
            .UseConsoleLifetime()
            .Build();

        Console.WriteLine("Starting service");

        await host.RunAsync();

        Console.WriteLine("Service stopped");
    }

    static void ConfigureServices(IServiceCollection services)
    {
        // Регистрируем объект TelegramClient с токеном подключения

        AppSettings appSettings = BuildAppSettings();
        services.AddSingleton(appSettings);
        services.AddSingleton<IStorage, MemoryStorage>();

        services.AddTransient<IFileHandler>();
        services.AddTransient<DefaultMessageController>();
        services.AddTransient<InlineKeyboardController>();
        services.AddTransient<TextMessageController>();

        services.AddSingleton<IStorage, MemoryStorage>();
        services.AddSingleton<ITelegramBotClient>(provide => new TelegramBotClient(appSettings.BotToken));
        //Регистрируем постоянно активный сервис бота 
        services.AddHostedService<Bot>();

    }

    static AppSettings BuildAppSettings()
    {
        return new AppSettings
        {
            BotToken = "8102144811:AAF9-nUWAENxJlPU0DtIcYssMREce3kePws"
        };
    }
}
