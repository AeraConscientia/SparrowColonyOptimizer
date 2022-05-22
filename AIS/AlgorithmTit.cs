using System;
using System.Collections.Generic;
using System.Linq;

namespace AIS
{
    public class AlgorithmTit
    {
        //Tit Alg 2.0
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
        public List<Tit> I = new List<Tit>();                       //Текущее множество синиц
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
            //Шаг 1.1
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
            double r = 1;

            Initilize();
            InitalPopulationGeneration();       //Шаг 1.2

            for (int p = 0; p < P; p++)
            {
                //Диффузионный поиск со скачками
                int k = 0;                      //Шаг 2.1
                do
                {
                    I = I.OrderBy(t => t.fitness).ToList();     //Шаг 2.2
                    best = I[0];                                //x^1,k
                    ProcessInfoAboutFlock();                    //Шаг 2.3
                    SolveStohasticDiffEq();                     //Шаг 2.4-2.6

                    if ((k < K) && (Math.Pow(r, k) < eps))                          //Шаг 2.7
                    {
                        Pool.Add(memory.OrderBy(t => t.fitness).ToList()[0]);       //Шаг 2.7 K=k
                        break;
                    }

                    r *= gamma;
                    ++k;

                    // >>>>> Положение Вожака       Шаг 2.8
                    Vector bestCoords = new Vector(best.coords.dim);
                    for (int i = 0; i < best.coords.dim; i++)
                        bestCoords[i] = best.coords[i];

                    best.coords += (alpha / (k + 1)) * Levy();                      
                    for (int j = 0; j < dim; j++)
                    {
                        if ((best.coords[j] < D[j, 0]) || (best.coords[j] > D[j, 1]))
                        {
                            int NumTries = 0;
                            double tmp = 0;
                            do
                            {
                                tmp = (j % 2 == 0) ? LeviX() : LeviY();
                                tmp *= (alpha / (k + 1));
                                tmp += bestCoords[j];
                                ++NumTries;
                            } while (((tmp < D[j, 0]) || (tmp > D[j, 1])) && (NumTries <= 10));

                            best.coords[j] = (NumTries > 10) ? ((D[j, 1] - D[j, 0]) * random.NextDouble() + D[j, 0]) : tmp;
                        }
                    }
                    best.fitness = Function.function(best.coords[0], best.coords[1],f);
                    I[0] = best;            //?
                    // <<<<<< Положение Вожака
                    
                    //Шаг 2.9
                    for (int i = 1; i < NP; i++)
                    {
                        for (int j = 0; j < dim; j++)
                        {
                            double val = best.coords[j] - 0.5*(D[j,1] + D[j,0]) + r * ((D[j, 1] - D[j, 0]) * random.NextDouble() + D[j, 0]);

                            if (val < D[j, 0]) 
                                val = (best.coords[j] - D[j, 0]) * random.NextDouble() + D[j, 0];

                            if (val > D[j, 1])
                                val = (D[j, 1] - best.coords[j]) * random.NextDouble() + best.coords[j];

                            I[i].coords[j] = val;
                        }
                        I[i].fitness = Function.function(I[i].coords[0], I[i].coords[1], f);
                    }
                } while (true);


                r = Math.Pow(eta, p);           //Шаг 3
                memory = new List<Tit>();

                I[0] = Pool.OrderBy(t => t.fitness).ToList()[0];
                I[0].fitness = Function.function(I[0].coords[0], I[0].coords[1], f);

                for (int i = 1; i < NP; i++)
                {
                    for (int j = 0; j < dim; j++)
                    {
                        double val = I[0].coords[j] - 0.5*(D[j,1] + D[j,0]) + r * ((D[j, 1] - D[j, 0]) * random.NextDouble() + D[j, 0]);

                        if (val < D[j, 0])
                            val = (best.coords[j] - D[j, 0]) * random.NextDouble() + D[j, 0];

                        if (val > D[j, 1])
                            val = (D[j, 1] - best.coords[j]) * random.NextDouble() + best.coords[j];

                        I[i].coords[j] = val;
                    }

                    I[i].fitness = Function.function(I[i].coords[0], I[i].coords[1], f);
                }
            }

