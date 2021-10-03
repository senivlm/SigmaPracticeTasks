using System.Text;

namespace hw1
{
    public class Check
    {
        public static string PrintBuy(Buy buy)
        {
            var result = new StringBuilder();

            for (int i = 0; i < buy.PurchaseList.Count; ++i)
            {
                result.Append("\n" + PrintProduct(buy.PurchaseList[i]));
            }

            result.Append($"\n\nAmount: {buy.Amount.ToString() } pcs");
            result.Append($"\nTotal Price: {buy.TotalPrice.ToString("$0.00")}");
            result.Append($"\nTotal Weight: {buy.TotalWeight.ToString("0.00 kg")}\n");

            return result.ToString();
        }

        public static string PrintProduct(Product product)
        {
            var result = new StringBuilder();

            result.Append($"\nName: {product.Name}");
            result.Append($"\nPrice: {product.Price.ToString("$0.00")}");
            result.Append($"\nWeight: {product.Weight.ToString("0.00 kg")}");

            return result.ToString();
        }
    }
}
