namespace Repository.Contracts
{
    public interface IDiscountRuleRepository
    {
        IList<DiscountRule> GetDiscountRules();
        void UpdateDiscountRule()
    }
}
