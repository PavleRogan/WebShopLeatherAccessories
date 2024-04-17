using WebShop.Application.Users.Dtos;
using WebShop.Domain.Entities;

namespace WebShop.Application.Users
{
    public interface IUsersService
    {
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto?> GetUserById(Guid userId);

        Task<Guid> Create(CreateUserDto dto);
    }
}