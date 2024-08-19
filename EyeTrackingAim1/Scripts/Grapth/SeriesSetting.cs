using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using EyeTrackingAim1.Scripts.Calibration;
using EyeTrackingAim1.Scripts.Prediction;
using System.Drawing;

namespace EyeTrackingAim1.Scripts.Grapth
{
    public class SeriesSetting
    {
        public static void Add_RawEye_chart_series(EstimateClass.EstimateRecordDataHozon esh, int num)
        {
            string grapth_name = "RawEye" + num.ToString();
            Color grapth_color = Color.Red;
            GrapthSetting.chart.Series.Add(grapth_name);
            GrapthSetting.chart.Series[grapth_name].ChartType = SeriesChartType.Point;
            GrapthSetting.chart.Series[grapth_name].Color = grapth_color;
            GrapthSetting.chart.Series[grapth_name].MarkerSize = 10;
            GrapthSetting.chart.Series[grapth_name].MarkerStyle = MarkerStyle.Circle;

            double time = 0;
            for (int i = 0; i < esh.EyeDataX.Length; i++)
            {
                double point = 0;
                double dt = 0;

                if (i == 0)
                {
                    dt = 0;
                    time = 0;
                }
                else
                {
                    dt = (double)(esh.TimeStumps[i] - esh.TimeStumps[i - 1]) / 1000000.0;
                    time = time + dt;
                }

                point = esh.EyeDataX[i];
                GrapthSetting.chart.Series[grapth_name].Points.AddXY(time, point);
            }

            
        }


        public static void Add_Target_chart_series(EstimateClass.EstimateRecordDataHozon esh, int num)
        {
            string grapth_name = "Target" + num.ToString();
            Color grapth_color = Color.Blue;
            GrapthSetting.chart.Series.Add(grapth_name);
            GrapthSetting.chart.Series[grapth_name].ChartType = SeriesChartType.Point;
            GrapthSetting.chart.Series[grapth_name].Color = grapth_color;
            GrapthSetting.chart.Series[grapth_name].MarkerSize = 10;
            GrapthSetting.chart.Series[grapth_name].MarkerStyle = MarkerStyle.Circle;

            double time = 0;
            for (int i = 0; i < esh.EyeDataX.Length; i++)
            {
                double point = 0;
                double dt = 0;

                if (i == 0)
                {
                    dt = 0;
                    time = 0;
                }
                else
                {
                    dt = (double)(esh.TimeStumps[i] - esh.TimeStumps[i - 1]) / 1000000.0;
                    time = time + dt;
                }

                point = esh.TargetdataX[i];
                GrapthSetting.chart.Series[grapth_name].Points.AddXY(time, point);
            }

            
        }


        public static void Add_EstimateEye_chart_series(EstimateClass.EstimateRecordDataHozon esh, int num)
        {
            string grapth_name = "EstimateEye" + num.ToString();
            Color grapth_color = Color.Purple;
            GrapthSetting.chart.Series.Add(grapth_name);
            GrapthSetting.chart.Series[grapth_name].ChartType = SeriesChartType.Point;
            GrapthSetting.chart.Series[grapth_name].Color = grapth_color;
            GrapthSetting.chart.Series[grapth_name].MarkerSize = 10;
            GrapthSetting.chart.Series[grapth_name].MarkerStyle = MarkerStyle.Circle;

            double time = 0;
            double pointes = 0;

            for (int i = 0; i < esh.EyeDataX.Length; i++)
            {

                double kasokudo = 0;
                double velo = 0;
                double dt = 0;

                if (i == 0)
                {
                    dt = 0;
                    velo = 0;
                    time = 0;
                }
                else
                {
                    dt = (double)(esh.TimeStumps[i] - esh.TimeStumps[i - 1]) / 1000000.0;
                    velo = (esh.EyeDataX[i] - esh.EyeDataX[i - 1]) / dt;
                    time = time + dt;
                }


                if (i == 0 || i == 1)
                {
                    kasokudo = 0;
                }
                else
                {
                    double dt2 = (double)(esh.TimeStumps[i - 1] - esh.TimeStumps[i - 2]) / 1000000.0;
                    double velo2 = (esh.EyeDataX[i - 1] - esh.EyeDataX[i - 2]) / dt2;
                    kasokudo = (velo2 - velo) / dt;
                }


                if (i == 0 || i == 1)
                {
                    pointes = 0;
                }
                else
                {
                    double point = esh.EyeDataX[i];
                    double namasi = 0.5;
                    //pointes = (1 - namasi) * pointes + (namasi) * (point + ActiveEyeKannsuu.ALSKeisuu(velo));
                    pointes = 1;
                }


                GrapthSetting.chart.Series[grapth_name].Points.AddXY(time, pointes);
            }

        }


