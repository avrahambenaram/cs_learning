namespace cs_learn.core;

using System;

public class Entity
{
    public readonly string id;
    public readonly long createdAt;
    public long updatedAt;

    protected Entity(EntityProps? entityProps)
    {
        DateTimeOffset timeOffset = DateTimeOffset.Now;
        long now = timeOffset.ToUnixTimeMilliseconds();
        
        this.id = entityProps?.id ?? Guid.NewGuid().ToString();
        this.createdAt = entityProps?.createdAt ?? now;
        this.updatedAt = entityProps?.updatedAt ?? now;
    }
}