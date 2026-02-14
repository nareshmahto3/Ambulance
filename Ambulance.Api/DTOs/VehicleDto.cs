using Ambulance.Api.DbEntities;
using System.ComponentModel.DataAnnotations;

namespace Ambulance.Api.DTOs
{
    public class VehicleDto
    {
        public int VehicleId { get; set; }
        public string RegistrationNumber { get; set; }
        public string? EngineNumber { get; set; }
        public string? ChassisNumber { get; set; }
        public int? ManufacturingYear { get; set; }
        public string? Company { get; set; }
        public string? Model { get; set; }

        public DeviceAdminDto? DeviceAdmin { get; set; }
        public RegistrationDto? Registration { get; set; }
        public InsuranceDto? Insurance { get; set; }
        public MaintenanceDto? Maintenance { get; set; }

    }
    public class DeviceAdminDto
    {
        public int? DispatchAdminId { get; set; }

        [MaxLength(50)]
        public string? IMEINumber { get; set; }

        [MaxLength(150)]
        public string? OwnerName { get; set; }

        [Phone]
        [MaxLength(15)]
        public string? OwnerMobileNumber { get; set; }
    }
    public class RegistrationDto
    {
        [MaxLength(50)]
        public string? VehicleClass { get; set; }

        [MaxLength(50)]
        public string? FuelType { get; set; }

        public DateTime? RCValidUpto { get; set; }
        public DateTime? FitnessValidUpto { get; set; }

        [MaxLength(100)]
        public string? PermitLevel { get; set; }

        public DateTime? PermitExpiryDate { get; set; }
    }
    public class InsuranceDto
    {
        [MaxLength(100)]
        public string? InsuranceNumber { get; set; }

        [MaxLength(150)]
        public string? InsuranceProvider { get; set; }

        public DateTime? InsuranceExpiryDate { get; set; }
    }
    public class MaintenanceDto
    {
        public DateTime? LastServiceDate { get; set; }
        public DateTime? NextServiceDate { get; set; }
        public DateTime? LastPUCCheckDate { get; set; }

        [Range(0, 999999)]
        public decimal? FuelCalibrationMinimum { get; set; }

        [Range(0, 999999)]
        public decimal? FuelCalibrationMaximum { get; set; }
    }



}

