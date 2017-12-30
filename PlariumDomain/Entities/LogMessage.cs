using PlariumDomain.Abstraction.Base; 
using System;

namespace Plarium.Domain.Entities
{
    public class LogMessage: IBaseEntity
    {
        public Guid Id { get; set; }

        public string IpAddress { get; set; }

        public DateTime RequestTime { get; set; }

        public string Route { get; set; }

        public string UrlQueryParameters { get; set; }

        public int RequestResult { get; set; }

        /// <summary>
        /// The size in bytes transferred to the client
        /// </summary>
        public int RequestSize { get; set; }

        public string Location { get; set; }

    }
}
