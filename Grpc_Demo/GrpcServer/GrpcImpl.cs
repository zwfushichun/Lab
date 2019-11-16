using Grpc.Core;
using GrpcLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcServer
{
    class GrpcImpl : GrpcService.GrpcServiceBase
    {
        public override async Task SayHello(HelloRequest request, IServerStreamWriter<HelloResponse> responseStream, ServerCallContext context)
        {
            string sql = "SELECT TOP 40000 * FROM dbo.Customer";
            HelloResponse response = new HelloResponse();
            var items = await DBOperationHelper.GetListAsync<CustomerOutput>(sql);
            response.Customers.AddRange(items);
            await responseStream.WriteAsync(response);
        }

        //public override async Task<HelloResponse> SayHello(HelloRequest request, ServerCallContext context)
        //{
        //    try
        //    {
        //        string sql = "SELECT TOP 10000 * FROM dbo.Customer";
        //        HelloResponse response = new HelloResponse();
        //        var items = await DBOperationHelper.GetListAsync<CustomerOutput>(sql);
        //        response.Customers.AddRange(items);
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //public override Task<IEnumerable<CustomerOutput>> SayHello(HelloRequest request, ServerCallContext context)
        //{
        //    string sql = "SELECT TOP 10 * FROM dbo.Customer ";
        //    return DBOperationHelper.GetListAsync<CustomerOutput>(sql);
        //    //return Task.FromResult(new CustomerOutput { CnAddress = "China" + request.Id });
        //}
    }
}
