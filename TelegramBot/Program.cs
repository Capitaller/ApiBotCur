using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

//using Telegram.Bot.Types.;


namespace TelegramBot

{
    class Program
    {
        static TelegramBotClient Bot;
       // public static Dictionary<int, string> user_status2 = JsonConvert.DeserializeObject<Dictionary<int, string>>(System.IO.File.ReadAllText("userstatus.json"));
        public static Dictionary<int, string> user_status = new Dictionary<int, string>();
        public static Dictionary<int, string> user_status_1 = Program.trydictionary(user_status_1);
        public static Dictionary<int, string> currency_to_change_from_button = new Dictionary<int, string>();
       // public static Dictionary<int, UserCryptoCount> message_to_edit = new Dictionary<int, UserCryptoCount>();
        public static Dictionary<int, string> trydictionary(Dictionary<int, string> user_status4)
        {
            Dictionary<int, string> user_status3;
            if (user_status4 != null)
            {
                user_status3 = user_status4;
            }
            else
            {
                user_status3 = new Dictionary<int, string>();
            }
            return user_status3;
        }
        //private static Dictionary<int, string> trydictionary(Dictionary<int, string> user_status2)
        //{
        //    throw new NotImplementedException();
        //}

        static void Main(string[] args)
        {
            Bot = new TelegramBotClient("1201346940:AAH0ky_C9fDNvqpyV4Tt29No67Yi2RdWaNI");
            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            var me = Bot.GetMeAsync().Result;
            Console.WriteLine(me.FirstName);
            Bot.StartReceiving();
            Console.ReadLine();
            Bot.StopReceiving();
        }
        private static void BotOnCallbackQueryReceived(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            string buttonText = e.CallbackQuery.Data;
           // string name = $"{e.CallbackQuery.From.f}"
        }
        
        private static async void BotOnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
           
                var message = e.Message;
                if (message.Type != MessageType.Text)
                {
                    return;
                }
                bool exist = false;
                foreach (int c in user_status.Keys)
                {
                    if (c == message.Chat.Id)
                    {
                        exist = true;
                        break;
                    }
                }
                if (exist == false)
                {
                    try { user_status.Add(message.From.Id, "normal"); }
                    catch { return; }
                }

                string name = $"{message.From.FirstName} {message.From.LastName}";
                Console.WriteLine($"{name} sand message: '{message.Text}'");
                Deskription desk = new Deskription();
                int id = message.From.Id;
                string text;

