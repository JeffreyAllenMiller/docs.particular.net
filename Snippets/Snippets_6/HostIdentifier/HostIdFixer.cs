﻿namespace Snippets6.HostIdentifier
{
    using System;
    using NServiceBus;

    class HostIdFixer
    {
        HostIdFixer(EndpointConfiguration endpointConfiguration)
        {
            #region HostIdFixer

            endpointConfiguration.UniquelyIdentifyRunningInstance()
                .UsingNames("endpointName", Environment.MachineName);
            // or
            Guid hostId = CreateMyUniqueIdThatIsTheSameAcrossRestarts();
            endpointConfiguration.UniquelyIdentifyRunningInstance()
                .UsingCustomIdentifier(hostId);
            
            #endregion
        }

        Guid CreateMyUniqueIdThatIsTheSameAcrossRestarts()
        {
            throw new NotImplementedException();
        }
    }
}
    
