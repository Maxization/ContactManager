using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfApp2
{
    [Serializable]
    public enum GENDER { Male, Female }
    
    [Serializable]
    public class Contact
    {
        [XmlElement(IsNullable = true)]
        public string Name { get; set; }

        [XmlElement(IsNullable = true)]
        public string Surname { get; set; }
        
        [XmlElement(IsNullable = true)]
        public string Email { get; set; }
        
        [XmlElement(IsNullable = true)]
        public string Phone { get; set; }
        
        public GENDER Gender { get; set; }

        public Contact() {}
        public Contact(string first, string last, string email, string number, GENDER gender)
        {
            Name = first;
            Surname = last;
            Email = email;
            Phone = number;
            Gender = gender;
        }
    }
}
