using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace зад3
{
    public class Tovar
    {
        protected string name;
        protected double price;
        protected int quantity;
      
        

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public Tovar (string name, double price, int quantity)
        {
            this.name = name;
            this.price = price;
            this.quantity = quantity;
        }

        public double Q ()
        {
            return price / quantity;
        }

        public virtual double Qp (int currentYear, int releaseYear)
        {
            return Q( ) + 0.5 * (currentYear - releaseYear);
        }

        public string GetInfo ()
        {
            int yearysterday = DateTime.Now.Year;
            return $"Имя: {name}, Цена: {price}, Количество: {quantity}, Качество: {Math.Round(Q( ),3)},Текущий год {yearysterday}";
        }

        public static void AddProduct (ICollection<Tovar> collection, Tovar product)
        {
            collection.Add(product);
        }

        public static void RemoveProduct (ICollection<Tovar> collection, Tovar product)
        {
            collection.Remove(product);
        }
    }
}
