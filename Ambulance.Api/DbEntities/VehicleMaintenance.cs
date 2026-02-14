using System;
using System.Collections.Generic;

namespace Ambulance.Api.DbEntities;

public partial class VehicleMaintenance
{
    public int VehicleMaintenanceId { get; set; }

    public int VehicleId { get; set; }

    public DateOnly? LastServiceDate { get; set; }

    public DateOnly? NextServiceDate { get; set; }

    public DateOnly? LastPuccheckDate { get; set; }

    public decimal? FuelCalibrationMinimum { get; set; }

    public decimal? FuelCalibrationMaximum { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Vehicle Vehicle { get; set; } = null!;
}
