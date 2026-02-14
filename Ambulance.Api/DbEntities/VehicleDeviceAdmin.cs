using System;
using System.Collections.Generic;

namespace Ambulance.Api.DbEntities;

public partial class VehicleDeviceAdmin
{
    public int VehicleDeviceAdminId { get; set; }

    public int VehicleId { get; set; }

    public int? DispatchAdminId { get; set; }

    public string? Imeinumber { get; set; }

    public string? OwnerName { get; set; }

    public string? OwnerMobileNumber { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Vehicle Vehicle { get; set; } = null!;
}
