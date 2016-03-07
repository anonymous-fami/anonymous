using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public class DiagonalSLAE
    {
        private IMatrix<DiagonalMatrix> matrix;
        private Vector rightpart;

        public IMatrix<DiagonalMatrix> Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }

        public Vector RightPart
        {
            get { return rightpart; }
            set { rightpart = value; }
        }
    }
}
