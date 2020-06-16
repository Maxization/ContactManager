using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp2
{
    public partial class Window2 : Window
    {
        ObservableCollection<Contact> data;

        public string Phone { get; set; }
        public string Email { get; set; }
        public string cName { get; set; }
        public string LastName { get; set; }
        public GENDER Gender { get; set; }

        List<TextBox> textBoxes;

        public Window2(ObservableCollection<Contact> data, ValidationRule phoneVal, ValidationRule emailVal, ValidationRule nameVal,ValidationRule surnameVal)
        {
            InitializeComponent();
            this.data = data;
            DataContext = this;
            textBoxes = new List<TextBox> { textboxName, textboxLastName, textboxEmail, textboxPhone };
            List<ValidationRule> validations = new List<ValidationRule> { nameVal, surnameVal, emailVal, phoneVal };
            for(int i=0;i<textBoxes.Count;i++)
            {
                if (validations[i] == null) continue;
                Binding binding = BindingOperations.GetBinding(textBoxes[i], TextBox.TextProperty);
                binding.ValidationRules.Add(validations[i]);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach(TextBox tb in textBoxes)
            {
                if(Validation.GetHasError(tb))
                {
                    return;
                }
            }
            Contact contact = new Contact(cName, LastName, Email, Phone, Gender);
            data.Add(contact);
            this.Close();
        }
    }
}
