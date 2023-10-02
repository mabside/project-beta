using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Review.Domain.Entities.Items;
using Review.Entities.QueryObjects;

namespace Review.Application.Usecases.Items.GetItems;

internal record GetItemsQuery : IRequest<PaginatedResult<Item>>
{
}
