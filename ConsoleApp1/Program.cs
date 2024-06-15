
//using System;
//using System.Net;
//using System.Net.Http;
//using System.IO;
//using System.Xml;
//using Newtonsoft.Json.Linq;

//namespace ConsoleApp1
//{
//    class Program
//    {
//        static HttpClient client = new HttpClient();
//        static void Main(string[] args)
//        {
//            //서울시 지하철 실시간 도착정보(일괄) (xml로 파싱한거) https://www.data.go.kr/data/15125683/openapi.do
//            string url = "http://swopenAPI.seoul.go.kr/api/subway/645a445a4575687637335151626c53/xml/realtimePosition/0/5/1호선"; // URL
//            //url += "0/";
//            //url += "5/";
//            //url += "1호선";
//            var request = (HttpWebRequest)WebRequest.Create(url);
//            request.Method = "GET";

//            string results = string.Empty;
//            HttpWebResponse response;
//            using (response = request.GetResponse() as HttpWebResponse)
//            {
//                StreamReader reader = new StreamReader(response.GetResponseStream());
//                results = reader.ReadToEnd();
//            }

//            //Console.WriteLine(results);
//            //// Load the XML data into an XmlDocument
//            XmlDocument doc = new XmlDocument();
//            doc.LoadXml(results);

//            //// Parse the XML data
//            XmlNodeList itemList = doc.GetElementsByTagName("row");

//            foreach (XmlNode item in itemList)
//            {
//                string subwayNm = item["statnNm"]?.InnerText;
//                if (!string.IsNullOrEmpty(subwayNm))
//                {
//                    Console.WriteLine($"Subway Name: {subwayNm}");
//                }
//                else
//                {
//                    Console.WriteLine("Subway Name not found in the item.");
//                }
//            }

//        }
//    }
//}


//using System;
//using System.Net.Http;
//using Newtonsoft.Json.Linq;
//using System.Threading.Tasks;
//using Newtonsoft.Json;

//namespace ConsoleApp1
//{
//    class Program
//    {
//        static readonly HttpClient client = new HttpClient();

//        static async Task Main(string[] args)
//        {
//            //서울시 지하철 실시간 도착정보(일괄) (json으로 파싱한거)https://www.data.go.kr/data/15125683/openapi.do
//            string url = "http://swopenAPI.seoul.go.kr/api/subway/645a445a4575687637335151626c53/json/realtimePosition/";
//            url += "0/";
//            url += "5/";
//            url += "1호선";
//            // API에 HTTP GET 요청을 보내고 응답을 받음
//            HttpResponseMessage response = await client.GetAsync(url);

//            // 응답이 성공적인지 확인
//            if (response.IsSuccessStatusCode)
//            {
//                // JSON 형식의 응답 데이터를 문자열로 읽어옴
//                string jsonString = await response.Content.ReadAsStringAsync();

//                // 문자열로 읽어온 JSON 데이터를 JObject로 파싱
//                JObject jObject = JObject.Parse(jsonString);

//                Console.WriteLine(jObject["realtimePositionList"]); //다뽑기

//                // 파싱된 데이터에서 recptnDt 필드 값만 추출하여 출력 
//                //JArray realtimePositionList = (JArray)jObject["realtimePositionList"];

//                //foreach (JObject item in realtimePositionList)
//                //{
//                //    Console.WriteLine(item["recptnDt"]);
//                //}
//            }
//            else
//            {
//                // 응답이 실패한 경우 오류 메시지 출력
//                Console.WriteLine("Failed to retrieve subway position data.");
//            }
//        }
//    }
//}

using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Text.Json;

namespace ConsoleApp1
{
    class Program
    {

        public class Realtimeposition
        {
            public Result result { get; set; }
            public List<Row> row { get; set; }
        }

        public class Result
        {
            public string code { get; set; }
            public string developermessage { get; set; }
            public string link { get; set; }
            public string message { get; set; }
            public int status { get; set; }
            public int total { get; set; }
        }

