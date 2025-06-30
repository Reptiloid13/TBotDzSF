using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using TBotDZ.Models;

namespace TBotDZ.Services;

public class MemoryStorage : IStorage
{
    private readonly ConcurrentDictionary<long, Session> _sessions;

    public MemoryStorage()
    {
        _sessions = new ConcurrentDictionary<long, Session>();
    }

    public Session GetSession(long chatId)
    {
        // возвращаем сессию по ключу если она существует
        if (_sessions.ContainsKey(chatId))
            return _sessions[chatId];

        //Создааем новую если такой не было 
        var newSession = new Session() { TextCode = "textlength" };
        _sessions[chatId] = newSession;
        return newSession;


    }

}
