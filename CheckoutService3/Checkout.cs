using Entities.DomainModels;
using Repository.Contracts;

namespace CheckoutService3
{
    public class Checkout : ICheckout
    { 
        //private List<Item> items;
        //private List<DiscountRule> discountRules;
        private readonly IItemRepository itemRepository;
        private readonly IDiscountRuleRepository discountRuleRepository;


        public Dictionary<string, int> ScannedItems { get;}

        public Checkout(IItemRepository itemRepository, IDiscountRuleRepository discountRuleRepository)
        {
            itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
            discountRuleRepository = discountRuleRepository ?? throw new ArgumentNullException(nameof(discountRuleRepository));
        }
    
        //public Checkout(List<Item> items, List<DiscountRule> discountRules)
        //{
        //    this.items = items ?? throw new ArgumentNullException(nameof(items)) ; //null checks here
        //    this.discountRules = discountRules ?? throw new ArgumentNullException(nameof(items)); //null checks here
        //    ScannedItems = new Dictionary<string, int>();
        //}
        public decimal GetTotalPrice()
        {
            var items = itemRepository.GetItems();
            var discountRules = discountRuleRepository.GetDiscountRules();
            decimal total=0;
            /*
             * Implement the logic to calculate the total price of the scanned items, based on the following rules:
             * In a normal supermarket, things are identified using Stock Keeping Units, or SKUs. In our shop, we’ll use individual letters of the alphabet (A, B, C, and so on). 
             * Our goods are priced individually. In addition, some items are multipriced: buy n of them, and they’ll cost you y pounds. 
             * For example, item ‘A’ might cost 50 pounds individually, but this week we have a special offer: buy three ‘A’s and they’ll cost you 130. The current pricing and offers are as follows:
            * SKU	Unit Price	Special Price
            * A	50	3 for 130
            * B	30	2 for 45
            * C	20	
            * D	15	
            * Our checkout accepts items in any order, so that if we scan a B, an A, and another B, we’ll recognize the two B’s and price them at 45 (for a total price so far of 95). 
            * Because the pricing changes frequently, we need to be able to pass in a set of pricing rules each time we start handling a checkout transaction.           
             */
            if (ScannedItems.Count == 0)
              {
                return 0;   
              }   

              foreach(var item in ScannedItems)
            {
                var itemPrice = items.Find(i => i.SKU == item.Key).Price;
                var itemQuantity = item.Value;
                var discountRule = discountRules.Find(d => d.SKU == item.Key);
                if(discountRule != null)
                {
                    //var discountQuantity = discountRule.Quantity;
                    //var discountPrice = discountRule.Discount;
                    //var discount = itemQuantity / discountQuantity;
                    //var remainder = itemQuantity % discountQuantity;
                    //total += (discount * discountPrice) + (remainder * itemPrice);
                    total += GetTotalPriceWithDiscount(itemPrice, itemQuantity, discountRule);
                }
                else
                {
                    total += itemPrice * itemQuantity;
                }

                               
            }   

              return total;
        }

        public void Scan(string sku)
        {
            //check sku is not null
            if (string.IsNullOrEmpty(sku))
            {
                throw new ArgumentNullException(nameof(sku));
            }
            
            if (items.Exists(i => i.SKU == sku))
            {
                if (ScannedItems.ContainsKey(sku))
                {
                    ScannedItems[sku]++;
                }
                else
                {
                    ScannedItems[sku] = 1;
                }
            }
            else
            {
                throw new ArgumentException("Invalid SKU");
            }


        }
    }
}
