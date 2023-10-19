using Review.Entities.Validators;
using Review.Extensions;
using Review.Models.Bases;

namespace Review.Domain.Entities.Links;

public class Link : BaseEntity<Guid>
{
    public string Code { get; private set; } = default!;
    public LinkType LinkType { get; private set; }

    private Link() { }

    private Link(
        string code,
        LinkType linkType)
    {
        Code = code;
        LinkType = linkType;
    }

    public static Result<Link> Create(
        string code,
        LinkType linkType)
    {
        var result = Result<Link>.Create(
            new Link(code, linkType))
            .Validate(RequiredField.Create(code));

        if (result.HasError)
            return result.Error;

        return result.Value;
    }
}
