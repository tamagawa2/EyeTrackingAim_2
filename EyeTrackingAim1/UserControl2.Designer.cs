namespace EyeTrackingAim1
{
    partial class UserControl2
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxAppName = new System.Windows.Forms.TextBox();
            this.textBoxSensitivityX = new System.Windows.Forms.TextBox();
            this.textBoxSensitivityY = new System.Windows.Forms.TextBox();
            this.labelSensitivityX = new System.Windows.Forms.Label();
            this.labelSensitivityY = new System.Windows.Forms.Label();
            this.labelInputKey = new System.Windows.Forms.Label();
            this.buttonInputKey = new System.Windows.Forms.Button();
            this.buttonReleaseEyeTrackingKey = new System.Windows.Forms.Button();
            this.labelReleaseEyeTrackingKey = new System.Windows.Forms.Label();
            this.buttonBack = new System.Windows.Forms.Button();
            this.textBoxFovWidth = new System.Windows.Forms.TextBox();
            this.textBoxDelayTime = new System.Windows.Forms.TextBox();
            this.labelFovWidth = new System.Windows.Forms.Label();
            this.labelDelayTime = new System.Windows.Forms.Label();
            this.textBoxOffsetX = new System.Windows.Forms.TextBox();
            this.textBoxOffsetY = new System.Windows.Forms.TextBox();
            this.labelOffsetX = new System.Windows.Forms.Label();
            this.labelOffsetY = new System.Windows.Forms.Label();
            this.labelFPS = new System.Windows.Forms.Label();
            this.textBox1FPS = new System.Windows.Forms.TextBox();
            this.checkBoxShowBall = new System.Windows.Forms.CheckBox();
            this.textBoxEyeCorectionRange = new System.Windows.Forms.TextBox();
            this.labelEyeCorectionRange = new System.Windows.Forms.Label();
            this.textBoxMagnification = new System.Windows.Forms.TextBox();
            this.labelMagnification = new System.Windows.Forms.Label();
            this.checkBoxRawEyePoint = new System.Windows.Forms.CheckBox();
            this.checkBoxCancelY = new System.Windows.Forms.CheckBox();
            this.checkBoxUseMagWindow = new System.Windows.Forms.CheckBox();
            this.labelForcedreleaseEyeTrackingAim = new System.Windows.Forms.Label();
            this.buttonForcedreleaseEyeTrackingAim = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxAppName
            // 
            this.textBoxAppName.ForeColor = System.Drawing.Color.Black;
            this.textBoxAppName.Location = new System.Drawing.Point(307, 36);
            this.textBoxAppName.Name = "textBoxAppName";
            this.textBoxAppName.Size = new System.Drawing.Size(100, 19);
            this.textBoxAppName.TabIndex = 0;
            this.textBoxAppName.TextChanged += new System.EventHandler(this.textBoxAppName_TextChanged);
            // 
            // textBoxSensitivityX
            // 
            this.textBoxSensitivityX.Location = new System.Drawing.Point(218, 77);
            this.textBoxSensitivityX.Name = "textBoxSensitivityX";
            this.textBoxSensitivityX.Size = new System.Drawing.Size(100, 19);
            this.textBoxSensitivityX.TabIndex = 1;
            // 
            // textBoxSensitivityY
            // 
            this.textBoxSensitivityY.Location = new System.Drawing.Point(461, 77);
            this.textBoxSensitivityY.Name = "textBoxSensitivityY";
            this.textBoxSensitivityY.Size = new System.Drawing.Size(100, 19);
            this.textBoxSensitivityY.TabIndex = 2;
            // 
            // labelSensitivityX
            // 
            this.labelSensitivityX.AutoSize = true;
            this.labelSensitivityX.Location = new System.Drawing.Point(127, 80);
            this.labelSensitivityX.Name = "labelSensitivityX";
            this.labelSensitivityX.Size = new System.Drawing.Size(66, 12);
            this.labelSensitivityX.TabIndex = 3;
            this.labelSensitivityX.Text = "SensitivityX";
            // 
            // labelSensitivityY
            // 
            this.labelSensitivityY.AutoSize = true;
            this.labelSensitivityY.Location = new System.Drawing.Point(364, 80);
            this.labelSensitivityY.Name = "labelSensitivityY";
            this.labelSensitivityY.Size = new System.Drawing.Size(66, 12);
            this.labelSensitivityY.TabIndex = 4;
            this.labelSensitivityY.Text = "SensitivityY";
            // 
            // labelInputKey
            // 
            this.labelInputKey.AutoSize = true;
            this.labelInputKey.Location = new System.Drawing.Point(127, 343);
            this.labelInputKey.Name = "labelInputKey";
            this.labelInputKey.Size = new System.Drawing.Size(49, 12);
            this.labelInputKey.TabIndex = 5;
            this.labelInputKey.Text = "InputKey";
            // 
            // buttonInputKey
            // 
            this.buttonInputKey.BackColor = System.Drawing.SystemColors.Control;
            this.buttonInputKey.Location = new System.Drawing.Point(202, 340);
            this.buttonInputKey.Name = "buttonInputKey";
            this.buttonInputKey.Size = new System.Drawing.Size(100, 19);
            this.buttonInputKey.TabIndex = 6;
            this.buttonInputKey.UseVisualStyleBackColor = false;
            this.buttonInputKey.Click += new System.EventHandler(this.buttonInputKey_Click);
            // 
            // buttonReleaseEyeTrackingKey
            // 
            this.buttonReleaseEyeTrackingKey.Location = new System.Drawing.Point(479, 338);
            this.buttonReleaseEyeTrackingKey.Name = "buttonReleaseEyeTrackingKey";
            this.buttonReleaseEyeTrackingKey.Size = new System.Drawing.Size(100, 21);
            this.buttonReleaseEyeTrackingKey.TabIndex = 7;
            this.buttonReleaseEyeTrackingKey.UseVisualStyleBackColor = true;
            this.buttonReleaseEyeTrackingKey.Click += new System.EventHandler(this.buttonReleaseEyeTrackingKey_Click);
            // 
            // labelReleaseEyeTrackingKey
            // 
            this.labelReleaseEyeTrackingKey.AutoSize = true;
            this.labelReleaseEyeTrackingKey.Location = new System.Drawing.Point(331, 342);
            this.labelReleaseEyeTrackingKey.Name = "labelReleaseEyeTrackingKey";
            this.labelReleaseEyeTrackingKey.Size = new System.Drawing.Size(128, 12);
            this.labelReleaseEyeTrackingKey.TabIndex = 8;
            this.labelReleaseEyeTrackingKey.Text = "ReleaseEyeTrackingKey";
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(295, 365);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(121, 32);
            this.buttonBack.TabIndex = 9;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // textBoxFovWidth
            // 
            this.textBoxFovWidth.Location = new System.Drawing.Point(307, 158);
            this.textBoxFovWidth.Name = "textBoxFovWidth";
            this.textBoxFovWidth.Size = new System.Drawing.Size(100, 19);
            this.textBoxFovWidth.TabIndex = 11;
            // 
            // textBoxDelayTime
            // 
            this.textBoxDelayTime.Location = new System.Drawing.Point(307, 249);
            this.textBoxDelayTime.Name = "textBoxDelayTime";
            this.textBoxDelayTime.Size = new System.Drawing.Size(100, 19);
            this.textBoxDelayTime.TabIndex = 12;
            // 
            // labelFovWidth
            // 
            this.labelFovWidth.AutoSize = true;
            this.labelFovWidth.Location = new System.Drawing.Point(238, 165);
            this.labelFovWidth.Name = "labelFovWidth";
            this.labelFovWidth.Size = new System.Drawing.Size(24, 12);
            this.labelFovWidth.TabIndex = 14;
            this.labelFovWidth.Text = "Fov";
            // 
            // labelDelayTime
            // 
            this.labelDelayTime.AutoSize = true;
            this.labelDelayTime.Location = new System.Drawing.Point(221, 252);
            this.labelDelayTime.Name = "labelDelayTime";
            this.labelDelayTime.Size = new System.Drawing.Size(59, 12);
            this.labelDelayTime.TabIndex = 15;
            this.labelDelayTime.Text = "DelayTime";
            // 
            // textBoxOffsetX
            // 
            this.textBoxOffsetX.Location = new System.Drawing.Point(186, 542);
            this.textBoxOffsetX.Name = "textBoxOffsetX";
            this.textBoxOffsetX.Size = new System.Drawing.Size(100, 19);
            this.textBoxOffsetX.TabIndex = 16;
            // 
            // textBoxOffsetY
            // 
            this.textBoxOffsetY.Location = new System.Drawing.Point(495, 542);
            this.textBoxOffsetY.Name = "textBoxOffsetY";
            this.textBoxOffsetY.Size = new System.Drawing.Size(100, 19);
            this.textBoxOffsetY.TabIndex = 17;
            // 
            // labelOffsetX
            // 
            this.labelOffsetX.AutoSize = true;
            this.labelOffsetX.Location = new System.Drawing.Point(100, 545);
            this.labelOffsetX.Name = "labelOffsetX";
            this.labelOffsetX.Size = new System.Drawing.Size(76, 12);
            this.labelOffsetX.TabIndex = 18;
            this.labelOffsetX.Text = "OffsetX [-1:1]";
            // 
            // labelOffsetY
            // 
            this.labelOffsetY.AutoSize = true;
            this.labelOffsetY.Location = new System.Drawing.Point(396, 545);
            this.labelOffsetY.Name = "labelOffsetY";
            this.labelOffsetY.Size = new System.Drawing.Size(76, 12);
            this.labelOffsetY.TabIndex = 19;
            this.labelOffsetY.Text = "OffsetY [-1:1]";
            // 
            // labelFPS
            // 
            this.labelFPS.AutoSize = true;
            this.labelFPS.Location = new System.Drawing.Point(260, 515);
            this.labelFPS.Name = "labelFPS";
            this.labelFPS.Size = new System.Drawing.Size(26, 12);
            this.labelFPS.TabIndex = 22;
            this.labelFPS.Text = "FPS";
            // 
            // textBox1FPS
            // 
            this.textBox1FPS.Location = new System.Drawing.Point(307, 512);
            this.textBox1FPS.Name = "textBox1FPS";
            this.textBox1FPS.Size = new System.Drawing.Size(100, 19);
            this.textBox1FPS.TabIndex = 23;
            // 
            // checkBoxShowBall
            // 
            this.checkBoxShowBall.AutoSize = true;
            this.checkBoxShowBall.Location = new System.Drawing.Point(249, 415);
            this.checkBoxShowBall.Name = "checkBoxShowBall";
            this.checkBoxShowBall.Size = new System.Drawing.Size(69, 16);
            this.checkBoxShowBall.TabIndex = 24;
            this.checkBoxShowBall.Text = "Showball";
            this.checkBoxShowBall.UseVisualStyleBackColor = true;
            this.checkBoxShowBall.CheckedChanged += new System.EventHandler(this.checkBoxShowBall_CheckedChanged);
            // 
            // textBoxEyeCorectionRange
            // 
            this.textBoxEyeCorectionRange.Location = new System.Drawing.Point(307, 208);
            this.textBoxEyeCorectionRange.Name = "textBoxEyeCorectionRange";
            this.textBoxEyeCorectionRange.Size = new System.Drawing.Size(100, 19);
            this.textBoxEyeCorectionRange.TabIndex = 25;
            // 
            // labelEyeCorectionRange
            // 
            this.labelEyeCorectionRange.AutoSize = true;
            this.labelEyeCorectionRange.Location = new System.Drawing.Point(175, 211);
            this.labelEyeCorectionRange.Name = "labelEyeCorectionRange";
            this.labelEyeCorectionRange.Size = new System.Drawing.Size(105, 12);
            this.labelEyeCorectionRange.TabIndex = 26;
            this.labelEyeCorectionRange.Text = "EyeCorectionRange";
            // 
            // textBoxMagnification
            // 
            this.textBoxMagnification.Location = new System.Drawing.Point(307, 118);
            this.textBoxMagnification.Name = "textBoxMagnification";
            this.textBoxMagnification.Size = new System.Drawing.Size(100, 19);
            this.textBoxMagnification.TabIndex = 27;
            // 
            // labelMagnification
            // 
            this.labelMagnification.AutoSize = true;
            this.labelMagnification.Location = new System.Drawing.Point(200, 121);
            this.labelMagnification.Name = "labelMagnification";
            this.labelMagnification.Size = new System.Drawing.Size(73, 12);
            this.labelMagnification.TabIndex = 28;
            this.labelMagnification.Text = "Magnification";
            // 
            // checkBoxRawEyePoint
            // 
            this.checkBoxRawEyePoint.AutoSize = true;
            this.checkBoxRawEyePoint.Location = new System.Drawing.Point(249, 454);
            this.checkBoxRawEyePoint.Name = "checkBoxRawEyePoint";
            this.checkBoxRawEyePoint.Size = new System.Drawing.Size(91, 16);
            this.checkBoxRawEyePoint.TabIndex = 31;
            this.checkBoxRawEyePoint.Text = "RawEyePoint";
            this.checkBoxRawEyePoint.UseVisualStyleBackColor = true;
            this.checkBoxRawEyePoint.CheckedChanged += new System.EventHandler(this.checkBoxRawEyePoint_CheckedChanged);
            // 
            // checkBoxCancelY
            // 
            this.checkBoxCancelY.AutoSize = true;
            this.checkBoxCancelY.Location = new System.Drawing.Point(249, 490);
            this.checkBoxCancelY.Name = "checkBoxCancelY";
            this.checkBoxCancelY.Size = new System.Drawing.Size(66, 16);
            this.checkBoxCancelY.TabIndex = 34;
            this.checkBoxCancelY.Text = "CancelY";
            this.checkBoxCancelY.UseVisualStyleBackColor = true;
            this.checkBoxCancelY.CheckedChanged += new System.EventHandler(this.checkBoxCancelY_CheckedChanged);
            // 
            // checkBoxUseMagWindow
            // 
            this.checkBoxUseMagWindow.AutoSize = true;
            this.checkBoxUseMagWindow.Location = new System.Drawing.Point(398, 415);
            this.checkBoxUseMagWindow.Name = "checkBoxUseMagWindow";
            this.checkBoxUseMagWindow.Size = new System.Drawing.Size(103, 16);
            this.checkBoxUseMagWindow.TabIndex = 35;
            this.checkBoxUseMagWindow.Text = "UseMagWindow";
            this.checkBoxUseMagWindow.UseVisualStyleBackColor = true;
            this.checkBoxUseMagWindow.CheckedChanged += new System.EventHandler(this.checkBoxUseMagWindow_CheckedChanged);
            // 
            // labelForcedreleaseEyeTrackingAim
            // 
            this.labelForcedreleaseEyeTrackingAim.AutoSize = true;
            this.labelForcedreleaseEyeTrackingAim.Location = new System.Drawing.Point(627, 343);
            this.labelForcedreleaseEyeTrackingAim.Name = "labelForcedreleaseEyeTrackingAim";
            this.labelForcedreleaseEyeTrackingAim.Size = new System.Drawing.Size(160, 12);
            this.labelForcedreleaseEyeTrackingAim.TabIndex = 36;
            this.labelForcedreleaseEyeTrackingAim.Text = "ForcedreleaseEyeTrackingAim";
            // 
            // buttonForcedreleaseEyeTrackingAim
            // 
            this.buttonForcedreleaseEyeTrackingAim.Location = new System.Drawing.Point(807, 338);
            this.buttonForcedreleaseEyeTrackingAim.Name = "buttonForcedreleaseEyeTrackingAim";
            this.buttonForcedreleaseEyeTrackingAim.Size = new System.Drawing.Size(75, 23);
            this.buttonForcedreleaseEyeTrackingAim.TabIndex = 37;
            this.buttonForcedreleaseEyeTrackingAim.UseVisualStyleBackColor = true;
            this.buttonForcedreleaseEyeTrackingAim.Click += new System.EventHandler(this.buttonForcedreleaseEyeTrackingAim_Click);
            // 
            // UserControl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonForcedreleaseEyeTrackingAim);
            this.Controls.Add(this.labelForcedreleaseEyeTrackingAim);
            this.Controls.Add(this.checkBoxUseMagWindow);
            this.Controls.Add(this.checkBoxCancelY);
            this.Controls.Add(this.checkBoxRawEyePoint);
            this.Controls.Add(this.labelMagnification);
            this.Controls.Add(this.textBoxMagnification);
            this.Controls.Add(this.labelEyeCorectionRange);
            this.Controls.Add(this.textBoxEyeCorectionRange);
            this.Controls.Add(this.checkBoxShowBall);
            this.Controls.Add(this.textBox1FPS);
            this.Controls.Add(this.labelFPS);
            this.Controls.Add(this.labelOffsetY);
            this.Controls.Add(this.labelOffsetX);
            this.Controls.Add(this.textBoxOffsetY);
            this.Controls.Add(this.textBoxOffsetX);
            this.Controls.Add(this.labelDelayTime);
            this.Controls.Add(this.labelFovWidth);
            this.Controls.Add(this.textBoxDelayTime);
            this.Controls.Add(this.textBoxFovWidth);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.labelReleaseEyeTrackingKey);
            this.Controls.Add(this.buttonReleaseEyeTrackingKey);
            this.Controls.Add(this.buttonInputKey);
            this.Controls.Add(this.labelInputKey);
            this.Controls.Add(this.labelSensitivityY);
            this.Controls.Add(this.labelSensitivityX);
            this.Controls.Add(this.textBoxSensitivityY);
            this.Controls.Add(this.textBoxSensitivityX);
            this.Controls.Add(this.textBoxAppName);
            this.Name = "UserControl2";
            this.Size = new System.Drawing.Size(911, 606);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxAppName;
        private System.Windows.Forms.TextBox textBoxSensitivityX;
        private System.Windows.Forms.TextBox textBoxSensitivityY;
        private System.Windows.Forms.Label labelSensitivityX;
        private System.Windows.Forms.Label labelSensitivityY;
        private System.Windows.Forms.Label labelInputKey;
        private System.Windows.Forms.Label labelReleaseEyeTrackingKey;
        private System.Windows.Forms.Button buttonBack;
        public System.Windows.Forms.Button buttonInputKey;
        public System.Windows.Forms.Button buttonReleaseEyeTrackingKey;
        private System.Windows.Forms.TextBox textBoxFovWidth;
        private System.Windows.Forms.TextBox textBoxDelayTime;
        private System.Windows.Forms.Label labelFovWidth;
        private System.Windows.Forms.Label labelDelayTime;
        private System.Windows.Forms.TextBox textBoxOffsetX;
        private System.Windows.Forms.TextBox textBoxOffsetY;
        private System.Windows.Forms.Label labelOffsetX;
        private System.Windows.Forms.Label labelOffsetY;
        private System.Windows.Forms.Label labelFPS;
        private System.Windows.Forms.TextBox textBox1FPS;
        private System.Windows.Forms.CheckBox checkBoxShowBall;
        private System.Windows.Forms.TextBox textBoxEyeCorectionRange;
        private System.Windows.Forms.Label labelEyeCorectionRange;
        private System.Windows.Forms.TextBox textBoxMagnification;
        private System.Windows.Forms.Label labelMagnification;
        private System.Windows.Forms.CheckBox checkBoxRawEyePoint;
        private System.Windows.Forms.CheckBox checkBoxCancelY;
        private System.Windows.Forms.CheckBox checkBoxUseMagWindow;
        private System.Windows.Forms.Label labelForcedreleaseEyeTrackingAim;
        public System.Windows.Forms.Button buttonForcedreleaseEyeTrackingAim;
    }
}
