using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeTrackingAim1.Scripts.KannsuuHozon;
using MathNet.Numerics.LinearAlgebra.Double;


namespace EyeTrackingAim1.Scripts.Prediction
{
    public class ActiveEyeKannsuu
    {

        
        public static EstimateClass.mlp_class mlp_predict(double[] z, EstimateClass.mlp_class mlp_class)
        {

            EstimateClass.mlp_class output_mlp_class = mlp_class;


            DenseVector y = new DenseVector(z);

            for (int i = 0; i < mlp_class.mlp_h.coef.Length; i++)
            {
                y = y * mlp_class.mlp_h.coef[i] + mlp_class.mlp_h.intercept[i];
                y = relu(y);
            }

            
            for (int i = 0; i < mlp_class.mlp_o.coef.Length; i++)
            {
                output_mlp_class.reg[i] = y * mlp_class.mlp_o.coef[i] + mlp_class.mlp_o.intercept[i];
                output_mlp_class.f[i] = Softmax(output_mlp_class.reg[i]);
            }

            output_mlp_class.reg_yosoku = output_mlp_class.reg[0][output_mlp_class.of_from - 1];

            return output_mlp_class;
        }


        public static EstimateClass.mlp_class mlp_cali_co(double[] z, EstimateClass.mlp_class mlp_class)
        {

            EstimateClass.mlp_class output_mlp_class = mlp_class;


            DenseVector y = new DenseVector(z);

            y = y * mlp_class.mlp_h.coef[0] + mlp_class.mlp_h.intercept[0];
            y = relu(y);
            y = y * mlp_class.mlp_h.coef[1] + mlp_class.mlp_h.intercept[1];
            y = relu(y);
            y = y * mlp_class.mlp_h.coef[2] + mlp_class.mlp_h.intercept[2];


            output_mlp_class.reg[0] = y;

            return output_mlp_class;
        }

        public static EstimateClass.mlp_class mlp_cali_x_head(double[] z, EstimateClass.mlp_class mlp_class)
        {

            EstimateClass.mlp_class output_mlp_class = mlp_class;


            DenseVector y = new DenseVector(z);

            y = y * mlp_class.mlp_h.coef[0] + mlp_class.mlp_h.intercept[0];
            y = relu(y);
            y = y * mlp_class.mlp_h.coef[1] + mlp_class.mlp_h.intercept[1];
            y = relu(y);
            y = y * mlp_class.mlp_h.coef[2] + mlp_class.mlp_h.intercept[2];


            output_mlp_class.reg[0] = y;

            return output_mlp_class;
        }

        public static EstimateClass.mlp_class mlp_cali_x_pre(double[] z, EstimateClass.mlp_class mlp_class)
        {

            EstimateClass.mlp_class output_mlp_class = mlp_class;


            DenseVector y = new DenseVector(z);

            y = mlp_class.mlp_h.coef[0] * y + mlp_class.mlp_h.intercept[0];
            y = relu(y);
            y = mlp_class.mlp_h.coef[1] * y + mlp_class.mlp_h.intercept[1];
            y = relu(y);
            y = mlp_class.mlp_h.coef[2] * y + mlp_class.mlp_h.intercept[2];
            

            output_mlp_class.reg[0] = y;

            return output_mlp_class;
        }

        public static EstimateClass.mlp_class mlp_calibrate(double[] z, double[] HR_Z, EstimateClass.mlp_class mlp_class)
        {

            EstimateClass.mlp_class output_mlp_class = mlp_class;


            DenseVector y = new DenseVector(z);

            for (int i = 0; i < mlp_class.mlp_h.coef.Length; i++)
            {

                y = y * mlp_class.mlp_h.coef[i] + mlp_class.mlp_h.intercept[i];
                y = relu(y);
            }


            for (int i = 0; i < mlp_class.mlp_o.coef.Length; i++)
            {
                output_mlp_class.reg[i] = y * mlp_class.mlp_o.coef[i] + mlp_class.mlp_o.intercept[i];
                output_mlp_class.f[i] = Softmax(output_mlp_class.reg[i]);
            }

            //HR_Z
            DenseVector y_ = new DenseVector(HR_Z);
            for (int i = 0; i < mlp_class.sub.coef.Length; i++)
            {
                if (i == mlp_class.sub.coef.Length - 1)
                {
                    y_ = y_ * mlp_class.sub.coef[i] + mlp_class.sub.intercept[i];
                }
                else
                {
                    y_ = y_ * mlp_class.sub.coef[i] + mlp_class.sub.intercept[i];
                    y_ = relu(y_);
                    
                }
            }
            
            
            output_mlp_class.reg_yosoku = output_mlp_class.reg[0][0] + y_[0];

            return output_mlp_class;
        }



