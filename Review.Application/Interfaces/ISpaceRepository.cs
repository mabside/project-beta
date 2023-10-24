using Review.DataAccess;
using Review.Domain.Entities.Spaces;

namespace Review.Application.Interfaces;

public interface ISpaceRepository
    : IWriteOnlyRepository<Space, Guid>,
    IReadOnlyRepository<Space, Guid>
{
}
