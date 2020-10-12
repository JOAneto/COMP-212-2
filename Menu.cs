using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantPrg
{
    public class Menu
    {
        public List<string> app  { get; set; }
        public List<string> des { get; set; }
        public List<string> main { get; set; }
        public List<string> bev { get; set; }
        public Menu() {
            app = new List<string>()
            {
                "None", "Buffalo Wings", "Buffalo Fingers", "Potato Skins", "Nachos", "Mushroom Caos", "Shrimp Cocktail", "Chips & Salsa"
            };
            des = new List<string>()
            {
                "None", "Apple Pie", "Sundae", "Carrot Cake", "Mud Pie", "Apple Crisp"
            };
            main = new List<string>() {
                "None", "Seafood Alfredo", "Chicken Alfredo", "Chicken Picatta", "Turkey Club", "Lobster Pie", "Prime Rib", "Shrimp Scampi", "Turkey Dinner", "Stuffed Chicken"
            };
            bev = new List<string>()
            {
                "None", "Soda", "Tea", "Coffee", "Mineral Water", "Juice", "Milk"
            };
        }   
    }
}
