﻿namespace VideoStore.ECommerce.Handlers
{
    using System.Linq;
    using Microsoft.AspNet.SignalR;
    using Messages.Events;
    using NServiceBus;

    public class OrderIsReadyHandler :  IHandleMessages<DownloadIsReady>
    {
        public void Handle(DownloadIsReady message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<OrdersHub>();

            context.Clients.Group(message.ClientId).orderReady(new
                {
                    message.OrderNumber, 
                    VideoUrls = message.VideoUrls.Select(pair => new {Id = pair.Key, Url = pair.Value}).ToArray()
                });
        }
    }
}