using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Core.Entities.Base
{
    public abstract class EntityBase<T> : IEntityBase<T>
    {
        public virtual T Id { get; protected set; }
        int? _requestedHashCode;

        public bool IsTransient()
        {
            return Id.Equals(default(T));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EntityBase<T>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = (EntityBase<T>)obj;

            if (item.IsTransient() || IsTransient())
                return false;
            else
                return item == this;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = Id.GetHashCode() ^ 31; 

                return _requestedHashCode.Value;
            }
            return Id.GetHashCode();
        }

        public static bool operator ==(EntityBase<T> left, EntityBase<T> right)
        {
            if (Equals(left, null))
                return Equals(right, null);
            else
                return left.Equals(right);
        }

        public static bool operator !=(EntityBase<T> left, EntityBase<T> right)
        {
            return !(left == right);
        }
    }
}
