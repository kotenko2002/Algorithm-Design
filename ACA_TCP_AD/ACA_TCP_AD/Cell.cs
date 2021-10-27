using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ACA_TCP_AD
{
    public class Cell
    {
        public int CityNumber { get; set; }
        public Point Coordinates { get; set; }

        public Cell(int number, Point coordinates)
        {
            CityNumber = number;
            Coordinates = coordinates;
        }
    }
}
