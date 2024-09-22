using MySpot.Application.Abstractions;

namespace MySpot.Infrastructure.DAL.Decorators;

internal class UnitOfWorkHandlerDecorator<TCommand>(
    ICommandHandler<TCommand> commandHandler, IUnitOfWork unitOfWork) : ICommandHandler<TCommand> where TCommand : class, ICommand
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICommandHandler<TCommand> _commandHandler = commandHandler;

    async public Task Handle(TCommand command)
    {
        await _unitOfWork.Execute(() => _commandHandler.Handle(command));
    }
}