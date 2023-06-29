using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecondhandStore.EntityRequest;
using SecondhandStore.EntityViewModel;
using SecondhandStore.Models;
using SecondhandStore.Services;

namespace SecondhandStore.Controllers;

[ApiController]
[Route("role")]
public class RoleController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly RoleService _roleService;

    public RoleController(RoleService roleService, IMapper mapper)
    {
        _roleService = roleService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoleList()
    {
        var roleList = await _roleService.GetAllRoles();

        if (!roleList.Any())
            return NotFound();

        var mappedRoleList = _mapper.Map<List<RoleEntityViewModel>>(roleList);

        return Ok(mappedRoleList);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewRole(RoleCreateRequest roleCreateRequest)
    {
        var mappedRole = _mapper.Map<Role>(roleCreateRequest);

        await _roleService.AddRole(mappedRole);

        return CreatedAtAction(nameof(GetRoleList),
            new { id = mappedRole.RoleId },
            mappedRole);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRole(string id, RoleUpdateRequest roleUpdateRequest)
    {
        var mappedRole = _mapper.Map<Role>(roleUpdateRequest);

        var existingRole = await _roleService.GetRoleById(id);

        if (existingRole is null)
            return NotFound();

        await _roleService.UpdateRole(mappedRole);

        return NoContent();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var existingRole = await _roleService.GetRoleById(id);

        if (existingRole is null)
            return NotFound();

        await _roleService.DeleteRole(existingRole);

        return NoContent();
    }
}