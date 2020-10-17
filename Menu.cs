using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace RestaurantPrg
{
    public class Menu : INotifyPropertyChanged
    {
        private int quantity;
        public string Name { get; set; }
        public double Price { get; set; }
        public string FoodType { get; set; }
        //  public int Quantity { get; set; }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public int Quantity
        {
            get { return quantity; }
            set
            {

                quantity = value;
                OnPropertyChanged("Quantity");

            }
        }


    }
    
}
