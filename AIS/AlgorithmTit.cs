using System;
using System.Collections.Generic;
using System.Linq;

namespace AIS
{
    public class AlgorithmTit
    {
        public int NP;
        public double alpha;
        public double gamma;
        public double lambda;
        public double eta;
        public double rho;
        public double c1, c2, c3;
        public double K;
        public double h;
        public double L;
        public double T;
        public double P;
        public double mu;
        public double eps;
        public Tit best;
        public List<Tit> I = new List<Tit>();
        public List<Tit> search_tits = new List<Tit> ();            //Массив лучших положений всех синиц после скачков (см. Шаг 2.6)
        public List<Tit> Pool = new List<Tit>();
        public List<Tit> memory;

        private int dim;
        private int f;
        public double[,] D;

        Random random;

        public Tit StartAlg(int NP, double alpha, double gamma, double lambda, double eta, double rho, double c1, double c2, double c3,
            double K, double h, double L, double P, double mu, double eps, double[,] D, int f) 
        {
            this.NP = NP;
            this.alpha = alpha;
            this.gamma = gamma;
            this.lambda = lambda;
            this.eta = eta;
            this.rho = rho;
            this.c1 = c1;
            this.c2 = c2;
            this.c3 = c3;
            this.K = K;
            this.h = h;
            this.L = L;
            this.P = P;
            this.mu = mu;
            this.eps = eps;
            this.D = D;
            this.f = f;

            dim = 2;
            random = new Random();
            memory = new List<Tit>();

            InitalPopulationGeneration();       //Шаг 1.2
            double r = 1;

            for (int p = 0; p < P; p++)
            {
                //Диффузионный поиск со скачками
                int k = 0;              //Шаг 2.1

                do
                {
                    I = I.OrderBy(t => t.fitness).ToList();     //Шаг 2.2
                    best = I[0];                                //same
                    ProcessInfoAboutFlock();                    //Шаг 2.3-2.7

                    best.coords += (alpha / (k + 1)) * Levy();        //Шаг 2.8
                    best.fitness = function(best.coords[0],best.coords[1],f);

                    I[0] = best;

                    for (int i = 1; i < NP; i++)
                    {
                        double x = random.NextDouble();
                        double y = random.NextDouble();

                        x = best.coords[0] + Math.Pow(r, k) * ((Math.Abs(D[0, 0]) + Math.Abs(D[0, 1])) * x - Math.Abs(D[0, 0]));  //TODO: проверить!
                        y = best.coords[1] + Math.Pow(r, k) * ((Math.Abs(D[1, 0]) + Math.Abs(D[1, 1])) * y - Math.Abs(D[1, 0]));

                        I[i].coords[0] = x;
                        I[i].coords[1] = y;
                        I[i].fitness = function(x, y, f);

                        if (I[i].fitness < I[i].best.fitness)
                            I[i].best = I[i];
                    }

                    r *= gamma;
                    ++k;
                } while ((k < K) && (Math.Pow(r, k) < eps));

                Pool.Add(memory.OrderBy(t => t).ToList()[0]);       //2.10
                memory = new List<Tit>();

                r = Math.Pow(eta, p);           //Шаг 3

                I[0] = Pool.OrderBy(t => t.fitness).ToList()[0];
                I[0].fitness = function(I[0].coords[0], I[0].coords[1], f);
                for (int i = 1; i < NP; i++)
                {
                    double x = random.NextDouble();
                    double y = random.NextDouble();

                    x = I[0].coords[0] + r * ((Math.Abs(D[0, 0]) + Math.Abs(D[0, 1])) * x - Math.Abs(D[0, 0]));  //!!
                    y = I[0].coords[1] + r * ((Math.Abs(D[1, 0]) + Math.Abs(D[1, 1])) * y - Math.Abs(D[1, 0]));  //!!

                    I[i].coords[0] = x;
                    I[i].coords[1] = y;
                    I[i].fitness = function(x, y, f);

                    if (I[i].fitness < I[i].best.fitness)
                        I[i].best = I[i];
                }
            }

            return Pool.OrderBy(t => t.fitness).ToList()[0];        //Шаг 4
        }

