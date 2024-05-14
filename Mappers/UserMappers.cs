using petshop.Dtos.User;
using petshop.Models;


namespace api.Mappers{
  public static class UserMappers
  {
      public static userDTO toUserDTO(this User userObject){
       
        return new userDTO {
          Id = userObject.Id,
          UserName = userObject.UserName,
          Email = userObject.Email,
          Gender = userObject.Gender,
        };
      }
  }
}