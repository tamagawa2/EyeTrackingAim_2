using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EyeTrackingAim1.Scripts.Calibration;
using EyeTrackingAim1.Scripts.Prediction;

namespace EyeTrackingAim1
{
    public partial class CalibrationCenter : Form
    {
        Form1 form;
        public CalibrationCenter(Form1 form1)
        {
            InitializeComponent();
            form = form1;
            double we = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2.0;
            double he = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 2.0;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // フォントを定義
            /*Font font = new Font("Times New Roman", 10, FontStyle.Regular);

            SolidBrush brush2 = new SolidBrush(Color.FromArgb(255, 50, 50, 255));
            string HRstring = "ActiveEyeKannsuu.fhozon[3]  " + (ActiveEyeKannsuu.fhozon[3]).ToString();
            string offsethabitxstring = "offsethabitx  " + CalibrationClass.caribrationResult.offsethabitx.ToString() + 
                                        "   offsethabitmousex  " + CalibrationClass.caribrationResult.offsethabitmousex.ToString();
           

            e.Graphics.DrawString(HRstring, font, 
            brush2, (int)(0), (int)(form.calibrationCenter.Size.Height / 2.0 + 25));

            e.Graphics.DrawString(offsethabitxstring, font,
            brush2, (int)(0), (int)(form.calibrationCenter.Size.Height / 2.0 + 50));

            font.Dispose();
            brush2.Dispose();*/

            
        }
    }
}
