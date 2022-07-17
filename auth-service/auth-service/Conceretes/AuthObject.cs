using auth_service.Interfaces;

namespace auth_service.Conceretes
{
    public class AuthObject : IAuthObject
    {
        public string? Key { get; set; }
        public int QueueNumber { get; set; }

        public AuthObject() { }

        public AuthObject(string key, int queueNumber)
        {
            Key = key;
            QueueNumber = queueNumber;
        }

    }
}
