using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace зад3
{
    public class TovarNaSkalde : Tovar
    {
        private int releaseYear;     
        
        public int ReleaseYear
        {
            get { return releaseYear; }
            set { releaseYear = value; }
        }

        public TovarNaSkalde (string name, double price, int quantity, int releaseYear)
            : base(name, price, quantity)
        {
            this.releaseYear = releaseYear;
        }

        public override double Qp (int currentYear, int r)
        {
            return base.Qp(currentYear, r) + 0.5 * (currentYear - releaseYear);
        }

        public string GetInfoWithYear ()
        {
            int yearysterday = DateTime.Now.Year;
            return $"Имя: {name}, Цена: {price}, Количество: {quantity}, Качество: {Math.Round(Q( ),3)}, Год выпуска: {releaseYear}, Качество с указанием года: {Math.Round(Qp(2023, releaseYear),3)}, Текущий год {yearysterday}";
        }

    }
}
