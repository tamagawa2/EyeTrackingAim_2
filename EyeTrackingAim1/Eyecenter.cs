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
    public partial class Eyecenter : Form
    {
        Form1 form;

        public Eyecenter(Form1 form1)
        {
            form = form1;
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Pen pen = new Pen(Color.FromArgb(255, 50, 255, 50), 1);
            Brush brush = new SolidBrush(Color.FromArgb(255, 255, 0, 0));

            Rectangle rect = new Rectangle(0, 0, (int)(this.Size.Width), (int)(this.Size.Height));
            
            
            e.Graphics.FillRectangle(brush, rect);
            

            pen.Dispose();
            brush.Dispose();
           


        }
    }
}
