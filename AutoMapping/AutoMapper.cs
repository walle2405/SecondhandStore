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
        MapExchangeOrder();
        MapExchangeRequest();
        MapExchange();
        MapReview();
        MapImage();
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
        CreateMap<TopUp, TopUpEntityViewModel>()
            .ForMember(d => d.FullName, map => map.MapFrom(p => p.Account.Fullname))
            .ForMember(d => d.Email, map => map.MapFrom(p => p.Account.Email))
            .ForMember(d => d.PhoneNumber, map => map.MapFrom(p => p.Account.PhoneNo))
            .ForMember(d => d.TopUpStatus, map => map.MapFrom(p => p.TopupStatus.StatusName));
        CreateMap<TopUpEntityViewModel, TopUp>();
        CreateMap<TopUp, TopUpCreateRequest>()
            .ReverseMap();
        
    }
    
    private void MapPost()
    {
        CreateMap<Post, PostEntityViewModel>()
            .ForMember(d => d.Fullname, map => map.MapFrom(p => p.Account.Fullname))
            .ForMember(d => d.PhoneNo, map => map.MapFrom(p => p.Account.PhoneNo))
            .ForMember(d => d.Address, map => map.MapFrom(p => p.Account.Address))
            .ForMember(d => d.Email, map => map.MapFrom(p => p.Account.Email))
            .ForMember(d => d.CategoryName, map => map.MapFrom(p => p.Category.CategoryName))
            .ForMember(d => d.CategoryValue, map => map.MapFrom(p => p.Category.CategoryValue))
            .ForMember(d => d.StatusName, map => map.MapFrom(p => p.PostStatus.StatusName));

        CreateMap<PostEntityViewModel, Post>();
        CreateMap<PostCreateRequest, Post>()
            .ReverseMap();
        CreateMap<Post, PostUpdateRequest>()
            .ReverseMap();
        CreateMap<PostUpdateRequest, Post>()
            .ReverseMap();
        CreateMap<Post, PostType>()
            .ReverseMap();
        CreateMap<PostType, Post>()
            .ReverseMap();
    }
    public void MapReport() {
        CreateMap<Report, ReportEntityViewModel>()
            .ForMember(d => d.ReporterName, map => map.MapFrom(d => d.Reporter.Fullname))
            .ForMember(d=> d.ReporterEmail, map => map.MapFrom(d=>d.Reporter.Email))
            .ForMember(d => d.ReportedUserName, map => map.MapFrom(d => d.ReportedAccount.Fullname))
            .ForMember(d => d.ReportedUserEmail, map => map.MapFrom(d => d.ReportedAccount.Email))
            .ForMember(d => d.Status, map => map.MapFrom(d => d.ReportStatus.StatusName));
        CreateMap<ReportEntityViewModel, Report>();
        CreateMap<Report,ReportCreateRequest>()
            .ReverseMap();
        CreateMap<ReportCreateRequest,Report>()
            .ReverseMap();
        
    }
    public void MapExchangeOrder() {
        CreateMap<ExchangeOrder, ExchangeOrderEntityViewModel>()
            .ForMember(d => d.BuyerName, map => map.MapFrom(p => p.Buyer.Fullname))
            .ForMember(d => d.BuyerPhoneNumber, map => map.MapFrom(p => p.Buyer.PhoneNo))
            .ForMember(d => d.BuyerEmail, map => map.MapFrom(p => p.Buyer.Email))
            .ForMember(d => d.ProductName, map => map.MapFrom(p => p.Post.ProductName))
            .ForMember(d => d.OrderStatusName, map => map.MapFrom(p => p.OrderStatus.StatusName))
            .ForMember(d => d.Price, map => map.MapFrom(p => p.Post.Price));
            
        CreateMap<ExchangeOrderEntityViewModel, ExchangeOrder>();
        CreateMap<ExchangeOrder, ExchangeOrderCreateRequest>()
            .ReverseMap();
        CreateMap<ExchangeOrderCreateRequest,ExchangeOrder>()
            .ReverseMap();

    }
    public void MapExchangeRequest() {
        CreateMap<ExchangeOrder, ExchangeRequestEntityViewModel>()
            .ForMember(d => d.SellerName, map => map.MapFrom(p => p.Seller.Fullname))
            .ForMember(d => d.SellerPhoneNumber, map => map.MapFrom(p => p.Seller.PhoneNo))
            .ForMember(d => d.SellerEmail, map => map.MapFrom(p => p.Seller.Email))
            .ForMember(d => d.ProductName, map => map.MapFrom(p => p.Post.ProductName))
            .ForMember(d => d.OrderStatusName, map => map.MapFrom(p => p.OrderStatus.StatusName))
            .ForMember(d => d.Price, map => map.MapFrom(p => p.Post.Price));
        CreateMap<ExchangeRequestEntityViewModel, ExchangeOrder>();
    }
    public void MapExchange() { 
        CreateMap<ExchangeOrder,ExchangeViewEntityModel>()
            .ForMember(d=>d.SellerName, map => map.MapFrom(p => p.Seller.Fullname))
            .ForMember(d=>d.BuyerName, map => map.MapFrom(p =>p.Buyer.Fullname))
            .ForMember(d => d.ProductName, map => map.MapFrom(p => p.Post.ProductName))
            .ForMember(d => d.OrderStatusName, map => map.MapFrom(p => p.OrderStatus.StatusName))
            .ForMember(d => d.Price, map => map.MapFrom(p => p.Post.Price))
            .ForMember(d => d.BuyerEmail,map =>map.MapFrom(p=>p.Buyer.Email))
            .ForMember(d => d.SellerEmail, map => map.MapFrom(p => p.Seller.Email));
        CreateMap<ExchangeViewEntityModel, ExchangeOrder>();
    }
    public void MapReview() {
        CreateMap<Review, ReviewEntityViewModel>()
            .ForMember(d => d.ReviewerName, map => map.MapFrom(d => d.Reviewer.Fullname))
            .ForMember(d => d.ReviewerEmail, map => map.MapFrom(d => d.Reviewer.Email))
            .ForMember(d => d.ReviewedName, map => map.MapFrom(d => d.Reviewed.Fullname))
            .ForMember(d => d.ReviewedEmail, map => map.MapFrom(d => d.Reviewed.Email));
        CreateMap<Review,ReviewCreateRequest>()
            .ReverseMap();
        CreateMap<ReviewCreateRequest,Review>()
            .ReverseMap();
    }

    public void MapImage()
    {
        CreateMap<Image, ImageViewModel>()
            .ReverseMap();
        CreateMap<ImageViewModel, Image>()
            .ReverseMap();
    }
}