using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public static class InputOutput
    {
        public static int formattype;
        //Ввод плотной матрицы
        public static void InputMatrix(int Flag, string FileName, out double[,] Plot) { Plot = new double[10, 10]; }
        //Ввод матрицы в профильном формате
        public static void InputMatrix(int Flag, string FileName, out int[] ia, out double[] gg, out double[] diag)
        {
            gg = new double[10];
            diag = new double[10];
            ia = new int[10];
        }
        //Ввод матрицы в разреженном формате
        public static void InputMatrix(int Flag, string FileName, out int[] ia, out int[] ja, out double[] gg, out double[] diag)
        {
            gg = new double[10];
            diag = new double[10];
            ia = new int[10];
            ja = new int[10];
        }
        //Ввод матрицы в диагональном формате
        //void InputMatrix(int Flag, string FileName, double[] gg, double[] diag);
        //Ввод матрицы в ленточном формате
        public static void InputMatrix(int Flag, string FileName, out double[] gg, out double[] diag)
        {
            gg = new double[10];
            diag = new double[10];
        }

        //ввод вектора правой части
        public static void InputRightPart(string FileName, out double[] pr)
        {
            pr = new double[10];
        }
        //самый нужный комент
    }
}