        private Vector Levy()
        {
            Vector res = new Vector(dim);
            for (int i = 0; i < dim; i++) 
                res[i] = (i % 2 == 0) ? LeviX() : LeviY();

            return res;
        }

        /// <summary>Распределение Леви, координата х,  для худшей стаи</summary>
        public double LeviX()
        {
            double R1 = random.Next(Convert.ToInt32(0), Convert.ToInt32((D[0, 1] - D[0, 0]) * 100)) / 100f; // (0, b1-a1)
            double thetta1 = R1 * 2 * Math.PI;
            double L1 = Math.Pow(R1 + 0.0001f, -1 / lambda);

            double  x = L1 * Math.Sin(thetta1);
            return x;
        }

        /// <summary>Распределение Леви, координата y,  для худшей стаи</summary>
        public double LeviY()
        {
            double R2 = random.Next(Convert.ToInt32(0), Convert.ToInt32((D[1, 1] - D[1, 0]) * 100)) / 100f; // (0, b2-a2)
            double thetta2 = R2 * 2 * Math.PI;
            double L2 = Math.Pow(R2 + 0.0001f, -1 / lambda);   //!

            double y = L2 * Math.Cos(thetta2);
            return y;
        }

        private void ProcessInfoAboutFlock()
        {
            for (int j = 1; j < NP; j++)
            {
                FindLocalBest(I[j]);
                SolveStohasticDiffEq(I[j]);
            }
            search_tits.Add(best);
            search_tits = search_tits.OrderBy(t => t.fitness).ToList();
            memory.Add(search_tits[0]);
            best = search_tits[0];
        }

        private void SolveStohasticDiffEq(Tit tit)
        {
            Tit current_tit = tit;

            List<Tit> search = new List<Tit>(); //список всех скачков
            search.Add(tit);        //x^j,k (0)
            for (int l = 0; l < L; l++)
            {
                Tit new_tit = new Tit(tit.coords.dim);

                double r1 = random.NextDouble();
                double r2 = random.NextDouble();
                double r3 = random.NextDouble();
                double alpha1 = random.NextDouble();
                double alpha2 = random.NextDouble();
                double beta = random.NextDouble();
                double ksi = Math.Sqrt(-2 * Math.Log(alpha1)) * Math.Cos(2 * Math.PI * alpha2);

                Vector f = c1 * r1 * (best.coords - current_tit.coords);
                Vector sigma = c2 * r2 * (tit.best.coords - current_tit.coords) + c3 * r3 * (tit.local_best.coords - current_tit.coords);

                new_tit.coords = current_tit.coords + h * f + Math.Sqrt(h) * sigma * ksi;
                new_tit.fitness = function(new_tit.coords[0], new_tit.coords[1], this.f);

                if (beta < mu * h)
                {
                    Vector Thetta = new Vector(tit.coords.dim);
                    for (int i = 0; i < tit.coords.dim; i++)
                    {
                        double delta_i = Math.Min(D[i, 1] - new_tit.coords[i], new_tit.coords[i] - D[i, 0]);
                        Thetta[i] = random.NextDouble() * 2 * delta_i - delta_i;        
                    }
                    
                    new_tit.coords += Thetta;
                    new_tit.fitness = function(new_tit.coords[0], new_tit.coords[1], this.f);
                }

                search.Add(new_tit);   //x^j,k (l)
                current_tit = new_tit;
            }

            search = search.OrderBy(t => t.fitness).ToList();
            search_tits.Add(search[0]);
        }

        private void FindLocalBest(Tit tit)
        {
            foreach (Tit item in I)
            {
                bool ok = true;
                for (int i = 0; i < item.coords.dim; i++)
                {
                    if (Math.Abs(item.coords[i] - tit.coords[i]) > rho) 
                    {
                        ok = false;
                        break;
                    }
                }
                if (ok == true)
                    if (item.fitness < tit.local_best.fitness)
                        tit.local_best = item;
            }
        }

