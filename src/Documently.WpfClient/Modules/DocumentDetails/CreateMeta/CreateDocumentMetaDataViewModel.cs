using System;
using Caliburn.Micro;

using Documently.Infrastructure;
using Documently.Messages.DocMetaCommands;
using Documently.WpfClient.Commands;
using Magnum;
using MassTransit;

namespace Documently.WpfClient.Modules.DocumentDetails.CreateMeta {
    public class CreateDocumentMetaDataViewModel : Screen {
        private readonly IBus _Bus;
        private readonly IEventAggregator _EventAggregator;

        public CreateDocumentMetaDataViewModel(IBus bus, IEventAggregator eventAggregator) {
            _Bus = bus;
            _EventAggregator = eventAggregator;
            Command = new CreateDocumentMetaData(NewId.Next(), 0, "Title goes here", DateTime.UtcNow);
        }

        public Create Command { get; private set; }

        public void Save() {
            _Bus.Send(Command);

            // Signal for UI
            _EventAggregator.Publish(new DocumentMetaDataSaved());
        }
    }

    public class DocumentMetaDataSaved { }
}