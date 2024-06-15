using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace testproject
{
    /// <summary>
    /// Page2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        public void CS_login(string id, string pw) // 상담원 로그인 
        {
            try
            {
                List<string> aaaa = new List<string>();
                byte[] data;

                TcpClient client = new TcpClient("10.10.21.111", 5558); //연결객체
                NetworkStream stream = client.GetStream(); //데이터 전송에 사용된 스트림
                //string message = "Hello from test";

                string send_msg;
                //data = Encoding.ASCII.GetBytes(aaaa[0]);
                //Console.WriteLine("-----");
                //Console.WriteLine(data);
                //Console.WriteLine("-----");
                //stream.Write(data, 0, data.Length);//전송할 데이터의 바이트 배열, 전송을 시작할 배열의 인덱스, 전송할 데이터의 길이.
                //send_msg = "_for_abc_signup_";
                //data = null;
                //data = Encoding.UTF8.GetBytes(send_msg);
                ////txtbox_chat1.Text += send_msg;
                //stream.Write(data, 0, data.Length);//전송할 데이터의 바이트 배열, 전송을 시작할 배열의 인덱스, 전송할 데이터의 길이.
                //txtbox_chat1.Text += Encoding.UTF8.GetString(data);
                //send_msg = "";

                // 회원가입 플래그 보내기 

                data = null;
                data = new byte[256];
                send_msg = "_for_cs_login_";// 시그널
                data = Encoding.UTF8.GetBytes(send_msg);
                stream.Write(data, 0, data.Length);//전송할 데이터의 바이트 배열, 전송을 시작할 배열의 인덱스, 전송할 데이터의 길이.

                Thread.Sleep(100);


                data = null;
                data = new byte[256];
                send_msg = id;// 아이디

                data = Encoding.UTF8.GetBytes(send_msg);
                stream.Write(data, 0, data.Length);//전송할 데이터의 바이트 배열, 전송을 시작할 배열의 인덱스, 전송할 데이터의 길이.

                Thread.Sleep(100);



                data = null;
                data = new byte[256];
                send_msg = pw;// 비밀번호 
                data = Encoding.UTF8.GetBytes(send_msg);
                stream.Write(data, 0, data.Length);//전송할 데이터의 바이트 배열, 전송을 시작할 배열의 인덱스, 전송할 데이터의 길이.

                Thread.Sleep(100);


                //_for_abc_login_
                // 채팅아니라서 주석처리 
                //회원가입을 하려면 3가지를 전송해야함 플래그 아이디 비번 이름 

                int bytes = stream.Read(data, 0, data.Length);//받는 데이터의 바이트배열, 인덱스, 길이
                string responses = Encoding.UTF8.GetString(data, 0, bytes);
                MessageBox.Show(responses);
                //txtbox_chat1.Text += ("Received: " + responses + "\n");
                //bytes = stream.Read(data, 0, data.Length);//받는 데이터의 바이트배열, 인덱스, 길이
                //responses = Encoding.UTF8.GetString(data, 0, bytes);
                //txtbox_chat1.Text += ("Received: " + responses + "\n");
                //bytes = stream.Read(data, 0, data.Length);//받는 데이터의 바이트배열, 인덱스, 길이
                //responses = Encoding.UTF8.GetString(data, 0, bytes);
                //txtbox_chat1.Text += ("Received: " + responses + "\n");
                //bytes = stream.Read(data, 0, data.Length);//받는 데이터의 바이트배열, 인덱스, 길이
                //responses = Encoding.UTF8.GetString(data, 0, bytes);
                //txtbox_chat1.Text += ("Received: " + responses + "\n");

                stream.Close();
                client.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
            }
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            
           string gg= Login_id.Text.ToUpper();
            string ggg= Login_pw.Text.ToUpper();
            CS_login(gg,ggg);
            Login_id.Clear();
            Login_pw.Clear();
        }
    }
}
