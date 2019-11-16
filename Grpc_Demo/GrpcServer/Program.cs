using Grpc.Core;
using GrpcLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcServer
{
    class Program
    {

        #region Consts
        /// <summary>4MB(4 * 1024 * 1024) * N, 4MB是Grpc默认传输大小。</summary>
        private const int GRPC_MAX_RECEIVE_MESSAGE_LENGTH = (4 * 1024 * 1024) * 10;
        #endregion


        const int Port = 9007;
        static void Main(string[] args)
        {
            #region Set Channel Options
            var channelOptions = new List<ChannelOption>();

            // add max message length option 设最大接收传输大小
            //channelOptions.Add(new ChannelOption(ChannelOptions.MaxReceiveMessageLength, GRPC_MAX_RECEIVE_MESSAGE_LENGTH));
            channelOptions.Add(new ChannelOption(ChannelOptions.MaxSendMessageLength, GRPC_MAX_RECEIVE_MESSAGE_LENGTH));
            #endregion

            Server server = new Server(channelOptions)
            {
                Services = { GrpcService.BindService(new GrpcImpl()) },
                //Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) },
                Ports = { new ServerPort("115.29.242.211", Port, ServerCredentials.Insecure) },
            };
            server.Start();

            Console.WriteLine("GrpcService server listening on port " + Port);
            Console.WriteLine("任意键退出...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
