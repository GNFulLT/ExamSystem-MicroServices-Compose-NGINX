namespace auth_service.Interfaces
{
    public interface IAuthObject
    {
        public string Key { get; set; }
        public int QueueNumber { get; set; }
    }
}
