
using Microsoft.EntityFrameworkCore;
using SecondhandStore.Models;
using SecondhandStore.Repository;

namespace SecondhandStore.Services;

public class RoleService
{
    private readonly RoleRepository _roleRepository;

    public RoleService(RoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<IEnumerable<Role>> GetAllRoles()
    {
        //.Include(p => p) all you need
        return await _roleRepository.GetAll().ToListAsync();
    }

    public async Task<Role?> GetRoleById(string id)
    {
        return await _roleRepository.GetById(id);
    }

    public async Task AddRole(Role role)
    {
        await _roleRepository.Add(role);
    }

    public async Task UpdateRole(Role role)
    {
        await _roleRepository.Update(role);
    }

    public async Task DeleteRole(Role role)
    {
        await _roleRepository.Delete(role);
    }
}