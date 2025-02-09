
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private static List<User> Users = new List<User>
    {
        new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
        new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com" }
    };

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers() => Ok(Users);

    [HttpGet("{id}")]
    public ActionResult<User> GetUserById(int id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound(new { Message = "User not found" });
        return Ok(user);
    }

    [HttpPost]
    public ActionResult CreateUser([FromBody] User user)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        user.Id = Users.Any() ? Users.Max(u => u.Id) + 1 : 1;
        Users.Add(user);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateUser(int id, [FromBody] User user)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var existingUser = Users.FirstOrDefault(u => u.Id == id);
        if (existingUser == null) return NotFound(new { Message = "User not found" });

        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        return Ok(new { Message = "User updated successfully" });
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound(new { Message = "User not found" });

        Users.Remove(user);
        return Ok(new { Message = "User deleted successfully" });
    }
}
    