using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    private static readonly HttpClient client = new HttpClient();
    private const string apiKey = "0bbef86f96702509c74ef6e45f607d0598ac1066";
    private const string apiUrl = "http://suggestions.dadata.ru/suggestions/api/4_1/rs/iplocate/address?ip=";

    static async Task Main(string[] args)
    {
        Console.WriteLine("Введите IP адрес:");
        string ipAddress = Console.ReadLine();

        var city = await GetCityByIp(ipAddress);
        if (city != "null")
        {
            Console.WriteLine($"Город для IP адреса {ipAddress}: {city}");
        }
    }

    private static async Task<string> GetCityByIp(string ipAddress)
    {
        // Пробуем получить ответ
        try
        {
            var response = await client.GetStringAsync($"{apiUrl}{ipAddress}&token={apiKey}");
            var json = JObject.Parse(response);

            // Проверяем найден ли город
            try
            {
                return json["location"]["data"]["city"].ToString();
            }
            catch
            {
                Console.WriteLine("Ошибка: город не найден");
                return "null";
            }
        }
        catch
        {
            Console.WriteLine("Ошибка: не удалось получить ответ");
            return "null";
        }
    }
}