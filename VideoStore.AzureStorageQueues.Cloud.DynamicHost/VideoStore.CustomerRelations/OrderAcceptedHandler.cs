﻿using NServiceBus.Logging;

namespace VideoStore.CustomerRelations
{
    using System;
    using System.Diagnostics;
    using Messages.Events;
    using NServiceBus;
    using Common;

    class OrderAcceptedHandler : IHandleMessages<OrderAccepted>
    {
        static ILog Log = LogManager.GetLogger(typeof(OrderAcceptedHandler));

        public IBus Bus { get; set; }
        public void Handle(OrderAccepted message)
        {
            if (DebugFlagMutator.Debug)
            {
                Debugger.Break();
            }

            Log.DebugFormat("Customer: {0} is now a preferred customer -- raising in-memory event, & publishing for other service concerns", message.ClientId);
            // Call the domain object to do the raising of the event based on the relevant condition
            Bus.InMemory.Raise<ClientBecamePreferred>(m =>
            {
                m.ClientId = message.ClientId;
                m.PreferredStatusExpiresOn = DateTime.Now.AddMonths(2);
            });

            // Yes, you can also publish this event as an asynchronous event
            Bus.Publish<ClientBecamePreferred>(m =>
            {
                m.ClientId = message.ClientId;
                m.PreferredStatusExpiresOn = DateTime.Now.AddMonths(2);
            });
        }
    }
}
