using System;
using PlariumDomain.Abstraction.Base;

namespace Plarium.Domain.Entities
{
    public class BaseEntity: IBaseEntity
    {
        public Guid Id { get; set; } //Internal identifier
    }
}