        public void InitalPopulationGeneration()
        {
            for (int i = 0; i < NP; i++)
            {
                double x = random.NextDouble();
                double y = random.NextDouble();

                x = ((Math.Abs(D[0, 0]) + Math.Abs(D[0, 1])) * x - Math.Abs(D[0, 0]));
                y = ((Math.Abs(D[1, 0]) + Math.Abs(D[1, 1])) * y - Math.Abs(D[1, 0]));

                Tit Agent = new Tit(dim);
                Agent.coords[0] = x;
                Agent.coords[1] = y;
                Agent.fitness = function(x, y, f);
                I.Add(Agent);
            }
        }

        private float function(double x1, double x2, int f)
        {
            float funct = 0;
            if (f == 0) // Швефель
            {
                funct = (float)(-(x1 * Math.Sin(Math.Sqrt(Math.Abs(x1))) + x2 * Math.Sin(Math.Sqrt(Math.Abs(x2)))));
            }
            else if (f == 1) // Мульти
                funct = (float)(-(x1 * Math.Sin(4 * Math.PI * x1) - x2 * Math.Sin(4 * Math.PI * x2 + Math.PI) + 1));
            else if (f == 2) // корневая
            {
                double[] c6 = Cpow(x1, x2, 6);
                funct = (float)(-1 / (1 + Math.Sqrt((c6[0] - 1) * (c6[0] - 1) + c6[1] * c6[1])));
            }
            else if (f == 3) // Шафер
                funct = (float)(-(0.5 - (Math.Pow(Math.Sin(Math.Sqrt(x1 * x1 + x2 * x2)), 2) - 0.5) / (1 + 0.001 * (x1 * x1 + x2 * x2))));
            else if (f == 4) // Растригин
            {
                funct = (float)(-(-20 + (-x1 * x1 + 10 * Math.Cos(2 * Math.PI * x1)) + (-x2 * x2 + 10 * Math.Cos(2 * Math.PI * x2))));
            }
            else if (f == 5) // Эклея
            {
                funct = (float)(-(-Math.E + 20 * Math.Exp(-0.2 * Math.Sqrt((x1 * x1 + x2 * x2) / 2)) + Math.Exp((Math.Cos(2 * Math.PI * x1) + Math.Cos(2 * Math.PI * x2)) / 2)));
            }
            else if (f == 6) // skin
            {
                funct = (float)(-(Math.Pow(Math.Cos(2 * x1 * x1) - 1.1, 2) + Math.Pow(Math.Sin(0.5 * x1) - 1.2, 2) - Math.Pow(Math.Cos(2 * x2 * x2) - 1.1, 2) + Math.Pow(Math.Sin(0.5 * x2) - 1.2, 2)));
            }
            else if (f == 7) //Trapfall
            {
                funct = (float)(-(-Math.Sqrt(Math.Abs(Math.Sin(Math.Sin(Math.Sqrt(Math.Abs(Math.Sin(x1 - 1))) + Math.Sqrt(Math.Abs(Math.Sin(x2 + 2))))))) + 1));
            }
            else if (f == 8) // Розенброк
            {
                funct = (float)(-(-(1 - x1) * (1 - x1) - 100 * (x2 - x1 * x1) * (x2 - x1 * x1)));
            }
            else if (f == 9) // Параболическая
            {
                funct = (float)(x1 * x1 + x2 * x2);
            }
            return funct;
        }
        private double[] Cpow(double x, double y, int p)
        {
            double[] Cp = new double[2];
            Cp[0] = x; Cp[1] = y;
            double x0 = 0;
            double y0 = 0;
            for (int i = 1; i < p; i++)
            {
                x0 = Cp[0] * x - Cp[1] * y;
                y0 = Cp[1] * x + Cp[0] * y;
                Cp[0] = x0; Cp[1] = y0;
            }
            return Cp;
        }
    }
}
