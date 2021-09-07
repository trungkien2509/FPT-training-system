using System.Data.Entity;
using System.Reflection;
using System.Web.Mvc;
using FPT_Training_System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using FPT_Training_System.Data.Migrations;
using FPT_Training_System.Data.Models;

namespace FPT_Training_System.Web.Services
{
    //Config Dependency Injection
    public class DependencyInjector
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);


            RegisterService(builder);

            //// OPTIONAL: Register model binders that require DI.
            //builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            //builder.RegisterModelBinderProvider();

            //// OPTIONAL: Register web abstractions like HttpContextBase.
            //builder.RegisterModule<AutofacWebTypesModule>();

            //// OPTIONAL: Enable property injection in view pages.
            //builder.RegisterSource(new ViewRegistrationSource());

            //// OPTIONAL: Enable property injection into action filters.
            //builder.RegisterFilterProvider();

            // OPTIONAL: Enable action method parameter injection (RARE).
            //builder.InjectActionInvoker();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static ContainerBuilder RegisterService(ContainerBuilder builder)
        {
            var dataAccess = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(dataAccess)
                //.Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<TrainingProgramManagerDbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TrainingProgramManagerDbContextNoTracking>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IServiceDI).Assembly)
                .Where(t => t.IsAssignableTo<IServiceDI>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();


            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TrainingProgramManagerDbContext, Configuration>());
            //Database.SetInitializer<TrainingProgramManagerDbContext>(new TrainingProgramManagerDbInitializer());

            return builder;
        }
    }
}
