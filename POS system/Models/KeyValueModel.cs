using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_system
{
    internal class KeyValueModel
    {
        public String Key { get; set; }
        public String Value { get; set; }
        public KeyValueModel(String Key,String Value) {
            this.Key = Key; 
            this.Value = Value;
        }
    }
}
