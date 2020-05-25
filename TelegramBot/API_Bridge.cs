using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TelegramBot.Class;
using Newtonsoft.Json;
using System;
using static TelegramBot.Class.ApiGetSet;

namespace TelegramBot
{
    class API_Bridge
    {
        public static async void AddUser(int id)
        {
            UserId userId = new UserId();
            userId.Id = id;
            var json = JsonConvert.SerializeObject(userId);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var content = await client.PostAsync("https://myapi20200525144856.azurewebsites.net/api/MyApi/money/AddUser", data);
        }

        public static async Task<System.Net.HttpStatusCode> AddCurr(string Currencyname, int userid)
        {
            Currencyname.ToUpper();
            UserMoney cur = new UserMoney();
            cur.Cur = Currencyname;
            var json = JsonConvert.SerializeObject(cur);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var content = await client.PutAsync("https://myapi20200525144856.azurewebsites.net/api/MyApi/money/AddCur/" + userid, data);
            System.Net.HttpStatusCode state = content.StatusCode;
            return state;
        }
        public static async Task<System.Net.HttpStatusCode> DeleteCurr(string Currencyname, int userid)
        {
            Currencyname.ToUpper();
            UserMoney cur = new UserMoney();
            cur.Cur = Currencyname;
            var json = JsonConvert.SerializeObject(cur);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var content = await client.PutAsync("https://myapi20200525144856.azurewebsites.net/api/MyApi/money/DeleteCur/" + userid, data);
            System.Net.HttpStatusCode state = content.StatusCode;
            return state;
        }
        public static async Task<System.Net.HttpStatusCode> AddBaseCurr(string Currencyname, int userid)
        {
            Currencyname.ToUpper();
            UserInfo cur = new UserInfo();
            cur.Base = Currencyname;
            var json = JsonConvert.SerializeObject(cur);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var content = await client.PutAsync("https://myapi20200525144856.azurewebsites.net/api/MyApi/money/AddNewBase/" + userid, data);
            System.Net.HttpStatusCode state = content.StatusCode;
            return state;
        }
        public static async Task<string> CurrList(int userid)
        {
            var client = new HttpClient();
            var content = await client.GetStringAsync("https://myapi20200525144856.azurewebsites.net/api/MyApi/money/AllUserCur/" + userid);
            Dictionary<string, string> list = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            //string result = Convert.ToString(list);
            return content;
        }
        public static async Task<string> Forecast(string Cur)
        {
            Cur=Cur.ToUpper();
            var client = new HttpClient();
            var content = await client.GetStringAsync("https://myapi20200525144856.azurewebsites.net/api/MyApi/money/forecast/" + Cur);
            
            //string result = Convert.ToString(list);
            return content;
        }
        public static async Task<string> Calculator(string number1, string number2, string Cur1, string Cur2, string CurResult)
        {
            Cur1.ToUpper();
            Cur2.ToUpper();
            CurResult.ToUpper();
            var client = new HttpClient();
            //string path = "https://localhost:44313/api/MyApi/money/calc/from/" + number1 + "/" + Cur1 + "/plus/" + number2 + "/" + Cur2 + "/to" + CurResult;
            string content = await client.GetStringAsync("https://myapi20200525144856.azurewebsites.net/api/MyApi/money/calc/from/" + number1+"/"+Cur1+"/plus/"+number2+ "/" + Cur2 + "/to" + CurResult);
           // Dictionary<string, string> list = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            //string result = Convert.ToString(list);
            return content;
        }

        public bool ChechCommand(string tocheck)
        {
            Regex regex = new Regex(@"^/");
            bool match = regex.IsMatch(tocheck);
            return match;
        }
        public bool ChechCur(string checkcur)
        {
            Regex regex = new Regex(@"[A-Z]{3}");
            bool match = regex.IsMatch(checkcur);
            if (checkcur.Length !=3)
            {
                match = false;
            }
            return match;
        }
        public bool ChechNumber(string checkcur)
        {
            Regex regex = new Regex(@"[A-Z]{3}");
            bool match = regex.IsMatch(checkcur);
            return match;
        }

    }
}
