
namespace ClassDef
{
    public class InvenItemInfo
    {
        public long uid;
        public int count;
        public float price;

        public void AddCount(int _count, float _price)
        {
            double totalPrice = ((double)count * (double)price) + ((double)_count * (double)_price);

            count = count + _count;
            price = (float)(totalPrice / (double)count);
        }

        public void RemoveCount(int _count)
        {
            count -= _count;
        }
    }
}
