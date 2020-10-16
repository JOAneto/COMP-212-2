using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Caliburn.Micro;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace RestaurantPrg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Menu> foodMenu;
        public ObservableCollection<Menu> billMenu = new ObservableCollection<Menu>() { };
        public double Total = 0;
        public double tax = 0.13;
        private const string COLLEGE_URL = "https://www.centennialcollege.ca/";
        enum FoodTypes
        {
            Appetizer,
            MainCourse,
            Beverage,
            Dessert
        }

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            Bill.ItemsSource = billMenu;
            
        }

        public void LoadData()
        {
            // ** Load menu list to ObservableCollection
            foodMenu = new ObservableCollection<Menu>()
            {
                new Menu() { Name = "Buffalo Wings", Price = 5.95, FoodType = "Appetizer", Quantity =1 },
                new Menu() { Name = "Buffalo Fingers", Price = 6.95, FoodType = "Appetizer", Quantity =1 },
                new Menu() { Name = "Potato Skins", Price = 8.95, FoodType = "Appetizer", Quantity =1 },
                new Menu() { Name = "Nachos", Price = 8.95, FoodType = "Appetizer", Quantity =1 },
                new Menu() { Name = "Mushroom Caps", Price = 10.95, FoodType = "Appetizer", Quantity =1 },
                new Menu() { Name = "Shrimp Cocktail", Price = 12.95, FoodType = "Appetizer", Quantity =1 },
                new Menu() { Name = "Chips and Salsa", Price = 6.95, FoodType = "Appetizer", Quantity =1 },

                new Menu() { Name = "Seafood Alfredo", Price = 15.95, FoodType = "MainCourse", Quantity =1 },
                new Menu() { Name = "Chicken Alfredo", Price = 13.95, FoodType = "MainCourse", Quantity =1 },
                new Menu() { Name = "Chicken Picatta", Price = 13.95, FoodType = "MainCourse", Quantity =1 },
                new Menu() { Name = "Turkey Club", Price = 11.95, FoodType = "MainCourse", Quantity =1 },
                new Menu() { Name = "Lobster Pie", Price = 19.95, FoodType = "MainCourse", Quantity =1 },
                new Menu() { Name = "Prime Rib", Price = 20.95, FoodType = "MainCourse", Quantity =1 },
                new Menu() { Name = "Shrimp Scampi", Price = 18.95, FoodType = "MainCourse", Quantity =1 },
                new Menu() { Name = "Turkey Dinner", Price = 13.95, FoodType = "MainCourse", Quantity =1 },
                new Menu() { Name = "Stuffed Chicken", Price = 14.95, FoodType = "MainCourse", Quantity =1 },

                new Menu() { Name = "Soda", Price = 1.95, FoodType = "Beverage", Quantity =1 },
                new Menu() { Name = "Tea", Price = 1.50, FoodType = "Beverage", Quantity =1 },
                new Menu() { Name = "Coffee", Price = 1.25, FoodType = "Beverage", Quantity =1 },
                new Menu() { Name = "Mineral Water", Price = 2.95, FoodType = "Beverage", Quantity =1 },
                new Menu() { Name = "Juice", Price = 2.50, FoodType = "Beverage", Quantity =1 },
                new Menu() { Name = "Milk", Price = 1.50, FoodType = "Beverage", Quantity =1 },

                new Menu() { Name = "Apple Pie", Price = 5.95, FoodType = "Dessert", Quantity =1 },
                new Menu() { Name = "Sundae", Price = 3.95, FoodType = "Dessert", Quantity =1 },
                new Menu() { Name = "Carrot Cake", Price = 5.95, FoodType = "Dessert", Quantity =1 },
                new Menu() { Name = "Mud Pie", Price = 4.95, FoodType = "Dessert", Quantity =1 },
                new Menu() { Name = "Apple Crisp", Price = 5.95, FoodType = "Dessert", Quantity =1 }
            };
            // ** Load Appetizer list
            this.LoadAppetizers(foodMenu, Enum.GetName(typeof(FoodTypes), 0));

            // ** Load Main Course list
            this.LoadMainCourses(foodMenu, Enum.GetName(typeof(FoodTypes), 1));

            // ** Load Beverage list
            this.LoadBeverages(foodMenu, Enum.GetName(typeof(FoodTypes), 2));

            // ** Load Dessert list
            this.LoadDesserts(foodMenu, Enum.GetName(typeof(FoodTypes), 3));

        }
        private void LoadAppetizers(ObservableCollection<Menu> foodMenu, string foodType)
        {
            app.Items.Add("None");
            app.SelectedIndex = 0;

            foreach (Menu f in foodMenu.Where(fm => fm.FoodType == foodType))
            {
                app.Items.Add(f.Name);
            }
        }
        private void LoadMainCourses(ObservableCollection<Menu> foodMenu, string foodType)
        {
            main.Items.Add("None");
            main.SelectedIndex = 0;

            foreach (Menu f in foodMenu.Where(fm => fm.FoodType == foodType))
            {
                main.Items.Add(f.Name);
            }
        }
        private void LoadBeverages(ObservableCollection<Menu> foodMenu, string foodType)
        {
            bev.Items.Add("None");
            bev.SelectedIndex = 0;

            foreach (Menu f in foodMenu.Where(fm => fm.FoodType == foodType))
            {
                bev.Items.Add(f.Name);
            }
        }
        private void LoadDesserts(ObservableCollection<Menu> foodMenu, string foodType)
        {
            des.Items.Add("None");
            des.SelectedIndex = 0;

            foreach (Menu f in foodMenu.Where(fm => fm.FoodType == foodType))
            {
                des.Items.Add(f.Name);
            }
        }
        public void handleTotal(Menu input)
        {
            double Tax = Math.Round(((Total - (Total * tax)) + (input.Price * input.Quantity)) * tax, 2);
            Total = Math.Round(((Total - (Total*tax)) + (input.Price * input.Quantity)) + (((Total - (Total * tax)) + (input.Price * input.Quantity))*tax), 2);
            total.Text = "$" + Total;
            taxText.Text = "$" + Tax;
        }
        /*public void handleTotal(Menu input, double qn)
        {
            Total = Math.Round((Total / tax) + (((input.Price * qn) * tax) + (input.Price*qn)), 2);
            total.Text = "$" + Total;
        }*/
        private void App_DropDownClosed(object sender, EventArgs e)
        {
            if (app.Text != "None")
            {
                Menu price = foodMenu.FirstOrDefault(r => r.Name == app.Text);
                billMenu.Add(price);
                handleTotal(price);
            }
        }

        private void Des_DropDownClosed(object sender, EventArgs e)
        {
            if (des.Text != "None")
            {
                Menu price = foodMenu.FirstOrDefault(r => r.Name == des.Text);
                billMenu.Add(price);
                handleTotal(price);
            }
        }

        private void Main_DropDownClosed(object sender, EventArgs e)
        {
            if (main.Text != "None")
            {
                Menu price = foodMenu.FirstOrDefault(r => r.Name == main.Text);
                billMenu.Add(price);
                handleTotal(price);
            }
        }

        private void Bev_DropDownClosed(object sender, EventArgs e)
        {
            if (bev.Text != "None")
            {
                Menu price = foodMenu.FirstOrDefault(r => r.Name == bev.Text);
                billMenu.Add(price);
                handleTotal(price);
            }
        }

        private void Link_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(COLLEGE_URL);
        }
    }
}
