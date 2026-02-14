using System;
using System.Collections.Generic;

namespace Ambulance.Api.DbEntities;

public partial class VehicleRegistration
{
    public int VehicleRegistrationId { get; set; }

    public int VehicleId { get; set; }

    public string? VehicleClass { get; set; }

    public string? FuelType { get; set; }

    public DateOnly? RcvalidUpto { get; set; }

    public DateOnly? FitnessValidUpto { get; set; }

    public string? PermitLevel { get; set; }

    public DateOnly? PermitExpiryDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Vehicle Vehicle { get; set; } = null!;
}
