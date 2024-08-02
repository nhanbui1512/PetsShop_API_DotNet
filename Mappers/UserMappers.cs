using System.Reflection;
using petshop.Dtos.User;
using petshop.Models;

namespace api.Mappers
{
  public static class UserMappers
  {

    public static UserDTO ToUserDTO(this User userObject)
    {
      return new UserDTO
      {
        Id = userObject.Id,
        FirstName = userObject.FirstName,
        LastName = userObject.LastName,
        Gender = userObject.Gender,
        Email = userObject.Email,
        Avatar = userObject.Avatar,
        FullName = userObject.FullName,
        CreatedAt = userObject.CreatedAt,
        UpdatedAt = userObject.UpdatedAt,
        CreatedAtStr = userObject.CreatedAtStr,
        UpdatedAtStr = userObject.UpdatedAtStr
      };
    }

    public static User ToFormCreateUser(this CreateUserDTO form)
    {
      return new User
      {
        FirstName = form.FirstName,
        LastName = form.LastName,
        Gender = form.Gender,
        Password = form.Password,
        Email = form.Email,
        Avatar = "DefaultAvatar.png",
        RoleId = form.RoleId
      };
    }
  }
}