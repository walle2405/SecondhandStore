using AutoMapper;
using SecondhandStore.EntityRequest;
using SecondhandStore.EntityViewModel;
using SecondhandStore.Models;

namespace SecondhandStore.AutoMapping;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        MapRole();
        MapAccount();
        MapTopUp();
    }

    private void MapRole()
    {
        CreateMap<Role, RoleCreateRequest>()
            .ReverseMap();

        CreateMap<Role, RoleUpdateRequest>()
            .ReverseMap();
        
        CreateMap<RoleCreateRequest, Role>()
            .ReverseMap();

        CreateMap<RoleUpdateRequest, Role>()
            .ReverseMap();

        CreateMap<RoleEntityViewModel, Role>()
            .ReverseMap();
        
        CreateMap<Role, RoleEntityViewModel>()
            .ReverseMap();
    }

    private void MapAccount()
    {
        CreateMap<Account, AccountCreateRequest>()
            .ReverseMap();
        CreateMap<Account, AccountUpdateRequest>()
            .ReverseMap();
        CreateMap<AccountCreateRequest, Account>()
            .ReverseMap();
        CreateMap<AccountUpdateRequest, Account>()
            .ReverseMap();
    }
    private void MapDeactivateAccount()
    {
        CreateMap<Account, AccountDeactivateRequest>() 
            .ReverseMap();
        CreateMap<AccountDeactivateRequest, Account>()
            .ReverseMap();
    }
    private void MapTopUp()
    {
        CreateMap<TopUp, TopUpCreateRequest>()
            .ReverseMap();
        CreateMap<TopUpCreateRequest, TopUp>()
           .ReverseMap();
    }

    private void MapPost()
    {
        CreateMap<Post, PostCreateRequest>()
            .ReverseMap();
        CreateMap<PostCreateRequest,Post>()
            .ReverseMap();
    }
}