using Ambulance.Api.DbEntities;
using Ambulance.Api.DTOs;

namespace Ambulance.Api.Interfaces
{
    public interface IVehicle
    {
        //Task<IEnumerable<Vehicle>> GetAllVehicleAsync();
        //Task<Vehicle> GetVehicleByIdAsync(int id);
        Task<bool> CreateVehicleAsync(VehicleDto vehicle);
        //Task UpdateVehicleAsync(VehicleDto vehicle);
        //Task DeleteVehicleAsync(int id);
        Task<bool> UpdateVehicleAsync(VehicleDto dto);
    }
}
