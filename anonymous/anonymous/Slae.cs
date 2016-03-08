using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public class Slae<T>
    {
        private IMatrix<T> matrix;     //Матрица
        private IMatrix<T> pmatrix;    //Предобусловленная матрица
        private Vector rightpart;      //Правая часть матрицы

        public IMatrix<T> Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }

        public IMatrix<T> PMatrix
        {
            get { return pmatrix; }
            set { pmatrix = value; }
        }

        public Vector RightPart
        {
            get { return rightpart; }
            set { rightpart = value; }
        }
    }
}
