using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public class Vector 
    {
        public int size;
        public double[] values;

        public Vector(int size, double[] values) //Конструктор, получает данные на входе
        {
            this.size = size;
            this.values = values;
        }

        public Vector(string FilePath) //Конструктор, считывает данные из файла
        {
            InputOutput.InputVector(out this.size, FilePath, out this.values);
        }

        public Vector(Vector Original) //Конструктор копий 
        {
            this.size = Original.size;
            this.values = new double[this.size];
            Array.Copy(Original.values, this.values, this.size);
        }

        public Vector(int N) //Конструктор, создаёт нулевой вектор (для начального приближения, например)
        {
            this.size = N;
            this.values = new double[this.size];

            for (int i=0;i< N;i++)
            {
                this.values[i] = 0;
            }
        }

        public double Norm()//норма вектора
        {
            double res = 0;
            for (int i = 0; i < size; i++)
            {
                res += values[i] * values[i];
            }
            res = Math.Sqrt(res);
            return res;
        }

        public Vector Sum(Vector B)//сумма векторов
        {
            double[] values_res = new double[size];
            for (int i = 0; i < size; i++)
            {
                values_res[i] = 0;
            }
            var res = new Vector(size, values_res);

            for (int i = 0; i < size; i++)
            {
                res.values[i] += this.values[i];
                res.values[i] += B.values[i];
            }
            return res;
        }

        public Vector Differ(Vector B)//разность векторов 
        {   
            var res = new Vector(size, this.values);
            for (int i = 0; i < size; i++)
            res.values[i] = res.values[i] - B.values[i];
            return res;
        }

        public Vector Mult(double A)//умножение вектора на число
        {
            double[] values_res = new double[size];

            var res = new Vector(size, values_res);

            for (int i = 0; i < size; i++)
            {
                res.values[i] = this.values[i] * A;
            }
            return res;
        }

        public double Scalar(Vector A)//скалярное произведение векторов
        {
           double res = 0;
            for (int i = 0; i < size; i++)
            {
                res+= this.values[i] * A.values[i];
            }
            return res;
        }

        public int SIZE
        {
            get { return size; }
            set { size = value; }
        }

        public double[] VALUES
        {
            get { return values; }
            set { values = value; }
        }
    }
}
