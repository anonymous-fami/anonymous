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
        double MultiplyU(int index, Vector x);//функция для Гаусс-Зейделя перемножает  элементы index-строки на вектор до index-элемента(верхний треуг)
        double MultiplyL(int index, Vector x);//функция для Гаусс-Зейделя перемножает  элементы index-строки на вектор до index-элемента(нижний треуг)
        double abs_discrepancy(Vector x, Vector F);//абсолютная невязка
        double rel_discrepancy(Vector x, Vector F);//относительная невязка
        void setMatrix(Type matrix);
        Type getMatrix();
    }   
}