            return Pool.OrderBy(t => t.fitness).ToList()[0];        //Шаг 4
        }

        public void Initilize()
        {
            dim = 2;
            random = new Random();
            memory = new List<Tit>();
        }

        private bool outOfBorders(Tit tit)
        {
            for (int i = 0; i < dim; i++)
            {
                if (tit.coords[i] < D[i, 0])
                    return true;

                if (tit.coords[i] > D[i, 1])
                    return true;
            }
            return false;
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
                if (I[j].fitness < I[j].best.fitness)
                    I[j].best = I[j];

                FindLocalBest(I[j]);
            }
        }


        private void SolveStohasticDiffEq()
        {
            for (int j = 1; j < NP; j++)
            {
                Tit current_tit = I[j];

                List<Tit> search = new List<Tit>(); //список всех скачков
                search.Add(I[j]);        //x^j,k (0)
                for (int l = 0; l < L; l++)
                {
                    Tit new_tit = new Tit(I[j].coords.dim);

                    double r1 = random.NextDouble();
                    double r2 = random.NextDouble();
                    double r3 = random.NextDouble();
                    double alpha1 = random.NextDouble();
                    double alpha2 = random.NextDouble();
                    double beta = random.NextDouble();
                    double ksi = Math.Sqrt(-2 * Math.Log(alpha1)) * Math.Cos(2 * Math.PI * alpha2);

                    Vector f = c1 * r1 * (best.coords - current_tit.coords);
                    Vector sigma = c2 * r2 * (I[j].best.coords - current_tit.coords) + c3 * r3 * (I[j].local_best.coords - current_tit.coords);

                    new_tit.coords = current_tit.coords + h * f + Math.Sqrt(h) * sigma * ksi;
                    new_tit.fitness = Function.function(new_tit.coords[0], new_tit.coords[1], this.f);

                    if (beta < mu * h)
                    {
                        Vector Thetta = new Vector(I[j].coords.dim);
                        for (int i = 0; i < I[j].coords.dim; i++)
                        {
                            double delta_i = Math.Min(D[i, 1] - new_tit.coords[i], new_tit.coords[i] - D[i, 0]);
                            Thetta[i] = random.NextDouble() * 2 * delta_i - delta_i;
                        }

                        new_tit.coords += Thetta;
                        new_tit.fitness = Function.function(new_tit.coords[0], new_tit.coords[1], this.f);
                    }

                    //проверка выхода за границы (оставить по алгоритму)
                    for (int i = 0; i < dim; i++)
                    {
                        if (new_tit.coords[i] < D[i, 0])
                            new_tit.coords[i] = D[i, 0];

                        if (new_tit.coords[i] > D[i, 1])
                            new_tit.coords[i] = D[i, 1];
                    }

                    search.Add(new_tit);   //x^j,k (l)
                    current_tit = new_tit;
                }

                search = search.OrderBy(t => t.fitness).ToList();
                search_tits.Add(search[0]);
            }
            
            //>>>>>>>>Шаг 2.6
            search_tits.Add(best);
            search_tits = search_tits.OrderBy(t => t.fitness).ToList();
            memory.Add(search_tits[0]);
            best = search_tits[0];      
            //<<<<<<<<
        }

        //Шаг 2.3   +
        public void FindLocalBest(Tit tit)
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

        //+
        public void InitalPopulationGeneration()
        {
            for (int i = 0; i < NP; i++)
            {
                Tit Agent = new Tit(dim);
                for (int j = 0; j < dim; j++)
                {
                    double val = random.NextDouble();
                    val = (D[j, 1] - D[j, 0]) * val + D[j, 0];
                    Agent.coords[j] = val;
                }

                Agent.fitness = Function.function(Agent.coords[0], Agent.coords[1], f);
                I.Add(Agent);
            }
        }
    }
}
