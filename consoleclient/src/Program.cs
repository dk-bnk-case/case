  
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace consoleclient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            Start:
            Console.Write("Enter Operation ('Get' or 'Set'): ");
            var operation = Console.ReadLine();
            var apiUrl = "http://webapi:80/api/Municipality/";

            if (operation == "Get") {
                Console.Write("Enter Municipality eg. ('Copenhagen'): ");
                string name = Console.ReadLine();
                Console.Write("Enter Date eg. ('2016.01.01'): ");
                string date = Console.ReadLine();
                var requestUrl = apiUrl + name + "/" + date;
                var municipality = await ReadMunicipality(requestUrl);
                if (municipality != null) {
                    Console.WriteLine("Municipality: " + municipality.Municipality_name);
                    Console.WriteLine("Tax: " + municipality.Daily + municipality.Weekly + municipality.Monthly + municipality.Yearly);
                } else {
                    Console.WriteLine("Nothing found!");
                }
                goto Start;

            } else if (operation == "Set") {
                var municipality = new Municipality();
                Console.Write("Enter Municipality eg. ('Copenhagen'): ");
                municipality.Municipality_name = Console.ReadLine();

                Console.Write("Enter Start date eg. ('2016.01.01'): ");
                string startDate = Console.ReadLine();
                try {
                    municipality.Period_start = DateTime.Parse(startDate);
                } catch (FormatException) {
                    Console.WriteLine("Unable to parse '{0}'", startDate);
                    goto Start;
                }

                Console.Write("Enter End date eg. ('2016.12.31'): ");
                string endDate = Console.ReadLine();
                try {
                    municipality.Period_end = DateTime.Parse(endDate);
                } catch (FormatException) {
                    Console.WriteLine("Unable to parse '{0}'", endDate);
                    goto Start;
                }

                Console.Write("Enter Tax period valid values: 'Yearly', 'Monthly', 'Weekly', 'Daily'): ");
                string period = Console.ReadLine();
                Console.Write("Enter Tax value eg. ('0.3'): ");
                string taxString = Console.ReadLine();
                double tax;
                try {
                    tax = Convert.ToDouble(taxString);
                } catch (FormatException) {
                    Console.WriteLine("Unable to parse '{0}'", endDate);
                    goto Start;
                }
                
                switch (period) {
                    case "Yearly":
                        municipality.Yearly = tax;
                        break;
                    case "Monthly":
                        municipality.Monthly = tax;
                        break;
                    case "Weekly":
                        municipality.Weekly = tax;
                        break;
                    case "Daily":
                        municipality.Daily = tax;
                        break;
                    default:
                        Console.WriteLine("Tax period was not filled out correctly!");
                        goto Start;
                }

                await WriteMunicipality(apiUrl, municipality);

                goto Start;
            } else {
                goto Start;
            }
        }

        private static async Task<Municipality> ReadMunicipality(string apiUrl)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var streamTask = client.GetStreamAsync(apiUrl);
            try {
            var municipality = await JsonSerializer.DeserializeAsync<Municipality>(await streamTask);
            return municipality;
            }
            catch (Exception){
                return null;
            }
        }

        private static async Task WriteMunicipality(string apiUrl, Municipality mun)
        {
            var payload = await Task.Run(() => JsonSerializer.Serialize(mun));
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            Console.WriteLine("payload: " + payload);
            
            var response = await client.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
