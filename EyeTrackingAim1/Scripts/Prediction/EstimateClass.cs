using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.Json.Serialization;
using MathNet.Numerics.LinearAlgebra.Double;


namespace EyeTrackingAim1.Scripts.Prediction
{
    public class EstimateClass
    {
        public class EstimateData
        {
            public List<double> EyeData = new List<double>();

            public List<double> TargetData = new List<double>();

            public List<long> TimeStumps = new List<long>();

        }

        public class EstimateRecordDataHozon
        {
            [JsonPropertyName("EyeDataX")] public double[] EyeDataX { get; set; } = new double[0];
            [JsonPropertyName("TargetdataX")] public double[] TargetdataX { get; set; } = new double[0];
            [JsonPropertyName("TimeStumps")] public long[] TimeStumps { get; set; } = new long[0];
            [JsonPropertyName("Time_Array")] public double[] Time_Array { get; set; } = new double[0];

        }

        public static EstimateRecordDataHozon estimateRecordDataHozon = new EstimateRecordDataHozon();

        public class Py_Pre
        {
            [JsonPropertyName("betas")] public double[][] betas { get; set; } = new double[][] { };
        }

        public class Py_svm_json_data
        {
            [JsonPropertyName("kari")] public double kari { get; set; } = 0;
            [JsonPropertyName("gamma")] public double gamma { get; set; } = 0;
            [JsonPropertyName("coef0")] public double coef0 { get; set; } = 0;
            [JsonPropertyName("xi")] public double[][] xi { get; set; } = new double[][] { };
            [JsonPropertyName("b")] public double b { get; set; } = 0;
            [JsonPropertyName("alpha")] public double[] alpha { get; set; } = new double[1];
            [JsonPropertyName("d")] public double d { get; set; } = 0;

        }

        public class Py_svm_data
        {
            public double kari = 0;
            public double gamma = 0;
            public double coef0 = 0;
            public DenseMatrix xi = new DenseMatrix(1, 1);
            public double b = 0;
            public DenseVector alpha = new DenseVector(1);
            public double d = 0;

        }

        public class mlp_data
        {
            [JsonPropertyName("coef")] public double[][] coef { get; set; } = new double[][] { };
            [JsonPropertyName("intercept")] public double[] intercept { get; set; } = new double[1];

        }

        public class output_mlp
        {
            [JsonPropertyName("hidden_layer")] public mlp_data[] hidden_layer { get; set; } = new mlp_data[1];
            [JsonPropertyName("output_layer")] public mlp_data[] output_layer { get; set; } = new mlp_data[1];
            [JsonPropertyName("of_from")] public int of_from { get; set; } = 0;
            [JsonPropertyName("of_to")] public int of_to { get; set; } = 0;
            [JsonPropertyName("sub_layer")] public mlp_data[] sub_layer { get; set; } = new mlp_data[1];
        }

        

        public class mlp_active
        {
            public DenseVector[] intercept = new DenseVector[1];
            public DenseMatrix[] coef = new DenseMatrix[1];
        }

        public class mlp_class
        {
            public mlp_active mlp_h = new mlp_active();
            public mlp_active mlp_o = new mlp_active();
            public mlp_active sub = new mlp_active();

            public int of_from = 0;
            public int of_to = 0;

            public DenseVector[] reg = new DenseVector[2];
            public DenseVector[] f = new DenseVector[2];

            public double reg_yosoku = 0;


        }

        public class output_lstm
        {
            [JsonPropertyName("w_ii")] public double[][] w_ii { get; set; } = new double[][] { };
            [JsonPropertyName("w_if")] public double[][] w_if { get; set; } = new double[][] { };
            [JsonPropertyName("w_ig")] public double[][] w_ig { get; set; } = new double[][] { };
            [JsonPropertyName("w_io")] public double[][] w_io { get; set; } = new double[][] { };

            [JsonPropertyName("b_ii")] public double[][] b_ii { get; set; } = new double[][] { };
            [JsonPropertyName("b_if")] public double[][] b_if { get; set; } = new double[][] { };
            [JsonPropertyName("b_ig")] public double[][] b_ig { get; set; } = new double[][] { };
            [JsonPropertyName("b_io")] public double[][] b_io { get; set; } = new double[][] { };

