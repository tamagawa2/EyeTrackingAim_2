using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EyeTrackingAim1.Scripts.Face_Byouga;
using System.Numerics;

namespace EyeTrackingAim1
{
    public partial class Face_Byouga_Form : Form
    {
        public Face_Byouga_Form()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Brush brush_enn0 = new SolidBrush(Color.Red);
            Brush brush_enn1 = new SolidBrush(Color.Blue);
            Brush brush_enn2 = new SolidBrush(Color.Green);

            for (int i = 0; i < Byouga_Setting.enn_0.Length; i++)
            {
                Vector2 s_p = Byouga_Setting.GetScreenPoint(Byouga_Setting.enn_0[i], this.Width, this.Height);
                Rectangle Rect_enn = new Rectangle((int)(s_p.X), (int)(s_p.Y), 1, 1);
                e.Graphics.FillRectangle(brush_enn0, Rect_enn);
            }

            for (int i = 0; i < Byouga_Setting.enn_1.Length; i++)
            {
                Vector2 s_p = Byouga_Setting.GetScreenPoint(Byouga_Setting.enn_1[i], this.Width, this.Height);
                Rectangle Rect_enn = new Rectangle((int)(s_p.X), (int)(s_p.Y), 1, 1);
                e.Graphics.FillRectangle(brush_enn1, Rect_enn);
            }

            for (int i = 0; i < Byouga_Setting.enn_2.Length; i++)
            {
                Vector2 s_p = Byouga_Setting.GetScreenPoint(Byouga_Setting.enn_2[i], this.Width, this.Height);
                Rectangle Rect_enn = new Rectangle((int)(s_p.X), (int)(s_p.Y), 1, 1);
                e.Graphics.FillRectangle(brush_enn2, Rect_enn);
            }

        }
    }
}
