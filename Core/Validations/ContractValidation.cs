using Core.Contracts;
using Core.Notifications;

namespace Core.Validations
{
    public partial class ContractValidation<T> where T : IContract
    {
        private List<Notification> _notifications;

        public ContractValidation() => _notifications = [];
        IReadOnlyList<Notification> Notifications => _notifications;

        public void AddNotification(Notification notification) => _notifications.Add(notification);
        public bool IsValid() => !_notifications.Any();
    }
}