            [JsonPropertyName("w_hi")] public double[][] w_hi { get; set; } = new double[][] { };
            [JsonPropertyName("w_hf")] public double[][] w_hf { get; set; } = new double[][] { };
            [JsonPropertyName("w_hg")] public double[][] w_hg { get; set; } = new double[][] { };
            [JsonPropertyName("w_ho")] public double[][] w_ho { get; set; } = new double[][] { };

            [JsonPropertyName("b_hi")] public double[][] b_hi { get; set; } = new double[][] { };
            [JsonPropertyName("b_hf")] public double[][] b_hf { get; set; } = new double[][] { };
            [JsonPropertyName("b_hg")] public double[][] b_hg { get; set; } = new double[][] { };
            [JsonPropertyName("b_ho")] public double[][] b_ho { get; set; } = new double[][] { };
            [JsonPropertyName("output_layer")] public mlp_data[] output_layer { get; set; } = new mlp_data[1];


        }

        public class lstm_data
        {
            public DenseMatrix w_ii;
            public DenseMatrix w_if;
            public DenseMatrix w_ig;
            public DenseMatrix w_io;

            public DenseMatrix b_ii;
            public DenseMatrix b_if;
            public DenseMatrix b_ig;
            public DenseMatrix b_io;

            public DenseMatrix w_hi;
            public DenseMatrix w_hf;
            public DenseMatrix w_hg;
            public DenseMatrix w_ho;

            public DenseMatrix b_hi;
            public DenseMatrix b_hf;
            public DenseMatrix b_hg;
            public DenseMatrix b_ho;

            public mlp_active mlp_o;
            

        }

        public class lstm_clas
        {
            public lstm_data lstm = new lstm_data();
            public DenseMatrix h;
            public DenseMatrix c;
            public DenseVector[] reg = new DenseVector[2];
        }

        public class Conv1d
        {
            [JsonPropertyName("weight")] public double[][][] weight { get; set; } = new double[][][] { };
            [JsonPropertyName("bias")] public double[] bias { get; set; }
            [JsonPropertyName("padding")] public int padding { get; set; }
            [JsonPropertyName("stride")] public int stride { get; set; }
        }

        public class AvgPool1d
        {
            [JsonPropertyName("kernel_size")] public int kernel_size { get; set; }

        }

        public class output_Conv1d
        {
            [JsonPropertyName("Conv1d_array")] public Conv1d[] Conv1d_array { get; set; }
            [JsonPropertyName("Avgpool_array")] public AvgPool1d[] AvgPool1d_array { get; set; }
            [JsonPropertyName("fc_array")] public mlp_data[] fc_array { get; set; }

        }

        public class Conv1d_network
        {
            public class Conv1d_weight_class
            {
                public double[,,] Conv1d_weight;
            }
            public Conv1d_weight_class[] conv1d_weight_class;
            public DenseVector[] Conv1d_bias;
            public int[] padding ;
            public int[] stride;

            public int[] kernel_size;
            
            public DenseMatrix[] fc_weight;
            public DenseVector[] fc_bias;

            public DenseVector reg;

        }

        

        //予測
        public static mlp_class py_mtl_reg_x = new mlp_class();

        //ズレ
        //mlp
        public static mlp_class py_mlp_reg_Cali = new mlp_class();
        public static mlp_class py_mlp_cali_y = new mlp_class();
        public static mlp_class py_cali_co = new mlp_class();
        public static mlp_class py_cali_x_head = new mlp_class();
        public static mlp_class py_cali_x_pre_mlp = new mlp_class();


        public static DenseVector f_color = new double[10];

        //lstm
        public static lstm_clas py_lstm_cali_y = new lstm_clas();
        public static lstm_clas py_lstm_cali_y_flick_stop = new lstm_clas();
        //conv1d
        public static Conv1d_network py_Conv1d_cali_y = new Conv1d_network();
        public static Conv1d_network py_Conv1d_cali_y_v_reg = new Conv1d_network();
        public static Conv1d_network py_Conv1d_cali_x_pre = new Conv1d_network();
        public static Conv1d_network py_Conv1d_cali_x_pre_label = new Conv1d_network();
    }
}
