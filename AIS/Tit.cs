using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIS
{
    public class Tit
    {
        public Vector coords;
        public double fitness = 0;
        public Tit best;
        public Tit local_best;

        public Tit(int dim)
        {
            best = this;
            local_best = this;
            coords = new Vector(dim);

            for (int i = 0; i < dim; i++)
                coords[i] = 0;
            fitness = 0;
        }
    }
}
