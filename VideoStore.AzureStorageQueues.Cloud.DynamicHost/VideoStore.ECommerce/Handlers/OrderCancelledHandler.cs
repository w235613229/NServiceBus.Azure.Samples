﻿namespace VideoStore.ECommerce.Handlers
{
    using Microsoft.AspNet.SignalR;
    using Messages.Events;
    using NServiceBus;

    public class OrderCancelledHandler : IHandleMessages<OrderCancelled>
    {
        public void Handle(OrderCancelled message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<OrdersHub>();

            context.Clients.Group(message.ClientId).orderCancelled(new
                {
                    message.OrderNumber,
                });
        }
    }
}