using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RestaurantPrg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Menu> foodMenu;
        private ObservableCollection<Menu> dataGrid = new ObservableCollection<Menu>();
        public double Total = 0;
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
            Bill.ItemsSource = dataGrid;
            Bill.CanUserAddRows = false;

        }

        public void LoadData()
        {
            // ** Load menu list to ObservableCollection
            foodMenu = new ObservableCollection<Menu>()
            {
                new Menu() { Name = "Buffalo Wings", Price = 5.95, FoodType = "Appetizer",  },
                new Menu() { Name = "Buffalo Fingers", Price = 6.95, FoodType = "Appetizer" },
                new Menu() { Name = "Potato Skins", Price = 8.95, FoodType = "Appetizer" },
                new Menu() { Name = "Nachos", Price = 8.95, FoodType = "Appetizer" },
                new Menu() { Name = "Mushroom Caps", Price = 10.95, FoodType = "Appetizer" },
                new Menu() { Name = "Shrimp Cocktail", Price = 12.95, FoodType = "Appetizer" },
                new Menu() { Name = "Chips and Salsa", Price = 6.95, FoodType = "Appetizer" },

                new Menu() { Name = "Seafood Alfredo", Price = 15.95, FoodType = "MainCourse" },
                new Menu() { Name = "Chicken Alfredo", Price = 13.95, FoodType = "MainCourse" },
                new Menu() { Name = "Chicken Picatta", Price = 13.95, FoodType = "MainCourse" },
                new Menu() { Name = "Turkey Club", Price = 11.95, FoodType = "MainCourse" },
                new Menu() { Name = "Lobster Pie", Price = 19.95, FoodType = "MainCourse" },
                new Menu() { Name = "Prime Rib", Price = 20.95, FoodType = "MainCourse" },
                new Menu() { Name = "Shrimp Scampi", Price = 18.95, FoodType = "MainCourse" },
                new Menu() { Name = "Turkey Dinner", Price = 13.95, FoodType = "MainCourse" },
                new Menu() { Name = "Stuffed Chicken", Price = 14.95, FoodType = "MainCourse" },

                new Menu() { Name = "Soda", Price = 1.95, FoodType = "Beverage" },
                new Menu() { Name = "Tea", Price = 1.50, FoodType = "Beverage" },
                new Menu() { Name = "Coffee", Price = 1.25, FoodType = "Beverage" },
                new Menu() { Name = "Mineral Water", Price = 2.95, FoodType = "Beverage" },
                new Menu() { Name = "Juice", Price = 2.50, FoodType = "Beverage" },
                new Menu() { Name = "Milk", Price = 1.50, FoodType = "Beverage" },

                new Menu() { Name = "Apple Pie", Price = 5.95, FoodType = "Dessert" },
                new Menu() { Name = "Sundae", Price = 3.95, FoodType = "Dessert" },
                new Menu() { Name = "Carrot Cake", Price = 5.95, FoodType = "Dessert" },
                new Menu() { Name = "Mud Pie", Price = 4.95, FoodType = "Dessert" },
                new Menu() { Name = "Apple Crisp", Price = 5.95, FoodType = "Dessert" }
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
            Total = Total + input.Price;
            total.Text = "$" + Total;
        }
        private void App_DropDownClosed(object sender, EventArgs e)
        {
            Menu price = foodMenu.FirstOrDefault(r => r.Name == app.Text);

            Console.WriteLine(dataGrid.Any(p => p.Name == app.Text));

            if (dataGrid.Any(p => p.Name == app.Text))
            {
                Console.WriteLine("here");
                Menu found = dataGrid.FirstOrDefault(x => x.Name == app.Text);
                int i = dataGrid.IndexOf(found);
                dataGrid[i].Quantity++;
                Console.WriteLine(dataGrid[i].Quantity);

            }
            else
            {
                dataGrid.Add(new Menu() { Name = app.Text, FoodType = "Appetizer", Price = price.Price, Quantity = 1 });
            }
            handleTotal(price);
        }
        // <DataGrid.Columns>
        //    <DataGridTextColumn Header = "Name" Width="auto" Binding="{Binding Name}"/>
        //   <DataGridTextColumn Header = "Price" Width="auto" Binding="{Binding Price}"/>
        //     <DataGridTextColumn Header = "Quantity" Width="auto" Binding="{Binding Quantity}" />
        //    <DataGridTextColumn Header = "Category" Width="auto" Binding="{Binding FoodType}" />
        //  </DataGrid.Columns>
        private void Des_DropDownClosed(object sender, EventArgs e)
        {
            Menu price = foodMenu.FirstOrDefault(r => r.Name == des.Text);
            // Bill.Items.Add(new Menu() { Name = des.Text, FoodType = "Dessert", Price = price.Price, Quantity = 1 });
            if (dataGrid.Any(p => p.Name == des.Text))
            {
                Console.WriteLine("here");
                Menu found = dataGrid.FirstOrDefault(x => x.Name == des.Text);
                int i = dataGrid.IndexOf(found);
                dataGrid[i].Quantity++;
                Console.WriteLine(dataGrid[i].Quantity);

            }
            else
            {
                dataGrid.Add(new Menu() { Name = des.Text, FoodType = "Dessert", Price = price.Price, Quantity = 1 });

            }
            handleTotal(price);
        }

        private void Main_DropDownClosed(object sender, EventArgs e)
        {
            Menu price = foodMenu.FirstOrDefault(r => r.Name == main.Text);
            //Bill.Items.Add(new Menu() { Name = main.Text, FoodType = "Main Course", Price = price.Price, Quantity = 1 });
            Console.WriteLine(dataGrid.Any(p => p.Name == main.Text));

            if (dataGrid.Any(p => p.Name == main.Text))
            {

                Menu found = dataGrid.FirstOrDefault(x => x.Name == main.Text);
                int i = dataGrid.IndexOf(found);
                dataGrid[i].Quantity++;
                Console.WriteLine(dataGrid[i].Quantity);

            }
            else
            {
                dataGrid.Add(new Menu() { Name = main.Text, FoodType = "Main Course", Price = price.Price, Quantity = 1 });
            }
            handleTotal(price);
        }

        private void Bev_DropDownClosed(object sender, EventArgs e)
        {
            Menu price = foodMenu.FirstOrDefault(r => r.Name == bev.Text);
            // Bill.Items.Add(new Menu() { Name = bev.Text, FoodType = "Beverage", Price = price.Price, Quantity = 1 });
            Console.WriteLine(dataGrid.Any(p => p.Name == bev.Text));

            if (dataGrid.Any(p => p.Name == bev.Text))
            {
                Console.WriteLine("here");
                Menu found = dataGrid.FirstOrDefault(x => x.Name == bev.Text);
                int i = dataGrid.IndexOf(found);
                dataGrid[i].Quantity++;
                Console.WriteLine(dataGrid[i].Quantity);

            }
            else
            {

                dataGrid.Add(new Menu() { Name = bev.Text, FoodType = "Beverage", Price = price.Price, Quantity = 1 });
            }
            handleTotal(price);
        }

        private void Bill_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(Bill.SelectedCells);
        }

        private void main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.centennialcollege.ca");
        }
    }
}
