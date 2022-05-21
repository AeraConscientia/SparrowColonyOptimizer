using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AIS
{
    public partial class FormStepTit : Form
    {
        public FormStepTit(int z, double[,] obl, int NP, double alpha, double gamma, double lambda, double eta, double rho, double c1, double c2, double c3,
            double K, double h, double L, double P, double mu, double eps, double exact)
        {
            InitializeComponent();

            this.z = z;
            this.obl = obl;
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
        }

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
        public List<Tit> memory;

        private int dim;

        double exact;


        /// <summary>Номер функции</summary>
        public int z;

        public float[] Ar = new float[8];
        public bool[] flines = new bool[8];
        public float[] A = new float[8];
        public double[,] showoblbase = new double[2, 2];
        public double[,] oblbase = new double[2, 2];
        public double[,] obl;
        public int stepsCount = 9; // TODO: Что это вообще?

        public AlgorithmTit algo = new AlgorithmTit();

        public double[,] showobl = new double[2, 2];

        bool flag = false;
        /// <summary>Массив состояний</summary>
        bool[] Red = new bool[9];

        public int iterationGraph = 0;

        private void button2_Click(object sender, EventArgs e)
        {
            algo.Initilize();
            algo.InitalPopulationGeneration();
        }

        /// <summary>Сохранение графика приспособленности и положения стаи</summary>
        private void buttonSavePictures_Click(object sender, EventArgs e)
        {
            chart1.SaveImage($"Fitness.tiff", System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Tiff);
            if (pictureBox2.Image != null)
                pictureBox2.Image.Save($"Iteration.tiff", System.Drawing.Imaging.ImageFormat.Tiff); //pictureBox2.Image.Save($"Iteration{Iteration}.tiff", System.Drawing.Imaging.ImageFormat.Tiff);
            // TODO: Добавить в названия итерацию
        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void buttonBestLeader_Click(object sender, EventArgs e)
        {
            //На графике показать лучшую синицу
        }

        /// <summary>Отрисовка стрелочек</summary>
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen pBlack = new Pen(Color.Black, 2);
            Pen pGray  = new Pen(Color.Gray, 2);
            Pen pRed   = new Pen(Color.Red, 2);
            Font f1    = new Font("TimesNewRoman", 12, FontStyle.Bold);

            // 2-3 шаги
            e.Graphics.DrawLine(pBlack, 112, 152, 135, 152);

            // 3-4 шаги
            e.Graphics.DrawLine(pBlack, 231, 152, 254, 152);

            // 6-8 шаги
            e.Graphics.DrawLine(pBlack, 231, 287, 254, 287);

            // 8-9 шаги
            e.Graphics.DrawLine(pBlack, 112, 287, 135, 287);

            // 10-13 шаги
            e.Graphics.DrawLine(pBlack, 112, 414, 254, 414);

            //e.Graphics.DrawLine(pBlack, 250, 250, 250, 135);        // проверка -> деление  вертик 
            //e.Graphics.DrawLine(pBlack, 250, 135, 135, 135);        // проверка -> деление  горизонт
            //e.Graphics.DrawLine(pBlack, 135+2, 134, 145+3, 130-1);          // Верхнее крыло повернутой стрелочки
            //e.Graphics.DrawLine(pBlack, 135+2, 134, 145+3, 140);            // Нижнее крыло повернутой стрелочки
            // 
            //if (Red[6] == true)
            //{
            //    e.Graphics.DrawLine(pRed, 250, 250, 250, 135);      // проверка -> деление  вертик 
            //    e.Graphics.DrawLine(pRed, 250, 135, 140, 135);      // проверка -> деление  горизонт
            //    e.Graphics.DrawLine(pRed, 135 + 2, 134, 145 + 3, 130 - 1);
            //    e.Graphics.DrawLine(pRed, 135 + 2, 134, 145 + 3, 140);
            //}
            //
            //e.Graphics.DrawLine(pBlack, 140, 405, 250, 405);        // лидер Pool -> проверка горизонт
            //
            //e.Graphics.DrawLine(pBlack, 250, 405, 250, 210);        // лидер Pool -> проверка вертик
            //e.Graphics.DrawLine(pBlack, 250, 258, 255, 268);
            //e.Graphics.DrawLine(pBlack, 249, 258, 244, 268);
            //
            //if (Red[4] == true)
            //{
            //    e.Graphics.DrawLine(pRed, 140, 405, 250, 405);      // лидер Pool -> проверка горизонт
            //                         
            //    e.Graphics.DrawLine(pRed, 250, 405, 250, 210);      // лидер Pool -> проверка вертик
            //    e.Graphics.DrawLine(pRed, 250, 258, 255, 268);
            //    e.Graphics.DrawLine(pRed, 249, 258, 244, 268);
            //}
            //
            //e.Graphics.DrawLine(pBlack, 70, 300, 70, 373);          // плаванье -> лидер Pool 
            //e.Graphics.DrawLine(pBlack, 70, 370, 75, 360);
            //e.Graphics.DrawLine(pBlack, 69, 370, 64, 360);
            //
            //if (Red[3] == true)
            //{
            //    e.Graphics.DrawLine(pRed, 70, 300, 70, 373);        // плаванье -> лидер Pool 
            //    e.Graphics.DrawLine(pRed, 70, 370, 75, 360);
            //    e.Graphics.DrawLine(pRed, 69, 370, 64, 360);
            //}
            //
            //e.Graphics.DrawLine(pBlack, 70, 210, 70, 283);          // котлы -> плаванье !!!
            //e.Graphics.DrawLine(pBlack, 70, 280, 75, 270);
            //e.Graphics.DrawLine(pBlack, 69, 280, 64, 270);
            //
            //if (Red[2] == true)
            //{
            //    e.Graphics.DrawLine(pRed, 70, 210, 70, 283);        // котлы -> плаванье !!!
            //    e.Graphics.DrawLine(pRed, 70, 280, 75, 270);
            //    e.Graphics.DrawLine(pRed, 69, 280, 64, 270);
            //}
            //
            //e.Graphics.DrawLine(pBlack, 70, 120, 70, 193);          // деление -> котлы
            //e.Graphics.DrawLine(pBlack, 70, 195, 75, 185);
            //e.Graphics.DrawLine(pBlack, 69, 195, 64, 185);
            //
            //if (Red[1] == true)
            //{
            //    e.Graphics.DrawLine(pRed, 70, 120, 70, 193);        // деление -> котлы
            //    e.Graphics.DrawLine(pRed, 70, 195, 75, 185);
            //    e.Graphics.DrawLine(pRed, 69, 195, 64, 185);
            //}
            //
            //e.Graphics.DrawLine(pBlack, 70, 30, 70, 105);           // генерация -> деление
            //e.Graphics.DrawLine(pBlack, 70, 102, 75, 92);
            //e.Graphics.DrawLine(pBlack, 69, 102, 64, 92);
            //
            //if (Red[0] == true)
            //{
            //    e.Graphics.DrawLine(pRed, 70, 30, 70, 105);         // генерация -> деление
            //    e.Graphics.DrawLine(pRed, 70, 102, 75, 92);
            //    e.Graphics.DrawLine(pRed, 69, 102, 64, 92);
            //}
            //
            //
            //e.Graphics.DrawLine(pBlack, 310, 225, 340, 225);        // проверка -> интенсивный поиск  горизонт
            //e.Graphics.DrawLine(pBlack, 340, 225, 340, 530);        // проверка -> интенсивный поиск  вертик
            //e.Graphics.DrawLine(pBlack, 310, 530, 340, 530);        // проверка -> интенсивный поиск  горизонт
            //e.Graphics.DrawLine(pBlack, 310, 305+224, 321, 300+224);        // Верхнее крыло повернутой стрелочки
            //e.Graphics.DrawLine(pBlack, 310, 305+224, 321, 311+225);        // Нижнее крыло повернутой стрелочки
            //
            //if (Red[7] == true)
            //{
            //    e.Graphics.DrawLine(pRed, 310, 225, 340, 225);      // проверка -> интенсивный поиск  горизонт
            //    e.Graphics.DrawLine(pRed, 340, 225, 340, 530);      // проверка -> интенсивный поиск  вертик
            //    e.Graphics.DrawLine(pRed, 310, 530, 340, 530);      // проверка -> интенсивный поиск  горизонт
            //    e.Graphics.DrawLine(pRed, 310, 305 + 224, 321, 300 + 224);  // Верхнее крыло повернутой стрелочки
            //    e.Graphics.DrawLine(pRed, 310, 305 + 224, 321, 311 + 225);  // Нижнее крыло повернутой стрелочки 
            //}
            //
            //e.Graphics.DrawLine(pBlack, 70, 530, 310, 530);         // интенсивный поиск -> окончание  горизонт НУЖНА СТРЕЛОЧКА
            //e.Graphics.DrawLine(pBlack, 137, 305 + 224, 148, 300 + 224);    // Верхнее крыло повернутой стрелочки 
            //e.Graphics.DrawLine(pBlack, 137, 305 + 224, 148, 311 + 225);    // Нижнее крыло повернутой стрелочки 
            //if (Red[8] == true) 
            //{
            //    e.Graphics.DrawLine(pRed, 70, 530, 310, 530);       // интенсивный поиск -> окончание  горизонт НУЖНА СТРЕЛОЧКА
            //    e.Graphics.DrawLine(pRed, 137, 305 + 224, 148, 300 + 224);  // Верхнее крыло повернутой стрелочки 
            //    e.Graphics.DrawLine(pRed, 137, 305 + 224, 148, 311 + 225);  // Нижнее крыло повернутой стрелочки 
            //}

        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            float w = pictureBox2.Width;
            float h = pictureBox2.Height;
            float x0 = w / 2;
            float y0 = h / 2;
            float a = 30;
            float k = 1;

            List<PointF> points = new List<PointF>();
            Pen p1  = new Pen(Color.PaleGreen, 1);
            Pen p2  = new Pen(Color.GreenYellow, 1);
            Pen p3  = new Pen(Color.YellowGreen, 1);
            Pen p4  = new Pen(Color.Yellow, 1);
            Pen p5  = new Pen(Color.Orange, 1);
            Pen p6  = new Pen(Color.OrangeRed, 1);
            Pen p7  = new Pen(Color.Red, 1);
            Pen p8  = new Pen(Color.Brown, 1);
            Pen p9  = new Pen(Color.Maroon, 1);
            Pen p10 = new Pen(Color.Black, 1);
            Pen p11 = new Pen(Color.Blue, 4);

            Pen p12 = new Pen(Color.DarkOrange, 2);
            Pen p13 = new Pen(Color.DarkGreen, 2);
            Pen p14 = new Pen(Color.Red, 2);

            Font font1 = new Font("TimesNewRoman", 10, FontStyle.Bold | FontStyle.Italic);
            Font font2 = new Font("TimesNewRoman", 8);

            pictureBox2.BackColor = Color.White;

            double x1 = showobl[0, 0];
            double x2 = showobl[0, 1];
            double y1 = showobl[1, 0];
            double y2 = showobl[1, 1];

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
            double dxy = dx - dy;

            double bxy = Math.Max(dx, dy);
            double step = 0;
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

            if (dxy > 0)
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

                    if (((f2 < a1) || (f3 < a1) || (f4 < a1) || (f5 < a1) || (f6 < a1) || (f7 < a1) || (f8 < a1) || (f9 < a1)) && (f > a1) && (flines[4] == true)) e.Graphics.FillRectangle(Brushes.PaleGreen, (float)(ii), (float)(h - jj), 1, 1);
                    else if (((f2 < a3) || (f3 < a3) || (f4 < a3) || (f5 < a3) || (f6 < a3) || (f7 < a3) || (f8 < a3) || (f9 < a3)) && (f > a3) && (flines[3] == true)) e.Graphics.FillRectangle(Brushes.YellowGreen, (float)(ii), (float)(h - jj), 1, 1);
                    else if (((f2 < a5) || (f3 < a5) || (f4 < a5) || (f5 < a5) || (f6 < a5) || (f7 < a5) || (f8 < a5) || (f9 < a5)) && (f > a5) && (flines[2] == true)) e.Graphics.FillRectangle(Brushes.Orange, (float)(ii), (float)(h - jj), 1, 1);
                    else if (((f2 < a7) || (f3 < a7) || (f4 < a7) || (f5 < a7) || (f6 < a7) || (f7 < a7) || (f8 < a7) || (f9 < a7)) && (f > a7) && (flines[1] == true)) e.Graphics.FillRectangle(Brushes.Red, (float)(ii), (float)(h - jj), 1, 1);
                    else if (((f2 < a9) || (f3 < a9) || (f4 < a9) || (f5 < a9) || (f6 < a9) || (f7 < a9) || (f8 < a9) || (f9 < a9)) && (f > a9) && (flines[0] == true)) e.Graphics.FillRectangle(Brushes.Maroon, (float)(ii), (float)(h - jj), 1, 1);
                    else if (((f2 < a10) || (f3 < a10) || (f4 < a10) || (f5 < a10) || (f6 < a10) || (f7 < a10) || (f8 < a10) || (f9 < a10)) && (f > a10) && (flines[5] == true)) e.Graphics.FillRectangle(Brushes.Pink, (float)(ii), (float)(h - jj), 1, 1);
                    else if (((f2 < a11) || (f3 < a11) || (f4 < a11) || (f5 < a11) || (f6 < a11) || (f7 < a11) || (f8 < a11) || (f9 < a11)) && (f > a11) && (flines[6] == true)) e.Graphics.FillRectangle(Brushes.Violet, (float)(ii), (float)(h - jj), 1, 1);
                    else if (((f2 < a12) || (f3 < a12) || (f4 < a12) || (f5 < a12) || (f6 < a12) || (f7 < a12) || (f8 < a12) || (f9 < a12)) && (f > a12) && (flines[7] == true)) e.Graphics.FillRectangle(Brushes.MediumOrchid, (float)(ii), (float)(h - jj), 1, 1);
                }

           

            for (int i = -6; i < 12; i++)
            {
                e.Graphics.DrawLine(p10, (float)((x1 - i * step) * w / (x1 - x2)), h - a - 5, (float)((x1 - i * step) * w / (x1 - x2)), h - a + 5);
                e.Graphics.DrawLine(p10, a - 5, (float)(h - (y1 - i * step) * h / (y1 - y2)), a + 5, (float)(h - (y1 - i * step) * h / (y1 - y2)));
                e.Graphics.DrawString((i * step).ToString(), font2, Brushes.Black, (float)((x1 - i * step) * w / (x1 - x2)), h - a + 5);
                e.Graphics.DrawString((i * step).ToString(), font2, Brushes.Black, a - 30, (float)(h - 5 - (y1 - i * step) * h / (y1 - y2)));
            }

            //Стрелочки абцисс и ординат
            e.Graphics.DrawLine(p10, 0, h - a, w, h - a);
            e.Graphics.DrawLine(p10, a, h, a, 0);
            e.Graphics.DrawLine(p10, a, 0, a - 5, 10);
            e.Graphics.DrawLine(p10, a, 0, a + 5, 10);
            e.Graphics.DrawLine(p10, w - 5, h - a, w - 15, h - a - 5);
            e.Graphics.DrawLine(p10, w - 5, h - a, w - 15, h - a + 5);
            e.Graphics.DrawString("x", font1, Brushes.Black, w - 20, h - a + 5);
            e.Graphics.DrawString("y", font1, Brushes.Black, a - 20, 1);

        }
    }
}
