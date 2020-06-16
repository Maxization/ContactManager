using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace WpfApp2
{
    public class WindowData : INotifyPropertyChanged
    {
        ObservableCollection<Contact> contacts;
        public ObservableCollection<Contact> Contacts
        {
            get
            {
                return contacts;
            }
            set
            {
                contacts = value;
                OnPropertyRaised("Contacts");
            }
        }
        bool my_lock;
        public bool Lock
        {
            get
            {
                return my_lock;
            }
            set
            {
                my_lock = value;
                OnPropertyRaised("Lock");
            }
        }

        public List<ValidationRule> Validations { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
    public partial class MainWindow : Window
    {
        WindowData data;
        public MainWindow()
        {
            InitializeComponent();
            data = new WindowData();
            data.Lock = true;
            data.Contacts = new ObservableCollection<Contact>();
            data.Validations = new List<ValidationRule>();
            DataContext = data;

            string folder = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\Validations\";
            string filter = "*.dll";
            string[] files = Directory.GetFiles(folder, filter);
            foreach(string file in files)
            {
                Assembly assembly = Assembly.LoadFrom(file);
                AppDomain.CurrentDomain.Load(assembly.GetName());
                Type type = assembly.GetExportedTypes()[0];
                ValidationRule rule = (ValidationRule)Activator.CreateInstance(type);
                data.Validations.Add(rule);
            }

        }

        //Exit
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //About
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is example contact manager", "Contact Manager", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Clear list
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            data.Contacts.Clear();
        }

        //Open new contact window
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            var wnd = new Window2(data.Contacts,(ValidationRule)comboboxPhone.SelectedItem,(ValidationRule)comboboxEmail.SelectedItem, (ValidationRule)comboboxName.SelectedItem, (ValidationRule)comboboxSurname.SelectedItem);
            Opacity = 0.5;
            wnd.ShowDialog();
            Opacity = 1;
        }

        //Delete selected item
        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if (mainListBox.SelectedIndex == -1) return;
            Contact contact = (Contact)mainListBox.SelectedItem;
            data.Contacts.Remove(contact);
        }

        //Export
        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Contacts.xml";
            saveFileDialog.Filter = "Xml files (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == true)
            {
                XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<Contact>));
                FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
                ser.Serialize(fs, data.Contacts);
            }
        }

        //Import
        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Xml files (*.xml)|*.xml";
                if (openFileDialog.ShowDialog() == true)
                {
                    XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<Contact>));
                    FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open);
                    ObservableCollection<Contact> k = ser.Deserialize(fs) as ObservableCollection<Contact>;
                    data.Contacts = k;
                    mainListBox.SelectedIndex = -1;
                }
            }
            catch(InvalidOperationException)
            {
                MessageBox.Show("Import failed!","Import Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            data.Lock = !data.Lock;
        }
    }

}
