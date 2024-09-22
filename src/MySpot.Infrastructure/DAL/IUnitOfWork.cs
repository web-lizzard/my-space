namespace MySpot.Infrastructure.DAL;


internal interface IUnitOfWork
{
    Task Execute(Func<Task> action);
}