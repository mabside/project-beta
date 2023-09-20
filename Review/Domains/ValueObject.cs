using System.Reflection;
using Review.Abstractions;
using Review.Models.Bases;

namespace Review.Domains;

public class ValueObject<T> : IEquatable<ValueObject<T>>
{
    private List<FieldInfo> _fields;

    private List<PropertyInfo> _properties;

    public T Value { get; }

    protected ValueObject(T value)
    {
        if (value == null)
        {
            throw new InvalidOperationException("value");
        }

        Value = value;
    }

    public static bool operator ==(ValueObject<T> obj, ValueObject<T> other)
    {
        if (object.Equals(obj, null))
        {
            if (object.Equals(other, null))
            {
                return true;
            }

            return false;
        }

        return obj.Equals(other);
    }

    public static bool operator !=(ValueObject<T> obj, ValueObject<T> other)
    {
        return !(obj == other);
    }

    public bool Equals(ValueObject<T>? other)
    {
        return Equals((object?)other);
    }

    public override bool Equals(object? obj)
    {
        object obj2 = obj;
        if (obj2 == null || GetType() != obj2.GetType())
        {
            return false;
        }

        return GetProperties().All((PropertyInfo p) => PropertiesAreEqual(obj2, p)) && GetFields().All((FieldInfo f) => FieldsAreEqual(obj2, f));
    }

    public override int GetHashCode()
    {
        int num = 17;
        foreach (FieldInfo field in GetFields())
        {
            object value = field.GetValue(this);
            num = HashValue(num, value);
        }

        foreach (PropertyInfo property in GetProperties())
        {
            object value2 = property.GetValue(this, null);
            num = HashValue(num, value2);
        }

        return num;
    }

    protected static Result CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            return new DomainValidationErr(rule);
        }

        return default(Success);
    }

    private bool FieldsAreEqual(object obj, FieldInfo f)
    {
        return object.Equals(f.GetValue(this), f.GetValue(obj));
    }

    private bool PropertiesAreEqual(object obj, PropertyInfo p)
    {
        return object.Equals(p.GetValue(this, null), p.GetValue(obj, null));
    }

    private IEnumerable<FieldInfo> GetFields()
    {
        if (_fields == null)
        {
            _fields = (from f in GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                       where f.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null
                       select f).ToList();
        }

        return _fields;
    }

    private IEnumerable<PropertyInfo> GetProperties()
    {
        if (_properties == null)
        {
            _properties = (from p in GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                           where p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null
                           select p).ToList();
        }

        return _properties;
    }

    private int HashValue(int seed, object value)
    {
        int num = value?.GetHashCode() ?? 0;
        return seed * 23 + num;
    }
}