        public class Row
        {
            public int rowNum { get; set; } // 몇번째 줄인지
            public int selectedCount { get; set; } //몇개 출력할건지
            public int totalcount { get; set; } // 현재 운행중인 열차 수 
            public int subwayId { get; set; }// 지하철 호선 id 
            public string subwayNm { get; set; } // 지하철호선명
            public int statnId { get; set; }// 지하철역 id 
            public string statnNm { get; set; } // 지하철역명
            public int trainNo { get; set; } // 열차번호
            public int lastRecptnDt { get; set; } // 최종수신날짜 
            public string recptnDt { get; set; } // 최종수신 시간
            public int updnLine { get; set; }// 상하행선구분
            public int statnTid { get; set; }// 종착지하철역id
            public string statnTnm { get; set; } // 종착 지하철역명
            public int trainSttus { get; set; }//   열차 상태 구분  0진입 1도착 2출발 3전역출발
            public int directAt { get; set; }// 급행여부
            public int lstcarAt { get; set; }// 막차여부 
        }





        static readonly HttpClient client = new HttpClient();

        public async Task<Realtimeposition> www()
        {

            //서울시 지하철 실시간 열차 위치정보 (json으로 파싱한거) https://www.data.go.kr/data/15058569/openapi.do
            string url = "http://swopenapi.seoul.go.kr/api/subway/65724277537568763738636a587a61/json/realtimePosition/";
            url += "0/";
            url += "1/";
            url += "1호선";
            // API에 HTTP GET 요청을 보내고 응답을 받음
            HttpResponseMessage response = await client.GetAsync(url);

            // 응답이 성공적인지 확인
            if (response.IsSuccessStatusCode)
            {
                //var options = new JsonSerializerOptions
                //{
                //    PropertyNameCaseInsensitive = true
                //};


                // JSON 형식의 응답 데이터를 문자열로 읽어옴
                string jsonString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(response.GetType());
                // 문자열로 읽어온 JSON 데이터를 JObject로 파싱
                JObject jObject = JObject.Parse(jsonString);
                Console.WriteLine(jObject.GetType());
                Console.WriteLine(jObject["realtimePositionList"]); //다뽑기

                // 파싱된 데이터에서 recptnDt 필드 값만 추출하여 출력
                JArray realtimePositionList = (JArray)jObject["realtimePositionList"];
                Realtimeposition realtimeposition = new Realtimeposition();
                realtimeposition.row = new List<Row>();
                Row rowasdf = new Row();
               
                //realtimeposition = JsonSerializer.Deserialize<Realtimeposition>(jsonString, options); 왜안됨
                foreach (JObject item in realtimePositionList)
                {
                    //row.rownum  = (int)item["rownum"];
                    //row.selectedcount = (int)item["selectedcount"];
                    rowasdf.rowNum = (int)item["rowNum"];
                    rowasdf.selectedCount = (int)item["selectedCount"];
                    rowasdf.totalcount = (int)item["totalcount"];
                    rowasdf.subwayId = (int)item["subwayId"];
                    rowasdf.subwayNm = (string)item["subwayNm"];
                    rowasdf.trainNo = (int)item["trainNo"];
                    rowasdf.lastRecptnDt = (int)item["lastRecptnDt"];
                    rowasdf.recptnDt = (string)item["recptnDt"];
                    rowasdf.updnLine = (int)item["updnLine"];
                    rowasdf.statnTid = (int)item["statnTid "];
                    rowasdf.statnTnm = (string)item["statnTnm"];
                    rowasdf.trainSttus = (int)item["trainSttus"];
                    rowasdf.directAt = (int)item["directAt"];
                    rowasdf.lstcarAt = (int)item["lstcarAt"];

                    realtimeposition.row.Add(rowasdf);

                }
                return realtimeposition;

            }
            else
            {
                // 응답이 실패한 경우 오류 메시지 출력
                Console.WriteLine("Failed to retrieve subway position data.");
                return null;

            }
        }
    }
}

//(서울특별시_버스 위치정보조회 서비스)
//https://www.data.go.kr/data/15000332/openapi.do

//(서울특별시_버스 도착정보조회 서비스)
//https://www.data.go.kr/data/15000314/openapi.do


//역직렬화를 거쳐야함.
//JsonConvert.DeserializeObject() <- 사용되는 메소드








