using Byhands.Models.Bases;

namespace Byhands.Entities.Errors;

public record DomainValidationError(string Message)
        : Error(Message, "88", false);