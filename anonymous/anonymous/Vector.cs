using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public class Vector : IVector
    {
        public int size;
        public double[] values;

        public Vector(int size, double[] values)//конструктор
        {
            this.size = size;
            this.values = values;
        }

        public Vector()//конструктор
        {
            InputOutput.InputRightPart(out this.size, Data.vectorPath, out this.values);
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
            double[] values_res = new double[size];
            double res = 0;
            for (int i = 0; i < size; i++)
            {
                res+= this.values[i] * A.values[i];

            }
            return res;
        }

    }
}
