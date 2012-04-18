using System;
using Caliburn.Micro;
using Documently.Commands;
using Documently.Infrastructure;
using Magnum;

namespace Documently.WpfClient.Modules.DocumentDetails.CreateMeta {
    public class CreateDocumentMetaDataViewModel : Screen {
        private readonly IBus _Bus;
        private readonly IEventAggregator _EventAggregator;

        public CreateDocumentMetaDataViewModel(IBus bus, IEventAggregator eventAggregator) {
            _Bus = bus;
            _EventAggregator = eventAggregator;
            Command = new CreateDocumentMetaData(CombGuid.Generate(), "New meta data", DateTime.UtcNow);
        }

        public CreateDocumentMetaData Command { get; private set; }

        public void Save() {
            _Bus.Send(Command);

            // Signal for UI
            _EventAggregator.Publish(new DocumentMetaDataSaved());
        }
    }

    public class DocumentMetaDataSaved { }
}