                if (user_status[message.From.Id] == "add_currency_mode")
                {
                message.Text = message.Text.ToUpper();
                

                var reqest = await API_Bridge.AddCurr(message.Text, message.From.Id);
                    if (reqest == System.Net.HttpStatusCode.BadRequest)
                    {
                        await Bot.SendTextMessageAsync(message.From.Id, desk.FailureAddCur);
                        user_status[message.From.Id] = "normal";
                        return;
                    }
                    if (reqest == System.Net.HttpStatusCode.Conflict)
                    {
                        await Bot.SendTextMessageAsync(message.From.Id, desk.FailureAddCur_exist);
                        user_status[message.From.Id] = "normal";
                        return;
                    }
                    if (reqest == System.Net.HttpStatusCode.NoContent)
                    {
                        await Bot.SendTextMessageAsync(message.From.Id, desk.SuccessAddCur);
                        user_status[message.From.Id] = "normal";
                        return;
                    }
                    if (reqest == System.Net.HttpStatusCode.PaymentRequired)
                    {
                        await Bot.SendTextMessageAsync(message.From.Id, desk.FailureTooMuchCur);
                        user_status[message.From.Id] = "normal";
                        return;
                    }
                if (reqest == System.Net.HttpStatusCode.UnsupportedMediaType)
                {
                    await Bot.SendTextMessageAsync(message.From.Id, desk.FailureAddCur);
                    user_status[message.From.Id] = "normal";
                    return;
                }
            }
                if (user_status[message.From.Id] == "delete_currency_mode")
                {
                message.Text = message.Text.ToUpper();
                
                var reqest = await API_Bridge.DeleteCurr(message.Text, message.From.Id);
                    if (reqest == System.Net.HttpStatusCode.NotFound)
                    {
                        await Bot.SendTextMessageAsync(message.From.Id, desk.FailureDeleteCur);
                        user_status[message.From.Id] = "normal";
                        return;
                    }
                if (reqest == System.Net.HttpStatusCode.PaymentRequired)
                {
                    await Bot.SendTextMessageAsync(message.From.Id, desk.FailureToofewCur);
                    user_status[message.From.Id] = "normal";
                    return;
                }
                if (reqest == System.Net.HttpStatusCode.NoContent)
                    {
                        await Bot.SendTextMessageAsync(message.From.Id, desk.SuccessDeleteCur);
                        user_status[message.From.Id] = "normal";
                        return;
                    }
                if (reqest == System.Net.HttpStatusCode.UnsupportedMediaType)
                {
                    await Bot.SendTextMessageAsync(message.From.Id, desk.Exeption);
                    user_status[message.From.Id] = "normal";
                    return;
                }

            }
                if (user_status[message.From.Id] == "addNewBase_currency_mode")
                {
                //bool command = API_Bridge.ChechCommand(message.Text);
                // if (command != true)
                // {
                message.Text= message.Text.ToUpper();
                var reqest = await API_Bridge.AddBaseCurr(message.Text, message.From.Id);
                    if (reqest == System.Net.HttpStatusCode.NotFound)
                    {
                        await Bot.SendTextMessageAsync(message.From.Id, desk.FailureDeleteCur);
                        user_status[message.From.Id] = "normal";
                        return;
                    }
                    if (reqest == System.Net.HttpStatusCode.NoContent)
                    {
                        await Bot.SendTextMessageAsync(message.From.Id, desk.SuccessAddNewBase);
                        user_status[message.From.Id] = "normal";
                        return;
                    }
                if (reqest == System.Net.HttpStatusCode.UnsupportedMediaType)
                {
                    await Bot.SendTextMessageAsync(message.From.Id, desk.Exeption);
                    user_status[message.From.Id] = "normal";
                    return;
                }

            }
                if (user_status[message.From.Id] == "User_list_mode")
                {
                    //bool command = API_Bridge.ChechCommand(message.Text);
                    // if (command != true)
                    // {

                    var reqest = await API_Bridge.CurrList(message.From.Id);
                    if (reqest == null)
                    {
                        await Bot.SendTextMessageAsync(message.From.Id, desk.Exeption);
                        user_status[message.From.Id] = "normal";
                        return;
                    }
                    if (reqest != null)
                    {
                        await Bot.SendTextMessageAsync(message.From.Id, reqest);
                        user_status[message.From.Id] = "normal";
                        return;
                    }
                
            }
                if (user_status[message.From.Id] == "Forecast_mode")
                {
                message.Text=message.Text.ToUpper();
                Regex regex = new Regex(@"[A-Z,a-z]{3}");
                Regex regex2 = new Regex(@"/");

                if ((regex.IsMatch(message.Text) && message.Text.Length == 3) == false)
                {
                    await Bot.SendTextMessageAsync(message.From.Id, desk.ExeptionCur);
                    user_status[message.From.Id] = "normal";
                    return;
                }
                if ((regex2.IsMatch(message.Text) ) != false)
                {
                    //await Bot.SendTextMessageAsync(message.From.Id, desk.ExeptionCur);
                    user_status[message.From.Id] = "normal";
                    return;
                }
                var reqest = await API_Bridge.Forecast(message.Text);
                    if (reqest == null)
                    {
                        await Bot.SendTextMessageAsync(message.From.Id, desk.Exeption);
                        user_status[message.From.Id] = "normal";
                        return;
                    }
                    if (reqest != null)
                    {
                        await Bot.SendTextMessageAsync(message.From.Id, reqest);
                        user_status[message.From.Id] = "normal";
                        return;
                    }

                }
                if (user_status[message.From.Id] == "Calculate_mode")
                {
                
                string tocalculate = message.Text;
                tocalculate = tocalculate.Replace(" ", "");
                Regex regex = new Regex(@"\d{1,}[.,]{0,}\d{0,}[A-Z,a-z]{3}\+\d{1,}[.,]{0,}\d{0,}[A-Z,a-z]{3}\=[A-Z,a-z]{3}");
                //Regex regex = new Regex(@"[0-9][A-Z,a-z]{3}\+\d[A-Z,a-z]{3}\=[A-Z,a-z]{3}");
                //Regex regex3 = new Regex(@"(0 |[1 - 9]\d *)| ([.,]\d +)[A-Z,a-z]{3}\+(0|[1-9]\d*)([.,]\d+)[A-Z,a-z]{3}\=[A-Z,a-z]{3}");
                //Regex regex4 = new Regex(@"[0-9][A-Z,a-z]{3}\+(0|[1-9]\d*)([.,]\d+)[A-Z,a-z]{3}\=[A-Z,a-z]{3}");
                //Regex regex5 = new Regex(@"(0 |[1 - 9]\d *)| ([.,]\d +)[A-Z,a-z]{3}\+[0-9][A-Z,a-z]{3}\=[A-Z,a-z]{3}");
                
                if ((regex.IsMatch(tocalculate)) == false)
                {
                    await Bot.SendTextMessageAsync(message.From.Id, desk.ExeptionCur);
                    user_status[message.From.Id] = "normal";
                    return;
                }
                
                Regex regex2 = new Regex(@"/");
                if ((regex2.IsMatch(message.Text)) != false)
                {
                    //await Bot.SendTextMessageAsync(message.From.Id, desk.ExeptionCur);
                    user_status[message.From.Id] = "normal";
                    return;
                }

               
                    string part1 = "";
                    string part2 = "";
                    string part3 = "";
                    string part4 = "";
                    string part5 = "";

                    int i = 0;
                    tocalculate = tocalculate.Replace(",", ".");
                    tocalculate = tocalculate.Replace("=", "7");
                    tocalculate = tocalculate.Replace("+", "");
                    while (char.IsPunctuation(tocalculate[i]) || char.IsDigit(tocalculate[i]))
                    {
                        part1 += tocalculate[i];
                        i++;
                    }
                    while (char.IsLetter(tocalculate[i]))
                    {
                        part2 += tocalculate[i];
                        i++;
                    }
                    while (char.IsPunctuation(tocalculate[i]) || char.IsDigit(tocalculate[i]))
                    {
                        part3 += tocalculate[i];
                        i++;
                    }
                    while (char.IsLetter(tocalculate[i]))
                    {
                        part4 += tocalculate[i];
                        i++;
                    }
                    while (char.IsDigit(tocalculate[i]))
                    {

                        i++;
                    }
                    while (char.IsLetter(tocalculate[i]))
                    {
                        part5 += tocalculate[i];
                        i++;
                        if (i == tocalculate.Length)
                        {
                            break;
                        }
                    }

                    string Cur1 = part2;
                    string Cur2 = part4;
                    string CurResult = part5;
                Cur1 = Cur1.ToUpper();
                Cur2= Cur2.ToUpper();
                CurResult = CurResult.ToUpper();

                string result = await API_Bridge.Calculator(part1, part3, Cur1, Cur2, CurResult);
                    result = result.Replace(".", ",");

                    decimal result2 = Convert.ToDecimal(result);

                    if (result == null)
                    {
                        await Bot.SendTextMessageAsync(message.From.Id, desk.Exeption);
                        user_status[message.From.Id] = "normal";
                        return;
                    }
                if (result == "-1")
                {
                    await Bot.SendTextMessageAsync(message.From.Id, desk.Exeption);
                    user_status[message.From.Id] = "normal";
                    return;
                }
                if (result != null)
                    {
                        string result1 = part1 + part2 + "+" + part3 + part4 + "=" + result2;
                        await Bot.SendTextMessageAsync(message.From.Id, result1);
                        user_status[message.From.Id] = "normal";
                        return;
                    }

                }

