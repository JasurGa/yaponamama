namespace Atlas.Application.Enums
{
    public enum OrderStatus
    {
        Created         = 0,
        Сollecting      = 1,
        Delivering      = 2,
        Success         = 3,
        CanceledByUser  = 4,
        CanceledByAdmin = 5,
        ReadyForPickup  = 6,
    }
}
