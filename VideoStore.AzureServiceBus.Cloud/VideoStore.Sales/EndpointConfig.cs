using System.Diagnostics;

namespace VideoStore.Sales
{
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint, AsA_Worker, UsingTransport<AzureServiceBus>, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                .DefaultBuilder()
                .RijndaelEncryptionService();
        }
    }

    public class MyClass:IWantToRunWhenBusStartsAndStops
    {
        public void Start()
        {
            Trace.WriteLine("The VideoStore.Sales endpoint is now started and ready to accept messages");
        }

        public void Stop()
        {
            
        }
    }

}
