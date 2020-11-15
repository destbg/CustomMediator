using CustomMediator.Implementation.Interfaces;

namespace CustomMediator.Mediator.Stuff.Commands
{
    public class AddStuffHandler : IRequestHandler<AddStuffCommand, Stuff>
    {
        public Stuff Handle(AddStuffCommand request)
        {
            return new Stuff
            {
                Name = request.Name
            };
        }
    }
}
