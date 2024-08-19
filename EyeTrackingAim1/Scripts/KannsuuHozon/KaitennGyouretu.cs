using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Numerics;

namespace EyeTrackingAim1.Scripts.KannsuuHozon
{
    public class KaitennGyouretu
    {
        public static Vector3 Kaitenn(Vector3 x_, Vector3 eulerAngle)
        {
            //入力
            DenseVector x = new DenseVector(3);
            x[0] = x_.X;
            x[1] = x_.Y;
            x[2] = x_.Z;

            //回転
            DenseMatrix R_X = DenseMatrix.OfArray(new double[,] { {1.0, 0.0, 0.0}, {0.0, Math.Cos(eulerAngle.X), -Math.Sin(eulerAngle.X) }, {0.0, Math.Sin(eulerAngle.X), Math.Cos(eulerAngle.X) } });
            DenseMatrix R_Y = DenseMatrix.OfArray(new double[,] { {Math.Cos(eulerAngle.Y), 0.0, Math.Sin(eulerAngle.Y) }, {0.0, 1.0, 0.0}, {-Math.Sin(eulerAngle.Y), 0.0, Math.Cos(eulerAngle.Y) } });
            DenseMatrix R_Z = DenseMatrix.OfArray(new double[,] { {Math.Cos(eulerAngle.Z), -Math.Sin(eulerAngle.Z), 0.0}, {Math.Sin(eulerAngle.Z), Math.Cos(eulerAngle.Z), 0.0}, {0.0, 0.0, 1.0} });
            DenseVector y = new DenseVector(x.Count);
            y = R_X * R_Z * R_Y * x;

            //出力
            Vector3 re = new Vector3();
            re.X = (float)y[0];
            re.Y = (float)y[1];
            re.Z = (float)y[2];

            return re;

        }

    }
}
