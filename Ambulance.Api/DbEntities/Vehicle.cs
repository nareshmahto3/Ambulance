using System;
using System.Collections.Generic;

namespace Ambulance.Api.DbEntities;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public string RegistrationNumber { get; set; } = null!;

    public string? EngineNumber { get; set; }

    public string? ChassisNumber { get; set; }

    public int? ManufacturingYear { get; set; }

    public string? Company { get; set; }

    public string? Model { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<VehicleDeviceAdmin> VehicleDeviceAdmins { get; set; } = new List<VehicleDeviceAdmin>();

    public virtual ICollection<VehicleInsurance> VehicleInsurances { get; set; } = new List<VehicleInsurance>();

    public virtual ICollection<VehicleMaintenance> VehicleMaintenances { get; set; } = new List<VehicleMaintenance>();

    public virtual ICollection<VehicleRegistration> VehicleRegistrations { get; set; } = new List<VehicleRegistration>();
}
