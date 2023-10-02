using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Domain.DTOs.Items;

public record ItemInformation(
    Guid Id,
    string Description);
