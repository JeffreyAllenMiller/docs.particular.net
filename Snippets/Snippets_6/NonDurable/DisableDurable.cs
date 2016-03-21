﻿namespace Snippets6.NonDurable
{
    using NServiceBus;

    class DisableDurable
    {
        DisableDurable(EndpointConfiguration endpointConfiguration)
        {
            #region set-to-non-durable
            endpointConfiguration.DisableDurableMessages();

            #endregion
        }

    }
}