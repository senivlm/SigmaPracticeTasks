using System;
using System.Text;

namespace hw4_task1
{
    public class DairyProducts : Product
    {

        public bool IsValid { get => MadeDate.AddDays(Expiration) > DateTime.Today; }

        public DairyProducts(string name, double price, double weight, int expiration, DateTime madeDate)
        : base(name, price, weight, expiration, madeDate)
        {
        }
        public DairyProducts(string s) : base(s) { }

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
        protected override void Parse(string s)
        {
            string[] fields = s.Split(";", StringSplitOptions.RemoveEmptyEntries);

            if (fields.Length != 5)
            {
                throw new ArgumentException($"{nameof(s)} has wrong format. String must contain 5 values separated by \";\" symbol whithout spaces");
            }

            string prodName = fields[0];

            if (!double.TryParse(fields[1], out double price))
            {
                throw new ArgumentException("Wrong price format");
            }
            if (!double.TryParse(fields[2], out double weight))
            {
                throw new ArgumentException("Wrong weight format");
            }
            if (!int.TryParse(fields[3], out int expiration))
            {
                throw new ArgumentException("Wrong expiration days format");
            }
            if (!DateTime.TryParse(fields[4], out DateTime madeDate))
            {
                throw new ArgumentException("Wrong production date format");
            }



            this.Name = prodName;
            this.Price = price;
            this.Weight = weight;
            this.Expiration = expiration;
            this.MadeDate = madeDate;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(base.ToString());
            result.Append("Expiration date: " + Expiration.ToString() + "\n");
            return result.ToString();
        }
        public override int GetHashCode()
        {
            return (base.GetHashCode() << 2) ^ Expiration;
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
