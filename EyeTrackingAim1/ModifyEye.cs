using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EyeTrackingAim1.Scripts.EyeTrackingAim;
using EyeTrackingAim1.Scripts.Prediction;
using EyeTrackingAim1.Scripts.Calibration;
using MathNet.Numerics.LinearAlgebra.Double;

namespace EyeTrackingAim1
{
    public partial class ModifyEye : Form
    {
        Form1 form;
        public ModifyEye(Form1 form1)
        {
            InitializeComponent();
            form = form1;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


            int r = 10;
            Pen pen = new Pen(Color.FromArgb(255, (int)(255 * EstimateClass.f_color[0]), (int)(255 * EstimateClass.f_color[1]), (int)(255 * EstimateClass.f_color[2])), r);
            
            //Pen pen = new Pen(Color.Red);
            if (DoEyeTrackingAim.EyeTrackingAimState == 1 || DoEyeTrackingAim.EyeTrackingAimState == 2)
            { 
                pen = new Pen(Color.FromArgb(255, 0, 255, 0), r);
                if (DoEyeTrackingAim.EyeState == 1 || DoEyeTrackingAim.EyeState == 2)
                {
                    pen = new Pen(Color.FromArgb(255, 255, 41, 91), r);
                }

            } else if (DoEyeTrackingAim.offsetflag == 1)
            {
                pen = new Pen(Color.Yellow, r);
            }
           

            Rectangle rect = new Rectangle(r, r, (int)(this.Size.Width - 2 * r), (int)(this.Size.Height - 2 * r));     
            Brush brush = new SolidBrush(pen.Color);

            if (Form1.eyeDatas[Form1.nowvalue].UseRawEye == false)
            {
                e.Graphics.DrawEllipse(pen, rect);
            }
            
           

            pen.Dispose();
            brush.Dispose();


        }
    }
}
