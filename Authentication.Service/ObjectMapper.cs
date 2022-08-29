using System;
using AutoMapper;

namespace Authentication.Service
{
    public static class ObjectMapper
    {
        //Lazyloading yapıyor.Sadece ihtiyacı olduğunda yüklüyor. İstediğimiz zaman yüklenmesi için yapıyoruz.
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<DtoMapper>(); });

            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }
}