            switch (message.Text)
            {
                case "/start":
                    text = desk.start;
                    await Bot.SendTextMessageAsync(message.From.Id, text);
                    API_Bridge.AddUser(id);

                    break;
                case "/Instruction":
                    text = desk.Instruction;
                    await Bot.SendTextMessageAsync(message.From.Id, text);


                    break;
                case "/List":
                    string CurrList = await API_Bridge.CurrList(message.From.Id);

                    CurrList = CurrList.Replace("{", "");
                    CurrList = CurrList.Replace("}", "");
                    CurrList = CurrList.Replace(",", ",\n");
                    CurrList = CurrList.Replace(":", ": ");
                    
                    if (CurrList == null)
                    {
                        await Bot.SendTextMessageAsync(message.From.Id, desk.Exeption);
                        user_status[message.From.Id] = "normal";
                        return;
                    }
                    if (CurrList != null)
                    {
                        await Bot.SendTextMessageAsync(message.From.Id, CurrList);
                        user_status[message.From.Id] = "normal";
                        return;
                    }
                    break;

                case "/AddCurrency":
                    await Bot.SendTextMessageAsync(message.From.Id, desk.CoinInfoInstruction);
                    user_status[message.From.Id] = "add_currency_mode";
                    break;

                case "/DeleteCurrency":
                    await Bot.SendTextMessageAsync(message.From.Id, desk.CoinDeleteInstruction);
                    user_status[message.From.Id] = "delete_currency_mode";
                    break;
                case "/AddBaseCurrency":
                    await Bot.SendTextMessageAsync(message.From.Id, desk.AddNewBaseInstruction);
                    user_status[message.From.Id] = "addNewBase_currency_mode";
                    break;
                case "/Calculate":
                    //   string CurrList = await API_Bridge.Calculator(message.From.Id);
                    await Bot.SendTextMessageAsync(message.From.Id, desk.Calc);
                    user_status[message.From.Id] = "Calculate_mode";
                    break;
                case "/Forecast":
                    //   string CurrList = await API_Bridge.Calculator(message.From.Id);
                    await Bot.SendTextMessageAsync(message.From.Id, desk.Forecast);
                    user_status[message.From.Id] = "Forecast_mode";
                    break;
                case "/Keyboard":
                    var replyKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                        new[]
                        {
                             new KeyboardButton("/List"),

                        },
                         new[]
                        {
                               new KeyboardButton("/AddCurrency"),
                            new KeyboardButton("/DeleteCurrency")
                           
                        },
                           new[]
                        {
                              
                             new KeyboardButton("/AddBaseCurrency")
                        },
                          new[]
                        {
                            new KeyboardButton("/Forecast"),
                            new KeyboardButton("/Calculate")
                        }

                        }
                    );
                    replyKeyboard.ResizeKeyboard = true;
                    await Bot.SendTextMessageAsync(message.Chat.Id, "Теперь ваше взаимдействие с ботом станет еще удобнее", replyMarkup: replyKeyboard);
                    break;


                default:
                    await Bot.SendTextMessageAsync(message.From.Id, desk.CommandNotChuse);
                    user_status[message.From.Id] = "normal";
                    break;




            }
        }
    }
}
