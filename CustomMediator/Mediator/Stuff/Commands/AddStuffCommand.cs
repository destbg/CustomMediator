using CustomMediator.Implementation.Interfaces;

namespace CustomMediator.Mediator.Stuff.Commands
{
    public class AddStuffCommand : IRequest<Stuff>
    {
        public string Name { get; set; }
    }
}
