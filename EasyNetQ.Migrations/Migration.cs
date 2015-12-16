﻿namespace EasyNetQ.Migrations {
    using System;
    using System.Collections.Generic;
    using EasyNetQ.Migrations.Actions;
    using EasyNetQ.Management.Client;

    public abstract class Migration : IDeclare, IDelete {
        readonly List<MigrationAction> _actions = new List<MigrationAction>();

        public IDeclare Declare => this;
        public IDelete Delete => this;

        IBindingDeclare IDeclare.Binding() {
            var bindingDeclare = new BindingDeclareAction();
            _actions.Add(bindingDeclare);
            return bindingDeclare;
        }

        IExchangeDeclare IDeclare.Exchange(Type messageType) {
            var exchangeDeclare = new ExchangeDeclareAction(messageType);
            _actions.Add(exchangeDeclare);
            return exchangeDeclare;
        }

        IExchangeDeclare IDeclare.Exchange(String name) {
            var exchangeDeclare = new ExchangeDeclareAction(name);
            _actions.Add(exchangeDeclare);
            return exchangeDeclare;
        }

        IQueueDeclare IDeclare.Queue(String name) {
            var queueDeclare = new QueueDeclareAction(name);
            _actions.Add(queueDeclare);
            return queueDeclare;
        }

        IQueueDeclare IDeclare.Queue(Type messageType, String subscriptionId) {
            var queueDeclare = new QueueDeclareAction(messageType, subscriptionId);
            _actions.Add(queueDeclare);
            return queueDeclare;
        }

        IExchangeDelete IDelete.Exchange(String exchange) {
            var exchangeDelete = new ExchangeDeleteAction(exchange);
            _actions.Add(exchangeDelete);
            return exchangeDelete;
        }

        IExchangeDelete IDelete.Exchange(Type messageType) {
            var exchangeDelete = new ExchangeDeleteAction(messageType);
            _actions.Add(exchangeDelete);
            return exchangeDelete;
        }

        IQueueDelete IDelete.Queue(String queue) {
            var queueDelete = new QueueDeleteAction(queue);
            _actions.Add(queueDelete);
            return queueDelete;
        }

        IQueueDelete IDelete.Queue(Type messageType, String subscriptionId) {
            var queueDelete = new QueueDeleteAction(messageType, subscriptionId);
            _actions.Add(queueDelete);
            return queueDelete;
        }

        public abstract void Apply();

        public void Run(IManagementClient managementClient) {
            Apply();

            _actions.ForEach(action => action.VerifyState());
            _actions.ForEach(action => action.Apply(managementClient));
        }
    }
}