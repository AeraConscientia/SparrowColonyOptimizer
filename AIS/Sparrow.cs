using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIS
{
    public class Sparrow
    {
        public Vector coords;
        public double fitness = 0;

        public Sparrow(int dim)
        {
            coords = new Vector(dim);

            for (int i = 0; i < dim; i++)
                coords[i] = 0;
            fitness = 0;
        }
    }
}
