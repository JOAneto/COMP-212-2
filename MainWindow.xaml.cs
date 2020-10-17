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
        public double Tax = 0;
        public double Subtotal = 0;
        public double tax = 0.1;
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
        public void HandleTotal(Menu input)
        {
            Tax = ((Total/1.1) + (input.Price * input.Quantity)) * tax;
            Subtotal = (Total/1.1) + (input.Price * input.Quantity);
            Total = Subtotal + Tax;
            total.Text = "$" + Math.Round(Total, 2);
            taxText.Text = "$" + Math.Round(Tax, 2);
            SubTot.Text = "$" + Math.Round(Subtotal, 2);
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
                if (billMenu.Any(p => p.Name == app.Text))
                {
                    Console.WriteLine("here");
                    Menu found = billMenu.FirstOrDefault(x => x.Name == app.Text);
                    int i = billMenu.IndexOf(found);
                    billMenu[i].Quantity++;
                    Console.WriteLine(billMenu[i].Quantity);

                }
                else
                {
                    billMenu.Add(new Menu() { Name = app.Text, FoodType = "Appetizer", Price = price.Price, Quantity = 1 });
                }
                HandleTotal(price);
                //   Menu price = foodMenu.FirstOrDefault(r => r.Name == app.Text);
                // billMenu.Add(price);


                // handleTotal(price);
            }
        }

        private void Des_DropDownClosed(object sender, EventArgs e)
        {
            Menu price = foodMenu.FirstOrDefault(r => r.Name == des.Text);

            if (des.Text != "None")
            {
                if (billMenu.Any(p => p.Name == des.Text))
                {
                    Console.WriteLine("here");
                    Menu found = billMenu.FirstOrDefault(x => x.Name == des.Text);
                    int i = billMenu.IndexOf(found);
                    billMenu[i].Quantity++;
                    Console.WriteLine(billMenu[i].Quantity);

                }
                else
                {
                    billMenu.Add(new Menu() { Name = des.Text, FoodType = "Dessert", Price = price.Price, Quantity = 1 });

                }
                HandleTotal(price);
            }
        }

        private void Main_DropDownClosed(object sender, EventArgs e)
        {
            Menu price = foodMenu.FirstOrDefault(r => r.Name == main.Text);

            if (main.Text != "None"){
                if (billMenu.Any(p => p.Name == main.Text))
                {

                    Menu found = billMenu.FirstOrDefault(x => x.Name == main.Text);
                    int i = billMenu.IndexOf(found);
                    billMenu[i].Quantity++;
                    Console.WriteLine(billMenu[i].Quantity);

                }
                else
                {
                    billMenu.Add(new Menu() { Name = main.Text, FoodType = "Main Course", Price = price.Price, Quantity = 1 });
                }
                HandleTotal(price);
            }
        }

        private void Bev_DropDownClosed(object sender, EventArgs e)
        {
            Menu price = foodMenu.FirstOrDefault(r => r.Name == bev.Text);

            if (bev.Text != "None")
            {

                if (billMenu.Any(p => p.Name == bev.Text))
                {
                    Console.WriteLine("here");
                    Menu found = billMenu.FirstOrDefault(x => x.Name == bev.Text);
                    int i = billMenu.IndexOf(found);
                    billMenu[i].Quantity++;
                    Console.WriteLine(billMenu[i].Quantity);

                }
                else
                {

                    billMenu.Add(new Menu() { Name = bev.Text, FoodType = "Beverage", Price = price.Price, Quantity = 1 });
                }
                HandleTotal(price);

            }
        }

        private void Link_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(COLLEGE_URL);
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            Total = 0;
            Tax = 0;
            Subtotal = 0;
            billMenu.Clear();
            total.Text = "$" + Total;
            taxText.Text = "$" + Tax;
            SubTot.Text = "$" + Subtotal;
        }

        private void Cb1_DropDownClosed(object sender, EventArgs e)
        {
            //Menu a = Bill.SelectedItem as Menu;
            //int b = Bill.Columns[2].GetCellContent(dataGridRow:);
            
        }
        public void HandleEdit(Menu input)
        {
            Tax = Subtotal * tax;
            Total = Subtotal + Tax;
            total.Text = "$" + Math.Round(Total, 2);
            taxText.Text = "$" + Math.Round(Tax, 2);
            SubTot.Text = "$" + Math.Round(Subtotal, 2);
        }

        private void x1_Click(object sender, RoutedEventArgs e)
        {
            Menu a = Bill.SelectedItem as Menu;
            a.Quantity++;
            Subtotal = Subtotal + a.Price;
            HandleEdit(a);

        }

        private void x2_Click(object sender, RoutedEventArgs e)
        {
            Menu a = Bill.SelectedItem as Menu;
            if (a.Quantity > 0)
            {
                a.Quantity--;
                Subtotal = Subtotal - a.Price;
                HandleEdit(a);

            }
            else if (a.Quantity == 0)
            {
                billMenu.Remove(a);
            }

        }
    }
}
