using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Data;
//using System.Text.Json;
namespace ConsoleApp3
{
    internal class Program
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

        static async void Main(string[] args)
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
                //return realtimeposition;

            }
            else
            {
                // 응답이 실패한 경우 오류 메시지 출력
                Console.WriteLine("Failed to retrieve subway position data.");
                //return null;

            }
        }
    }
}
