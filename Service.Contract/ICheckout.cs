namespace Service.Contracts
{
    public interface ICheckout
    {
        void Scan(string item);
        decimal GetTotalPrice();
    }
}
