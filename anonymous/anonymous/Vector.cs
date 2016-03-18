using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
<<<<<<< HEAD
    public class Vector 
=======
    public class Vector
>>>>>>> 7c2739d6c03b5c534016dd9396dd8a5c53bb086d
    {
        public int size;
        public double[] values;

        public Vector(int size, double[] values)//конструктор
        {
            this.size = size;
            this.values = values;
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

        public Vector Scalar(Vector A)//скалярное произведение векторов
        {
<<<<<<< HEAD
           
            double res = 0;
=======
            double[] values_res = new double[size];
            var res = new Vector(size, values_res);
>>>>>>> 7c2739d6c03b5c534016dd9396dd8a5c53bb086d
            for (int i = 0; i < size; i++)
            {
                res.values[i] = this.values[i] * A.values[i];

            }
            return res;
        }

    }
}
