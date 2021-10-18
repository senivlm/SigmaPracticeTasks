using System;
using System.Text;

namespace hw2
{
    public class Meat : Product
    {
        private MeatCategory _category;
        private MeatSort _sort;

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

        public Meat(string name, double price, double weight, MeatCategory category, MeatSort sort) : base(name, price, weight)
        {
            Category = category;
            Sort = sort;
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
