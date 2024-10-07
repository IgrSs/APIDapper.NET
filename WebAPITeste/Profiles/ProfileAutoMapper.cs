using AutoMapper;
using WebAPITeste.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPITeste.Profiles
{
    public class ProfileAutoMapper : Profile

    {
        public ProfileAutoMapper()
        {
            CreateMap<Produto, ProdutoListarDto>();

        }


    }
}