        public static EstimateClass.mlp_class mlp_calibrate_y(double[] z, EstimateClass.mlp_class mlp_class)
        {

            EstimateClass.mlp_class output_mlp_class = mlp_class;


            DenseVector y = new DenseVector(z);

            for (int i = 0; i < mlp_class.mlp_h.coef.Length; i++)
            {

                y = y * mlp_class.mlp_h.coef[i] + mlp_class.mlp_h.intercept[i];
                y = relu(y);
            }


            for (int i = 0; i < mlp_class.mlp_o.coef.Length; i++)
            {
                output_mlp_class.reg[i] = y * mlp_class.mlp_o.coef[i] + mlp_class.mlp_o.intercept[i];
                output_mlp_class.reg[i] = Softmax(output_mlp_class.reg[i]);
            }

            
            output_mlp_class.reg_yosoku = output_mlp_class.reg[0][0];

            return output_mlp_class;
        }


        public static EstimateClass.lstm_clas lstm_predict(double x, EstimateClass.lstm_clas lstm_Clas)
        {
            EstimateClass.lstm_clas outputlstm = lstm_Clas;

            DenseMatrix i = Sigmoid(outputlstm.lstm.w_ii * x + outputlstm.lstm.b_ii + outputlstm.lstm.w_hi * outputlstm.h + outputlstm.lstm.b_hi);
            DenseMatrix f = Sigmoid(outputlstm.lstm.w_if * x + outputlstm.lstm.b_if + outputlstm.lstm.w_hf * outputlstm.h + outputlstm.lstm.b_hf);
            DenseMatrix g = tanH(outputlstm.lstm.w_ig * x + outputlstm.lstm.b_ig + outputlstm.lstm.w_hg * outputlstm.h + outputlstm.lstm.b_hg);
            DenseMatrix o = Sigmoid(outputlstm.lstm.w_io * x + outputlstm.lstm.b_io + outputlstm.lstm.w_ho * outputlstm.h + outputlstm.lstm.b_ho);

            DenseMatrix c = (DenseMatrix)(f.PointwiseMultiply(outputlstm.c) + i.PointwiseMultiply(g));
            DenseMatrix h = (DenseMatrix)(o.PointwiseMultiply(tanH(c)));

            outputlstm.h = h;
            outputlstm.c = c;

            DenseVector y = convert_matrix_to_vector(h);

            //Console.WriteLine("Colum", outputlstm.lstm.mlp_o.coef[0].ColumnCount);
            y = outputlstm.lstm.mlp_o.coef[0] * y + outputlstm.lstm.mlp_o.intercept[0];
            y = relu(y);

            //output
            var y1 = outputlstm.lstm.mlp_o.coef[1] * y + outputlstm.lstm.mlp_o.intercept[1];
            /*var y2 = outputlstm.lstm.mlp_o.coef[2] * y + outputlstm.lstm.mlp_o.intercept[2];
            y2 = Softmax(y2);*/


            outputlstm.reg[0] = y1;
            //outputlstm.reg[1] = y2;

            return outputlstm;
        }

