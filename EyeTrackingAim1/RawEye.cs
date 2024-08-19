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
    public partial class RawEye : Form
    {
        Form1 form;
       
        public RawEye(Form1 form1)
        {
            InitializeComponent();

            form = form1;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int r = 10;
            Pen pen = new Pen(Color.FromArgb(255, 255, 255, 255), r);
            Brush brush = new SolidBrush(pen.Color);
            Rectangle rect = new Rectangle(r, r, (int)(this.Size.Width - 2 * r), (int)(this.Size.Height - 2 * r));

            
            if (Form1.eyeDatas[Form1.nowvalue].UseRawEye == false)
            {
                e.Graphics.DrawEllipse(pen, rect);
            }

            pen.Dispose();
            brush.Dispose();
        }
    }
}
