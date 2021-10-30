using api.Dtos;
using api.Entities;
using AutoMapper;

namespace api
{
   public class AutoProfile:Profile
    {
        public AutoProfile()
        {
            CreateMap<UserInputModel,User>();
            CreateMap<User,UserOutputModel>();
            CreateMap<CategoryInputModel,Category>();
            CreateMap<Category,CategoryOutputModel>();
            CreateMap<AccountInputModel,Account>();
            CreateMap<Account,AccountOutputModel>();
            CreateMap<TransactionInputModel,Transaction>();
        }
    }
}