using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace pvTgBot.Services
{
    public class NovaPoshta
    {    
        public static NovaPoshtaJSON _npJson;
        public static HttpClient _client;
        public static HttpResponseMessage _response;
        public static object _JsonContent;
        public static string _dataContent;

        public static async Task<string> GetTrackingData(string num, string num2)
        {           
            _npJson = new NovaPoshtaJSON();
            _npJson.modelName = "TrackingDocument";
            _npJson.calledMethod = "getStatusDocuments";
            _npJson.methodProperties = new MethodProperties
            {
                Documents = new List<Document>() { new Document(num, num2)}
            };
            _npJson.apiKey = "[ВАШ КЛЮЧ]";

            string body = JsonConvert.SerializeObject(_npJson);

            _client = new HttpClient();          

            var uri = "http://api.novaposhta.ua/v2.0/json/";

            byte[] byteData = Encoding.UTF8.GetBytes(body);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                _response = await _client.PostAsync(uri, content);
                _dataContent = _response.Content.ReadAsStringAsync().Result;
                _JsonContent = JsonConvert.DeserializeObject(_dataContent);
                Console.WriteLine(_JsonContent.ToString());
            }

            NovaPoshtaResponse myDeserializedClass = JsonConvert.DeserializeObject<NovaPoshtaResponse>(_JsonContent.ToString());
            
            return $"Статус: {myDeserializedClass.data[0].Status}\n" +
                $"Маршрут: {myDeserializedClass.data[0].CitySender} - {myDeserializedClass.data[0].CityRecipient}\n" +
                $"Адреса доставки:\n{myDeserializedClass.data[0].WarehouseRecipient}\n" +
                $"Компанія: {myDeserializedClass.data[0].CounterpartySenderDescription}\n" +               
                $"Відправник: {myDeserializedClass.data[0].SenderFullNameEW}\n" +
                $"Телефон відправника: {myDeserializedClass.data[0].PhoneSender}\n" +
                $"Опис: {myDeserializedClass.data[0].CargoDescriptionString}";
        }
    }

    public class Document
    {
        public Document(string doc, string phone)
        {
            DocumentNumber = doc;
            Phone = phone;
        }
        public string DocumentNumber { get; set; }
        public string Phone { get; set; }
    }

    public class MethodProperties
    {
        public List<Document> Documents { get; set; }
    }

    public class NovaPoshtaJSON
    {
        public string apiKey { get; set; }
        public string modelName { get; set; }
        public string calledMethod { get; set; }
        public MethodProperties methodProperties { get; set; }
    }

    public class Datum
    {
        public string Number { get; set; }
        public string CargoDescriptionString { get; set; }
        public string CitySender { get; set; }
        public string CityRecipient { get; set; }
        public string WarehouseRecipient { get; set; }
        public string PhoneSender { get; set; }
        public string SenderFullNameEW { get; set; }
        public string CounterpartySenderDescription { get; set; }
        public string Status { get; set; }
    }

    public class NovaPoshtaResponse
    {
        public bool success { get; set; }
        public List<Datum> data { get; set; }
        public List<object> errors { get; set; }
        public List<object> warnings { get; set; }
        public List<object> info { get; set; }
        public List<object> messageCodes { get; set; }
        public List<object> errorCodes { get; set; }
        public List<object> warningCodes { get; set; }
        public List<object> infoCodes { get; set; }
    }
}