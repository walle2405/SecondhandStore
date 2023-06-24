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
        MapExchangeRequest();
        MapExchangeOrder();
        MapPost();
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
        CreateMap<Account, LoginModelRequest>()
            .ReverseMap();
        CreateMap<LoginModelRequest, Account>()
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

    public void MapExchangeRequest() { 
        CreateMap<ExchangeRequest,ExchangeRequestCreateRequest>()
            .ReverseMap();
        CreateMap<ExchangeRequestCreateRequest, ExchangeRequest>()
            .ReverseMap();
    }

    private void MapExchangeOrder()
    {
        CreateMap<ExchangeOrder, ExchangeOrderCreateRequest>()
            .ReverseMap();
        CreateMap<ExchangeOrderCreateRequest, ExchangeOrder>()
            .ReverseMap();
        CreateMap<ExchangeOrder, ExchangeOrderUpdateRequest>()
            .ReverseMap();
        CreateMap<ExchangeOrderUpdateRequest, ExchangeOrder>();
    }

    private void MapPost()
    {
        CreateMap<Post, PostCreateRequest>()
            .ReverseMap();
        CreateMap<PostCreateRequest,Post>()
            .ReverseMap();
    }
}