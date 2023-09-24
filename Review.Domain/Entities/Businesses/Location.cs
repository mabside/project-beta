using Review.Entities.Validators;
using Review.Extensions;
using Review.Models.Bases;

namespace Review.Domain.Entities.Businesses;

public class Location
{
    public string? Number { get; private set; } = default!;
    public string City { get; private set; } = default!;
    public string State { get; private set; } = default!;
    public string Street { get; private set; } = default!;
    public string Country { get; private set; } = default!;
    public string PostalCode { get; private set; } = default!;

    private Location() { }

    public Location(
        string? number,
        string city,
        string state,
        string street,
        string country,
        string postalCode)
    {
        Number = number;
        City = city;
        State = state;
        Street = street;
        Country = country;
        PostalCode = postalCode;
    }

    public static Result<Location> Create(
    string? number,
    string city,
    string state,
    string street,
    string country,
    string postalCode)
    {
        var result = Result<Location>.Create(
            new Location(
                number: number,
                city: city,
                state: state,
                street: street,
                country: country,
                postalCode: postalCode))
            .Validate(OptionalField.Create(number))
            .Validate(OptionalField.Create(city))
            .Validate(OptionalField.Create(state))
            .Validate(OptionalField.Create(street))
            .Validate(OptionalField.Create(country))
            .Validate(OptionalField.Create(postalCode));

        return result;
    }
}
