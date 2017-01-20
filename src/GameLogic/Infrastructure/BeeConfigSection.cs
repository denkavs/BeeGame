using GameLogic.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Infrastructure
{
    public class BeeConfigSection: ConfigurationSection
    {
        // Declare the UrlsCollection collection property.
        [ConfigurationProperty("bees", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(BeesCollection), 
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public BeesCollection Bees
        {
            get
            {
                BeesCollection beesCollection =
                    (BeesCollection)base["bees"];

                return beesCollection;
            }
        }
    }

    public class BeesCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new BeeConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((BeeConfigElement)element).Type;
        }

        public void Add(BeeConfigElement element)
        {
            BaseAdd(element);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(BeeConfigElement element)
        {
            if (BaseIndexOf(element) >= 0)
            {
                BaseRemove(element.Type);
            }
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string type)
        {
            BaseRemove(type);
        }

        public void Clear()
        {
            BaseClear();
        }
    }

    public class BeeConfigElement : ConfigurationElement
    {
        public BeeConfigElement(string type, int deduction, int count, int lifeSpan)
        {
            this.Type = type;
            this.Deduction = deduction;
            this.Count = count;
            this.LifeSpan = lifeSpan;
        }

        public BeeConfigElement()
        {
        }

        [ConfigurationProperty("type", IsRequired = true, IsKey = true)]
        public string Type
        {
            get
            {
                return (string)this["type"];
            }
            set
            {
                this["type"] = value;
            }
        }

        public BeeType GetBeeType()
        {
            StringBuilder sb = new StringBuilder(this.Type.ToLower());
            string tempName = this.Type.ToUpper();

            sb[0] = tempName[0];
            string name = sb.ToString();

            BeeType res;
            return Enum.TryParse<BeeType>(name, out res) ? res : BeeType.Unknown;
        }

        [ConfigurationProperty("deduction", IsRequired = true)]
        public int Deduction
        {
            get
            {
                return (int)this["deduction"];
            }
            set
            {
                this["deduction"] = value;
            }
        }

        [ConfigurationProperty("count", IsRequired = true)]
        public int Count
        {
            get
            {
                return (int)this["count"];
            }
            set
            {
                this["count"] = value;
            }
        }

        [ConfigurationProperty("lifeSpan", IsRequired = true)]
        public int LifeSpan
        {
            get
            {
                return (int)this["lifeSpan"];
            }
            set
            {
                this["lifeSpan"] = value;
            }
        }
    }
}
