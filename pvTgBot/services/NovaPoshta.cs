using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace pvTgBot.Services
{

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

    public class NovaPoshtaResponse
    {
        public string apiKey { get; set; }
        public string modelName { get; set; }
        public string calledMethod { get; set; }
        public MethodProperties methodProperties { get; set; }
    }

    public class Datum
    {
        public string Number { get; set; }
        public int Redelivery { get; set; }
        public int RedeliverySum { get; set; }
        public string RedeliveryNum { get; set; }
        public string RedeliveryPayer { get; set; }
        public string OwnerDocumentType { get; set; }
        public string LastCreatedOnTheBasisDocumentType { get; set; }
        public string LastCreatedOnTheBasisPayerType { get; set; }
        public string LastCreatedOnTheBasisDateTime { get; set; }
        public string LastTransactionStatusGM { get; set; }
        public string LastTransactionDateTimeGM { get; set; }
        public string DateCreated { get; set; }
        public int CheckWeight { get; set; }
        public int SumBeforeCheckWeight { get; set; }
        public string PayerType { get; set; }
        public string RecipientFullName { get; set; }
        public string RecipientDateTime { get; set; }
        public string ScheduledDeliveryDate { get; set; }
        public string PaymentMethod { get; set; }
        public string CargoDescriptionString { get; set; }
        public string CargoType { get; set; }
        public string CitySender { get; set; }
        public string CityRecipient { get; set; }
        public string WarehouseRecipient { get; set; }
        public string CounterpartyType { get; set; }
        public int AfterpaymentOnGoodsCost { get; set; }
        public string ServiceType { get; set; }
        public string UndeliveryReasonsSubtypeDescription { get; set; }
        public int WarehouseRecipientNumber { get; set; }
        public string LastCreatedOnTheBasisNumber { get; set; }
        public string PhoneSender { get; set; }
        public string SenderFullNameEW { get; set; }
        public string WarehouseRecipientInternetAddressRef { get; set; }
        public string MarketplacePartnerToken { get; set; }
        public string ClientBarcode { get; set; }
        public string SenderAddress { get; set; }
        public string RecipientAddress { get; set; }
        public string CounterpartySenderDescription { get; set; }
        public string CounterpartySenderType { get; set; }
        public string DateScan { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentStatusDate { get; set; }
        public string AmountToPay { get; set; }
        public string AmountPaid { get; set; }
        public string LastAmountTransferGM { get; set; }
        public string LastAmountReceivedCommissionGM { get; set; }
        public string DocumentCost { get; set; }
        public double DocumentWeight { get; set; }
        public int AnnouncedPrice { get; set; }
        public string UndeliveryReasonsDate { get; set; }
        public string RecipientWarehouseTypeRef { get; set; }
        public string OwnerDocumentNumber { get; set; }
        public string InternationalDeliveryType { get; set; }
        public string WarehouseSender { get; set; }
        public string WarehouseRecipientRef { get; set; }
        public string LoyaltyCardSender { get; set; }
        public string LoyaltyCardRecipient { get; set; }
        public string FactualWeight { get; set; }
        public string DeliveryTimeframe { get; set; }
        public string VolumeWeight { get; set; }
        public string SeatsAmount { get; set; }
        public string ActualDeliveryDate { get; set; }
        public string RefCitySender { get; set; }
        public string RefCityRecipient { get; set; }
        public string CardMaskedNumber { get; set; }
        public string BarcodeRedBox { get; set; }
        public List<object> Packaging { get; set; }
        public int AviaDelivery { get; set; }
        public string OnlineCreditStatus { get; set; }
        public string AdjustedDate { get; set; }
        public int FreeShipping { get; set; }
        public string CheckWeightMethod { get; set; }
        public bool SecurePayment { get; set; }
        public List<object> PartialReturnGoods { get; set; }
        public string CalculatedWeight { get; set; }
        public string CategoryOfWarehouse { get; set; }
        public string WarehouseSenderAddress { get; set; }
        public string WarehouseRecipientAddress { get; set; }
        public bool PossibilityCreateRedirecting { get; set; }
        public bool PossibilityCreateReturn { get; set; }
        public bool PossibilityCreateRefusal { get; set; }
        public bool PossibilityChangeEW { get; set; }
        public bool PossibilityChangeDeliveryIntervals { get; set; }
        public string Status { get; set; }
        public string StatusCode { get; set; }
        public string RefEW { get; set; }
        public string DatePayedKeeping { get; set; }
        public string OnlineCreditStatusCode { get; set; }
    }

    public class NovaPoshtaJSON
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

    public class NovaPoshta
    {
        public static async Task<string> GetTrackingData(string num, string num2)
        {
            object dataObject1;
            string dataObject;

            NovaPoshtaResponse bodyStr = new NovaPoshtaResponse();
            bodyStr.modelName = "TrackingDocument";
            bodyStr.calledMethod = "getStatusDocuments";
            bodyStr.methodProperties = new MethodProperties
            {
                Documents = new List<Document>() { new Document(num, num2)}
            };
            bodyStr.apiKey = "[ВАШ КЛЮЧ]";

            string body = JsonConvert.SerializeObject(bodyStr);

            var client = new HttpClient();
           //var queryString = HttpUtility.ParseQueryString(string.Empty);

            var uri = "http://api.novaposhta.ua/v2.0/json/";

            HttpResponseMessage response;

            byte[] byteData = Encoding.UTF8.GetBytes(body);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
                dataObject = response.Content.ReadAsStringAsync().Result;
                dataObject1 = JsonConvert.DeserializeObject(dataObject);
                //Console.WriteLine(dataObject1.ToString());
            }

            NovaPoshtaJSON myDeserializedClass = JsonConvert.DeserializeObject<NovaPoshtaJSON>(dataObject1.ToString());
            
            return myDeserializedClass.data[0].RecipientFullName;

        }
    }
    
}