        /*public static EstimateClass.Conv1d_network Conv1d_network_predict(double[,] input_, EstimateClass.Conv1d_network conv1d_class)
        {
            EstimateClass.Conv1d_network output_conv1d_net = conv1d_class;
            //Console.WriteLine(input_.Length);
            DenseMatrix x = new DenseMatrix(input_.GetLength(0), input_.GetLength(1));
            for (int i = 0; i < x.RowCount; i++)
            {
                for (int j = 0; j < x.ColumnCount; j++)
                {
                    x[i, j] = input_[i, j];
                }
            }

            x = Conv1d(x, conv1d_class.conv1d_weight_class[0].Conv1d_weight, conv1d_class.Conv1d_bias[0]);
            x = relu_matrix(x);
            x = Conv1d(x, conv1d_class.conv1d_weight_class[1].Conv1d_weight, conv1d_class.Conv1d_bias[1]);
            x = relu_matrix(x);
            x = Conv1d(x, conv1d_class.conv1d_weight_class[2].Conv1d_weight, conv1d_class.Conv1d_bias[2]);
            x = relu_matrix(x);
            x = avgpool1d(x, conv1d_class.kernel_size[0]);

            DenseVector x_ = convert_matrix_to_allvector(x);
            x_ = conv1d_class.fc_weight[0] * x_ + conv1d_class.fc_bias[0];
            //x_ = tanH_vector(x_, 10);
            x_ = Softmax(x_);

            output_conv1d_net.reg = x_;

            return output_conv1d_net;
        }
        public static EstimateClass.Conv1d_network Conv1d_network_predict_reg(double[,] input_, EstimateClass.Conv1d_network conv1d_class)
        {
            EstimateClass.Conv1d_network output_conv1d_net = conv1d_class;
            //Console.WriteLine(input_.Length);
            DenseMatrix x = new DenseMatrix(input_.GetLength(0), input_.GetLength(1));
            for (int i = 0; i < x.RowCount; i++)
            {
                for (int j = 0; j < x.ColumnCount; j++)
                {
                    x[i, j] = input_[i, j];
                }
            }

            x = Conv1d(x, conv1d_class.conv1d_weight_class[0].Conv1d_weight, conv1d_class.Conv1d_bias[0]);
            x = relu_matrix(x);
            x = Conv1d(x, conv1d_class.conv1d_weight_class[1].Conv1d_weight, conv1d_class.Conv1d_bias[1]);
            x = relu_matrix(x);
            x = Conv1d(x, conv1d_class.conv1d_weight_class[2].Conv1d_weight, conv1d_class.Conv1d_bias[2]);
            x = relu_matrix(x);
            x = avgpool1d(x, conv1d_class.kernel_size[0]);

            DenseVector x_ = convert_matrix_to_allvector(x);
            var y1 = conv1d_class.fc_weight[0] * x_ + conv1d_class.fc_bias[0];
            
            

            output_conv1d_net.reg = y1;

            return output_conv1d_net;
        }*/
        
