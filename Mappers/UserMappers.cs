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
        FirstName = userObject.FirstName,
        LastName = userObject.LastName,
        Gender = userObject.Gender,
        Password = userObject.Password,
        Email = userObject.Email,
        Avatar = userObject.Avatar
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
        Avatar = "DefaultAvatar.png"
      };
    }
  }
}