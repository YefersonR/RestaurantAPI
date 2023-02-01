using AutoMapper;
using Core.Application.DTOs.Account;
using Core.Application.ViewModels.Ingrediente;
using Core.Application.ViewModels.Mesa;
using Core.Application.ViewModels.Orden;
using Core.Application.ViewModels.Ordenes;
using Core.Application.ViewModels.Platos;
using Core.Application.ViewModels.User;
using Core.Domain.Entities;
namespace Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Mesa, MesaViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.Created, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());

            CreateMap<Mesa, MesaSaveViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.Created, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());
            
            CreateMap<Orden, OrdenViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.Created, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());

            CreateMap<Orden, OrdenSaveViewModel>()
                .ForMember(dest => dest.Platos, opt => opt.Ignore())
                .ReverseMap()
                    .ForMember(dest => dest.Platos, opt => opt.Ignore())
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.Created, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());

            CreateMap<Plato, PlatoViewModel>()
                .ForMember(dest=>dest.Ingredientes, opt=>opt.Ignore())
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.Created, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());
            
            CreateMap<Plato, PlatoSaveViewModel>()
                .ForMember(dest=>dest.Ingredientes, opt=>opt.Ignore())
                .ReverseMap()
                    .ForMember(dest=>dest.Ingredientes, opt=>opt.Ignore())
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.Created, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());
                                

            CreateMap<Ingrediente, IngredienteViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.Created, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());

            CreateMap<Ingrediente, IngredienteSaveViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.Created, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());
            
            //CreateMap<PlatoIngredientes, PlatoIngredientesSaveViewModel>()
            //    .ReverseMap()
            //        .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            //            .ForMember(dest => dest.Created, opt => opt.Ignore())
            //                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            //                    .ForMember(dest => dest.Updated, opt => opt.Ignore());

            //CreateMap<PlatoIngredientes, PlatoIngredientesViewModel>()
            //    .ReverseMap()
            //        .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            //            .ForMember(dest => dest.Created, opt => opt.Ignore())
            //                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            //                    .ForMember(dest => dest.Updated, opt => opt.Ignore());

            //CreateMap<OrdenesPlatos, OrdenesPlatosSaveViewModel>()
            //    .ReverseMap()
            //        .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            //            .ForMember(dest => dest.Created, opt => opt.Ignore())
            //                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            //                    .ForMember(dest => dest.Updated, opt => opt.Ignore());

            //CreateMap<PlatoIngredientesSaveViewModel, PlatoIngredientesViewModel>()
            //    .ReverseMap()
            //        .ForMember(dest => dest.Id, opt => opt.Ignore())
            //        .ForMember(dest => dest.PlatoId, opt => opt.Ignore());
            

            ////CreateMap<OrdenesPlatosSaveViewModel, OrdenesPlatosViewModel>()
            ////   .ReverseMap()
            ////       .ForMember(dest => dest.Ordenid, opt => opt.Ignore());

            //CreateMap<OrdenesPlatos, OrdenesPlatosViewModel>()
            //                .ReverseMap()
            //                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            //                        .ForMember(dest => dest.Created, opt => opt.Ignore())
            //                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            //                                .ForMember(dest => dest.Updated, opt => opt.Ignore());

            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                    .ForMember(dest => dest.Error, opt => opt.Ignore())
                        .ReverseMap();
            CreateMap<RegisterRequest, UserSaveViewModel>()
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                    .ForMember(dest => dest.Error, opt => opt.Ignore())
                        .ReverseMap();
            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                    .ForMember(dest => dest.Error, opt => opt.Ignore())
                        .ReverseMap();
            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                    .ForMember(dest => dest.Error, opt => opt.Ignore())
                        .ReverseMap();
        }
    }
}
