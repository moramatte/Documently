using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Documently.Messages.DocMetaCommands;
using MassTransit;

namespace Documently.WpfClient.Commands {
    public class CreateDocumentMetaData : Create {
        public CreateDocumentMetaData(NewId id, uint version, string title, DateTime utcTime) {
            AggregateId = id;
            Version = version;
            Title = title;
            UtcTime = utcTime;
        }
        public NewId AggregateId { get; set; }
        public uint Version { get; set; }
        public string Title { get; set; }
        public DateTime UtcTime { get; set; }

    }
}
