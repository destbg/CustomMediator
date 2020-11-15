using Autofac;
using CustomMediator.Implementation.Interfaces;
using CustomMediator.Mediator.Stuff.Commands;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CustomMediator
{
    internal static class Program
    {
        private static async Task Main()
        {
            var builder = new ContainerBuilder();

            builder.AddMediators();

            var container = builder.Build();

            var mediator = new Implementation.Mediator(container);

            var result = await mediator.Send(new AddStuffCommand { Name = "Test" });

            System.Console.WriteLine(result.Name);
        }

        private static void AddMediators(this ContainerBuilder builder)
        {
            var mediators = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(f => f.IsClass && typeof(IBaseRequestHandler).IsAssignableFrom(f));

            foreach (var mediator in mediators)
            {
                builder.RegisterType(mediator)
                    .AsImplementedInterfaces();
            }
        }
    }
}
