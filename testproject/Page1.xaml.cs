using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
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
    /// Page1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page1 : Page
    {
        TcpClient client = new TcpClient("10.10.21.111", 5558); //연결객체
        //NetworkStream stream = client.GetStream(); //데이터 전송에 사용된 스트림
        public Page1()
        {
            InitializeComponent();

        }
        public void persnal_chat(/*string send_message*/) // 회원가입
        {
            try
            {
                List<string> aaaa = new List<string>();
                byte[] data;

                //TcpClient client = new TcpClient("10.10.21.111", 5556); //연결객체
                NetworkStream stream = client.GetStream(); //데이터 전송에 사용된 스트림
                //string message = "Hello from test";

                string send_msg;
                //data = Encoding.ASCII.GetBytes(aaaa[0]);
                //Console.WriteLine("-----");
                //Console.WriteLine(data);
                //Console.WriteLine("-----");
                //stream.Write(data, 0, data.Length);//전송할 데이터의 바이트 배열, 전송을 시작할 배열의 인덱스, 전송할 데이터의 길이.
                send_msg = "_for_abc_signup_";
                data = null;
                data = Encoding.UTF8.GetBytes(send_msg);
                //txtbox_chat1.Text += send_msg;
                stream.Write(data, 0, data.Length);//전송할 데이터의 바이트 배열, 전송을 시작할 배열의 인덱스, 전송할 데이터의 길이.
                txtbox_chat1.Text += Encoding.UTF8.GetString(data);
                Thread.Sleep(100);
                send_msg = "";
                // 회원가입 플래그 보내기 

                data = null;
                data = new byte[256];
                send_msg = "kimgun";// 이름
                data = Encoding.UTF8.GetBytes(send_msg);
                txtbox_chat1.Text += Encoding.UTF8.GetString(data);
                stream.Write(data, 0, data.Length);//전송할 데이터의 바이트 배열, 전송을 시작할 배열의 인덱스, 전송할 데이터의 길이.
                send_msg = "";
                Thread.Sleep(100);


                // 이름

                data = null;
                data = new byte[256];
                send_msg = "qwer"; // id
                data = Encoding.UTF8.GetBytes(send_msg);
                txtbox_chat1.Text += Encoding.UTF8.GetString(data);



                stream.Write(data, 0, data.Length);//전송할 데이터의 바이트 배열, 전송을 시작할 배열의 인덱스, 전송할 데이터의 길이.
                send_msg = "";

                Thread.Sleep(100);

                data = null;
                data = new byte[256];
                send_msg = "1234"; // pw
                data = Encoding.UTF8.GetBytes(send_msg);
                txtbox_chat1.Text += Encoding.UTF8.GetString(data);


                stream.Write(data, 0, data.Length);//전송할 데이터의 바이트 배열, 전송을 시작할 배열의 인덱스, 전송할 데이터의 길이.
                send_msg = "";





                //_for_abc_login_
                // 채팅아니라서 주석처리 
                //회원가입을 하려면 3가지를 전송해야함 플래그 아이디 비번 이름 
                //data = null;
                //data = new byte[256];
                //int bytes = stream.Read(data, 0, data.Length);//받는 데이터의 바이트배열, 인덱스, 길이
                //string responses = Encoding.UTF8.GetString(data, 0, bytes);
                //txtbox_chat1.Text += ("Received: " + responses + "\n");

                //Thread.Sleep(100);

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

       

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
        }

        private void txtbox_send_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string send_message = txtbox_send1.Text;
                persnal_chat();
                txtbox_send1.Clear();
                txtbox_chat1.ScrollToEnd();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
