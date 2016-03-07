using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public class ProfileSLAE
    {
        private IMatrix<ProfileMatrix> matrix;
        private Vector rightpart;

        public IMatrix<ProfileMatrix> Matrix
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
