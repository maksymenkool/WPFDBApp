using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeStruct
{
    public class Element
    {
        private string name;
        private Dictionary<string, string> attributes = new Dictionary<string, string>();

        public Element()
        { }

        public Element(string name, string text = null, Dictionary<string, string> attributes = null)
        {
            Name = name;
            Text = text;
            Attributes = attributes;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (value != null)
                    this.name = value;
            }
        }

        public string Text { get; set; }

        public Dictionary<string, string> Attributes
        {
            get
            {
                return this.attributes;
            }
            set
            {

                this.attributes = value;
            }
        }

        public override bool Equals(Object obj)
        {
            bool flag = false;
            if (this == obj) return true;
            if (null == obj) return false;

            if (obj is Element)
            {
                var otherData = obj as Element;
                if (otherData.Name.ToLower() == this.Name.ToLower())
                {
                    flag = true;
                    if (otherData.Attributes != null && otherData.Attributes.Count > 0)
                    {
                        foreach (var otherAttr in otherData.Attributes)
                        {
                            if (this.Attributes != null && this.Attributes.TryGetValue(otherAttr.Key.ToLower(), out string value))
                                flag = (otherAttr.Value.ToLower() == value.ToLower()) ? true : false;
                            else
                                flag = false;
                            if (!flag) break;
                        }
                    }
                }
            }
            return flag;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = Name.GetHashCode() * 17;
                return hash;
            }
        }

        public override string ToString()
        {
            StringBuilder att = new StringBuilder(Name);
            if (Attributes != null)
                foreach (var at in Attributes)
                {
                    att.AppendLine(" " + at.Key + " = " + at.Value);
                }
            return att.Append(Text).ToString();
        }
    }
}
