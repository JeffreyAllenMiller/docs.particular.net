﻿namespace Snippets5.Routing
{
    using NServiceBus;

    class BasicOperations
    {
        void ConcreteMessage(IBus bus)
        {
            #region InstancePublish
            MyEvent message = new MyEvent { SomeProperty = "Hello world" };
            bus.Publish(message);
            #endregion
        }

        void InterfaceMessage(IBus bus)
        {
            #region InterfacePublish
            bus.Publish<IMyEvent>(m => { m.SomeProperty = "Hello world"; });
            #endregion
        }

        void Subscribe(IBus bus)
        {
            #region ExplicitSubscribe
            bus.Subscribe<MyEvent>();

            bus.Unsubscribe<MyEvent>();
            #endregion
        }

        class MyEvent
        {
            public string SomeProperty { get; set; }
        }

        interface IMyEvent
        {
            string SomeProperty { get; set; }
        }

    }
}