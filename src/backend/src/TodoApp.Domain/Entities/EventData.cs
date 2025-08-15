using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Domain.Entities
{
    public class EventData
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; } // UsuarioId
        public string EventType { get; set; }
        public string Payload { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
