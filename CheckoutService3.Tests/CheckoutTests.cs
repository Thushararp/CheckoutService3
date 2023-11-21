using CheckoutService3.DomainModels;

namespace CheckoutService3.Tests
{
    public class CheckoutTests
    {
        private Checkout checkout; // The checkout instance to be tested
        private List<Item> items; // The list of items available in the system
        private List<DiscountRule> discountRules;
        public CheckoutTests()
        {
            // Create the list of items
            items = new List<Item>()
            {
                new Item() { SKU = "A", Price = 50 },
                new Item() { SKU = "B", Price = 30 },
                new Item() { SKU = "C", Price = 20 },
                new Item() { SKU = "D", Price = 15 }
            };

            discountRules = new List<DiscountRule>()
            {
                new DiscountRule() { SKU = "A", Quantity = 3, Discount = 20 },
                new DiscountRule() { SKU = "B", Quantity = 2, Discount = 15 }
            };


            checkout = new Checkout(items, discountRules);
        }

        [Fact]
        public void Scan_ValidSKU_ShouldAddItemToScannedItems()
        {
            // Arrange
            string sku = "A";

            // Act
            checkout.Scan(sku);

            // Assert
            Assert.True(checkout.ScannedItems.ContainsKey(sku));
            Assert.Equal(1, checkout.ScannedItems[sku]);
        }
        
       [Fact]
        public void Scan_InvalidSKU_ShouldNotAddItemToScannedItems()
        {
            // Arrange
            string sku = "X";

            // Act
            checkout.Scan(sku);

            // Assert
            Assert.False(checkout.ScannedItems.ContainsKey(sku));
        }
    }
}