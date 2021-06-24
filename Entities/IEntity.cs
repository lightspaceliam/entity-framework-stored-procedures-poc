using System;

namespace Entities
{
    interface IEntity
    {
        int Id { get; set; }
        DateTime LastModified { get; set; }
        DateTime Created { get; set; }
    }
}
