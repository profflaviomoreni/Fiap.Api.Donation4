using AutoMapper;
using Fiap.Api.Donation4.Models;
using Fiap.Api.Donation4.ViewModel;

namespace Fiap.Api.Donation4Test
{
    public class BaseTest
    {

        protected IMapper _mapper;

        public BaseTest()
        {
            var mapperConfig = new AutoMapper.MapperConfiguration(m =>
            {
                m.AllowNullCollections = true;
                m.AllowNullDestinationValues = true;

                m.CreateMap<UsuarioModel, LoginResponseViewModel>();
                m.CreateMap<LoginRequestViewModel, UsuarioModel>();

                m.CreateMap<UsuarioModel, UsuarioResponseViewModel>();

                m.CreateMap<ProdutoModel, ProdutoResponseViewModel>()
                    .ForMember(dest => dest.NomeCategoria, opt => opt.MapFrom(src => src.Categoria.NomeCategoria))
                    .ForMember(dest => dest.NomeUsuario, opt => opt.MapFrom(src => src.Usuario.EmailUsuario));

            });
            
            _mapper = mapperConfig.CreateMapper();
        }

    }
}
