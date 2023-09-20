﻿using Review.Abstractions;
using Review.Abstractions.Entities;
using Review.Domains;

namespace Review.Domain;

public abstract class BaseEntity<TId> : IEntity<TId>, IAuditableEntity
{
    #region entity members
    public TId Id { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; } = Clock.Now;
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }

    protected BaseEntity()
    {
    }

    protected BaseEntity(TId id)
    {
        Id = id;
    }
    #endregion

    public void Precondition(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }

    public void Precondition<T>(IBusinessRule<T> rule) where T : BaseEntity<Guid>
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException<T>(rule);
        }
    }
}
