using AutoMapper;
using SecondhandStore.EntityRequest;
using SecondhandStore.EntityViewModel;
using SecondhandStore.Models;
using SecondhandStore.Services;
using SecondhandStore.Repository;
using SecondhandStore.Infrastructure;
namespace SecondhandStore.AutoMapping;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        MapRole();
        MapAccount();
        MapTopUp();
        MapPost();
        MapReport();
        MapReview();
        MapExchangeOrder();
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
        CreateMap<Account, AccountEntityViewModel>()
            .ReverseMap();
        CreateMap<AccountEntityViewModel, Account>()
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
        CreateMap<TopUp, TopUpEntityViewModel>()
            .ReverseMap();
        CreateMap<TopUpEntityViewModel,TopUp>()
            .ReverseMap();
    }
    
    private void MapPost()
    {
        CreateMap<Post, PostEntityViewModel>()
        .ForMember(d => d.Fullname, map => map.MapFrom(p => p.Account.Fullname))
        .ForMember(d => d.CategoryName, map => map.MapFrom(p => p.Category.CategoryName));
        CreateMap<PostEntityViewModel, Post>();
        CreateMap<PostCreateRequest, Post>()
        .ReverseMap();
    }

    public void MapReview()
    {
        CreateMap<Review, ReviewCreateRequest>()
            .ReverseMap();
        CreateMap<ReviewCreateRequest, Review>()
            .ReverseMap();
    }
    public void MapReport() { 
        CreateMap<Report,ReportCreateRequest>()
            .ReverseMap();
        CreateMap<ReportCreateRequest,Report>()
            .ReverseMap();
        CreateMap<Report, ReportEntityViewModel>()
            .ReverseMap();
        CreateMap<ReportEntityViewModel, Report>()
            .ReverseMap();
    }
    public void MapExchangeOrder() {
        CreateMap<ExchangeOrder, ExchangeOrderEntityViewModel>()
            .ReverseMap();
        CreateMap<ExchangeOrderEntityViewModel, ExchangeOrder>()
            .ReverseMap();
    }
}