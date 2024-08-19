using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EyeTrackingAim1.Scripts.EyeData;

namespace EyeTrackingAim1
{
    public partial class UserControl2 : UserControl
    {
        Form1 form;
        public bool InputKeyflag = false;
        public bool ReleaseKeyflag = false;
        public bool ForcedreleaseEyeTrackingAimflag = false;
        public UserControl2(Form1 form1)
        {
            InitializeComponent();

            form = form1;

            checkBoxShowBall.Checked = true;
            checkBoxRawEyePoint.Checked = Form1.eyeDatas[Form1.nowvalue].UseRawEye;
            checkBoxCancelY.Checked = Form1.eyeDatas[Form1.nowvalue].CancelY;
            checkBoxUseMagWindow.Checked = Form1.eyeDatas[Form1.nowvalue].UseMagWindow;

            textBoxSensitivityX.ImeMode = ImeMode.Disable;
            textBoxSensitivityY.ImeMode = ImeMode.Disable;
            textBoxMagnification.ImeMode = ImeMode.Disable;            
            textBoxFovWidth.ImeMode = ImeMode.Disable;
            textBoxDelayTime.ImeMode = ImeMode.Disable;
            textBoxOffsetX.ImeMode = ImeMode.Disable;
            textBoxOffsetY.ImeMode = ImeMode.Disable;           
            textBox1FPS.ImeMode = ImeMode.Disable;
            textBoxEyeCorectionRange.ImeMode = ImeMode.Disable;
           
            


            textBoxAppName.Text = Form1.eyeDatas[Form1.nowvalue].Appname;
            textBoxSensitivityX.Text = Form1.eyeDatas[Form1.nowvalue].Sensitivity.X.ToString();
            textBoxSensitivityY.Text = Form1.eyeDatas[Form1.nowvalue].Sensitivity.Y.ToString();
            textBoxMagnification.Text = Form1.eyeDatas[Form1.nowvalue].Magnification.ToString();            
            textBoxFovWidth.Text = Form1.eyeDatas[Form1.nowvalue].FovW.ToString();
            textBoxDelayTime.Text = Form1.eyeDatas[Form1.nowvalue].DelayTime.ToString();
            textBoxOffsetX.Text = Form1.eyeDatas[Form1.nowvalue].Offset.X.ToString();
            textBoxOffsetY.Text = Form1.eyeDatas[Form1.nowvalue].Offset.Y.ToString();            
            textBox1FPS.Text = Form1.eyeDatas[Form1.nowvalue].FPS.ToString();
            textBoxEyeCorectionRange.Text = Form1.eyeDatas[Form1.nowvalue].EyeCorectionRange.ToString();
            

            buttonInputKey.Text = Form1.eyeDatas[Form1.nowvalue].InputKey.ToString();
            buttonReleaseEyeTrackingKey.Text = Form1.eyeDatas[Form1.nowvalue].ReleaseEyeTrackingKey.ToString();
            buttonForcedreleaseEyeTrackingAim.Text = Form1.eyeDatas[Form1.nowvalue].ForcedreleaseEyeTrackingAimKey.ToString();



        }

        
        

        private void buttonInputKey_Click(object sender, EventArgs e)
        {
            InputKeyflag = true;
            Dialog dialog = new Dialog(this);
            dialog.ShowDialog();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.eyeDatas[Form1.nowvalue].Sensitivity = new System.Windows.Vector(double.Parse(textBoxSensitivityX.Text), double.Parse(textBoxSensitivityY.Text));               
                Form1.eyeDatas[Form1.nowvalue].FovW = double.Parse(textBoxFovWidth.Text);
                Form1.eyeDatas[Form1.nowvalue].DelayTime = double.Parse(textBoxDelayTime.Text);
                Form1.eyeDatas[Form1.nowvalue].Offset = new System.Windows.Vector(double.Parse(textBoxOffsetX.Text), double.Parse(textBoxOffsetY.Text));
                Form1.eyeDatas[Form1.nowvalue].Magnification = double.Parse(textBoxMagnification.Text);
                Form1.eyeDatas[Form1.nowvalue].EyeCorectionRange = double.Parse(textBoxEyeCorectionRange.Text);
               
            }
            catch { MessageBox.Show("すうじだけ", "かくにん");

                return;
            };

            try
            {              
                Form1.eyeDatas[Form1.nowvalue].FPS = int.Parse(textBox1FPS.Text);
                
            } catch
            {
                MessageBox.Show("せいすうだけ", "かくにん");

                return;
            };

            Recold.RecoldEyeData(Form1.eyeDatas);

            Form1.userControl1 = new UserControl1(form);
            Form1.userControl1.Anchor = AnchorStyles.Top;
            form.Controls.Add(Form1.userControl1);
            this.Dispose();
        }

        private void buttonReleaseEyeTrackingKey_Click(object sender, EventArgs e)
        {
            ReleaseKeyflag = true;
            Dialog dialog = new Dialog(this);
            dialog.ShowDialog();
        }

       

        private void textBoxAppName_TextChanged(object sender, EventArgs e)
        {
            Form1.eyeDatas[Form1.nowvalue].Appname = textBoxAppName.Text;
        }

        private void checkBoxShowBall_CheckedChanged(object sender, EventArgs e)
        {
            Form1.ShowBallFlag = checkBoxShowBall.Checked;
        }

        private void checkBoxRawEyePoint_CheckedChanged(object sender, EventArgs e)
        {
            Form1.eyeDatas[Form1.nowvalue].UseRawEye = checkBoxRawEyePoint.Checked;
        }

        private void checkBoxCancelY_CheckedChanged(object sender, EventArgs e)
        {
            Form1.eyeDatas[Form1.nowvalue].CancelY = checkBoxCancelY.Checked;
        }

        private void checkBoxUseMagWindow_CheckedChanged(object sender, EventArgs e)
        {
            Form1.eyeDatas[Form1.nowvalue].UseMagWindow = checkBoxUseMagWindow.Checked;
        }

        private void buttonForcedreleaseEyeTrackingAim_Click(object sender, EventArgs e)
        {
            ForcedreleaseEyeTrackingAimflag = true;
            Dialog dialog = new Dialog(this);
            dialog.ShowDialog();
        }
    }
}
