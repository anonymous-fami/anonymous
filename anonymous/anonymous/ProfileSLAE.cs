using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public class ProfileSLAE
    {
        private IMatrix<ProfileMatrix> matrix;      //Матрица
        private IMatrix<ProfileMatrix> pmatrix;     //Предобусловленная матрица
        private Vector rightpart;                   //Правая часть матрицы

        public IMatrix<ProfileMatrix> Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }

        public IMatrix<ProfileMatrix> PMatrix
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
