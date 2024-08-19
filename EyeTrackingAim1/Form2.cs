using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace EyeTrackingAim1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }
        Chart chart = new Chart();
        void CreateGrapth()
        {
            chart = new Chart();
            chart.Location = new System.Drawing.Point(0, 0);
            chart.Size = new System.Drawing.Size(1920, 1080);
            chart.Series.Clear();
            chart.ChartAreas.Clear();

            string chart_area1 = "Area1";
            chart.ChartAreas.Add(new ChartArea(chart_area1));

            string legend1 = "HeadPoseX";
            chart.Series.Add(legend1);
            chart.Series[legend1].ChartType = SeriesChartType.Point;

            string legend2 = "HeadPoseY";
            chart.Series.Add(legend2);
            chart.Series[legend2].ChartType = SeriesChartType.Line;

            string legend3 = "HeadPoseZ";
            chart.Series.Add(legend3);
            chart.Series[legend3].ChartType = SeriesChartType.Point;

            string legend4 = "Z";
            chart.Series.Add(legend4);
            chart.Series[legend4].ChartType = SeriesChartType.Point;

            //string legend5 = "vt";
            //chart.Series.Add(legend5);
            //chart.Series[legend5].ChartType = SeriesChartType.Point;

            //chart.ChartAreas[0].AxisX.Maximum = 10;
            chart.ChartAreas[0].AxisY.Maximum = 1920;
            chart.ChartAreas[0].AxisY.Minimum = 0;

            //Estimate
            int esCount = 0;
            int doCount = 0;

            //EstimateClass.EstimateXALs(eyeTargetDatas, esCount);
            //Console.WriteLine(EstimateClass.xt.Count);

            //for (int i = 0; i < eyeTargetDatas[esCount].TimeStumps.Count; i++)
            //{
            //    double b = (double)((eyeTargetDatas[esCount].TimeStumps[i] - eyeTargetDatas[esCount].TimeStumps[0]) / 1000);
            //    chart.Series[legend1].Points.AddXY(b, eyeTargetDatas[esCount].EyeDataX[i]);
            //    chart.Series[legend2].Points.AddXY(b, eyeTargetDatas[esCount].TargetDataX[i]);

            //}

            //for (int i = 0; i < EstimateClass.xt.Count; i++)
            //{
            //    double b = (double)((eyeTargetDatas[esCount].TimeStumps[i] - eyeTargetDatas[esCount].TimeStumps[0]) / 1000);
            //    chart.Series[legend3].Points.AddXY(b, EstimateClass.xt[i]);
            //    chart.Series[legend4].Points.AddXY(b, EstimateClass.Z[i]);

            //}

            ////EstimateClass.EstimateXALsAll(eyeTargetDatas, esCount, doCount);
            ////Console.WriteLine(EstimateClass.xt.Count);

            ////for (int i = 0; i < eyeTargetDatas[doCount].TimeStumps.Count; i++)
            ////{
            ////    double b = (double)((eyeTargetDatas[doCount].TimeStumps[i] - eyeTargetDatas[doCount].TimeStumps[0]) / 1000);
            ////    chart.Series[legend1].Points.AddXY(b, eyeTargetDatas[doCount].EyeDataX[i]);
            ////    chart.Series[legend2].Points.AddXY(b, eyeTargetDatas[doCount].TargetDataX[i]);

            ////}

            ////for (int i = 0; i < EstimateClass.xt.Count; i++)
            ////{
            ////    double b = (double)((eyeTargetDatas[doCount].TimeStumps[i] - eyeTargetDatas[doCount].TimeStumps[0]) / 1000);
            ////    chart.Series[legend3].Points.AddXY(b, EstimateClass.xt[i]);
            ////    //chart.Series[legend4].Points.AddXY(b, EstimateClass.Z[i]);

            ////}

            //this.Controls.Add(chart);
        }
    }
}
