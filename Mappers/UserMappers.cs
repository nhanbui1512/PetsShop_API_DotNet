using petshop.Dtos.User;
using petshop.Models;

namespace api.Mappers
{
  public static class UserMappers
  {
    public static userDTO toUserDTO(this User userObject)
    {
      return new userDTO
      {
        Id = userObject.Id,
        UserName = userObject.UserName,
        Email = userObject.Email,
        Gender = userObject.Gender,
      };
    }

    public static User ToFormCreateUser(this CreateUserDOT form)
    {
      return new User
      {
        UserName = form.UserName,
        Gender = form.Gender,
        Password = form.Password,
        Email = form.Email
      };
    }
  }
}