using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public class DiagonalSLAE
    {
        private IMatrix<DiagonalMatrix> matrix;     //Матрица
        private IMatrix<DiagonalMatrix> pmatrix;    //Предобусловленная матрица
        private Vector rightpart;                   //Правая часть матрицы

        public IMatrix<DiagonalMatrix> Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }

        public IMatrix<DiagonalMatrix> PMatrix
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
