using Byhands.Entities.Validators;
using Byhands.Extensions;
using Byhands.Models.Bases;

namespace Byhands.Domain.Entities.Businesses;

public class SocialHandle
{
    public string PlatformName { get; private set; }
    public string PlatformLink { get; private set; }

    private SocialHandle() { }

    private SocialHandle(string platformName, string platformLink)
    {
        PlatformName = platformName;
        PlatformLink = platformLink;
    }

    public static Result<SocialHandle> Create(
        string platformName, string platformLink)
    {
        var result = Result<SocialHandle>.Create(
            new SocialHandle(
                platformName: platformName,
                platformLink: platformLink))
            .Validate(RequiredField.Create(platformName))
            .Validate(RequiredField.Create(platformLink));

        if (result.HasError)
            return result.Error;

        return result.Value;
    }
}
