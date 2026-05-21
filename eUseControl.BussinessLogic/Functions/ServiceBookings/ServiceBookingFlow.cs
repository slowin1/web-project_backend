using EUseControl.DataAccess.Context;
using eUseControl.BussinessLogic.Core.ServiceBookings;
using eUseControl.Domain.DTOs;

namespace eUseControl.BussinessLogic.Functions.ServiceBookings;

public interface IServiceBookingFlow
{
    Task<IEnumerable<ServiceBookingResponseDto>> GetAllAsync();
    Task<ServiceBookingResponseDto?> GetByIdAsync(string id);
    Task<ServiceBookingResponseDto> CreateAsync(CreateServiceBookingDto dto);
    Task<ServiceBookingResponseDto?> UpdateAsync(string id, UpdateServiceBookingDto dto);
    Task<bool> DeleteAsync(string id);
}

public class ServiceBookingFlow : ServiceBookingActions, IServiceBookingFlow
{
    public ServiceBookingFlow(UserContext context)
        : base(context)
    {
    }
}
