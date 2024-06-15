using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static testproject.Window1;

namespace testproject
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window
    {
        static readonly HttpClient client = new HttpClient();
        List<JObject> list_jobject;
        List<string> aaaa = new List<string>();
        public Window1()
        {
            InitializeComponent();
        }


        //public async void info()
        //{
        //    //서울시 지하철 실시간 열차 위치정보 (json으로 파싱한거) https://www.data.go.kr/data/15058569/openapi.do
        //    string url = "http://swopenapi.seoul.go.kr/api/subway/65724277537568763738636a587a61/json/realtimePosition/";
        //    url += "0/";
        //    url += "5/";
        //    url += "1호선";

        //    // API에 HTTP GET 요청을 보내고 응답을 받음
        //    HttpResponseMessage response = await client.GetAsync(url);

        //    // 응답이 성공적인지 확인
        //    if (response.IsSuccessStatusCode)
        //    {
        //        // JSON 형식의 응답 데이터를 문자열로 읽어옴
        //        string jsonString = await response.Content.ReadAsStringAsync();

        //        // 문자열로 읽어온 JSON 데이터를 JObject로 파싱
        //        JObject jObject = JObject.Parse(jsonString);

        //        //Console.WriteLine(jObject["realtimePositionList"]); //다뽑기

        //        // 파싱된 데이터에서 recptnDt 필드 값만 추출하여 출력
        //        JArray realtimePositionList = (JArray)jObject["realtimePositionList"];

        //        foreach (JObject item in realtimePositionList)
        //        {
        //            //Console.WriteLine(item["recptnDt"]);
        //            Console.WriteLine(Convert.ToString(item["recptnDt"]));
        //            aaaa.Add(Convert.ToString(item["recptnDt"]));
        //        }
        //    }
        //    else
        //    {
        //        // 응답이 실패한 경우 오류 메시지 출력
        //        Console.WriteLine("Failed to retrieve subway position data.");
        //    }
        //    try
        //    {

        //        byte[] data;

        //        TcpClient client = new TcpClient("10.10.21.109", 12345); //연결객체
        //        NetworkStream stream = client.GetStream(); //데이터 전송에 사용된 스트림
        //                                                   //string message = "Hello from test";

        //        //data = Encoding.ASCII.GetBytes(aaaa[0]);
        //        //Console.WriteLine("-----");
        //        //Console.WriteLine(data);
        //        //Console.WriteLine("-----");
        //        //stream.Write(data, 0, data.Length);//전송할 데이터의 바이트 배열, 전송을 시작할 배열의 인덱스, 전송할 데이터의 길이.

        //        foreach (var rrr in aaaa)
        //        {
        //            data = null;
        //            data = Encoding.ASCII.GetBytes(rrr);
        //            stream.Write(data, 0, data.Length);//전송할 데이터의 바이트 배열, 전송을 시작할 배열의 인덱스, 전송할 데이터의 길이.
        //        }

        //        data = null;
        //        data = new byte[256];

        //        int bytes = stream.Read(data, 0, data.Length);//받는 데이터의 바이트배열, 인덱스, 길이
        //        string responses = Encoding.ASCII.GetString(data, 0, bytes);
        //        Console.WriteLine("Received: " + responses);

        //        stream.Close();
        //        client.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Exception: " + e.Message);
        //    }
        //}


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
            public int totalCount { get; set; } // 현재 운행중인 열차 수 
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





        //static readonly HttpClient client = new HttpClient();

        public async Task<Realtimeposition> www(string keyword)
        {

            //서울시 지하철 실시간 열차 위치정보 (json으로 파싱한거) https://www.data.go.kr/data/15058569/openapi.do
            string url = "http://swopenapi.seoul.go.kr/api/subway/65724277537568763738636a587a61/json/realtimePosition/";
            url += "0/"; // 몇번째 부터 뽑을건지 
            url += "1/"; // 몇개까지 뽑을 건지
            url += keyword; // 
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
                //Console.WriteLine(response.GetType());
                // 문자열로 읽어온 JSON 데이터를 JObject로 파싱
                JObject jObject = JObject.Parse(jsonString);
                //Console.WriteLine(jObject.GetType());
                //Console.WriteLine(jObject["realtimePositionList"]); //다뽑기

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
                    rowasdf.totalCount = (int)item["totalCount"];
                    rowasdf.subwayId = (int)item["subwayId"];
                    rowasdf.subwayNm = (string)item["subwayNm"];
                    rowasdf.trainNo = (int)item["trainNo"];
                    rowasdf.lastRecptnDt = (int)item["lastRecptnDt"];
                    rowasdf.recptnDt = (string)item["recptnDt"];
                    rowasdf.updnLine = (int)item["updnLine"];
                    rowasdf.statnTid = (int)item["statnTid"];
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }

        public async void Button_Click_1(object sender, RoutedEventArgs e)
        {
         

            string bb;
            bb =aa.Text;
           
            Realtimeposition realtimeposition1 = new Realtimeposition();
            realtimeposition1 =  await  www(bb);
            search_view.Text += realtimeposition1.row[0].rowNum+ "번"+"\n";
            search_view.Text += "현재 운행중인 열차: "+realtimeposition1.row[0].totalCount  +"\n";
            search_view.Text += "지하철 호선 아이디: " + realtimeposition1.row[0].subwayId + "\n";
            search_view.Text += "지하철 호선 명: "+ realtimeposition1.row[0].statnNm + "\n";
            search_view.Text += "지하철역 아이디: "+ realtimeposition1.row[0].trainNo + "\n";
            search_view.Text += "최종수신 날짜: " + realtimeposition1.row[0].lastRecptnDt + "\n";
            search_view.Text += "최종수신 시간: "+ realtimeposition1.row[0].recptnDt + "\n";
            search_view.Text += "상하행선 구분: " + realtimeposition1.row[0].updnLine + "\n";
            search_view.Text += "종착역id" + realtimeposition1.row[0].statnTid + "\n";
            search_view.Text += "종착역이름: " + realtimeposition1.row[0].statnTnm + "\n";
            search_view.Text += "열차상태: " + realtimeposition1.row[0].trainSttus + "\n";
            search_view.Text += "급행여부:  " +realtimeposition1.row[0].directAt + "\n";
            search_view.Text += "막차여부: "+realtimeposition1.row[0].lstcarAt + "\n";





            //scroll_view

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
