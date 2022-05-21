using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace AIS
{
    public partial class FormMain : Form
    {
        //Мое

        private AlgorithmTit algTit;

        private int MaxIteration = 0;
        private Tit resultBest;
        private double[,] obl = new double[2, 2];

        private const int NumOfStarts = 100;

        List<Vector> exactPoints = new List<Vector>();

        /// <summary>Размер популяции</summary>
        int NP;
        /// <summary>шаг при передвижении вожака стаи</summary>
        double alpha;
        /// <summary>параметр сокращения множества поиска</summary>
        double gamma;
        /// <summary>параметр распределения  Леви</summary>
        double lambda;
        /// <summary>параметр восстановления множества поиска</summary>
        double eta;
        /// <summary>величина радиуса, определяющая окрестность члена стаи</summary>
        double rho;
        double c1;
        double c2;
        double c3;
        /// <summary>максимальное число записей в матрице памяти</summary>
        double K;
        /// <summary>шаг интегрирования дифференциального уравнения</summary>
        double h;
        /// <summary>максимальное число дискретных шагов</summary>
        double L;
        /// <summary>максимальное число проходов </summary>
        double P;
        /// <summary>параметр интенсивности скачков</summary>
        double mu;
        double eps;
        double[,] D;
        

        //Не мое
        private bool[] flines = new bool[8];
        private float k = 1;
        /// <summary>Константы для линий уровня. Тут - для минимума функции сделаны. Нужно переделать под максимум - умножить все коэффициенты на -1</summary>
        private float[] Ar = new float[8];
        private double[,] showobl = new double[2, 2];
        private bool flag = false;
        private bool flag2 = false;
        /// <summary>Точное значение функции в минимуме. Нужно переделать на максимум - домножить на -1</summary>
        private double exact = 0;

        public FormMain()
        {
            InitializeComponent();
            InitDataGridView();

            if (File.Exists("protocol.txt"))
                File.Delete("protocol.txt");

            FileStream fs = new FileStream("protocol.txt", FileMode.Append, FileAccess.Write);

            StreamWriter r = new StreamWriter(fs);
            r.Write($"+-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+------------+----------+\r\n" +
            $"|  Номер  |   Размер    | Gamma |  eta  |   Alpha  | lambda |  P  |  K  |  L  |   mu   |   h   |  с1  |  с2  |  с3  | rho  | Cреднее значение отклонения | Наименьшее значение | Среднеквадратическое |   Кол-во   |  Число   |\r\n" +
            $"| функции |  популяции  |       |       |          |        |     |     |     |        |       |      |      |      |      |     от точного решения      |      отклонения     |      отклонение      |  успехов   | запусков |\r\n" +
            $"+---------+-------------+-------+-------+----------+--------+-----+-----+-----+--------+-------+------+------+------+------+-----------------------------+---------------------+----------------------+------------+----------+\r\n");
            
            r.Close();
            fs.Close();

        }

        private void InitDataGridView()
        {
            dataGridView1.RowCount = 2;
            dataGridView1.Rows[0].Cells[0].Value = "x";
            dataGridView1.Rows[1].Cells[0].Value = "y";

            dataGridView2.RowCount = 13;
            dataGridView2.Rows[0].Cells[0].Value = "Размер популяции";
            dataGridView2.Rows[0].Cells[1].Value = 100;

            dataGridView2.Rows[1].Cells[0].Value = "ϒ"; 
            dataGridView2.Rows[1].Cells[1].Value = 0.75.ToString();

            dataGridView2.Rows[2].Cells[0].Value = "η";
            dataGridView2.Rows[2].Cells[1].Value = 0.9.ToString();

            dataGridView2.Rows[3].Cells[0].Value = "Радиус окрестности ро";
            dataGridView2.Rows[3].Cells[1].Value = 5;   

            dataGridView2.Rows[4].Cells[0].Value = "c3";
            dataGridView2.Rows[4].Cells[1].Value = 5;

            dataGridView2.Rows[5].Cells[0].Value = "Матрица памяти K";
            dataGridView2.Rows[5].Cells[1].Value = 10;

            dataGridView2.Rows[6].Cells[0].Value = "h";
            dataGridView2.Rows[6].Cells[1].Value = 0.1.ToString();

            dataGridView2.Rows[7].Cells[0].Value = "L";
            dataGridView2.Rows[7].Cells[1].Value = 10;

            dataGridView2.Rows[8].Cells[0].Value = "P";
            dataGridView2.Rows[8].Cells[1].Value = 30;

            dataGridView2.Rows[9].Cells[0].Value = "mu";
            dataGridView2.Rows[9].Cells[1].Value = 5;

            dataGridView2.Rows[10].Cells[0].Value = "P";
            dataGridView2.Rows[10].Cells[1].Value = 30;

            dataGridView2.Rows[11].Cells[0].Value = "µ";
            dataGridView2.Rows[11].Cells[1].Value = 5;

            dataGridView2.Rows[12].Cells[0].Value = "ε";
            dataGridView2.Rows[12].Cells[1].Value = 0.000000001.ToString();

            //СТАРОЕ

            dataGridView3.RowCount = 4;
            dataGridView3.Rows[0].Cells[0].Value = "x";
            dataGridView3.Rows[1].Cells[0].Value = "y";
            dataGridView3.Rows[2].Cells[0].Value = "f*";
            dataGridView3.Rows[3].Cells[0].Value = "Exact value of f";

            dataGridView4.RowCount = 2;
            dataGridView4.Rows[0].Cells[0].Value = "Лямбда";//"Параметр распределения";
            dataGridView4.Rows[0].Cells[1].Value = (1.5).ToString();

            dataGridView4.Rows[1].Cells[0].Value = "Альфа";
            dataGridView4.Rows[1].Cells[1].Value = (0.001).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                //создать начальную популяцию
                if ((comboBox1.SelectedIndex != -1) )
                {
                    int z = comboBox1.SelectedIndex;

                    // область определения
                    obl[0, 0] = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
                    obl[0, 1] = Convert.ToDouble(dataGridView1.Rows[0].Cells[2].Value);
                    obl[1, 0] = Convert.ToDouble(dataGridView1.Rows[1].Cells[1].Value);
                    obl[1, 1] = Convert.ToDouble(dataGridView1.Rows[1].Cells[2].Value);

                    NP       = Convert.ToInt32(dataGridView2.Rows[0].Cells[1].Value);
                    gamma    = Convert.ToDouble(dataGridView2.Rows[1].Cells[1].Value);
                    eta      = Convert.ToDouble(dataGridView2.Rows[2].Cells[1].Value);
                    rho      = Convert.ToDouble(dataGridView2.Rows[3].Cells[1].Value);
                    c1       = Convert.ToDouble(dataGridView2.Rows[4].Cells[1].Value);
                    c2       = Convert.ToDouble(dataGridView2.Rows[5].Cells[1].Value);
                    c3       = Convert.ToDouble(dataGridView2.Rows[6].Cells[1].Value);
                    K        = Convert.ToDouble(dataGridView2.Rows[7].Cells[1].Value);
                    h        = Convert.ToDouble(dataGridView2.Rows[8].Cells[1].Value);
                    L        = Convert.ToDouble(dataGridView2.Rows[9].Cells[1].Value);
                    P        = Convert.ToDouble(dataGridView2.Rows[10].Cells[1].Value);
                    mu       = Convert.ToDouble(dataGridView2.Rows[11].Cells[1].Value);

                    lambda   = Convert.ToDouble(dataGridView4.Rows[0].Cells[1].Value);
                    alpha    = Convert.ToDouble(dataGridView4.Rows[1].Cells[1].Value);

                    eps      = Convert.ToDouble(dataGridView2.Rows[12].Cells[1].Value);

                    algTit = new AlgorithmTit();

                    resultBest = algTit.StartAlg(NP, alpha, gamma, lambda, eta, rho, c1, c2, c3, K, h, L, P, mu, eps, obl, z);
                    flag2 = true;


                    //result = algPerch.StartAlg(population, MaxIteration, obl, z, param);
                    dataGridView3.Rows[0].Cells[1].Value = string.Format($"{resultBest.coords[0]:F8}");
                    dataGridView3.Rows[1].Cells[1].Value = string.Format($"{resultBest.coords[1]:F8}");
                    dataGridView3.Rows[2].Cells[1].Value = string.Format($"{resultBest.fitness:F8}");
                    dataGridView3.Rows[3].Cells[1].Value = string.Format($"{exact:F8}");
                    //flag2 = true;
                    pictureBox1.Refresh();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBox1.SelectedIndex != -1) )
            {
                buttonAnswer.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;

            }

            if (comboBox1.SelectedIndex == 0)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-500";
                dataGridView1.Rows[0].Cells[2].Value = "500";
                dataGridView1.Rows[1].Cells[1].Value = "-500";
                dataGridView1.Rows[1].Cells[2].Value = "500";
                exact = -837.9658;

                Ar[0] = 200;
                Ar[1] = 1;
                Ar[2] = -300;
                Ar[3] = -600;
                Ar[4] = -800;
                exactPoints.Add(new Vector(420.9687, 420.9687));
                flag = true;
                pictureBox2.Image = Properties.Resources.ШвефельМин;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-2";
                dataGridView1.Rows[0].Cells[2].Value = "2";
                dataGridView1.Rows[1].Cells[1].Value = "-2";
                dataGridView1.Rows[1].Cells[2].Value = "2";
                exact = -4.253888;

                exactPoints.Add(new Vector(-1.6288, -1.6288));
                exactPoints.Add(new Vector(1.6288, 1.6288));
                exactPoints.Add(new Vector(-1.6288, 1.6288));
                exactPoints.Add(new Vector(1.6288, -1.6288));

                Ar[0] = 0;
                Ar[1] = -1;
                Ar[2] = -2;
                Ar[3] = -3;
                Ar[4] = -4;
                flag = true;
                pictureBox2.Image = Properties.Resources.МультиМин;

            }
            else if (comboBox1.SelectedIndex == 2)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-2";
                dataGridView1.Rows[0].Cells[2].Value = "2";
                dataGridView1.Rows[1].Cells[1].Value = "-2";
                dataGridView1.Rows[1].Cells[2].Value = "2";
                exact = -1;

                exactPoints.Add(new Vector(0.5, -0.866));
                exactPoints.Add(new Vector(-0.5, 0.866));
                exactPoints.Add(new Vector(0.5, 0.866));
                exactPoints.Add(new Vector(-0.5, -0.866));
                exactPoints.Add(new Vector(1, 0));
                exactPoints.Add(new Vector(-1, 0));

                Ar[0] = -0.2F;
                Ar[1] = -0.45F;
                Ar[2] = -0.499999F;//0.5000001F;
                Ar[3] = -0.6F;
                Ar[4] = -0.9F;
                flag = true;
                pictureBox2.Image = Properties.Resources.КорневаяМин;

            }
            else if (comboBox1.SelectedIndex == 3)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-10";
                dataGridView1.Rows[0].Cells[2].Value = "10";
                dataGridView1.Rows[1].Cells[1].Value = "-10";
                dataGridView1.Rows[1].Cells[2].Value = "10";
                exact = -1;

                exactPoints.Add(new Vector(0, 0));

                Ar[0] = -0.2F;
                Ar[1] = -0.4F;
                Ar[2] = -0.6F;//0.5000001F;
                Ar[3] = -0.8F;
                Ar[4] = -0.99F;
                flag = true;
                pictureBox2.Image = Properties.Resources.ШаферМин;

            }
            else if (comboBox1.SelectedIndex == 4)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-5";
                dataGridView1.Rows[0].Cells[2].Value = "5";
                dataGridView1.Rows[1].Cells[1].Value = "-5";
                dataGridView1.Rows[1].Cells[2].Value = "5";
                exact = 0;

                exactPoints.Add(new Vector(0, 0));

                Ar[0] = 20F;
                Ar[1] = 10F;
                Ar[2] = -0F;//0.5000001F;
                Ar[3] = -10F;
                Ar[4] = -19F;
                flag = true;
                pictureBox2.Image = Properties.Resources.РастригинМин;

            }
            else if (comboBox1.SelectedIndex == 5)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-10";
                dataGridView1.Rows[0].Cells[2].Value = "10";
                dataGridView1.Rows[1].Cells[1].Value = "-10";
                dataGridView1.Rows[1].Cells[2].Value = "10";
                exact = -20;

                exactPoints.Add(new Vector(0, 0));

                Ar[0] = -4F;
                Ar[1] = -7F;
                Ar[2] = -10F;//0.5000001F;
                Ar[3] = -14F;
                Ar[4] = -19F;
                flag = true;
                pictureBox2.Image = Properties.Resources.ЭклеяМин;
            }
            else if (comboBox1.SelectedIndex == 6)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-5";
                dataGridView1.Rows[0].Cells[2].Value = "5";
                dataGridView1.Rows[1].Cells[1].Value = "-5";
                dataGridView1.Rows[1].Cells[2].Value = "5";
                exact = -14.060606;

                exactPoints.Add(new Vector(-3.3157, -3.0725));

                Ar[0] = -2F;
                Ar[1] = -8F;
                Ar[2] = -10F;//0.5000001F;
                Ar[3] = -12F;
                Ar[4] = -14F;
                flag = true;
                pictureBox2.Image = Properties.Resources.SkinMin;
            }
            else if (comboBox1.SelectedIndex == 7)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-5";
                dataGridView1.Rows[0].Cells[2].Value = "5";
                dataGridView1.Rows[1].Cells[1].Value = "-5";
                dataGridView1.Rows[1].Cells[2].Value = "5";
                exact = -1;

                exactPoints.Add(new Vector(1, -2));

                Ar[0] = -0.1F;
                Ar[1] = -0.15F;
                Ar[2] = -0.2F;//0.5000001F;
                Ar[3] = -0.3F;
                Ar[4] = -0.5F;
                flag = true;
                pictureBox2.Image = Properties.Resources.TrapfallMin;
            }
            else if (comboBox1.SelectedIndex == 8)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-3";
                dataGridView1.Rows[0].Cells[2].Value = "3";
                dataGridView1.Rows[1].Cells[1].Value = "-1";
                dataGridView1.Rows[1].Cells[2].Value = "5";
                exact = 0;

                exactPoints.Add(new Vector(1, 1));

                Ar[0] = 350F;
                Ar[1] = 180F;
                Ar[2] = 30F;//0.5000001F;
                Ar[3] = 4F;
                Ar[4] = 0.5F;
                flag = true;
                pictureBox2.Image = Properties.Resources.РозенброкМин;
            }
            else if (comboBox1.SelectedIndex == 9)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-5";
                dataGridView1.Rows[0].Cells[2].Value = "5";
                dataGridView1.Rows[1].Cells[1].Value = "-5";
                dataGridView1.Rows[1].Cells[2].Value = "5";
                exact = 0;

                exactPoints.Add(new Vector(0, 0));

                Ar[0] = 7F;
                Ar[1] = 4F;
                Ar[2] = 2F;//0.5000001F;
                Ar[3] = 0.8F;
                Ar[4] = 0.1F;
                flag = true;
                pictureBox2.Image = Properties.Resources.ПараболическаяМин;
            }
            Ar[5] = 0;
            Ar[6] = 0;
            Ar[7] = 0;
            for (int i = 0; i < 5; i++)
                flines[i] = true;
            flines[5] = false;
            flines[6] = false;
            flines[7] = false;

            flag2 = false;

            showobl[0, 0] = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
            showobl[0, 1] = Convert.ToDouble(dataGridView1.Rows[0].Cells[2].Value);
            showobl[1, 0] = Convert.ToDouble(dataGridView1.Rows[1].Cells[1].Value);
            showobl[1, 1] = Convert.ToDouble(dataGridView1.Rows[1].Cells[2].Value);

            pictureBox1.Refresh();
            dataGridView1.Refresh();
        }

        /// <summary>Отрисовка линий уровня. НЕ ТРОГАТЬ</summary>
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            float w = pictureBox1.Width;
            float h = pictureBox1.Height;
            float x0 = w/2;
            float y0 = h/2;
            float a = 30;
            
            List<PointF> points = new List<PointF>();

            Pen p1 = new Pen(Color.PaleGreen, 1);
            Pen p2 = new Pen(Color.GreenYellow, 1);
            Pen p3 = new Pen(Color.YellowGreen, 1);
            Pen p4 = new Pen(Color.Yellow, 1);
            Pen p5 = new Pen(Color.Orange, 1);
            Pen p6 = new Pen(Color.OrangeRed, 1);
            Pen p7 = new Pen(Color.Red, 1);
            Pen p8 = new Pen(Color.Brown, 1);
            Pen p9 = new Pen(Color.Maroon, 1);
            Pen p10 = new Pen(Color.Black, 1);
            Pen p11 = new Pen(Color.Blue, 4);

            Font font1 = new Font("TimesNewRoman", 10, FontStyle.Bold | FontStyle.Italic);
            Font font2 = new Font("TimesNewRoman", 8);
            
            pictureBox1.BackColor = Color.White;
            if (flag)
            {
                double x1 = showobl[0, 0];
                double x2 = showobl[0, 1];
                double y1 = showobl[1, 0];
                double y2 = showobl[1, 1];

                int z = comboBox1.SelectedIndex;
                double a1 = Ar[0];//5
                double a3 = Ar[1];//4
                double a5 = Ar[2];//3
                double a7 = Ar[3];//2
                double a9 = Ar[4];//1

                double a10 = Ar[5];//6
                double a11 = Ar[6];//7
                double a12 = Ar[7];//8

                double dx = x2 - x1;
                double dy = y2 - y1;
                double dxy = dx-dy;


                double bxy = Math.Max(dx, dy);
                double step;
                if (bxy < 1.1) step = 0.1;
                else if (bxy < 2.1) step = 0.2;
                else if (bxy < 5.1) step = 0.5;
                else if (bxy < 10.1) step = 1;
                else if (bxy < 20.1) step = 2;
                else if (bxy < 50.1) step = 5;
                else if (bxy < 100.1) step = 10;
                else if (bxy < 200.1) step = 20;
                else if (bxy < 500.1) step = 50;
                else if (bxy < 1000.1) step = 100;
                else if (bxy < 2000.1) step = 200;
                else step = 1000;

                if (dxy>0)
                {
                    y1 = y1 - dxy / 2;
                    y2 = y2 + dxy / 2;
                }
                else if (dxy < 0)
                {
                    x1 = x1 - Math.Abs(dxy) / 2;
                    x2 = x2 + Math.Abs(dxy) / 2;
                }
                x1 = x1 - 0.05 * Math.Abs(x2 - x1);
                x2 = x2 + 0.05 * Math.Abs(x2 - x1);
                y1 = y1 - 0.05 * Math.Abs(y2 - y1);
                y2 = y2 + 0.05 * Math.Abs(y2 - y1);

                float mw = k * (w) / ((float)(Math.Max(x2 - x1, y2 - y1)));
                float mh = k * (h) / ((float)(Math.Max(x2 - x1, y2 - y1)));
                        for (int ii = 0; ii < w; ii++)
                            for (int jj = 0; jj < h; jj++)
                            {
                                double i = (ii * (Math.Max(x2 - x1, y2 - y1)) / w + x1) / k;
                                double j = (jj * (Math.Max(x2 - x1, y2 - y1)) / h + y1) / k;
                                double i1 = ((ii + 1) * (Math.Max(x2 - x1, y2 - y1)) / w + x1) / k;
                                double j1 = ((jj + 1) * (Math.Max(x2 - x1, y2 - y1)) / h + y1) / k;
                                double i0 = ((ii - 1) * (Math.Max(x2 - x1, y2 - y1)) / w + x1) / k;
                                double j0 = ((jj - 1) * (Math.Max(x2 - x1, y2 - y1)) / h + y1) / k;
                                double f = Function.function(i, j, z);
                                double f2 = Function.function(i0, j, z); 
                                double f3 = Function.function(i, j0, z); 
                                double f4 = Function.function(i1, j, z); 
                                double f5 = Function.function(i, j1, z); 
                                double f6 = Function.function(i1, j1, z);
                                double f7 = Function.function(i0, j1, z);
                                double f8 = Function.function(i1, j0, z);
                                double f9 = Function.function(i0, j0, z);

                                if (((f2 < a1) || (f3 < a1) || (f4 < a1) || (f5 < a1) || (f6 < a1) || (f7 < a1) || (f8 < a1) || (f9 < a1)) && (f > a1)&&(flines[4]==true)) e.Graphics.FillRectangle(Brushes.PaleGreen, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a3) || (f3 < a3) || (f4 < a3) || (f5 < a3) || (f6 < a3) || (f7 < a3) || (f8 < a3) || (f9 < a3)) && (f > a3)&&(flines[3]==true)) e.Graphics.FillRectangle(Brushes.YellowGreen, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a5) || (f3 < a5) || (f4 < a5) || (f5 < a5) || (f6 < a5) || (f7 < a5) || (f8 < a5) || (f9 < a5)) && (f > a5)&&(flines[2]==true)) e.Graphics.FillRectangle(Brushes.Orange, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a7) || (f3 < a7) || (f4 < a7) || (f5 < a7) || (f6 < a7) || (f7 < a7) || (f8 < a7) || (f9 < a7)) && (f > a7)&&(flines[1]==true)) e.Graphics.FillRectangle(Brushes.Red, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a9) || (f3 < a9) || (f4 < a9) || (f5 < a9) || (f6 < a9) || (f7 < a9) || (f8 < a9) || (f9 < a9)) && (f > a9)&&(flines[0]==true)) e.Graphics.FillRectangle(Brushes.Maroon, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a10) || (f3 < a10) || (f4 < a10) || (f5 < a10) || (f6 < a10) || (f7 < a10) || (f8 < a10) || (f9 < a10)) && (f > a10) && (flines[5] == true)) e.Graphics.FillRectangle(Brushes.Pink, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a11) || (f3 < a11) || (f4 < a11) || (f5 < a11) || (f6 < a11) || (f7 < a11) || (f8 < a11) || (f9 < a11)) && (f > a11) && (flines[6] == true)) e.Graphics.FillRectangle(Brushes.Violet, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a12) || (f3 < a12) || (f4 < a12) || (f5 < a12) || (f6 < a12) || (f7 < a12) || (f8 < a12) || (f9 < a12)) && (f > a12) && (flines[7] == true)) e.Graphics.FillRectangle(Brushes.MediumOrchid, (float)(ii), (float)(h - jj), 1, 1);

                            }

                        //Отрисовка результата работы алгоритма
                        if (flag2 == true)
                        {
                            for (int i = 0; i < algTit.Pool.Count; i++) // раскраска лучших окуней
                                e.Graphics.FillEllipse(Brushes.Red, (float)((algTit.Pool[i].coords[0] * k - x1) * w / (x2 - x1) - 3), (float)(h - (algTit.Pool[i].coords[1] * k - y1) * h / (y2 - y1) - 3), 6, 6);


                    //e.Graphics.FillEllipse(Brushes.Red, (float)((algPerch.alfa.coords.vector[0] * k - x1) * w / (x2 - x1) - 4), (float)(h - (algPerch.alfa.coords.vector[1] * k - y1) * h / (y2 - y1) - 4), 8, 8);
                }

                //отрисовка Осей
                for (int i = -6; i < 12; i++)
                        {
                            e.Graphics.DrawLine(p10, (float)((x1 - i*step) * w / (x1 - x2)), h - a - 5, (float)((x1 - i*step) * w / (x1 - x2)), h - a + 5);
                            e.Graphics.DrawLine(p10, a - 5, (float)(h - (y1 - i*step) * h / (y1 - y2)), a + 5, (float)(h - (y1 - i*step) * h / (y1 - y2)));
                            e.Graphics.DrawString((i * step).ToString(), font2, Brushes.Black, (float)((x1 - i * step) * w / (x1 - x2)), h - a + 5);
                            e.Graphics.DrawString((i * step).ToString(), font2, Brushes.Black, a - 30, (float)(h -5- (y1 - i * step) * h / (y1 - y2)));
                        }
            }
            
            //Стрелочки абцисс и ординат
            p10.EndCap = LineCap.ArrowAnchor;
            e.Graphics.DrawLine(p10, 0, h - a, w - 10, h - a);
            e.Graphics.DrawLine(p10, a, h, a, 0);
            e.Graphics.DrawString("x", font1, Brushes.Black, w - 20, h - a + 11);
            e.Graphics.DrawString("y", font1, Brushes.Black, a - 20, 1);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBoxSelectParams_SelectedIndexChanged(object sender, EventArgs e)
        {

            if ((comboBox1.SelectedIndex != -1))
            {
                buttonAnswer.Enabled = true;
                button1.Enabled = true;
            }
        }

        /// <summary>Вызов pdf файла с алгоритмом</summary>
        private void buttonHelp_Click(object sender, EventArgs e)
        {
            Process.Start("HelpPerchMethod.pdf"); // TODO: сделать pdf синиц и заменить
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            obl = new double[2, 2];

            // область определения
            obl[0, 0] = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
            obl[0, 1] = Convert.ToDouble(dataGridView1.Rows[0].Cells[2].Value);
            obl[1, 0] = Convert.ToDouble(dataGridView1.Rows[1].Cells[1].Value);
            obl[1, 1] = Convert.ToDouble(dataGridView1.Rows[1].Cells[2].Value);

            NP = Convert.ToInt32(dataGridView2.Rows[0].Cells[1].Value);
            gamma = Convert.ToDouble(dataGridView2.Rows[1].Cells[1].Value);
            eta = Convert.ToDouble(dataGridView2.Rows[2].Cells[1].Value);
            rho = Convert.ToDouble(dataGridView2.Rows[3].Cells[1].Value);
            c1 = Convert.ToDouble(dataGridView2.Rows[4].Cells[1].Value);
            c2 = Convert.ToDouble(dataGridView2.Rows[5].Cells[1].Value);
            c3 = Convert.ToDouble(dataGridView2.Rows[6].Cells[1].Value);
            K = Convert.ToDouble(dataGridView2.Rows[7].Cells[1].Value);
            h = Convert.ToDouble(dataGridView2.Rows[8].Cells[1].Value);
            L = Convert.ToDouble(dataGridView2.Rows[9].Cells[1].Value);
            P = Convert.ToDouble(dataGridView2.Rows[10].Cells[1].Value);
            mu = Convert.ToDouble(dataGridView2.Rows[11].Cells[1].Value);

            lambda = Convert.ToDouble(dataGridView4.Rows[0].Cells[1].Value);
            alpha = Convert.ToDouble(dataGridView4.Rows[1].Cells[1].Value);

            eps = Convert.ToDouble(dataGridView2.Rows[12].Cells[1].Value);

            FormStepTit formPerch = new FormStepTit(comboBox1.SelectedIndex, obl, NP, alpha, gamma, lambda, eta, rho, c1, c2, c3, K, h, L, P, mu, eps, exact)
            {
                flines = flines,
                showobl = showobl,
                Ar = Ar
            };
            formPerch.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                if (dataGridView1.Rows[0].Cells[1].Value != null &&
                    dataGridView1.Rows[0].Cells[2].Value != null &&
                    dataGridView1.Rows[1].Cells[1].Value != null &&
                    dataGridView1.Rows[1].Cells[2].Value != null)
                {
                    if (comboBox1.SelectedIndex != -1)
                    {
                        obl = new double[2, 2];

                        // область определения
                        obl[0, 0] = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
                        obl[0, 1] = Convert.ToDouble(dataGridView1.Rows[0].Cells[2].Value);
                        obl[1, 0] = Convert.ToDouble(dataGridView1.Rows[1].Cells[1].Value);
                        obl[1, 1] = Convert.ToDouble(dataGridView1.Rows[1].Cells[2].Value);

                        NP = Convert.ToInt32(dataGridView2.Rows[0].Cells[1].Value);
                        gamma = Convert.ToDouble(dataGridView2.Rows[1].Cells[1].Value);
                        eta = Convert.ToDouble(dataGridView2.Rows[2].Cells[1].Value);
                        rho = Convert.ToDouble(dataGridView2.Rows[3].Cells[1].Value);
                        c1 = Convert.ToDouble(dataGridView2.Rows[4].Cells[1].Value);
                        c2 = Convert.ToDouble(dataGridView2.Rows[5].Cells[1].Value);
                        c3 = Convert.ToDouble(dataGridView2.Rows[6].Cells[1].Value);
                        K = Convert.ToDouble(dataGridView2.Rows[7].Cells[1].Value);
                        h = Convert.ToDouble(dataGridView2.Rows[8].Cells[1].Value);
                        L = Convert.ToDouble(dataGridView2.Rows[9].Cells[1].Value);
                        P = Convert.ToDouble(dataGridView2.Rows[10].Cells[1].Value);
                        mu = Convert.ToDouble(dataGridView2.Rows[11].Cells[1].Value);


                        lambda = Convert.ToDouble(dataGridView4.Rows[0].Cells[1].Value);
                        alpha = Convert.ToDouble(dataGridView4.Rows[1].Cells[1].Value);

                        eps = Convert.ToDouble(dataGridView2.Rows[12].Cells[1].Value);

                        for (int p = 0; p < 1; p++)
                        {          
                            List<double> averFuncDeviation = new List<double>();
                            double minDeviation = 0;
                            int successCount = 0;
                            double eps = Math.Max(Math.Abs(obl[0, 0] - obl[0, 1]), Math.Abs(obl[1, 0] - obl[1, 1])) / (float)NumOfStarts;
                            double averDer = 0;
                            double normalDerivation = 0;
                            int z = comboBox1.SelectedIndex;
            
                            for (int i = 0; i < NumOfStarts; i++)
                            {
                                algTit = new AlgorithmTit();

                                Tit result = algTit.StartAlg(NP, alpha, gamma, lambda, eta, rho, c1, c2, c3, K, h, L, P, mu, eps, obl, z);
            
                                foreach (Vector item in exactPoints)
                                {
                                    if ((Math.Abs(result.coords[0] - item[0]) < eps) && (Math.Abs(result.coords[1] - item[1]) < eps))
                                    {
                                        successCount++;
                                        break;
                                    }
                                }
            
                                averFuncDeviation.Add(Math.Abs(result.fitness - exact));
                            }
            
                            double deltaSum = 0;
                            for (int i = 0; i < NumOfStarts; i++)
                                deltaSum += averFuncDeviation[i];
            
                            // СК отлонение?
                            averDer = deltaSum / (float)NumOfStarts;
            
                            averFuncDeviation.Sort();/////////////////////////////////////////////////
                            minDeviation = averFuncDeviation[0];
            
                            double dispersion = 0;
                            for (int i = 0; i < NumOfStarts; i++)
                                dispersion += Math.Pow(averFuncDeviation[i] - averDer, 2);
                            normalDerivation = Math.Sqrt((dispersion / (float)NumOfStarts));
            
                            FileStream fs = new FileStream("protocol.txt", FileMode.Append, FileAccess.Write);
                            StreamWriter r = new StreamWriter(fs);
                            //           r.Write($"+---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+------------+----------+\r\n" +
                            //                   $"|  Номер  |   Размер    | Gamma |  eta  |   Alpha  | lambda |  P  |  K  |  L |   mu   |   h   |  с1  |  с2  |  с3  | rho | Cреднее значение отклонения | Наименьшее значение | Среднеквадратическое |   Кол-во   |  Число   |\r\n" +
                            //                   $"| функции |  популяции  |       |       |          |        |     |     |    |        |       |      |      |      |     |     от точного решения      |      отклонения     |      отклонение      |  успехов   | запусков |\r\n" +
                            //                   $"+---------+-------------+-------+-------+----------+--------+-----+-----+----+--------+-------+------+------+------+-----+-----------------------------+---------------------+----------------------+------------+----------+\r\n");
                            //                         0           1           2       3        4          5      6      7    8     9        10     11     12     13     14               15                          16                  17                 18           19
                            r.Write(String.Format(@"|{0,6}   |    {1,4}     |{2,3:f4} |{3,3:f4} | {4,3:f6} | {5,3:f3}  | {6,1}  | {7,1}  | {8,1}  | {9,3:f4} |{10,3:f4} |{11,3:f3} |{12,3:f3} |{13,3:f3} |{14,3:f3} | {15,16:f6}            | {16,14:f6}      | {17,15:f6}      | {18,5}      | {19,4}     |",
                                                      z+1,          NP,       gamma,      eta,     alpha,    lambda,       P,      K,      L,        mu,        h,          c1,        c2,         c3,         rho,     averDer,  minDeviation, normalDerivation, successCount, NumOfStarts));
                            //                         0            1           2          3         4         5           6       7       8         9          10          11         12          13           14         15         16               17              18           19
                            r.Write("\r\n");

                            r.Close();
            
                            fs.Close();
                        }
                        Process.Start("protocol.txt");
                    }
                }
                else
                    MessageBox.Show("Введите корректные параметры", "Ошибка при запуске алгоритма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}