using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EyeTrackingAim1.Scripts.EyeData;
namespace EyeTrackingAim1
{
    class Dialog : Form
    {
        private Label labelShowInputKey;
        UserControl2 user;
        public Dialog(UserControl2 user2)
        {
            InitializeComponent();

            user = user2;

            this.Text = "InputKey";

            // ダイアログボックス用の設定
            this.MaximizeBox = false;         // 最大化ボタン
            this.MinimizeBox = false;         // 最小化ボタン
            this.ShowInTaskbar = false;       // タスクバー上に表示
            this.FormBorderStyle =
                FormBorderStyle.FixedDialog;  // 境界のスタイル
            this.StartPosition =
                FormStartPosition.CenterParent;  // 親フォームの中央に配置

            if (user.InputKeyflag)
            {
                labelShowInputKey.Text = Form1.eyeDatas[Form1.nowvalue].InputKey.ToString() + "    がせっていされてる";
            }


            if (user.ReleaseKeyflag)
            {
                labelShowInputKey.Text = Form1.eyeDatas[Form1.nowvalue].ReleaseEyeTrackingKey.ToString() + "    がせっていされてる";
            }

            if (user.ForcedreleaseEyeTrackingAimflag)
            {
                labelShowInputKey.Text = Form1.eyeDatas[Form1.nowvalue].ForcedreleaseEyeTrackingAimKey.ToString() + "    がせっていされてる";
            }

            
            
            this.KeyDown += Dialog_KeyDown;
        }

        void Dialog_KeyDown(object sender , KeyEventArgs e)
        {
            if (user.InputKeyflag)
            {
                user.InputKeyflag = false;
                Form1.eyeDatas[Form1.nowvalue].InputKey = (KeyData)e.KeyCode;
                user.buttonInputKey.Text = ((KeyData)e.KeyCode).ToString();
            }

            if (user.ReleaseKeyflag)
            {
                user.ReleaseKeyflag = false;
                Form1.eyeDatas[Form1.nowvalue].ReleaseEyeTrackingKey = (KeyData)e.KeyCode;
                user.buttonReleaseEyeTrackingKey.Text = ((KeyData)e.KeyCode).ToString();
            }

            if (user.ForcedreleaseEyeTrackingAimflag)
            {
                user.ForcedreleaseEyeTrackingAimflag = false;
                Form1.eyeDatas[Form1.nowvalue].ForcedreleaseEyeTrackingAimKey = (KeyData)e.KeyCode;
                user.buttonForcedreleaseEyeTrackingAim.Text = ((KeyData)e.KeyCode).ToString();
            }
            
            this.Close();
        }

        private void InitializeComponent()
        {
            this.labelShowInputKey = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelShowInputKey
            // 
            this.labelShowInputKey.AutoSize = true;
            this.labelShowInputKey.Location = new System.Drawing.Point(121, 116);
            this.labelShowInputKey.Name = "labelShowInputKey";
            this.labelShowInputKey.Size = new System.Drawing.Size(0, 12);
            this.labelShowInputKey.TabIndex = 0;
            // 
            // Dialog
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.labelShowInputKey);
            this.Name = "Dialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
