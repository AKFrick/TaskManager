using System;
using TaskManager.TaskProxy;
using System.Threading.Tasks;
using Workstation.ServiceModel.Ua;
using Workstation.ServiceModel.Ua.Channels;


namespace TaskManager.Model
{
    public class OpcUa
    {
        ApplicationDescription clientDescription;
        UaTcpSessionChannel channel;
        CallResponse callResponse;
        string ServerUri = "opc.tcp://192.168.10.2:4840";
        CallMethodRequest callMethodRequest = new CallMethodRequest
        {
            ObjectId = new NodeId("\"SendNewTask\"", 3),
            MethodId = new NodeId("\"SendNewTask\".Method", 3)
        };
        public OpcUa()
        {
            clientDescription = new ApplicationDescription
            {
                ApplicationName = "Promatis.UaClient",
                ApplicationUri = $"urn:{System.Net.Dns.GetHostName()}:Promatis.UaClient",
                ApplicationType = ApplicationType.Client
            };
            channel = new UaTcpSessionChannel(
                clientDescription,
                null,
                new AnonymousIdentity(),
                ServerUri,
                SecurityPolicyUris.None);
        }
        public async System.Threading.Tasks.Task SendTask(TaskProxy.Task task)
        {
            try
            {
                Log.GetLog().logThis($"Отправляем испытание");
                await channel.OpenAsync();
                Log.GetLog().logThis($"открыли коннект");
                callMethodRequest.InputArguments = new Variant[]
                {
                    new Variant((Int16)task.ID),
                    new Variant((String)task.Number),
                    new Variant((String)task.Item),
                    new Variant((String)task.StartSerial),
                    new Variant((Int16)task.TargetCount),
                };
                CallRequest callRequest = new CallRequest
                {
                    MethodsToCall = new[] { callMethodRequest }
                };
                Log.GetLog().logThis($"Испытание отправлено в ПЛК: {task.PrintToString()}");
                callResponse = await channel.CallAsync(callRequest);
                await channel.CloseAsync();                
            }
            catch (Exception ex)
            {
                await channel.AbortAsync();
                Log.GetLog().logThis(ex.Message);
            }
        }
        public async System.Threading.Tasks.Task UaClient()
        {
            // describe this client application.
            var clientDescription = new ApplicationDescription
            {
                ApplicationName = "Workstation.UaClient.FeatureTests",
                ApplicationUri = $"urn:{System.Net.Dns.GetHostName()}:Workstation.UaClient.FeatureTests",
                ApplicationType = ApplicationType.Client
            };

            // create a 'UaTcpSessionChannel', a client-side channel that opens a 'session' with the server.
            var channel = new UaTcpSessionChannel(
                clientDescription,
                null, // no x509 certificates
                new AnonymousIdentity(), // no user identity
                "opc.tcp://192.168.10.2:4840", // the public endpoint of a server at opcua.rocks.
                SecurityPolicyUris.None); // no encryption
            try
            {
                // try opening a session and reading a few nodes.
                await channel.OpenAsync();
                Log.GetLog().logThis($"Opened session with endpoint '{channel.RemoteEndpoint.EndpointUrl}'.");
                Log.GetLog().logThis($"SecurityPolicy: '{channel.RemoteEndpoint.SecurityPolicyUri}'.");
                Log.GetLog().logThis($"SecurityMode: '{channel.RemoteEndpoint.SecurityMode}'.");
                Log.GetLog().logThis($"UserIdentityToken: '{channel.UserIdentity}'.");
                var readRequest = new ReadRequest
                {
                    // set the NodesToRead to an array of ReadValueIds.
                    NodesToRead = new[] {
                    // construct a ReadValueId from a NodeId and AttributeId.
                    new ReadValueId {
                        // you can parse the nodeId from a string.
                        // e.g. NodeId.Parse("ns=2;s=Demo.Static.Scalar.Double")
                        NodeId = NodeId.Parse(VariableIds.Server_ServerStatus),
                        // variable class nodes have a Value attribute.
                        AttributeId = AttributeIds.Value
                    },
                    new ReadValueId
                    {
                        NodeId = new NodeId("\"OPCUA\".\"MyInt\"", 3),
                        AttributeId = AttributeIds.Value
                    }
                }
                };
                var readResult = await channel.ReadAsync(readRequest);
                var MyInt = readResult.Results[1];
                Log.GetLog().logThis($" OPCUA.MyInt: { MyInt.GetValueOrDefault<Int16>()}");
                await channel.CloseAsync();
            }
            catch (Exception ex)
            {
                await channel.AbortAsync();
                Log.GetLog().logThis(ex.Message);
            }
        }
    }
}
