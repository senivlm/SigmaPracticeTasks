using System;
using System.Linq;
using System.Text;

namespace hw4_task1
{
    public class Meat : Product
    {
        private MeatCategory _category = MeatCategory.Second;
        private MeatSort _sort = MeatSort.Chicken;

        public MeatCategory Category
        {
            get => _category;
            set
            {
                if (Enum.IsDefined(typeof(MeatCategory), value))
                {
                    _category = value;
                }
            }
        }
        public MeatSort Sort
        {
            get => _sort;
            set
            {
                if (Enum.IsDefined(typeof(MeatSort), value))
                {
                    _sort = value;
                }
            }
        }

        public Meat(string name, double price, double weight, int expiration, DateTime madeDate, MeatCategory category, MeatSort sort)
        : base(name, price, weight, expiration, madeDate)
        {
            Category = category;
            Sort = sort;
        }
        public Meat(string s)
        {
            Parse(s);
        }
        
        protected override void Parse(string s)
        {
            string[] fields = s.Split(";", StringSplitOptions.RemoveEmptyEntries);
           
            if (fields.Length != 7)
            {
                throw new ArgumentException(@$"{nameof(s)} has wrong format. String must contain 9 values separated by ;
                symbol whithout spaces (spaces in product name are allowed");
            }

            base.Parse(String.Join(";", fields.Where( (_, index) => index < 5 )));

            if (!Enum.TryParse(typeof(MeatCategory), fields[5], true, out var category))
            {
                throw new ArgumentException("Wrong type of Meat Category. Allowed values: High, First, Second");
            }
            if (!Enum.TryParse(typeof(MeatSort), fields[6], true, out var sort))
            {
                throw new ArgumentException("Wrong type of Meat Sort. Allowed values: Mutton, Beef, Pork, Chicken");
            }
           
            this.Category = (MeatCategory)category;
            this.Sort = (MeatSort)sort;
        }
        public override void ChangePrice(double diff)
        {
            base.ChangePrice(diff);
            switch (Category)
            {
                case MeatCategory.High: base.ChangePrice(0.06); break;
                case MeatCategory.First: base.ChangePrice(0.01); break;
                case MeatCategory.Second: base.ChangePrice(-0.01); break;
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(base.ToString());
            result.Append("Category: " + _category + "\n");
            result.Append("Sort: " + _sort + "\n");
            return result.ToString();
        }
        public override int GetHashCode()
        {
            return base.GetHashCode() << 2;
        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !(this.GetType().Equals(obj.GetType())))
            {
                return false;
            }
            else
            {
                Meat temp = obj as Meat;
                return (base.Equals((Product)obj) && this.Category.Equals(temp.Category) && this.Sort.Equals(temp.Sort));
            }
        }
    }
}
