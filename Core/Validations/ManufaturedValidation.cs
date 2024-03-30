using Core.Notifications;

namespace Core.Validations
{
    public partial class ContractValidation<T>
    {
        public ContractValidation<T> ValidManufactureIsOk(DateTimeOffset Manufacturing, DateTimeOffset Validate, string message, string propertyName)
        {
            if (Manufacturing >= Validate)
                AddNotification(new Notification(message, propertyName));

            return this;

        }

    }
}
