using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using MathNet.Numerics.LinearAlgebra.Double;
using EyeTrackingAim1.Scripts.KannsuuHozon;

namespace EyeTrackingAim1.Scripts.Prediction
{
    public class RecordPrediction
    {
        
        static EstimateClass.mlp_active set_mlpdata(EstimateClass.mlp_data[] mlp_data)
        {
            EstimateClass.mlp_active mlp_active = new EstimateClass.mlp_active();
            mlp_active.coef = new DenseMatrix[mlp_data.Length];
            mlp_active.intercept = new DenseVector[mlp_data.Length];

            for (int i = 0; i < mlp_data.Length; i++)
            {
                DenseVector intercept = new DenseVector(mlp_data[i].intercept);
                DenseMatrix coef = new DenseMatrix(mlp_data[i].coef.Length, mlp_data[i].coef[0].Length);

                for (int a = 0; a < coef.RowCount; a++)
                {
                    for (int b = 0; b < coef.ColumnCount; b++)
                    {
                        coef[a, b] = mlp_data[i].coef[a][b];
                    }
                }

                mlp_active.coef[i] = coef;
                mlp_active.intercept[i] = intercept;
            }


            return mlp_active;
        }


        public static EstimateClass.mlp_class Recive_Py_mlp(string filename)
        {

            FileStream filestream = new FileStream(filename, FileMode.Open);
            byte[] read = new byte[filestream.Length];
            filestream.Read(read, 0, read.Length);
            string jsonstr = System.Text.Encoding.GetEncoding("Shift_JIS").GetString(read);
            filestream.Close();
            EstimateClass.output_mlp jsondata = JsonSerializer.Deserialize<EstimateClass.output_mlp>(jsonstr);

            EstimateClass.mlp_class mlp_Class = new EstimateClass.mlp_class();
            mlp_Class.mlp_h = set_mlpdata(jsondata.hidden_layer);
            mlp_Class.mlp_o = set_mlpdata(jsondata.output_layer);     
            mlp_Class.sub = set_mlpdata(jsondata.sub_layer);

            mlp_Class.of_from = jsondata.of_from;
            mlp_Class.of_to = jsondata.of_to;

            return mlp_Class;

        }

        public static DegitalFilter.Filter_Keisuu Receive_Filter_Keisuu(string filename)
        {

            FileStream filestream = new FileStream(filename, FileMode.Open);
            byte[] read = new byte[filestream.Length];
            filestream.Read(read, 0, read.Length);
            string jsonstr = System.Text.Encoding.GetEncoding("Shift_JIS").GetString(read);
            filestream.Close();

            DegitalFilter.Degital_Filter_Keisuu_json jsondata = JsonSerializer.Deserialize<DegitalFilter.Degital_Filter_Keisuu_json>(jsonstr);
            
            DegitalFilter.Filter_Keisuu filter_Keisuu = new DegitalFilter.Filter_Keisuu(jsondata.a, jsondata.b);

            return filter_Keisuu;
        }

        public static DegitalFilter.Filter_Keisuu_invvrese Receive_Filter_inverse_Keisuu(string filename)
        {

            FileStream filestream = new FileStream(filename, FileMode.Open);
            byte[] read = new byte[filestream.Length];
            filestream.Read(read, 0, read.Length);
            string jsonstr = System.Text.Encoding.GetEncoding("Shift_JIS").GetString(read);
            filestream.Close();

            DegitalFilter.Degital_Filter_Keisuu_json jsondata = JsonSerializer.Deserialize<DegitalFilter.Degital_Filter_Keisuu_json>(jsonstr);

            DegitalFilter.Filter_Keisuu_invvrese filter_Keisuu = new DegitalFilter.Filter_Keisuu_invvrese(jsondata.a, jsondata.b);

            return filter_Keisuu;
        }


        public static EstimateClass.lstm_clas Receive_lstm_Keisuu(string filename)
        {

            FileStream filestream = new FileStream(filename, FileMode.Open);
            byte[] read = new byte[filestream.Length];
            filestream.Read(read, 0, read.Length);
            string jsonstr = System.Text.Encoding.GetEncoding("Shift_JIS").GetString(read);
            filestream.Close();

            EstimateClass.output_lstm jsondata = JsonSerializer.Deserialize<EstimateClass.output_lstm>(jsonstr);
            EstimateClass.lstm_clas lstmoutput = new EstimateClass.lstm_clas();


            lstmoutput.lstm.w_ii = convert_array_to_matrix(jsondata.w_ii);
            lstmoutput.lstm.w_if = convert_array_to_matrix(jsondata.w_if);
            lstmoutput.lstm.w_ig = convert_array_to_matrix(jsondata.w_ig);
            lstmoutput.lstm.w_io = convert_array_to_matrix(jsondata.w_io);
            lstmoutput.lstm.b_ii = convert_array_to_matrix(jsondata.b_ii);
            lstmoutput.lstm.b_if = convert_array_to_matrix(jsondata.b_if);
            lstmoutput.lstm.b_ig = convert_array_to_matrix(jsondata.b_ig);
            lstmoutput.lstm.b_io = convert_array_to_matrix(jsondata.b_io);

            lstmoutput.lstm.w_hi = convert_array_to_matrix(jsondata.w_hi);
            lstmoutput.lstm.w_hf = convert_array_to_matrix(jsondata.w_hf);
            lstmoutput.lstm.w_hg = convert_array_to_matrix(jsondata.w_hg);
            lstmoutput.lstm.w_ho = convert_array_to_matrix(jsondata.w_ho);
            lstmoutput.lstm.b_hi = convert_array_to_matrix(jsondata.b_hi);
            lstmoutput.lstm.b_hf = convert_array_to_matrix(jsondata.b_hf);
            lstmoutput.lstm.b_hg = convert_array_to_matrix(jsondata.b_hg);
            lstmoutput.lstm.b_ho = convert_array_to_matrix(jsondata.b_ho);

            lstmoutput.lstm.mlp_o = set_mlpdata(jsondata.output_layer);
            

            lstmoutput.h = new DenseMatrix(lstmoutput.lstm.w_ii.RowCount, 1);
            lstmoutput.c = new DenseMatrix(lstmoutput.lstm.w_ii.RowCount, 1);

            return lstmoutput;
        }

