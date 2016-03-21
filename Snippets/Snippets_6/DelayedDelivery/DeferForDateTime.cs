namespace Snippets6.DelayedDelivery
{
    using System;
    using System.Threading.Tasks;
    using NServiceBus;

    class DeferForDateTime
    {
        async Task SendDelayedMessage(IEndpointInstance endpoint, IMessageHandlerContext handlerContext)
        {
            #region delayed-delivery-datetime
            SendOptions options = new SendOptions();
            options.DoNotDeliverBefore(new DateTime(2016, 12, 25));

            await handlerContext.Send(new MessageToBeSentLater(), options);
            // OR
            await endpoint.Send(new MessageToBeSentLater(), options);
            #endregion
        }

        class MessageToBeSentLater
        {
        }
    }
}