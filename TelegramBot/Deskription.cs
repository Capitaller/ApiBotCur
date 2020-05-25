using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class Deskription
    {
        public string start =
             @"Привет, данный бот предназначен  для удобного мониторинга курсов основных валют
Перед рекоммендую ознакомиться с инструкцией /Instruction
";

        public string addCur = @"Валюта добавлена";
        
    
        public string CoinInfoInstruction =
@"Данная команда добавляет валюты в ваш личный список валют.
Введите сокращенное название валюты, например - USD
Помните, в списке не может быть меньше одной и больше 5 валю.т"
;
        public string CoinDeleteInstruction =
@"Данная команда удаляет валюты из вашего личного списка валют. Выберите валюту, которую хотите удалить.
"
;
        
        public string SuccessAddCur =
@"Валюта добавлена."
;
        public string SuccessDeleteCur =
@"Валюта удалена."
;
        public string SuccessAddNewBase =
@"Базовая валюта установлена."
;
        public string Success =
@"Успех!!!"
;
        public string FailureTooMuchCur =
  @"Ошибка!Добавлено более 5 валют."
  ;
        public string FailureToofewCur =
 @"Ошибка! В пашем списке находиться только одна валюта, её нельзя удалить."
 ;
        public string Exeption =
@"Произошла ошибка!"
;
        public string ExeptionCur =
@"Произошла ошибка! Проверьте написание и вызовите команду повторно."
;
        public string FailureDeleteCur=
@"Произошла ошибка! Валюта не найдена или уже удалена."
;
        public string FailureAddCur =
@"Произошла ошибка! Валюта не найдена.";
        public string FailureAddCur_exist =
@"Произошла ошибка! Валюта уже находится в списке.";
        public string AddNewBaseInstruction =
@"Данная команда позволяет установить базовую валюту, относительно которой будет считаться курс."
;
        public string CommandNotChuse =
       @"Выберите команду!"
       ;
        public string Forecast =
      @"Данная функция позволяет определить будущий рост или падение курса вашего депозита!
Введите валюту депозита."
      ;
        public string Calc =
               @"Введите выражение, которое хотите посчитать согласно шаблону:
(количество валюты)(валюта)+(количество валюты)(валюта) = (валюта, в которой необходимо выдать результат)
Пример: 5USD+10EUR=UAH
"
               ;

        public string Instruction =
            @"Руководство по єксплуатации бота:
Бот представляет собой 6 комманд.
/Insruction - отправляет сообщение с данной инструкцией.
/Keyboard - для более удобного взаимодействия.
/AddCurrency - позволяет добавить валюту в ваш личный список. Помните, нельзя добавить более 5 валют.
/AddBaseCurrency - позволяет установить валюту, относительно которой будет считаться результат. Помните, в списке должна остаться хотя бы одна валюта.
/DeleteCurrency - позволяет Удалить валюту из вашего личного списка.
/Calculate - Позволяет конвертировать несколько валют в одну по курсу.
/List - позволяет посмотреть ваш личный список валют.
/Forecast - позволяет предположить дальнейший рост/падение курса вашего депозита.



Список валют, поддерживаемых ботом:
USD, EUR, PLN, KRW, GBP, CAD, JPY, RUB, TRY, NZD, AUD, CHF, UAH, HKD, SGD, NGN, PHP, MXN, BRL, THB, CLP, CNY, CZK, DKK, HUF, IDR, ILS, INR, MYR, NOK, PKR, SEK, TWD, ZAR, VND, BOB, COP, PEN, ARS, ISK.
Команды следуе отправлять с интервалом в 15 секунд или больше.

";



}
}

