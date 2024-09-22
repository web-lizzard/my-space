namespace MySpot.Application.Abstractions;


public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
{
    Task Handle(TCommand command);
}