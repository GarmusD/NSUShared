namespace NSU.Shared.DataContracts
{
    public interface IElHeatingDataDataContract
    {
        byte Index { get; set; }
        byte StartHour { get; set; }
        byte StartMin { get; set; }
        byte EndHour { get; set; }
        byte EndMin { get; set; }
    }
}