        public static EstimateClass.Conv1d_network py_cali_x_pre(double[,] input_, EstimateClass.Conv1d_network conv1d_class)
        {
            EstimateClass.Conv1d_network output_conv1d_net = conv1d_class;
            //Console.WriteLine(input_.Length);
            DenseMatrix x = new DenseMatrix(input_.GetLength(0), input_.GetLength(1));
            for (int i = 0; i < x.RowCount; i++)
            {
                for (int j = 0; j < x.ColumnCount; j++)
                {
                    x[i, j] = input_[i, j];
                }
            }

            //encode
            int conv_n = 0;
            x = Conv1d(x, conv1d_class.conv1d_weight_class[conv_n].Conv1d_weight, conv1d_class.Conv1d_bias[conv_n], conv1d_class.padding[conv_n], conv1d_class.stride[conv_n]);
            x = relu_matrix(x);
            conv_n += 1;
            x = Conv1d(x, conv1d_class.conv1d_weight_class[conv_n].Conv1d_weight, conv1d_class.Conv1d_bias[conv_n], conv1d_class.padding[conv_n], conv1d_class.stride[conv_n]);
            x = relu_matrix(x);

            //真ん中
            conv_n += 1;
            x = Conv1d(x, conv1d_class.conv1d_weight_class[conv_n].Conv1d_weight, conv1d_class.Conv1d_bias[conv_n], conv1d_class.padding[conv_n], conv1d_class.stride[conv_n]);
            x = relu_matrix(x);
            conv_n += 1;
            x = Conv1d(x, conv1d_class.conv1d_weight_class[conv_n].Conv1d_weight, conv1d_class.Conv1d_bias[conv_n], conv1d_class.padding[conv_n], conv1d_class.stride[conv_n]);
            x = relu_matrix(x);

            //decode
            conv_n += 1;
            x = Conv1d_transpose(x, conv1d_class.conv1d_weight_class[conv_n].Conv1d_weight, conv1d_class.Conv1d_bias[conv_n], conv1d_class.padding[conv_n], conv1d_class.stride[conv_n]);
            x = relu_matrix(x);
            conv_n += 1;
            x = Conv1d_transpose(x, conv1d_class.conv1d_weight_class[conv_n].Conv1d_weight, conv1d_class.Conv1d_bias[conv_n], conv1d_class.padding[conv_n], conv1d_class.stride[conv_n]);
           

            DenseVector x_ = convert_matrix_to_allvector(x);

            output_conv1d_net.reg = x_;

            return output_conv1d_net;
        }

        public static EstimateClass.Conv1d_network py_cali_x_pre_label(double[,] input_, EstimateClass.Conv1d_network conv1d_class)
        {
            EstimateClass.Conv1d_network output_conv1d_net = conv1d_class;
            //Console.WriteLine(input_.Length);
            DenseMatrix x = new DenseMatrix(input_.GetLength(0), input_.GetLength(1));
            for (int i = 0; i < x.RowCount; i++)
            {
                for (int j = 0; j < x.ColumnCount; j++)
                {
                    x[i, j] = input_[i, j];
                }
            }
            //encode
            /*x = Conv1d(x, conv1d_class.conv1d_weight_class[0].Conv1d_weight, conv1d_class.Conv1d_bias[0], conv1d_class.padding[0]);
            x = relu_matrix(x);
            x = maxpool1d(x, conv1d_class.kernel_size[0]);

            x = Conv1d(x, conv1d_class.conv1d_weight_class[1].Conv1d_weight, conv1d_class.Conv1d_bias[1], conv1d_class.padding[1]);
            x = relu_matrix(x);
            x = maxpool1d(x, conv1d_class.kernel_size[1]);
            
            
            //decode
            x = Conv1d_transpose(x, conv1d_class.conv1d_weight_class[2].Conv1d_weight, conv1d_class.Conv1d_bias[2], conv1d_class.padding[2], conv1d_class.stride[2]);
            x = relu_matrix(x);

            x = Conv1d_transpose(x, conv1d_class.conv1d_weight_class[3].Conv1d_weight, conv1d_class.Conv1d_bias[3], conv1d_class.padding[3], conv1d_class.stride[3]);*/


            DenseVector x_ = convert_matrix_to_allvector(x);
            x_ = conv1d_class.fc_weight[0] * x_ + conv1d_class.fc_bias[0];



            output_conv1d_net.reg = Softmax(x_);
            return output_conv1d_net;
        }


