using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EyeTrackingAim1
{
    public partial class eyecenter1 : Form
    {
        public eyecenter1()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Brush TargetBash = new SolidBrush(Color.FromArgb(255, 255, 255, 255));
            Pen TargetPen = new Pen(Color.White);


            Rectangle TargetRect = new Rectangle((int)0, (int)0, this.Size.Width, this.Size.Height);

            e.Graphics.FillRectangle(TargetBash, TargetRect);
            e.Graphics.DrawRectangle(TargetPen, TargetRect);

            TargetBash.Dispose();
            TargetPen.Dispose();

            //e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //e.Graphics.DrawImage(Form1.bitmap0, 0, 0, 1920, 1080);


        }
    }
}
