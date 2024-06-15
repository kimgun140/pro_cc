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
    /// signup.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class signup : Page
    {
        public static TcpClient client = new TcpClient("10.10.21.111", 5558);
        public signup()
        {
            InitializeComponent();

        }
        //여기 아니네 
        public int CS_login(string id, string pw) // 상담원 로그인 
        {
            try
            {
                List<string> aaaa = new List<string>();
                byte[] data;

                //연결객체
                NetworkStream stream = client.GetStream(); //데이터 전송에 사용된 스트림
                string send_msg;

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



                //read
                data = null;
                data = new byte[256];
                int bytes = stream.Read(data, 0, data.Length);//받는 데이터의 바이트배열, 인덱스, 길이
                string responses = Encoding.UTF8.GetString(data, 0, bytes);
                MessageBox.Show(responses);
                if (responses == "로그인 되었습니다.\n")
                {

                    return 1;
                }
                return -1;

            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
                return -1;
            }
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {

            string pw = MyTextBoxpw.Text.ToUpper();
            string id = MyTextBoxid.Text.ToUpper();

            int qqwwee= CS_login(id, pw);

            MyTextBoxid.Clear();
            MyTextBoxpw.Clear();
            if(qqwwee == 1)
            {
                NavigationService.Navigate(
                new Uri("/chatting.xaml", UriKind.Relative));
            }
        }

        private void Button_Click_close(object sender, RoutedEventArgs e)
        {
            NetworkStream stream = client.GetStream();
            byte[] data1;
            string send_msg = "Close_msg";
            data1 = null;
            data1 = new byte[256];

            data1 = Encoding.UTF8.GetBytes(send_msg);
            stream.Write(data1, 0, data1.Length);//전송할 데이터의 바이트 배열, 전송을 시작할 배열의 인덱스, 전송할 데이터의 길이.
            Thread.Sleep(100);


            data1 = null;
            data1 = new byte[256];
            int bytes = stream.Read(data1, 0, data1.Length);//받는 데이터의 바이트배열, 인덱스, 길이
            string responses = Encoding.UTF8.GetString(data1, 0, bytes); // 서버가 종료 시그널 받고 "종료"라고 보낼거임 
            if (responses == "종료\n")
            {
                stream.Close();
                client.Close();
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }


        }
    }
}
