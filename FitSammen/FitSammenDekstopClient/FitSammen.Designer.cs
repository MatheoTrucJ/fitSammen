namespace FitSammenDekstopClient
{
    partial class FitSammen
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            listViewAllClasses = new ListView();
            groupBox2 = new GroupBox();
            lblTextDescription = new Label();
            lblTextClassType = new Label();
            txtBoxMemberCount = new TextBox();
            txtBoxCapacity = new TextBox();
            txtBoxLocation = new TextBox();
            txtBoxDuration = new TextBox();
            txtBoxStartTime = new TextBox();
            lblMemberCount = new Label();
            lblDuration = new Label();
            Duration = new Label();
            txtBoxEmployee = new TextBox();
            txtBoxDateTime = new TextBox();
            lblCapacity = new Label();
            lblLocation = new Label();
            lblStartTime = new Label();
            lblDateTime = new Label();
            lblEmployee = new Label();
            lblClassDescription = new Label();
            lblClassType = new Label();
            labelProcessText = new Label();
            btnCreateNewClass = new Button();
            btnUpdateClass = new Button();
            btnDeleteClass = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(listViewAllClasses);
            groupBox1.Location = new Point(12, 67);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(593, 470);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "TræningsHold";
            // 
            // listViewAllClasses
            // 
            listViewAllClasses.Location = new Point(6, 22);
            listViewAllClasses.Name = "listViewAllClasses";
            listViewAllClasses.Size = new Size(577, 442);
            listViewAllClasses.TabIndex = 3;
            listViewAllClasses.UseCompatibleStateImageBehavior = false;
            listViewAllClasses.DoubleClick += listViewAllClasses_DoubleClick;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lblTextDescription);
            groupBox2.Controls.Add(lblTextClassType);
            groupBox2.Controls.Add(txtBoxMemberCount);
            groupBox2.Controls.Add(txtBoxCapacity);
            groupBox2.Controls.Add(txtBoxLocation);
            groupBox2.Controls.Add(txtBoxDuration);
            groupBox2.Controls.Add(txtBoxStartTime);
            groupBox2.Controls.Add(lblMemberCount);
            groupBox2.Controls.Add(lblDuration);
            groupBox2.Controls.Add(Duration);
            groupBox2.Controls.Add(txtBoxEmployee);
            groupBox2.Controls.Add(txtBoxDateTime);
            groupBox2.Controls.Add(lblCapacity);
            groupBox2.Controls.Add(lblLocation);
            groupBox2.Controls.Add(lblStartTime);
            groupBox2.Controls.Add(lblDateTime);
            groupBox2.Controls.Add(lblEmployee);
            groupBox2.Controls.Add(lblClassDescription);
            groupBox2.Controls.Add(lblClassType);
            groupBox2.Location = new Point(611, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(321, 422);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Hold Informationer";
            // 
            // lblTextDescription
            // 
            lblTextDescription.AutoSize = true;
            lblTextDescription.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTextDescription.Location = new Point(101, 55);
            lblTextDescription.MaximumSize = new Size(210, 0);
            lblTextDescription.Name = "lblTextDescription";
            lblTextDescription.Size = new Size(25, 21);
            lblTextDescription.TabIndex = 15;
            lblTextDescription.Text = ".....";
            // 
            // lblTextClassType
            // 
            lblTextClassType.AutoSize = true;
            lblTextClassType.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTextClassType.Location = new Point(101, 19);
            lblTextClassType.Name = "lblTextClassType";
            lblTextClassType.Size = new Size(25, 21);
            lblTextClassType.TabIndex = 5;
            lblTextClassType.Text = ".....";
            // 
            // txtBoxMemberCount
            // 
            txtBoxMemberCount.Location = new Point(101, 384);
            txtBoxMemberCount.Name = "txtBoxMemberCount";
            txtBoxMemberCount.Size = new Size(211, 23);
            txtBoxMemberCount.TabIndex = 11;
            // 
            // txtBoxCapacity
            // 
            txtBoxCapacity.Location = new Point(101, 342);
            txtBoxCapacity.Name = "txtBoxCapacity";
            txtBoxCapacity.Size = new Size(211, 23);
            txtBoxCapacity.TabIndex = 11;
            // 
            // txtBoxLocation
            // 
            txtBoxLocation.Location = new Point(101, 301);
            txtBoxLocation.Name = "txtBoxLocation";
            txtBoxLocation.Size = new Size(211, 23);
            txtBoxLocation.TabIndex = 11;
            // 
            // txtBoxDuration
            // 
            txtBoxDuration.Location = new Point(101, 257);
            txtBoxDuration.Name = "txtBoxDuration";
            txtBoxDuration.Size = new Size(211, 23);
            txtBoxDuration.TabIndex = 11;
            // 
            // txtBoxStartTime
            // 
            txtBoxStartTime.Location = new Point(101, 216);
            txtBoxStartTime.Name = "txtBoxStartTime";
            txtBoxStartTime.Size = new Size(211, 23);
            txtBoxStartTime.TabIndex = 11;
            // 
            // lblMemberCount
            // 
            lblMemberCount.AutoSize = true;
            lblMemberCount.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMemberCount.Location = new Point(13, 382);
            lblMemberCount.Name = "lblMemberCount";
            lblMemberCount.Size = new Size(77, 21);
            lblMemberCount.TabIndex = 14;
            lblMemberCount.Text = "Tilmeldte:";
            // 
            // lblDuration
            // 
            lblDuration.AutoSize = true;
            lblDuration.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDuration.Location = new Point(13, 255);
            lblDuration.Name = "lblDuration";
            lblDuration.Size = new Size(75, 21);
            lblDuration.TabIndex = 13;
            lblDuration.Text = "Varighed:";
            // 
            // Duration
            // 
            Duration.AutoSize = true;
            Duration.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Duration.Location = new Point(13, 302);
            Duration.Name = "Duration";
            Duration.Size = new Size(0, 21);
            Duration.TabIndex = 12;
            // 
            // txtBoxEmployee
            // 
            txtBoxEmployee.Location = new Point(101, 134);
            txtBoxEmployee.Name = "txtBoxEmployee";
            txtBoxEmployee.Size = new Size(211, 23);
            txtBoxEmployee.TabIndex = 11;
            // 
            // txtBoxDateTime
            // 
            txtBoxDateTime.Location = new Point(101, 172);
            txtBoxDateTime.Name = "txtBoxDateTime";
            txtBoxDateTime.Size = new Size(211, 23);
            txtBoxDateTime.TabIndex = 10;
            // 
            // lblCapacity
            // 
            lblCapacity.AutoSize = true;
            lblCapacity.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapacity.Location = new Point(13, 342);
            lblCapacity.Name = "lblCapacity";
            lblCapacity.Size = new Size(76, 21);
            lblCapacity.TabIndex = 9;
            lblCapacity.Text = "Kapacitet:";
            // 
            // lblLocation
            // 
            lblLocation.AutoSize = true;
            lblLocation.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLocation.Location = new Point(13, 299);
            lblLocation.Name = "lblLocation";
            lblLocation.Size = new Size(73, 21);
            lblLocation.TabIndex = 8;
            lblLocation.Text = "Lokation:";
            // 
            // lblStartTime
            // 
            lblStartTime.AutoSize = true;
            lblStartTime.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStartTime.Location = new Point(13, 214);
            lblStartTime.Name = "lblStartTime";
            lblStartTime.Size = new Size(81, 21);
            lblStartTime.TabIndex = 7;
            lblStartTime.Text = "Tidspunkt:";
            // 
            // lblDateTime
            // 
            lblDateTime.AutoSize = true;
            lblDateTime.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDateTime.Location = new Point(13, 174);
            lblDateTime.Name = "lblDateTime";
            lblDateTime.Size = new Size(46, 21);
            lblDateTime.TabIndex = 6;
            lblDateTime.Text = "Dato:";
            // 
            // lblEmployee
            // 
            lblEmployee.AutoSize = true;
            lblEmployee.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEmployee.Location = new Point(13, 134);
            lblEmployee.Name = "lblEmployee";
            lblEmployee.Size = new Size(81, 21);
            lblEmployee.TabIndex = 5;
            lblEmployee.Text = "Instruktør:";
            // 
            // lblClassDescription
            // 
            lblClassDescription.AutoSize = true;
            lblClassDescription.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblClassDescription.Location = new Point(13, 55);
            lblClassDescription.Name = "lblClassDescription";
            lblClassDescription.Size = new Size(46, 21);
            lblClassDescription.TabIndex = 4;
            lblClassDescription.Text = "Hold:";
            // 
            // lblClassType
            // 
            lblClassType.AutoSize = true;
            lblClassType.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblClassType.Location = new Point(13, 19);
            lblClassType.Name = "lblClassType";
            lblClassType.Size = new Size(82, 21);
            lblClassType.TabIndex = 3;
            lblClassType.Text = "Hold Type:";
            // 
            // labelProcessText
            // 
            labelProcessText.AutoSize = true;
            labelProcessText.Location = new Point(551, 49);
            labelProcessText.Name = "labelProcessText";
            labelProcessText.Size = new Size(44, 15);
            labelProcessText.TabIndex = 2;
            labelProcessText.Text = "Besked";
            // 
            // btnCreateNewClass
            // 
            btnCreateNewClass.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCreateNewClass.Location = new Point(607, 440);
            btnCreateNewClass.Name = "btnCreateNewClass";
            btnCreateNewClass.Size = new Size(326, 29);
            btnCreateNewClass.TabIndex = 0;
            btnCreateNewClass.Text = "Opret nyt hold";
            btnCreateNewClass.UseVisualStyleBackColor = true;
            btnCreateNewClass.Click += createNewClassBtn_Click;
            // 
            // btnUpdateClass
            // 
            btnUpdateClass.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnUpdateClass.Location = new Point(607, 475);
            btnUpdateClass.Name = "btnUpdateClass";
            btnUpdateClass.Size = new Size(325, 28);
            btnUpdateClass.TabIndex = 3;
            btnUpdateClass.Text = "Rediger valgt træningshold";
            btnUpdateClass.UseVisualStyleBackColor = true;
            // 
            // btnDeleteClass
            // 
            btnDeleteClass.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDeleteClass.Location = new Point(607, 509);
            btnDeleteClass.Name = "btnDeleteClass";
            btnDeleteClass.Size = new Size(325, 28);
            btnDeleteClass.TabIndex = 4;
            btnDeleteClass.Text = "Slet valgt træningshold";
            btnDeleteClass.UseVisualStyleBackColor = true;
            // 
            // FitSammen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(944, 549);
            Controls.Add(btnDeleteClass);
            Controls.Add(btnUpdateClass);
            Controls.Add(btnCreateNewClass);
            Controls.Add(labelProcessText);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "FitSammen";
            Text = "FitSammen";
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label labelProcessText;
        private ListView listViewAllClasses;
        private Button btnCreateNewClass;
        private Label lblClassDescription;
        private Label lblClassType;
        private Label lblDateTime;
        private Label lblEmployee;
        private Label Duration;
        private TextBox txtBoxEmployee;
        private TextBox txtBoxDateTime;
        private Label lblCapacity;
        private Label lblLocation;
        private Label lblStartTime;
        private Label lblMemberCount;
        private Label lblDuration;
        private TextBox txtBoxMemberCount;
        private TextBox txtBoxCapacity;
        private TextBox txtBoxLocation;
        private TextBox txtBoxDuration;
        private TextBox txtBoxStartTime;
        private Button btnUpdateClass;
        private Button btnDeleteClass;
        private Label lblTextDescription;
        private Label lblTextClassType;
    }
}
