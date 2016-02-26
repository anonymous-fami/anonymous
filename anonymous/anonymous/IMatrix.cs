using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace anonymous
{
    public interface IMatrix <Type>
    { 
        Vector Multiply(Vector x); //перемножение матрицы на вектор
        Vector TMultiply(Vector x);//перемножает транспонированную матрицу на вектор
        double abs_discrepancy(Vector x, Vector F);//абсолютная невязка
        double rel_discrepancy(Vector x, Vector F);//относительная невязка
        bool setMatrix(Type matrix);
        Type getMatrix();
    }   
}
