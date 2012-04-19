using System;
using System.Collections.Generic;
using System.Linq;
using Documently.Domain;
using Documently.Domain.CommandHandlers.ForDocMeta;
using Documently.Messages;
using Documently.Messages.DocMetaCommands;
using Documently.Messages.DocMetaEvents;
using MassTransit;
using MassTransit.Context;
using NUnit.Framework;
using SharpTestsEx;

namespace Documently.Specs.Documents
{
	public class when_document_is_associated_with_a_collection
		: CommandTestFixture<AssociateWithCollection, AssociateWithCollectionHandler, DocMeta>
	{
		private readonly NewId docId = NewId.Next();
		private readonly NewId collectionId = NewId.Next();

		protected override IEnumerable<DomainEvent> Given()
		{
			return new List<DomainEvent>
				{
					null //new Created(docId, "title",  DateTime.UtcNow)
				};
		}

		protected override AssociateWithCollection When()
		{
			return null;//new AssociateWithCollection(docId, collectionId);
		}

		protected override void Consume(ConsumeContext<AssociateWithCollection> cmd)
		{
			CommandHandler.Consume(cmd);
		}

		[Test]
		public void then_an_event_of_the_association_is_sent()
		{
			var evt = (AssociatedWithCollection) PublishedEventsT.First();
			evt.AggregateId.Should().Be(docId);
			evt.CollectionId.Should().Be(collectionId);
		}
	}
}