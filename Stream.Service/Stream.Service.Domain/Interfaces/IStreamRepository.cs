using Stream.Service.Domain.Models;

namespace Stream.Service.Domain.Interfaces;

public interface IStreamRepository
{
    Task<string> CreateAsync(StreamModel stream);
    Task<StreamModel?> GetByIdAsync(string id);
}