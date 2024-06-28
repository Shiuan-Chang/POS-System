using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_system.Strategies
{
    internal abstract class DiscountStrategy
    {
        protected List<Item> items;

        public DiscountStrategy(List<Item> items)
        {
            this.items = items;
        }

        public abstract void DiscountPolicyInterface();
    }
}