        public static EstimateClass.Conv1d_network ReceiveConv1d_network(string filename)
        {
            FileStream filestream = new FileStream(filename, FileMode.Open);
            byte[] read = new byte[filestream.Length];
            filestream.Read(read, 0, read.Length);
            string jsonstr = System.Text.Encoding.GetEncoding("Shift_JIS").GetString(read);
            filestream.Close();

            EstimateClass.output_Conv1d jsondata = JsonSerializer.Deserialize<EstimateClass.output_Conv1d>(jsonstr);
            
            EstimateClass.Conv1d_network Conv1d_output = new EstimateClass.Conv1d_network();

            Conv1d_output.conv1d_weight_class = new EstimateClass.Conv1d_network.Conv1d_weight_class[jsondata.Conv1d_array.Length];
            Conv1d_output.Conv1d_bias = new DenseVector[jsondata.Conv1d_array.Length];
            Conv1d_output.padding = new int[jsondata.Conv1d_array.Length];
            Conv1d_output.stride = new int[jsondata.Conv1d_array.Length];

            for (int i = 0; i < jsondata.Conv1d_array.Length; i++)
            {
                Conv1d_output.conv1d_weight_class[i] = new EstimateClass.Conv1d_network.Conv1d_weight_class();
                Conv1d_output.conv1d_weight_class[i].Conv1d_weight = convert_jagarray_to_array(jsondata.Conv1d_array[i].weight);
                Conv1d_output.Conv1d_bias[i] = convert_array_to_vector(jsondata.Conv1d_array[i].bias);
                Conv1d_output.padding[i] = jsondata.Conv1d_array[i].padding;
                Conv1d_output.stride[i] = jsondata.Conv1d_array[i].stride;

            }


            Conv1d_output.kernel_size = new int[jsondata.AvgPool1d_array.Length];
            for (int i = 0; i < jsondata.AvgPool1d_array.Length; i++)
            {
                Conv1d_output.kernel_size[i] = jsondata.AvgPool1d_array[i].kernel_size;
            }

            Conv1d_output.fc_weight = new DenseMatrix[jsondata.fc_array.Length];
            Conv1d_output.fc_bias = new DenseVector[jsondata.fc_array.Length];
            for (int i = 0; i < jsondata.fc_array.Length; i++)
            {
                Conv1d_output.fc_weight[i] = convert_array_to_matrix(jsondata.fc_array[i].coef);
                Conv1d_output.fc_bias[i] = convert_array_to_vector(jsondata.fc_array[i].intercept);
            }

            return Conv1d_output;

        }

        static DenseMatrix convert_array_to_matrix(double[][] data)
        {
            DenseMatrix re = new DenseMatrix(data.Length, data[0].Length);
            for (int i = 0; i < re.RowCount; i++)
            {
                for (int j = 0; j < re.ColumnCount; j++)
                {
                    re[i, j] = data[i][j];
                }
            }

            return re;
        }

        static DenseVector convert_array_to_vector(double[] data)
        {
            DenseVector re = new DenseVector(data.Length);
            for (int i = 0; i < re.Count; i++)
            {
                re[i] = data[i];       
            }

            return re;
        }

        static DenseMatrix convert_vector_to_matrix(DenseVector data, int flag)
        {
            DenseMatrix re;

            if (flag == 0)
            {
                re = new DenseMatrix(data.Count, 1);
                for (int i = 0; i < data.Count; i++)
                {
                    re[i, 0] = data[i];
                }
            }
            else
            {
                re = new DenseMatrix(1, data.Count);
                for (int i = 0; i < data.Count; i++)
                {
                    re[0, i] = data[i];
                }
            }

            return re;

        }

        static double[,,] convert_jagarray_to_array(double[][][] data)
        {
            double[,,] re = new double[data.Length, data[0].Length, data[0][0].Length];

            for (int i = 0; i < re.GetLength(0); i++)
            {
                for (int j = 0; j < re.GetLength(1); j++)
                {
                    for (int k = 0; k < re.GetLength(2); k++)
                    {
                        re[i, j, k] = data[i][j][k];
                    }
                }
            }

            return re;
        }

        public static void RecoldPredictionDataHozon(EstimateClass.EstimateRecordDataHozon record , string filename)
        {
            string jsonstr = JsonSerializer.Serialize(record);


            FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write);
            byte[] write = System.Text.Encoding.GetEncoding("Shift_JIS").GetBytes(jsonstr);
            fileStream.Write(write, 0, write.Length);
            fileStream.Close();
        }


        public static EstimateClass.EstimateRecordDataHozon RecivePredictionHozon(string filename)
        {

            FileStream filestream = new FileStream(filename, FileMode.Open);
            byte[] read = new byte[filestream.Length];
            filestream.Read(read, 0, read.Length);
            string jsonstr = System.Text.Encoding.GetEncoding("Shift_JIS").GetString(read);
            filestream.Close();


            return JsonSerializer.Deserialize<EstimateClass.EstimateRecordDataHozon>(jsonstr);
        }
    }
}
