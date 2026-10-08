
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models.users;
using BackEnd.Data;
[ApiController]
[Route("api/[controller]")]
public class UserModelsController : ControllerBase
{
    private readonly MyApplicationDBContext _context;

    public UserModelsController(MyApplicationDBContext context)
    {
        _context = context;
    }

   
}
