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
        MapReportImage();
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
        CreateMap<EditAccountRequest, Account>()
            .ReverseMap();
        CreateMap<Account, EditAccountRequest>()
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
            .ForMember(d => d.fullName, map => map.MapFrom(p => p.Account.Fullname))
            .ForMember(d => d.email, map => map.MapFrom(p => p.Account.Email))
            .ForMember(d => d.phoneNumber, map => map.MapFrom(p => p.Account.PhoneNo))
            .ForMember(d => d.topUpStatus, map => map.MapFrom(p => p.TopupStatus.StatusName));
        CreateMap<TopUpEntityViewModel, TopUp>();
        CreateMap<TopUp, TopUpCreateRequest>()
            .ReverseMap();

    }

    private void MapPost()
    {
        CreateMap<Post, PostEntityViewModel>()
            .ForMember(d => d.fullname, map => map.MapFrom(p => p.Account.Fullname))
            .ForMember(d => d.phoneNo, map => map.MapFrom(p => p.Account.PhoneNo))
            .ForMember(d => d.address, map => map.MapFrom(p => p.Account.Address))
            .ForMember(d => d.email, map => map.MapFrom(p => p.Account.Email))
            .ForMember(d => d.categoryName, map => map.MapFrom(p => p.Category.CategoryName))
            .ForMember(d => d.categoryValue, map => map.MapFrom(p => p.Category.CategoryValue))
            .ForMember(d => d.statusName, map => map.MapFrom(p => p.PostStatus.StatusName));

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
    public void MapReport()
    {
        CreateMap<Report, ReportEntityViewModel>()
            .ForMember(d => d.reporterName, map => map.MapFrom(d => d.Reporter.Fullname))
            .ForMember(d => d.reporterEmail, map => map.MapFrom(d => d.Reporter.Email))
            .ForMember(d => d.reportedUserName, map => map.MapFrom(d => d.ReportedAccount.Fullname))
            .ForMember(d => d.reportedUserEmail, map => map.MapFrom(d => d.ReportedAccount.Email))
            .ForMember(d => d.status, map => map.MapFrom(d => d.ReportStatus.StatusName));
        CreateMap<ReportEntityViewModel, Report>();
        CreateMap<Report, ReportCreateRequest>()
            .ReverseMap();
        CreateMap<ReportCreateRequest, Report>()
            .ReverseMap();

    }
    public void MapExchangeOrder()
    {
        CreateMap<ExchangeOrder, ExchangeOrderEntityViewModel>()
            .ForMember(d => d.buyerName, map => map.MapFrom(p => p.Buyer.Fullname))
            .ForMember(d => d.buyerPhoneNumber, map => map.MapFrom(p => p.Buyer.PhoneNo))
            .ForMember(d => d.buyerEmail, map => map.MapFrom(p => p.Buyer.Email))
            .ForMember(d => d.productName, map => map.MapFrom(p => p.Post.ProductName))
            .ForMember(d => d.orderStatusName, map => map.MapFrom(p => p.OrderStatus.StatusName))
            .ForMember(d => d.price, map => map.MapFrom(p => p.Post.Price));
        CreateMap<ExchangeOrderEntityViewModel, ExchangeOrder>();
        CreateMap<ExchangeOrder, ExchangeOrderCreateRequest>()
            .ReverseMap();
        CreateMap<ExchangeOrderCreateRequest, ExchangeOrder>()
            .ReverseMap();

    }
    public void MapExchangeRequest()
    {
        CreateMap<ExchangeOrder, ExchangeRequestEntityViewModel>()
            .ForMember(d => d.sellerName, map => map.MapFrom(p => p.Seller.Fullname))
            .ForMember(d => d.sellerPhoneNumber, map => map.MapFrom(p => p.Seller.PhoneNo))
            .ForMember(d => d.sellerEmail, map => map.MapFrom(p => p.Seller.Email))
            .ForMember(d => d.productName, map => map.MapFrom(p => p.Post.ProductName))
            .ForMember(d => d.orderStatusName, map => map.MapFrom(p => p.OrderStatus.StatusName))
            .ForMember(d => d.price, map => map.MapFrom(p => p.Post.Price));
        CreateMap<ExchangeRequestEntityViewModel, ExchangeOrder>();
    }
    public void MapExchange() { 
        CreateMap<ExchangeOrder,ExchangeViewEntityModel>()
            .ForMember(d=>d.sellerName, map => map.MapFrom(p => p.Seller.Fullname))
            .ForMember(d=>d.buyerName, map => map.MapFrom(p =>p.Buyer.Fullname))
            .ForMember(d => d.productName, map => map.MapFrom(p => p.Post.ProductName))
            .ForMember(d => d.orderStatusName, map => map.MapFrom(p => p.OrderStatus.StatusName))
            .ForMember(d => d.price, map => map.MapFrom(p => p.Post.Price))
            .ForMember(d => d.buyerEmail,map =>map.MapFrom(p=>p.Buyer.Email))
            .ForMember(d => d.sellerEmail, map => map.MapFrom(p => p.Seller.Email));
        CreateMap<ExchangeViewEntityModel, ExchangeOrder>();
    }
    public void MapReview()
    {
        CreateMap<Review, ReviewEntityViewModel>()
            .ForMember(d => d.reviewerName, map => map.MapFrom(d => d.Reviewer.Fullname))
            .ForMember(d => d.reviewerEmail, map => map.MapFrom(d => d.Reviewer.Email))
            .ForMember(d => d.reviewedName, map => map.MapFrom(d => d.Reviewed.Fullname))
            .ForMember(d => d.reviewedEmail, map => map.MapFrom(d => d.Reviewed.Email));
        CreateMap<Review,ReviewCreateRequest>()
            .ReverseMap();
        CreateMap<ReviewCreateRequest, Review>()
            .ReverseMap();
    }

    public void MapImage()
    {
        CreateMap<Image, ImageViewModel>()
            .ReverseMap();
        CreateMap<ImageViewModel, Image>()
            .ReverseMap();
    }
    
    public void MapReportImage()
    {
        CreateMap<ReportImage, ReportImageViewModel>()
            .ReverseMap();
        CreateMap<ReportImageViewModel, ReportImage>()
            .ReverseMap();
    }
}