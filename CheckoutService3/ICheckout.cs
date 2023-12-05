namespace CheckoutService3
{
    public interface ICheckout
    {
        void Scan(string item);
        decimal GetTotalPrice();
    }
}
