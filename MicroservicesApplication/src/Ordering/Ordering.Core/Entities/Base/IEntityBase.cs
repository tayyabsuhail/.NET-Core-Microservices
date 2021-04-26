using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Core.Entities.Base
{
    public interface IEntityBase<T>
    {
        T Id { get; }
    }
}
