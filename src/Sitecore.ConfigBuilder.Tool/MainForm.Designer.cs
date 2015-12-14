namespace Sitecore.ConfigBuilder.Tool
{
  partial class MainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.StatusBar = new System.Windows.Forms.StatusStrip();
      this.ErrorLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.ActionsGroup = new System.Windows.Forms.GroupBox();
      this.BuildShowConfig = new System.Windows.Forms.CheckBox();
      this.BuildWebConfigResult = new System.Windows.Forms.CheckBox();
      this.NormalizeOutput = new System.Windows.Forms.CheckBox();
      this.LabelDragNDrop = new System.Windows.Forms.Label();
      this.BrowseButton = new System.Windows.Forms.Button();
      this.FilePathTextbox = new System.Windows.Forms.TextBox();
      this.LabelForFilePathTextbox = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.NoDestinationPrompt = new System.Windows.Forms.CheckBox();
      this.CloseWhenDone = new System.Windows.Forms.CheckBox();
      this.OpenFolder = new System.Windows.Forms.CheckBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.LabelDescription = new System.Windows.Forms.Label();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.label3 = new System.Windows.Forms.Label();
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.label2 = new System.Windows.Forms.Label();
      this.groupBox5 = new System.Windows.Forms.GroupBox();
      this.SitecoreVersionComboBox = new System.Windows.Forms.ComboBox();
      this.RequireDefaultConfiguration = new System.Windows.Forms.CheckBox();
      this.SaveButton = new System.Windows.Forms.Button();
      this.StatusBar.SuspendLayout();
      this.ActionsGroup.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.groupBox5.SuspendLayout();
      this.SuspendLayout();
      // 
      // StatusBar
      // 
      this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ErrorLabel});
      this.StatusBar.Location = new System.Drawing.Point(0, 375);
      this.StatusBar.Name = "StatusBar";
      this.StatusBar.Size = new System.Drawing.Size(503, 22);
      this.StatusBar.SizingGrip = false;
      this.StatusBar.TabIndex = 15;
      this.StatusBar.Text = "statusStrip1";
      // 
      // ErrorLabel
      // 
      this.ErrorLabel.Name = "ErrorLabel";
      this.ErrorLabel.Size = new System.Drawing.Size(39, 17);
      this.ErrorLabel.Text = "Ready";
      // 
      // ActionsGroup
      // 
      this.ActionsGroup.Controls.Add(this.BuildShowConfig);
      this.ActionsGroup.Controls.Add(this.BuildWebConfigResult);
      this.ActionsGroup.Controls.Add(this.NormalizeOutput);
      this.ActionsGroup.Location = new System.Drawing.Point(255, 172);
      this.ActionsGroup.Name = "ActionsGroup";
      this.ActionsGroup.Size = new System.Drawing.Size(231, 94);
      this.ActionsGroup.TabIndex = 16;
      this.ActionsGroup.TabStop = false;
      this.ActionsGroup.Text = "STEP 4 – Actions";
      // 
      // BuildShowConfig
      // 
      this.BuildShowConfig.AutoSize = true;
      this.BuildShowConfig.Checked = true;
      this.BuildShowConfig.CheckState = System.Windows.Forms.CheckState.Checked;
      this.BuildShowConfig.Enabled = false;
      this.BuildShowConfig.Location = new System.Drawing.Point(10, 22);
      this.BuildShowConfig.Name = "BuildShowConfig";
      this.BuildShowConfig.Size = new System.Drawing.Size(124, 17);
      this.BuildShowConfig.TabIndex = 5;
      this.BuildShowConfig.Text = "Build showconfig.xml";
      this.BuildShowConfig.UseVisualStyleBackColor = true;
      // 
      // BuildWebConfigResult
      // 
      this.BuildWebConfigResult.AutoSize = true;
      this.BuildWebConfigResult.Location = new System.Drawing.Point(10, 45);
      this.BuildWebConfigResult.Name = "BuildWebConfigResult";
      this.BuildWebConfigResult.Size = new System.Drawing.Size(150, 17);
      this.BuildWebConfigResult.TabIndex = 6;
      this.BuildWebConfigResult.Text = "Build web.config.result.xml";
      this.BuildWebConfigResult.UseVisualStyleBackColor = true;
      // 
      // NormalizeOutput
      // 
      this.NormalizeOutput.AutoSize = true;
      this.NormalizeOutput.Location = new System.Drawing.Point(10, 68);
      this.NormalizeOutput.Name = "NormalizeOutput";
      this.NormalizeOutput.Size = new System.Drawing.Size(129, 17);
      this.NormalizeOutput.TabIndex = 7;
      this.NormalizeOutput.Text = "Normalize ouput file[s]";
      this.NormalizeOutput.UseVisualStyleBackColor = true;
      // 
      // LabelDragNDrop
      // 
      this.LabelDragNDrop.AutoSize = true;
      this.LabelDragNDrop.ForeColor = System.Drawing.SystemColors.ControlDark;
      this.LabelDragNDrop.Location = new System.Drawing.Point(322, 16);
      this.LabelDragNDrop.Name = "LabelDragNDrop";
      this.LabelDragNDrop.Size = new System.Drawing.Size(63, 13);
      this.LabelDragNDrop.TabIndex = 10;
      this.LabelDragNDrop.Text = "Drag\'n\'Drop";
      // 
      // BrowseButton
      // 
      this.BrowseButton.Location = new System.Drawing.Point(393, 30);
      this.BrowseButton.Name = "BrowseButton";
      this.BrowseButton.Size = new System.Drawing.Size(75, 23);
      this.BrowseButton.TabIndex = 1;
      this.BrowseButton.Text = "Browse";
      this.BrowseButton.UseVisualStyleBackColor = true;
      this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
      // 
      // FilePathTextbox
      // 
      this.FilePathTextbox.Location = new System.Drawing.Point(9, 32);
      this.FilePathTextbox.Name = "FilePathTextbox";
      this.FilePathTextbox.Size = new System.Drawing.Size(376, 20);
      this.FilePathTextbox.TabIndex = 0;
      this.FilePathTextbox.TextChanged += new System.EventHandler(this.FilePathTextbox_TextChanged);
      // 
      // LabelForFilePathTextbox
      // 
      this.LabelForFilePathTextbox.AutoSize = true;
      this.LabelForFilePathTextbox.Location = new System.Drawing.Point(6, 16);
      this.LabelForFilePathTextbox.Name = "LabelForFilePathTextbox";
      this.LabelForFilePathTextbox.Size = new System.Drawing.Size(114, 13);
      this.LabelForFilePathTextbox.TabIndex = 2;
      this.LabelForFilePathTextbox.Text = "Choose web.config file";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.NoDestinationPrompt);
      this.groupBox1.Controls.Add(this.CloseWhenDone);
      this.groupBox1.Controls.Add(this.OpenFolder);
      this.groupBox1.Location = new System.Drawing.Point(12, 172);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(231, 94);
      this.groupBox1.TabIndex = 14;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "STEP 3 – Advanced Options";
      // 
      // NoDestinationPrompt
      // 
      this.NoDestinationPrompt.AutoSize = true;
      this.NoDestinationPrompt.Location = new System.Drawing.Point(10, 22);
      this.NoDestinationPrompt.Name = "NoDestinationPrompt";
      this.NoDestinationPrompt.Size = new System.Drawing.Size(167, 17);
      this.NoDestinationPrompt.TabIndex = 2;
      this.NoDestinationPrompt.Text = "Save files into the same folder";
      this.NoDestinationPrompt.UseVisualStyleBackColor = true;
      // 
      // CloseWhenDone
      // 
      this.CloseWhenDone.AutoSize = true;
      this.CloseWhenDone.Location = new System.Drawing.Point(10, 68);
      this.CloseWhenDone.Name = "CloseWhenDone";
      this.CloseWhenDone.Size = new System.Drawing.Size(164, 17);
      this.CloseWhenDone.TabIndex = 4;
      this.CloseWhenDone.Text = "Close application after saving";
      this.CloseWhenDone.UseVisualStyleBackColor = true;
      // 
      // OpenFolder
      // 
      this.OpenFolder.AutoSize = true;
      this.OpenFolder.Location = new System.Drawing.Point(10, 45);
      this.OpenFolder.Name = "OpenFolder";
      this.OpenFolder.Size = new System.Drawing.Size(139, 17);
      this.OpenFolder.TabIndex = 3;
      this.OpenFolder.Text = "Open folder after saving";
      this.OpenFolder.UseVisualStyleBackColor = true;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Location = new System.Drawing.Point(12, 127);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(474, 39);
      this.groupBox2.TabIndex = 16;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "STEP 2 – Engine";
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(7, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(463, 18);
      this.label1.TabIndex = 31;
      this.label1.Text = "Since version 1.2 it uses embedded engine that behaves as stock Sitecore 7.0.";
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.LabelDescription);
      this.groupBox3.Controls.Add(this.LabelForFilePathTextbox);
      this.groupBox3.Controls.Add(this.LabelDragNDrop);
      this.groupBox3.Controls.Add(this.BrowseButton);
      this.groupBox3.Controls.Add(this.FilePathTextbox);
      this.groupBox3.Location = new System.Drawing.Point(12, 12);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(474, 105);
      this.groupBox3.TabIndex = 28;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "STEP 1 – Source";
      // 
      // LabelDescription
      // 
      this.LabelDescription.Location = new System.Drawing.Point(6, 56);
      this.LabelDescription.Name = "LabelDescription";
      this.LabelDescription.Size = new System.Drawing.Size(463, 45);
      this.LabelDescription.TabIndex = 29;
      this.LabelDescription.Text = resources.GetString("LabelDescription.Text");
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.label3);
      this.groupBox4.Controls.Add(this.linkLabel1);
      this.groupBox4.Controls.Add(this.label2);
      this.groupBox4.Location = new System.Drawing.Point(12, 324);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(275, 39);
      this.groupBox4.TabIndex = 32;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "EXTRA – Normalize showconfig.xml file";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(249, 16);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(20, 18);
      this.label3.TabIndex = 33;
      this.label3.Text = "it.";
      // 
      // linkLabel1
      // 
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Location = new System.Drawing.Point(201, 16);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(51, 13);
      this.linkLabel1.TabIndex = 11;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "normalize";
      this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.NormalizeLink_Click);
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(7, 16);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(198, 18);
      this.label2.TabIndex = 31;
      this.label2.Text = "If showconfig.xml already exists, you can";
      // 
      // groupBox5
      // 
      this.groupBox5.Controls.Add(this.SitecoreVersionComboBox);
      this.groupBox5.Controls.Add(this.RequireDefaultConfiguration);
      this.groupBox5.Location = new System.Drawing.Point(12, 272);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new System.Drawing.Size(474, 46);
      this.groupBox5.TabIndex = 33;
      this.groupBox5.TabStop = false;
      this.groupBox5.Text = "STEP 5 - Compare with defaults";
      // 
      // SitecoreVersionComboBox
      // 
      this.SitecoreVersionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.SitecoreVersionComboBox.FormattingEnabled = true;
      this.SitecoreVersionComboBox.Location = new System.Drawing.Point(253, 15);
      this.SitecoreVersionComboBox.Name = "SitecoreVersionComboBox";
      this.SitecoreVersionComboBox.Size = new System.Drawing.Size(215, 21);
      this.SitecoreVersionComboBox.TabIndex = 12;
      // 
      // RequireDefaultConfiguration
      // 
      this.RequireDefaultConfiguration.AutoSize = true;
      this.RequireDefaultConfiguration.Location = new System.Drawing.Point(10, 19);
      this.RequireDefaultConfiguration.Name = "RequireDefaultConfiguration";
      this.RequireDefaultConfiguration.Size = new System.Drawing.Size(239, 17);
      this.RequireDefaultConfiguration.TabIndex = 11;
      this.RequireDefaultConfiguration.Text = "Download a default Sitecore configuration for";
      this.RequireDefaultConfiguration.UseVisualStyleBackColor = true;
      // 
      // SaveButton
      // 
      this.SaveButton.Enabled = false;
      this.SaveButton.Location = new System.Drawing.Point(405, 335);
      this.SaveButton.Name = "SaveButton";
      this.SaveButton.Size = new System.Drawing.Size(75, 23);
      this.SaveButton.TabIndex = 13;
      this.SaveButton.Text = "Save";
      this.SaveButton.UseVisualStyleBackColor = true;
      this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
      // 
      // MainForm
      // 
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(503, 397);
      this.Controls.Add(this.groupBox5);
      this.Controls.Add(this.groupBox4);
      this.Controls.Add(this.SaveButton);
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.ActionsGroup);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.StatusBar);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "MainForm";
      this.Text = "Sitecore Config Builder {0}";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
      this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
      this.StatusBar.ResumeLayout(false);
      this.StatusBar.PerformLayout();
      this.ActionsGroup.ResumeLayout(false);
      this.ActionsGroup.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      this.groupBox5.ResumeLayout(false);
      this.groupBox5.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.StatusStrip StatusBar;
    private System.Windows.Forms.ToolStripStatusLabel ErrorLabel;
    private System.Windows.Forms.GroupBox ActionsGroup;
    private System.Windows.Forms.CheckBox BuildShowConfig;
    private System.Windows.Forms.CheckBox BuildWebConfigResult;
    private System.Windows.Forms.CheckBox NormalizeOutput;
    private System.Windows.Forms.Label LabelDragNDrop;
    private System.Windows.Forms.Button BrowseButton;
    private System.Windows.Forms.TextBox FilePathTextbox;
    private System.Windows.Forms.Label LabelForFilePathTextbox;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox NoDestinationPrompt;
    private System.Windows.Forms.CheckBox CloseWhenDone;
    private System.Windows.Forms.CheckBox OpenFolder;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Label LabelDescription;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.ComboBox SitecoreVersionComboBox;
    private System.Windows.Forms.CheckBox RequireDefaultConfiguration;
    private System.Windows.Forms.Button SaveButton;
  }
}

