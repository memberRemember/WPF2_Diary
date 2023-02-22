using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;   
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Prak2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                DatePicker.SelectedDate = DateTime.Today;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);   
                Close();
            }
        }
        
        public void Reload(string selected)
        {
            List<Zametka> reader = deserialization.Deserialize<List<Zametka>>();
            DateTime dates = Convert.ToDateTime(DatePicker.Text);

            foreach (var item in reader) {
                if (item.Name == selected)
                {
                    TextBoxName.Text = item.Name;
                    TextBoxDescription.Text = item.Description;
                }
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }
        private void DatePickerSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TextBoxName.Text = "";
                TextBoxDescription.Text = "";
                Read();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
            
        }
        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            Create();
        }
        private void ListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string selected = SpisokZametok.SelectedItem.ToString();
                Reload(selected);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                TextBoxName.Text = "";
                TextBoxDescription.Text = "";
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        public void Create()
        {
            List<Zametka> reader = deserialization.Deserialize<List<Zametka>>();
            DateTime dates = Convert.ToDateTime(DatePicker.Text);
            Zametka ZametkaCreated = new Zametka();

            ZametkaCreated.Name = TextBoxName.Text;
            ZametkaCreated.Description = TextBoxDescription.Text;
            ZametkaCreated.data = dates;
            reader.Add(ZametkaCreated);
            deserialization.Serialize(reader);
            TextBoxName.Text = "";
            TextBoxDescription.Text = "";
            Read();
        }
        public void Read()
        {
            List<string> NameReader = new List<string>();
            List<Zametka> reader = deserialization.Deserialize<List<Zametka>>();
            DateTime dates = Convert.ToDateTime(DatePicker.Text);
            foreach (var item in reader)
            {
                if (dates == item.data)
                {
                    NameReader.Add(item.Name);

                }
            }
            SpisokZametok.ItemsSource = NameReader;
        }
        public void Update()
        {
            List<Zametka> reader = deserialization.Deserialize<List<Zametka>>();
            Zametka ZametkaChanged = new Zametka();
            List<Zametka> ZametkaUpdated = new List<Zametka>();
            DateTime dates = Convert.ToDateTime(DatePicker.Text);
            foreach (var item in reader) {
                if ((item.Name != TextBoxName.Text && item.Description == TextBoxDescription.Text) || (item.Name == TextBoxName.Text && item.Description != TextBoxDescription.Text))
                {
                    ZametkaChanged.Name = TextBoxName.Text;
                    ZametkaChanged.Description = TextBoxDescription.Text;
                    ZametkaChanged.data = dates;
                }
            }
            foreach (var item in reader)
            {
                if (item.Name == ZametkaChanged.Name || item.Description == ZametkaChanged.Description) { }
                else
                {
                    ZametkaUpdated.Add(item);
                }
            }
            ZametkaUpdated.Add(ZametkaChanged);
            deserialization.Serialize(ZametkaUpdated);
            TextBoxName.Text = "";
            TextBoxDescription.Text = "";

            Read();
        }
        public void Delete()
        {
            List<Zametka> reader = deserialization.Deserialize<List<Zametka>>();
            Zametka ZametkaChanged = new Zametka();

            List<Zametka> ZametkaDeleted = new List<Zametka>();
            foreach (var item in reader) {
                if (item.Name == TextBoxName.Text)
                {
                    ZametkaChanged.Name = item.Name;
                    ZametkaChanged.Description = item.Description;
                    ZametkaChanged.data = item.data;
                }
            }
            foreach (var item in reader) {
                if (item.Description == ZametkaChanged.Description) { }
                else
                {
                    ZametkaDeleted.Add(item);
                }
            }
            deserialization.Serialize(ZametkaDeleted);
            Read();

        }
    }
}