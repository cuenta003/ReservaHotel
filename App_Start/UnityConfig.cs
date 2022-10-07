using ReservaHotel.Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace ReservaHotel
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ICalentarioService, CalendarioService>();
            container.RegisterType<IClienteService, ClienteService>();
            container.RegisterType<IHabitacionService, HabitacionService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}