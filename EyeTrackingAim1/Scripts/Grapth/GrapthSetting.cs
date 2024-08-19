using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using EyeTrackingAim1.Scripts.Calibration;
using System.Drawing;

namespace EyeTrackingAim1.Scripts.Grapth
{
    public class GrapthSetting
    {
        public static Chart chart;
        public static void Init_chart_setting(EstimateGrapth estimateGrapth)
        {
            chart = new Chart();
            chart.Location = new Point(0, 0);
            chart.Size = new Size(1920, 1080);

            string chart_area1 = "Area1";
            chart.ChartAreas.Add(new ChartArea(chart_area1));

            chart.Show();
            estimateGrapth.Controls.Add(chart);

            
        }

        public static void Axis_Setting(double Ymin, double Ymax)
        {
            chart.ChartAreas[0].AxisY.Minimum = Ymin;
            chart.ChartAreas[0].AxisY.Maximum = Ymax;
        }
       

    }
}
