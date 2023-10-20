using FluentValidation.Results;

namespace Pokedex.Application.Notifications;

public class Notificator : INotificator
{
    private readonly List<string> _notifications;
    private bool _notFoundResourse;

    public Notificator()
    {
        _notifications = new List<string>();
    }

    public void Handle(string message)
    {
        if (_notFoundResourse)
        {
            throw new InvalidOperationException("Não é possível chamar o Handle quando for NotFoundResourse!");
        }
        
        _notifications.Add(message);
    }

    public void Handle(List<ValidationFailure> failures)
    {
        if (HasNotification)
        {
            throw new InvalidOperationException("Não é possível chamar o HandleNotFoundResourse quando for Handle!");
        }
        
        failures.ForEach(error => Handle(error.ErrorMessage));
    }

    public void HandleNotFoundResourse()
    {
        _notFoundResourse = true;
    }

    public IEnumerable<string> GetNotifications() => _notifications;

    public bool HasNotification => _notifications.Any();

    public bool IsNotFoundResourse => _notFoundResourse;
}