using FluentValidation.Results;

namespace Pokedex.Application.Notifications;

public interface INotificator
{
    bool HasNotification { get; }
    bool IsNotFoundResourse { get; }
    
    void Handle(string message);
    void Handle(List<ValidationFailure> failures);
    void HandleNotFoundResourse();
    IEnumerable<string> GetNotifications();
}