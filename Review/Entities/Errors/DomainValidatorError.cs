using Review.Models.Bases;

namespace Review.Entities.Errors;

public record DomainValidationError(string Message)
        : Error(Message, "88", false);