using AutoMapper;
using DiscountGrpc.Entities;
using DiscountGrpc.Protos;

namespace DiscountGrpc.Mapper
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
