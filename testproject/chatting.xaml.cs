﻿using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace testproject
{
    /// <summary>
    /// chatting.xaml에 대한 상호 작용 논리
    /// </summary>
    class cc_info
    {
        public string CCNUM { get; set; }
        public string NAME { get; set; }
        public string ID { get; set; }

    }
    public partial class chatting : Page
    {
        NetworkStream stream = signup.client.GetStream();
        byte[] data = new byte[256];
        public chatting()
        {
            var mainWindow = Application.Current.MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Width = 500; // 원하는 너비로 설정
                mainWindow.Height = 1000; // 원하는 높이로 설정
            }
            InitializeComponent();
            txtbox_chat1.Text = "고객과의 연결을 대기합니다.";
            Task.Run(() => Wait_cc());

        }
        public void Wait_cc() //Task 함수 (서버에선 고객 대기방 함수)
        {
            List<cc_info> infos = new List<cc_info>();
            cc_info info = new cc_info();

            byte[] data1 = new byte[256];
            int bytes = stream.Read(data1, 0, data1.Length);//받는 데이터의 바이트배열, 인덱스, 길이
            string responses = Encoding.UTF8.GetString(data1, 0, bytes);
            Console.WriteLine(responses);
            info.CCNUM = responses;

            data1 = new byte[256];
            bytes = stream.Read(data1, 0, data1.Length);//받는 데이터의 바이트배열, 인덱스, 길이
            responses = Encoding.UTF8.GetString(data1, 0, bytes);
            Console.WriteLine(responses);
            info.NAME = responses;

            data1 = new byte[256];
            bytes = stream.Read(data1, 0, data1.Length);//받는 데이터의 바이트배열, 인덱스, 길이
            responses = Encoding.UTF8.GetString(data1, 0, bytes);
            Console.WriteLine(responses);
            info.ID = responses;

            infos.Add(info);
            Dispatcher.BeginInvoke(new Action(() =>
            {
                liv_info.ItemsSource = infos;
                liv_info.Items.Refresh();
            }));



            data1 = new byte[256];
            bytes = stream.Read(data1, 0, data1.Length);//받는 데이터의 바이트배열, 인덱스, 길이
            responses = Encoding.UTF8.GetString(data1, 0, bytes);
            Console.WriteLine($"responses: {responses}");

            if (responses == "채팅가능")
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    txtbox_send1.IsReadOnly = false;
                    txtbox_chat1.Text = null;

                }));
                Task.Run(() => Read_Chat());
            }

        }
        public void Read_Chat()
        {
            while (true)
            {
                byte[] recv_data = new byte[300];
                int bytes = stream.Read(recv_data, 0, recv_data.Length);
                string responses = Encoding.UTF8.GetString(recv_data, 0, bytes);
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    txtbox_chat1.Text += responses + "\n";
                }));
            }
        }
        void txtbox_send1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string send_message = txtbox_send1.Text;
                if (!string.IsNullOrEmpty(send_message))
                {
                    // 기존 텍스트에 새 메시지를 추가합니다.
                    //txtbox_chat1.Text += send_message + "\n";
                    data = null;
                    data = Encoding.UTF8.GetBytes(send_message);
                    stream.Write(data, 0, data.Length);
                    txtbox_send1.Clear();

                    // 스크롤을 맨 아래로 이동
                    txtbox_chat1.ScrollToEnd();
                }
            }
        }

        private void keyword_buton_Click(object sender, RoutedEventArgs e)
        {

            string send_message = "keyword_token";
            data = null;
            data = Encoding.UTF8.GetBytes(send_message);
            stream.Write(data, 0, data.Length);
            Thread.Sleep(100);

            send_message = keyword.Text;
            if (!string.IsNullOrEmpty(send_message))
            {
                // 기존 텍스트에 새 메시지를 추가합니다.
                //txtbox_chat1.Text += send_message + "\n";
                data = null;
                data = Encoding.UTF8.GetBytes(send_message);
                stream.Write(data, 0, data.Length);
                keyword.Text = "키워드 입력완료";

            }
        }
    }
}
