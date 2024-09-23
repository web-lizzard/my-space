using System.Diagnostics;
using Microsoft.Extensions.Logging;
using MySpot.Application.Abstractions;

namespace MySpot.Infrastructure.Logging.Decorators;


internal class LoggingHandlerDecorator<TCommand>(
    ILogger<ICommandHandler<TCommand>> logger,
    ICommandHandler<TCommand> commandHandler
    ) : ICommandHandler<TCommand> where TCommand : class, ICommand
{
    private readonly ILogger<ICommandHandler<TCommand>> _logger = logger;
    private readonly ICommandHandler<TCommand> _commandHandler = commandHandler;

    public async Task Handle(TCommand command)
    {
        var commandName = command.GetType().Name;
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        _logger.LogInformation("Triggered Command: {commandName}...", commandName);
        await _commandHandler.Handle(command);
        _logger.LogInformation(
            "Completed handling a command: {commandName} is {elapsed}", commandName, stopwatch.Elapsed);
    }
}