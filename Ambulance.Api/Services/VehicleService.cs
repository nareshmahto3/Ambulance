using Ambulance.Api.DbConnection;
using Ambulance.Api.DbEntities;
using Ambulance.Api.DTOs;
using Ambulance.Api.Interfaces;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Api.Services
{
    public class VehicleService: IVehicle
    {
        private readonly AmbulanceAppContext _context;

        public VehicleService(AmbulanceAppContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateVehicleAsync(VehicleDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1️⃣ Save Parent
                var vehicle = new Vehicle
                {
                    RegistrationNumber = dto.RegistrationNumber,
                    EngineNumber = dto.EngineNumber,
                    ChassisNumber = dto.ChassisNumber,
                    ManufacturingYear = dto.ManufacturingYear,
                    Company = dto.Company,
                    Model = dto.Model
                };

                _context.Vehicles.Add(vehicle);
                await _context.SaveChangesAsync();

                // 2️⃣ Save Device & Admin
                if (dto.DeviceAdmin != null)
                {
                    var device = new VehicleDeviceAdmin
                    {
                        VehicleId = vehicle.VehicleId,
                        DispatchAdminId = dto.DeviceAdmin.DispatchAdminId,
                        Imeinumber = dto.DeviceAdmin.IMEINumber,
                        OwnerName = dto.DeviceAdmin.OwnerName,
                        OwnerMobileNumber = dto.DeviceAdmin.OwnerMobileNumber
                    };

                    _context.VehicleDeviceAdmins.Add(device);
                }

                // 3️⃣ Save Registration
                if (dto.Registration != null)
                {
                    var registration = new VehicleRegistration
                    {
                        VehicleId = vehicle.VehicleId,
                        VehicleClass = dto.Registration.VehicleClass,
                        FuelType = dto.Registration.VehicleClass,
                        RcvalidUpto = dto.Registration.RCValidUpto.HasValue? DateOnly.FromDateTime(dto.Registration.RCValidUpto.Value): null,
                        FitnessValidUpto = dto.Registration.RCValidUpto.HasValue ? DateOnly.FromDateTime(dto.Registration.FitnessValidUpto.Value): null,                       
                        PermitLevel = dto.Registration.PermitLevel,
                        PermitExpiryDate = dto.Registration.RCValidUpto.HasValue ? DateOnly.FromDateTime(dto.Registration.PermitExpiryDate.Value) : null,
                       
                    };

                    _context.VehicleRegistrations.Add(registration);
                }

                // 4️⃣ Save Insurance
                if (dto.Insurance != null)
                {
                    var insurance = new VehicleInsurance
                    {
                        VehicleId = vehicle.VehicleId,
                        InsuranceNumber = dto.Insurance.InsuranceNumber,
                        InsuranceProvider = dto.Insurance.InsuranceProvider,
                        InsuranceExpiryDate = dto.Insurance.InsuranceExpiryDate.HasValue ? DateOnly.FromDateTime(dto.Insurance.InsuranceExpiryDate.Value) : null
                    };

                    _context.VehicleInsurances.Add(insurance);
                }

                // 5️⃣ Save Maintenance
                if (dto.Maintenance != null)
                {
                    var maintenance = new VehicleMaintenance
                    {
                        VehicleId = vehicle.VehicleId,
                        LastServiceDate = dto.Maintenance.LastServiceDate.HasValue? DateOnly.FromDateTime(dto.Maintenance.LastServiceDate.Value) : null,
                        NextServiceDate = dto.Maintenance.NextServiceDate.HasValue ? DateOnly.FromDateTime(dto.Maintenance.NextServiceDate.Value) : null,
                        LastPuccheckDate = dto.Maintenance.LastPUCCheckDate.HasValue ? DateOnly.FromDateTime(dto.Maintenance.LastPUCCheckDate.Value) : null,                       
                        FuelCalibrationMinimum = dto.Maintenance.FuelCalibrationMinimum,
                        FuelCalibrationMaximum = dto.Maintenance.FuelCalibrationMaximum
                    };

                    _context.VehicleMaintenances.Add(maintenance);
                }

                // 6️⃣ Save All Child Records
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> UpdateVehicleAsync(VehicleDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            int vehicleId = dto.VehicleId;
            try
            {
                // 1️⃣ Get Existing Vehicle
                var vehicle = await _context.Vehicles
                    .FirstOrDefaultAsync(v => v.VehicleId == vehicleId);

                if (vehicle == null)
                    return false;

                // 2️⃣ Update Parent
                vehicle.RegistrationNumber = dto.RegistrationNumber;
                vehicle.EngineNumber = dto.EngineNumber;
                vehicle.ChassisNumber = dto.ChassisNumber;
                vehicle.ManufacturingYear = dto.ManufacturingYear;
                vehicle.Company = dto.Company;
                vehicle.Model = dto.Model;

                await _context.SaveChangesAsync();

                // =========================
                // 3️⃣ Update DeviceAdmin
                // =========================
                if (dto.DeviceAdmin != null)
                {
                    var device = await _context.VehicleDeviceAdmins
                        .FirstOrDefaultAsync(x => x.VehicleId == vehicleId);

                    if (device != null)
                    {
                        device.DispatchAdminId = dto.DeviceAdmin.DispatchAdminId;
                        device.Imeinumber = dto.DeviceAdmin.IMEINumber;
                        device.OwnerName = dto.DeviceAdmin.OwnerName;
                        device.OwnerMobileNumber = dto.DeviceAdmin.OwnerMobileNumber;
                    }
                    else
                    {
                        _context.VehicleDeviceAdmins.Add(new VehicleDeviceAdmin
                        {
                            VehicleId = vehicleId,
                            DispatchAdminId = dto.DeviceAdmin.DispatchAdminId,
                            Imeinumber = dto.DeviceAdmin.IMEINumber,
                            OwnerName = dto.DeviceAdmin.OwnerName,
                            OwnerMobileNumber = dto.DeviceAdmin.OwnerMobileNumber
                        });
                    }
                }

                // =========================
                // 4️⃣ Update Registration
                // =========================
                if (dto.Registration != null)
                {
                    var registration = await _context.VehicleRegistrations
                        .FirstOrDefaultAsync(x => x.VehicleId == vehicleId);

                    if (registration != null)
                    {
                        registration.VehicleClass = dto.Registration.VehicleClass;
                        registration.FuelType = dto.Registration.FuelType;

                        registration.RcvalidUpto = dto.Registration.RCValidUpto.HasValue
                            ? DateOnly.FromDateTime(dto.Registration.RCValidUpto.Value)
                            : null;

                        registration.FitnessValidUpto = dto.Registration.FitnessValidUpto.HasValue
                            ? DateOnly.FromDateTime(dto.Registration.FitnessValidUpto.Value)
                            : null;

                        registration.PermitLevel = dto.Registration.PermitLevel;

                        registration.PermitExpiryDate = dto.Registration.PermitExpiryDate.HasValue
                            ? DateOnly.FromDateTime(dto.Registration.PermitExpiryDate.Value)
                            : null;
                    }
                    else
                    {
                        _context.VehicleRegistrations.Add(new VehicleRegistration
                        {
                            VehicleId = vehicleId,
                            VehicleClass = dto.Registration.VehicleClass,
                            FuelType = dto.Registration.FuelType,
                            RcvalidUpto = dto.Registration.RCValidUpto.HasValue
                                ? DateOnly.FromDateTime(dto.Registration.RCValidUpto.Value)
                                : null,
                            FitnessValidUpto = dto.Registration.FitnessValidUpto.HasValue
                                ? DateOnly.FromDateTime(dto.Registration.FitnessValidUpto.Value)
                                : null,
                            PermitLevel = dto.Registration.PermitLevel,
                            PermitExpiryDate = dto.Registration.PermitExpiryDate.HasValue
                                ? DateOnly.FromDateTime(dto.Registration.PermitExpiryDate.Value)
                                : null
                        });
                    }
                }

                // =========================
                // 5️⃣ Update Insurance
                // =========================
                if (dto.Insurance != null)
                {
                    var insurance = await _context.VehicleInsurances
                        .FirstOrDefaultAsync(x => x.VehicleId == vehicleId);

                    if (insurance != null)
                    {
                        insurance.InsuranceNumber = dto.Insurance.InsuranceNumber;
                        insurance.InsuranceProvider = dto.Insurance.InsuranceProvider;
                        insurance.InsuranceExpiryDate = dto.Insurance.InsuranceExpiryDate.HasValue
                            ? DateOnly.FromDateTime(dto.Insurance.InsuranceExpiryDate.Value)
                            : null;
                    }
                    else
                    {
                        _context.VehicleInsurances.Add(new VehicleInsurance
                        {
                            VehicleId = vehicleId,
                            InsuranceNumber = dto.Insurance.InsuranceNumber,
                            InsuranceProvider = dto.Insurance.InsuranceProvider,
                            InsuranceExpiryDate = dto.Insurance.InsuranceExpiryDate.HasValue
                                ? DateOnly.FromDateTime(dto.Insurance.InsuranceExpiryDate.Value)
                                : null
                        });
                    }
                }

                // =========================
                // 6️⃣ Update Maintenance
                // =========================
                if (dto.Maintenance != null)
                {
                    var maintenance = await _context.VehicleMaintenances
                        .FirstOrDefaultAsync(x => x.VehicleId == vehicleId);

                    if (maintenance != null)
                    {
                        maintenance.LastServiceDate = dto.Maintenance.LastServiceDate.HasValue
                            ? DateOnly.FromDateTime(dto.Maintenance.LastServiceDate.Value)
                            : null;

                        maintenance.NextServiceDate = dto.Maintenance.NextServiceDate.HasValue
                            ? DateOnly.FromDateTime(dto.Maintenance.NextServiceDate.Value)
                            : null;

                        maintenance.LastPuccheckDate = dto.Maintenance.LastPUCCheckDate.HasValue
                            ? DateOnly.FromDateTime(dto.Maintenance.LastPUCCheckDate.Value)
                            : null;

                        maintenance.FuelCalibrationMinimum = dto.Maintenance.FuelCalibrationMinimum;
                        maintenance.FuelCalibrationMaximum = dto.Maintenance.FuelCalibrationMaximum;
                    }
                    else
                    {
                        _context.VehicleMaintenances.Add(new VehicleMaintenance
                        {
                            VehicleId = vehicleId,
                            LastServiceDate = dto.Maintenance.LastServiceDate.HasValue
                                ? DateOnly.FromDateTime(dto.Maintenance.LastServiceDate.Value)
                                : null,
                            NextServiceDate = dto.Maintenance.NextServiceDate.HasValue
                                ? DateOnly.FromDateTime(dto.Maintenance.NextServiceDate.Value)
                                : null,
                            LastPuccheckDate = dto.Maintenance.LastPUCCheckDate.HasValue
                                ? DateOnly.FromDateTime(dto.Maintenance.LastPUCCheckDate.Value)
                                : null,
                            FuelCalibrationMinimum = dto.Maintenance.FuelCalibrationMinimum,
                            FuelCalibrationMaximum = dto.Maintenance.FuelCalibrationMaximum
                        });
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

    }

}