        public static void Add_General_chart_series(EstimateClass.EstimateRecordDataHozon esh, int num, double min, double max, bool limit, Color color0, Color color1, string name)
        {
            string grapth_name = "General" + name + num.ToString();
            GrapthSetting.chart.Series.Add(grapth_name);
            GrapthSetting.chart.Series[grapth_name].ChartType = SeriesChartType.Point;
            
            GrapthSetting.chart.Series[grapth_name].MarkerSize = 10;
            GrapthSetting.chart.Series[grapth_name].MarkerStyle = MarkerStyle.Circle;

            Color grapth_color = Color.FromArgb(255, 0, 255, 0);

            double rAcum = 0;
            double gAcum = 255;
            double bAcum = 0;

            double time = 0;      
            int counter = 0;

            double veloheikin = 0;
            double kasokudoheikin = 0;

            double[] veh = new double[5];
            

            for (int i = 0; i < esh.EyeDataX.Length; i++)
            {
                double[] ve = new double[5];
                double kasokudo = 0;
                double velo = 0;
                double dt = 0;

                if (i == 0)
                {
                    dt = 0; 
                    velo = 0;
                    time = 0;
                    
                }
                else
                {
                    dt = (double)(esh.TimeStumps[i] - esh.TimeStumps[i - 1]) / 1000000.0;
                    velo = (esh.EyeDataX[i] - esh.EyeDataX[i - 1]) / dt;
                    time = time + dt;
                }


                if (i == 0 || i == 1)
                {
                    kasokudo = 0;
                }
                else
                {
                    double dt2 = (double)(esh.TimeStumps[i - 1] - esh.TimeStumps[i - 2]) / 1000000.0;
                    double velo2 = (esh.EyeDataX[i - 1] - esh.EyeDataX[i - 2]) / dt2;
                    kasokudo = (velo2 - velo) / dt;
                }

                

                double velomomin = 0.9 * veloheikin + 0.1 * Math.Abs(velo);
                double kasokudomin = 0.9 * kasokudoheikin + 0.1 * Math.Abs(kasokudo);
                //dainyuu
                veloheikin = velomomin;
                kasokudoheikin = kasokudomin;



                ve[0] = 0.95 * veh[0] + 0.05 * Math.Abs(velo);
                veh[0] = ve[0];

                ve[1] = 0.99 * veh[1] + 0.01 * Math.Abs(velo);
                veh[1] = ve[1];



                double x = velomomin;
                double y = ve[0];

                if (limit)
                {
                    if (time >= min && time <= max)
                    {
                        GrapthSetting.chart.Series[grapth_name].Points.AddXY(x, y);
                        counter = counter + 1;
                    }

                } else
                {
                    //if (Math.Abs(y) < 100)
                    {
                        GrapthSetting.chart.Series[grapth_name].Points.AddXY(x, y);
                        counter = counter + 1;
                    }
                    
                }
                
               
            }

            double t = 1.0 / counter;
            double ac = color1.R - color0.R;
            double bc = color1.G - color0.G;
            double cc = color1.B - color0.B;

            for (int i = 0; i < counter; i++)
            {
                rAcum = color0.R + ac * t;
                gAcum = color0.G + bc * t;
                bAcum = color0.B + cc * t;

                grapth_color = Color.FromArgb(200, (int)rAcum, (int)gAcum, (int)bAcum);
                GrapthSetting.chart.Series[grapth_name].Points[i].Color = grapth_color;

                t = t + 1.0 / counter;
            }
            

        }


        public static void Add_Manual_chart_series(EstimateClass.EstimateRecordDataHozon esh, int num)
        {
            string grapth_name = "Manual" + num.ToString();
            Color grapth_color = Color.Orange;
            GrapthSetting.chart.Series.Add(grapth_name);
            GrapthSetting.chart.Series[grapth_name].ChartType = SeriesChartType.Point;
            GrapthSetting.chart.Series[grapth_name].Color = grapth_color;
            GrapthSetting.chart.Series[grapth_name].MarkerSize = 10;
            GrapthSetting.chart.Series[grapth_name].MarkerStyle = MarkerStyle.Circle;

            double[] Manual = new double[1000];
            for (int i = 0; i < Manual.Length; i++)
            {
                if (i == 0)
                {
                    Manual[i] = 0;
                }
                else
                {
                    Manual[i] = Manual[i - 1] + 1;
                }


                double bb = 0.8;
                double aa = (2000 / Math.Pow(200, bb));
                double yyy = 0.8 * aa * Math.Pow(Manual[i], bb) + 1000;
                GrapthSetting.chart.Series[grapth_name].Points.AddXY(Manual[i], yyy);
            }
        }










































































































    }
}
