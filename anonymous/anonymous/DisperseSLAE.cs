using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public class DisperseSLAE
    {
        private IMatrix<DisperseMatrix> matrix;     //Матрица
        private IMatrix<DisperseMatrix> pmatrix;    //Предобусловленная матрица
        private Vector rightpart;                   //Правая часть матрицы

        public IMatrix<DisperseMatrix> Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }

        public IMatrix<DisperseMatrix> PMatrix
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
