using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AioCore.Domain.Common
{
    public abstract class Entity
    {
        private int? _requestedHashCode;
        private Guid _id;

        [Keyword]
        public Guid Id
        {
            get => _id;
            protected set => _id = value;
        }

        public DateTimeOffset CreatedDate { get; set; }
        
        [StringLength(50)]
        public string CreatedBy { get; set; }
        
        public DateTimeOffset? UpdatedDate { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [Keyword]
        [JsonIgnore]
        [NotMapped]
        public string IndexType => GetType().Name;

        [JsonIgnore]
        [NotMapped]
        private List<DomainEvent> _domainEvents;

        [JsonIgnore]
        [NotMapped]
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(DomainEvent eventItem)
        {
            _domainEvents ??= new List<DomainEvent>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(DomainEvent eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public bool IsTransient()
        {
            return Id == default;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = (Entity)obj;

            if (item.IsTransient() || IsTransient())
                return false;
            return item.Id == Id;
        }

        public override int GetHashCode()
        {
            if (IsTransient()) return base.GetHashCode();
            _requestedHashCode ??= Id.GetHashCode() ^ 31;
            return _requestedHashCode.Value;
        }

        public static bool operator ==(Entity left, Entity right)
        {
            return left?.Equals(right) ?? Equals(right, null);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}