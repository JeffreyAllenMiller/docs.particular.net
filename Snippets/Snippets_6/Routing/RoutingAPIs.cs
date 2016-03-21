﻿// ReSharper disable UnusedParameter.Local
namespace Snippets6.Routing
{
    using System;
    using System.Collections.Generic;
    using NServiceBus;
    using NServiceBus.Routing;
    using NServiceBus.Settings;
    using NServiceBus.Transports;

    class RoutingAPIs
    {
        void StaticRoutes(EndpointConfiguration endpointConfiguration)
        {
            #region Routing-StaticRoutes-Endpoint

            endpointConfiguration.Routing()
                .UnicastRoutingTable.RouteToEndpoint(typeof(AcceptOrder), "Sales");

            #endregion

            #region Routing-StaticRoutes-Endpoint-Msmq

            endpointConfiguration.Routing()
                .UnicastRoutingTable.RouteToEndpoint(typeof(AcceptOrder), "Sales");

            #endregion

            #region Routing-StaticRoutes-Endpoint-Broker

            endpointConfiguration.Routing()
                .UnicastRoutingTable.RouteToEndpoint(typeof(AcceptOrder), "Sales");

            #endregion

            #region Routing-StaticRoutes-Address

            endpointConfiguration.Routing()
                .UnicastRoutingTable.RouteToAddress(typeof(AcceptOrder), "Sales@SomeMachine");

            #endregion
        }

        void DynamicRoutes(EndpointConfiguration endpointConfiguration)
        {
            #region Routing-DynamicRoutes

            endpointConfiguration.Routing().UnicastRoutingTable
                .AddDynamic((types, contextBag) => new[]
                {
                    //Use endpoint name
                    new UnicastRoute("Sales"),
                    //Use endpoint instance name
                    new UnicastRoute(new EndpointInstance("Sales", "1")),
                    //Use transport address (e.g. MSMQ)
                    new UnicastRoute("Sales-2@MachineA")
                });

            #endregion
        }

        void CustomRoutingStore(EndpointConfiguration endpointConfiguration)
        {
            #region Routing-CustomRoutingStore

            endpointConfiguration.Routing().UnicastRoutingTable.AddDynamic((t, c) =>
                LoadFromCache(t) ?? LoadFromDatabaseAndPutToCache(t));

            #endregion
        }

        void StaticEndpointMapping(EndpointConfiguration endpointConfiguration)
        {
            #region Routing-StaticEndpointMapping

            EndpointName sales = new EndpointName("Sales");
            endpointConfiguration.Routing().EndpointInstances
                .AddStatic(sales,
                    new EndpointInstance(sales, "1", null),
                    new EndpointInstance(sales, "2", null));

            #endregion
        }

        void DynamicEndpointMapping(EndpointConfiguration endpointConfiguration)
        {
            #region Routing-DynamicEndpointMapping

            endpointConfiguration.Routing().EndpointInstances.AddDynamic(async e =>
            {
                if (e.ToString().StartsWith("Sales"))
                {
                    return new[]
                    {
                        new EndpointInstance(e, "1").SetProperty("SomeProp", "SomeValue"),
                        new EndpointInstance(e, "2").AtMachine("B")
                    };
                }
                return null;
            });

            #endregion
        }

        void SpecialCaseTransportAddress(EndpointConfiguration endpointConfiguration)
        {
            #region Routing-SpecialCaseTransportAddress

            EndpointInstance endpointInstance = new EndpointInstance("Sales", "1");
            endpointConfiguration
                .UseTransport<MyTransport>()
                .AddAddressTranslationException(endpointInstance, "Sales-One@MachineA");

            #endregion
        }

        // ReSharper disable once ConvertClosureToMethodGroup
        void TransportAddressRules(EndpointConfiguration endpointConfiguration)
        {
            #region Routing-TransportAddressRule

            endpointConfiguration
                .UseTransport<MyTransport>()
                .AddAddressTranslationRule(i => CustomTranslationRule(i));

            #endregion
        }

        string CustomTranslationRule(LogicalAddress endpointInstanceName)
        {
            throw new NotImplementedException();
        }

        void FileBasedRouting(EndpointConfiguration endpointConfiguration)
        {
            #region Routing-FileBased-Config

            RoutingSettings routingSettings = endpointConfiguration.Routing();
            routingSettings.UnicastRoutingTable.RouteToEndpoint(typeof(AcceptOrder), "Sales");
            routingSettings.UnicastRoutingTable.RouteToEndpoint(typeof(SendOrder), "Shipping");
            routingSettings.UseFileBasedEndpointInstanceMapping(@"C:\Routes.xml");

            #endregion
        }


        IEnumerable<IUnicastRoute> LoadFromDatabaseAndPutToCache(List<Type> type)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IUnicastRoute> LoadFromCache(List<Type> type)
        {
            throw new NotImplementedException();
        }

        class AcceptOrder
        {
        }

        class SendOrder
        {
        }

        class OrderAccepted
        {
        }

        class MyTransport : TransportDefinition
        {
            protected override TransportInfrastructure Initialize(SettingsHolder settings, string connectionString)
            {
                throw new NotImplementedException();
            }

            public override string ExampleConnectionStringForErrorMessage { get; }
        }
    }
}