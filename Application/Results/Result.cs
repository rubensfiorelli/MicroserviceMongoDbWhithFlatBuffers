using Core.Contracts;
using Core.Notifications;

namespace Application.Results
{
    public sealed class Result : IResult
    {
        private List<Notification> _notifications;

        public Result(int resultCode, string message, bool success, object resultData)
        {
            ResultCode = resultCode;
            Message = message;
            Success = success;
            ResultData = resultData;

            _notifications = [];
        }

        public int ResultCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public object ResultData { get; set; }

        public IReadOnlyList<Notification> Notifications => _notifications;

        public void AddNotification(List<Notification> notifications)
        {
            _notifications = notifications;
        }

        public void SetResultData(object resultData)
        {
            ResultData = resultData;
        }
    }
}
