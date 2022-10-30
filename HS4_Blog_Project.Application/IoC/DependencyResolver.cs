using Autofac;
using AutoMapper;
using HS4_Blog_Project.Application.AutoMapper;
using HS4_Blog_Project.Application.Services.AppUserService;
using HS4_Blog_Project.Application.Services.GenreService;
using HS4_Blog_Project.Application.Services.PostService;
using HS4_Blog_Project.Domain.Repositories;
using HS4_Blog_Project.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_Blog_Project.Application.IoC
{
    //Inversion of Controls
    // Nuget AutoMapper.Extensions.Dependency.Injection
    public class DependencyResolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Repository Registration
            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<GenreRepository>().As<IGenreRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PostRepository>().As<IPostRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AuthorRepository>().As<IAuthorRepository>().InstancePerLifetimeScope();
            builder.RegisterType<Mapper>().As<IMapper>().InstancePerLifetimeScope();
            #endregion

            #region Service Registeration
            builder.RegisterType<PostService>().As<IPostService>().InstancePerLifetimeScope();
            builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();
            builder.RegisterType<GenreService>().As<IGenreService>().InstancePerLifetimeScope();

            #endregion

            // bu kısmı internetten bulup yapıştırdık.
            #region AutoMapper

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                //Register Mapper Profile
                cfg.AddProfile<Mapping>();
            }
            )).AsSelf().SingleInstance();
            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
            #endregion

            base.Load(builder);
        }
    }
}
