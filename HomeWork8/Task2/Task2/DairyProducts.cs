using System;
using System.Text;

namespace Task2
{
    public class DairyProducts : Product
    {
        private int _expiration;

        public int Expiration
        {
            get => _expiration;
            set
            {
                if (value > 0)
                {
                    _expiration = value;
                }
                else
                {
                    throw new ArgumentException("Number of days to expiration must be a positive number");
                }
            }
        }

        public DairyProducts(string name, double price, double weight, int expiration) : base(name, price, weight)
        {
            Expiration = expiration;
        }

        public override void ChangePrice(double diff)
        {
            if ((diff.CompareTo(-1d) == -1) || (diff.CompareTo(1d) == 1))
            {
                throw new ArgumentException("Difference must be a number between -1 and 1");
            }
            base.ChangePrice(diff);
            if (Expiration < 10)
            {
                base.ChangePrice(-0.3);

            }
            else if (10 <= Expiration && Expiration < 20)
            {
                base.ChangePrice(-0.1);
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(base.ToString());
            result.Append("Expiration date: " + _expiration.ToString() + "\n");
            return result.ToString();
        }
        public override int GetHashCode()
        {
            return (base.GetHashCode() << 2) ^ _expiration;
        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !(this.GetType().Equals(obj.GetType())))
            {
                return false;
            }
            else
            {
                DairyProducts temp = obj as DairyProducts;
                return (base.Equals((Product)obj) && (this.Expiration == temp.Expiration));
            }
        }
    }
}
