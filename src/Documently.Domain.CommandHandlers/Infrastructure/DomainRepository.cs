﻿// Copyright 2012 Henrik Feldt
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.

using System;
using System.Collections.Generic;
using Documently.Messages;
using EventStore;
using MassTransit;

namespace Documently.Domain.CommandHandlers.Infrastructure
{
	public interface DomainRepository
	{
		T GetById<T>(NewId aggregateId, uint version)
			where T : class, AggregateRoot, EventAccessor;

		void Save<T>(T aggregate, NewId commitId, IDictionary<string, string> headers)
			where T : class, AggregateRoot;
	}

	public class EventStoreRepository : DomainRepository
	{
		private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
		private readonly IStoreEvents _eventStore;
		private readonly AggregateRootFactory _factory;

		public EventStoreRepository(IStoreEvents eventStore, AggregateRootFactory factory)
		{
			if (eventStore == null) throw new ArgumentNullException("eventStore");
			if (factory == null) throw new ArgumentNullException("factory");
			_eventStore = eventStore;
			_factory = factory;
		}

		public T GetById<T>(NewId aggregateId, uint version)
			where T : class, AggregateRoot, EventAccessor
		{
			try
			{
				var stream = _eventStore.OpenStream(aggregateId.ToGuid(), checked((int) version), int.MaxValue);
				var ar = _factory.Build(typeof (T), aggregateId, null) as T;

				foreach (var evt in stream.CommittedEvents)
					ar.Events.ApplyEvent(evt.Body as DomainEvent);

				return ar;
			}
			catch (OverflowException e)
			{
				_logger.Error("Congratulations, your domain object has over two billion events; " +
				              "you should consider customizing the EventStore library for your purposes.", e);
				throw;
			}
		}

		public void Save<T>(T aggregate, NewId commitId, IDictionary<string, string> headers) 
			where T : class, AggregateRoot
		{
		}
	}
}