        static DenseMatrix Conv1d(DenseMatrix input_, double[,,] weight, DenseVector bias, int padding, int stride)
        {

            int L_in = input_.ColumnCount;
            int dilation = 1;
            int kernel_size = weight.GetLength(2);
            int L_out = (int)((L_in + 2 * padding - dilation * (kernel_size - 1) - 1) / stride) + 1;

            DenseMatrix add_bias = new DenseMatrix(bias.Count, L_out);
            for (int i = 0; i < add_bias.RowCount; i++)
            {
                for (int j = 0; j < add_bias.ColumnCount; j++)
                {
                    add_bias[i, j] = bias[i];
                }
            }
            DenseMatrix x = new DenseMatrix(weight.GetLength(0), L_out);
            DenseMatrix input_new = new DenseMatrix(input_.RowCount, input_.ColumnCount + 2 * padding);

            for (int i = 0; i < input_new.RowCount; i++)
            {
                for (int j = 0; j < input_new.ColumnCount; j++)
                {
                    if (j < padding || j > input_new.ColumnCount - padding - 1)
                    {
                        input_new[i, j] = 0.0;
                    }
                    else
                    {
                        input_new[i, j] = input_[i, j - padding];
                    }
                }
            }

            for (int i = 0; i < weight.GetLength(1); i++)
            {
                DenseMatrix x_kari = new DenseMatrix(x.RowCount, x.ColumnCount);

                for (int j = 0; j < L_out; j++)
                {
                    DenseMatrix a = new DenseMatrix(weight.GetLength(0), weight.GetLength(2));
                    for (int m = 0; m < a.RowCount; m++)
                    {
                        for (int n = 0; n < a.ColumnCount; n++)
                        {
                            a[m, n] = weight[m, i, n];
                        }
                    }

                    DenseVector b = new DenseVector(weight.GetLength(2));
                    for (int m = 0; m < b.Count; m++)
                    {
                        b[m] = input_new[i, j * stride + m];
                    }

                    var a_b_dot = a * b;

                    for (int m = 0; m < a_b_dot.Count; m++)
                    {
                        x_kari[m, j] = a_b_dot[m];
                    }


                }

                x = x + x_kari;
            }

            return x + add_bias;


        }

        static DenseMatrix Conv1d_transpose(DenseMatrix input_, double[,,] weight, DenseVector bias, int padding_, int stride_)
        {
            int L_in = input_.ColumnCount;
            int stride = stride_;
            int padding = padding_;
            int dilation = 1;
            int kernel_size = weight.GetLength(2);
            int output_padding = 0;
            int L_out = (L_in - 1) * stride - 2 * padding + dilation * (kernel_size - 1) + output_padding + 1;

            int c_in = weight.GetLength(0);
            int c_out = weight.GetLength(1);

            //bias
            DenseMatrix add_bias = new DenseMatrix(bias.Count, L_out);
            for (int i = 0; i < add_bias.RowCount; i++)
            {
                for (int j = 0; j < add_bias.ColumnCount; j++)
                {
                    add_bias[i, j] = bias[i];
                }
            }


            DenseMatrix x = new DenseMatrix(c_out, L_out);

            for (int i = 0; i < c_in; i++)
            {
                DenseMatrix x_kari = new DenseMatrix(x.RowCount, x.ColumnCount);

                DenseMatrix[] x_kari_array = new DenseMatrix[L_in];

                DenseMatrix a = new DenseMatrix(c_out, kernel_size);
                for (int m = 0; m < a.RowCount; m++)
                {
                    for (int n = 0; n < a.ColumnCount; n++)
                    {
                        a[m, n] = weight[i, m, n];
                    }
                }

                for (int j = 0; j < L_in; j++)
                {
                    x_kari_array[j] = input_[i, j] * a;
                }

                for (int m = 0; m < x_kari.RowCount; m++)
                {
                    for (int k = 0; k < L_in; k++)
                    {
                        int s = k * stride;
                        if (s < padding)
                        {
                            s = padding;
                        }
                        int e = L_out + 2 * padding - stride * (L_in - k - 1) - 1;
                        if (e > L_out + padding - 1) 
                        {
                            e = L_out + padding - 1;
                        } 

                        for (int n = s; n < e + 1; n++)
                        {
                            x_kari[m, n - padding] = x_kari[m, n - padding] + x_kari_array[k][m, n - k * stride];
                        }
                    }
                }


                x = x + x_kari;
            }

            return x + add_bias;
        }





