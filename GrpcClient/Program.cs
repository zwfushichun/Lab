using Grpc.Core;
using GrpcLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Do();
            Console.WriteLine("Doaaaa...");
            Console.ReadKey();
        }

        private const int GRPC_MAX_RECEIVE_MESSAGE_LENGTH = (4 * 1024 * 1024) * 10;

        private static async void Do()
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start(); //  开始监视代码运行时间
                var channelOptions = new List<ChannelOption>();
                channelOptions.Add(new ChannelOption(ChannelOptions.MaxReceiveMessageLength, GRPC_MAX_RECEIVE_MESSAGE_LENGTH));

                //Channel channel = new Channel("127.0.0.1:9007", ChannelCredentials.Insecure, channelOptions);
                Channel channel = new Channel("115.29.242.211:9007", ChannelCredentials.Insecure, channelOptions);

                //var client = new GrpcService.GrpcServiceClient(channel);
                //var reply = client.SayHello(new HelloRequest { Id = 100 });
                //Console.WriteLine("来自" + reply.Customers.Count);
                //foreach (var item in reply.Customers)
                //{
                //    Console.WriteLine(item.CnName);
                //}
                var client = new GrpcService.GrpcServiceClient(channel);
                var replay = client.SayHello(new HelloRequest { Id = 100 });

                while (await replay.ResponseStream.MoveNext())
                {
                    var customers = replay.ResponseStream.Current.Customers;
                    Console.WriteLine(customers.Count);
                    //int i = 1;
                    //foreach (var item in customers)
                    //{
                    //    Console.WriteLine(i + ":" + item.CnName);
                    //    //if (i == 10000)
                    //    //break;
                    //    i++;
                    //}
                }
                stopwatch.Stop(); //  停止监视
                TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
                double seconds = timespan.TotalSeconds;  //  总秒数
                double milliseconds = timespan.TotalMilliseconds;  //  总毫秒数
                Console.WriteLine("总秒数:" + seconds);
                Console.WriteLine("总毫秒数:" + milliseconds);
                channel.ShutdownAsync().Wait();
                Console.WriteLine("任意键退出333333333333333...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
