using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBotDZ.Models;

namespace TBotDZ.Services;

public interface IStorage
{
    // Получение сессии пользователя по индефикаатору
    Session GetSession(long chatId);
}
