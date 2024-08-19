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
using EyeTrackingAim1.Scripts.Prediction;
using EyeTrackingAim1.Scripts.Calibration;
using EyeTrackingAim1.Scripts.Grapth;

namespace EyeTrackingAim1
{
    public partial class EstimateGrapth : Form
    {
        public EstimateGrapth()
        {
            InitializeComponent();

            EstimateClass.EstimateRecordDataHozon esh = EstimateClass.estimateRecordDataHozon;
            

            GrapthSetting.Init_chart_setting(this);


            SeriesSetting.Add_RawEye_chart_series(esh, 0);
            SeriesSetting.Add_Target_chart_series(esh, 0);

            double min = Math.Min(esh.EyeDataX.Min(), esh.TargetdataX.Min());
            double max = Math.Max(esh.EyeDataX.Max(), esh.TargetdataX.Max());
            

            GrapthSetting.Axis_Setting(min, max);
        }
            
    }
}
