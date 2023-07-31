using API.Helpers.Pagination;
using AutoMapper;
using Core.Dtos;
using Core.Dtos.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ApplicationUserController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ApplicationUserController(ILoggerFactory factory, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = factory.CreateLogger<ApplicationUserController>();
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] PaginationParams paginationParams)
    {
        try
        {
            var result = await _unitOfWork.UserService.Load(paginationParams.PageNumber, paginationParams.PageSize);

            var data = _mapper.Map<IList<UserListDto>>(result);
            
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting users");
        }

        return BadRequest("Error while getting users");
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsers(string id)
    {
        try
        {
            var result = await _unitOfWork.UserService.Get(id);

            var data = _mapper.Map<UserDetailsDto>(result);
            
            return Ok(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting users");
        }

        return BadRequest("Error while getting users");
    }
    
}