using System;
using System.Collections.Generic;

namespace Ambulance.Api.DbEntities;

public partial class VehicleInsurance
{
    public int VehicleInsuranceId { get; set; }

    public int VehicleId { get; set; }

    public string? InsuranceNumber { get; set; }

    public string? InsuranceProvider { get; set; }

    public DateOnly? InsuranceExpiryDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Vehicle Vehicle { get; set; } = null!;
}
