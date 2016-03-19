using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace anonymous
{
    public interface IMatrix <Type>
    { 
        Vector Multiply(Vector x); //Перемножение матрицы на вектор.
        Vector TMultiply(Vector x); //Перемножает транспонированную матрицу на вектор.
        double MultiplyU(int index, Vector x); //Функция для Гаусс-Зейделя перемножает элементы index-строки на вектор до index-элемента(верхний треуг).
        double MultiplyL(int index, Vector x); //Функция для Гаусс-Зейделя перемножает элементы index-строки на вектор до index-элемента(нижний треуг).
        double abs_discrepancy(Vector x, Vector F); //Абсолютная невязка.
        double rel_discrepancy(Vector x, Vector F); //Относительная невязка.
        void setMatrix(Type matrix);
        Vector DirectProgress(Vector f); //Прямой ход.
        Vector ReverseProgress(Vector f); //Обратный ход.
        Type getMatrix();
        bool CheckSymmetry(); //Проверка матрицы на симметричность.
    }   
}
