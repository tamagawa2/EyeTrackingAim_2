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
    public partial class eyecenterkuro : Form
    {
        public eyecenterkuro()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


            Brush TargetBash = new SolidBrush(Color.FromArgb(255, 0, 0, 0));
            Pen TargetPen = new Pen(Color.FromArgb(255, 0, 0, 0));


            Rectangle TargetRect = new Rectangle((int)0, (int)0, this.Size.Width, this.Size.Height);

            e.Graphics.FillRectangle(TargetBash, TargetRect);
            e.Graphics.DrawRectangle(TargetPen, TargetRect);

            TargetBash.Dispose();
            TargetPen.Dispose();




        }
    }
}