        static DenseMatrix avgpool1d(DenseMatrix input_, int k_size)
        {
            int L = (int)((input_.ColumnCount - k_size) / k_size) + 1;
            DenseMatrix x = new DenseMatrix(input_.RowCount, L);

            for (int i = 0; i < x.RowCount; i++)
            {
                for (int j = 0; j < x.ColumnCount; j++)
                {
                    double x_kari = 0;
                    for (int m = 0; m < k_size; m++)
                    {
                        x_kari = x_kari + input_[i, j * k_size + m];
                    }
                    x_kari = x_kari / k_size;
                    x[i, j] = x_kari;
                }
            }

            return x;
        }
        static DenseMatrix maxpool1d(DenseMatrix input_, int k_size)
        {
            int L = (int)((input_.ColumnCount - (k_size - 1) - 1) / k_size) + 1;
            DenseMatrix x = new DenseMatrix(input_.RowCount, L);

            for (int i = 0; i < x.RowCount; i++)
            {
                for (int j = 0; j < x.ColumnCount; j++)
                {
                    double[] x_kari = new double[k_size];
                    for (int m = 0; m < k_size; m++)
                    {
                        x_kari[m] = input_[i, j * k_size + m];
                    }

                    x[i, j] = x_kari.Max();
                }
            }

            return x;
        }


        static DenseVector convert_matrix_to_vector(DenseMatrix y)
        {
            DenseVector f = new DenseVector(y.RowCount);

            for (int i = 0; i < f.Count; i++)
            {
                f[i] = y[i, 0];
            }
            return f;

        }

        static DenseVector convert_matrix_to_allvector(DenseMatrix x)
        {
            DenseVector y = new DenseVector(x.RowCount * x.ColumnCount);

            for (int i = 0; i < x.RowCount; i++)
            {
                for (int j = 0; j < x.ColumnCount; j++)
                {
                    y[i * x.ColumnCount + j] = x[i, j];
                }
            }

            return y;

        }

        static DenseVector relu(DenseVector y)
        {
            DenseVector f = new DenseVector(y.Count);

            for (int i = 0; i < f.Count; i++)
            {
                f[i] = Math.Max(0, y[i]);
            }
            return f;

        }

        static DenseMatrix relu_matrix(DenseMatrix x)
        {
            DenseMatrix y = new DenseMatrix(x.RowCount, x.ColumnCount);

            for (int i = 0; i < y.RowCount; i++)
            {
                for (int j = 0; j < y.ColumnCount; j++)
                {
                    y[i, j] = Math.Max(0, x[i, j]);
                }
            }
            return y;

        }

        static DenseMatrix Sigmoid(DenseMatrix y)
        {
            DenseMatrix f = new DenseMatrix(y.RowCount, y.ColumnCount);

            for (int i = 0; i < f.RowCount; i++)
            {
                for (int j = 0; j < f.ColumnCount; j++)
                {
                    f[i, j] = 1 / (1 + Math.Exp(-y[i, j]));
                }
            }
            return f;

        }

        static DenseMatrix tanH(DenseMatrix y)
        {
            DenseMatrix f = new DenseMatrix(y.RowCount, y.ColumnCount);

            for (int i = 0; i < f.RowCount; i++)
            {
                for (int j = 0; j < f.ColumnCount; j++)
                {
                    f[i, j] = Math.Tanh(y[i, j]);
                }
            }
            return f;

        }

        static DenseVector tanH_vector(DenseVector x, double kake)
        {
            DenseVector y = new DenseVector(x.Count);

            for (int i = 0; i < y.Count; i++)
            {
                y[i] = kake * Math.Tanh(x[i]);
            }

            
            return y;

        }


        static DenseVector Softmax(DenseVector y)
        {
            DenseVector f = new DenseVector(y.Count);
            if (f.Count == 1)
            {
                f = new DenseVector(2);
                double f_0 = 1.0 / (1.0 + Math.Exp(-y[0]));
                f[0] = f_0;
                f[1] = (1 - f_0);
            }
            else
            {
                for (int i = 0; i < y.Count; i++)
                {
                    double bunnbo = 0;
                    for (int j = 0; j < y.Count; j++)
                    {
                        bunnbo = bunnbo + Math.Exp(y[j] - y[i]);
                    }
                    f[i] = 1.0 / bunnbo;
                }
            }
            return f;

        }


        

    }
}
