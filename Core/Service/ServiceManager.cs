using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ServiceAbstraction;
using Shared.Dto;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyProductService;
        private readonly Lazy<IBasketService> _lazyBasketService;
        private readonly Lazy<ICacheService> _lazyCacheService;
        private readonly Lazy<IAuthService> _lazyAuthService;
        private readonly Lazy<IOrderService> _lazyOrderService;
       

        public ServiceManager(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IBasketRepository basketRepository,
            ICacheRepository cacheRepository,
            UserManager<AppUser> userManager,
            IOptions<JwtOptions> options)
        {
            _lazyProductService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            _lazyBasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
            _lazyCacheService = new Lazy<ICacheService>(() => new CacheService(cacheRepository, mapper));
            _lazyAuthService = new Lazy<IAuthService>(() => new AuthService(mapper, userManager, options));
            _lazyOrderService = new Lazy<IOrderService>(() => new OrderService(mapper, unitOfWork, basketRepository));
        }

        public IProductService ProductService => _lazyProductService.Value;
        public IBasketService BasketService => _lazyBasketService.Value;
        public ICacheService CacheService => _lazyCacheService.Value;
        public IAuthService AuthService => _lazyAuthService.Value;

        public IOrderService OrderService => _lazyOrderService.Value;
    }
}
