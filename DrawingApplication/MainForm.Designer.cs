﻿/*
 *   SamDiagrams - diagram component for .NET
 *   Copyright (C) 2011  Lucian Voinescu
 *
 *   This file is part of SamDiagrams
 *
 *   SamDiagrams is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU Lesser General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   SamDiagrams is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU Lesser General Public License for more details.
 *
 *   You should have received a copy of the GNU Lesser General Public License
 *   along with SamDiagrams. If not, see <http://www.gnu.org/licenses/>.
 */
namespace DrawingApplication
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private DeltaDrawing.DotDrawing.DotDrawing dotDrawing;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripButton toolStripButton2;
		private System.Windows.Forms.ToolStripButton toolStripButton3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton snapToGridButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripComboBox serialPortSelector;
		private System.Windows.Forms.ToolStripButton toolStripButton4;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripComboBox baudRateSelector;
		private System.Windows.Forms.ToolStripButton toolStripButton5;
		private System.Windows.Forms.ToolStripButton toolStripButton6;
		private System.Windows.Forms.ToolStripTextBox toolStripMaxX;
		private System.Windows.Forms.ToolStripTextBox toolStripMaxY;
		private System.Windows.Forms.ToolStripButton toolStripAppCloseButton;
		private System.Windows.Forms.ToolStripButton toolStripButton7;
		private System.Windows.Forms.ToolStripButton toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton toolStripButton8;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripButton toolStripButton9;
		private System.Windows.Forms.TreeView treeView1;
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.dotDrawing = new DeltaDrawing.DotDrawing.DotDrawing();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.snapToGridButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			this.serialPortSelector = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.baudRateSelector = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
			this.toolStripMaxX = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripMaxY = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripAppCloseButton = new System.Windows.Forms.ToolStripButton();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dotDrawing
			// 
			this.dotDrawing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.dotDrawing.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.dotDrawing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.dotDrawing.GridSize = 10;
			this.dotDrawing.Location = new System.Drawing.Point(0, 28);
			this.dotDrawing.Name = "dotDrawing";
			this.dotDrawing.Size = new System.Drawing.Size(400, 400);
			this.dotDrawing.TabIndex = 0;
			this.dotDrawing.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DotDrawingKeyDown);
			this.dotDrawing.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DotDrawingKeyUp);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripButton8,
			this.toolStripSeparator4,
			this.toolStripSeparator5,
			this.toolStripButton1,
			this.toolStripButton2,
			this.toolStripSeparator2,
			this.toolStripButton3,
			this.toolStripSeparator1,
			this.snapToGridButton,
			this.toolStripButton9,
			this.toolStripButton7,
			this.toolStripSeparator3,
			this.toolStripLabel2,
			this.serialPortSelector,
			this.toolStripLabel1,
			this.baudRateSelector,
			this.toolStripButton4,
			this.toolStripButton5,
			this.toolStripButton6,
			this.toolStripMaxX,
			this.toolStripMaxY,
			this.toolStripAppCloseButton});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(902, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton8
			// 
			this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
			this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton8.Name = "toolStripButton8";
			this.toolStripButton8.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton8.Text = "Load";
			this.toolStripButton8.Click += new System.EventHandler(this.ToolStripButton8Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripSeparator4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSeparator4.Image")));
			this.toolStripSeparator4.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(23, 22);
			this.toolStripSeparator4.Text = "Save";
			this.toolStripSeparator4.Click += new System.EventHandler(this.ToolStripSeparator4Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton1.Text = "Line tool";
			this.toolStripButton1.Click += new System.EventHandler(this.ToolStripButton1Click);
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton2.Text = "Circle tool";
			this.toolStripButton2.Click += new System.EventHandler(this.ToolStripButton2Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButton3
			// 
			this.toolStripButton3.CheckOnClick = true;
			this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
			this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton3.Name = "toolStripButton3";
			this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton3.Text = "Free tool";
			this.toolStripButton3.Click += new System.EventHandler(this.ToolStripButton3Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// snapToGridButton
			// 
			this.snapToGridButton.Checked = true;
			this.snapToGridButton.CheckOnClick = true;
			this.snapToGridButton.CheckState = System.Windows.Forms.CheckState.Checked;
			this.snapToGridButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.snapToGridButton.Image = ((System.Drawing.Image)(resources.GetObject("snapToGridButton.Image")));
			this.snapToGridButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.snapToGridButton.Name = "snapToGridButton";
			this.snapToGridButton.Size = new System.Drawing.Size(23, 22);
			this.snapToGridButton.Text = "Snap to grid";
			this.snapToGridButton.Click += new System.EventHandler(this.ToolStripButton4Click);
			// 
			// toolStripButton9
			// 
			this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton9.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton9.Image")));
			this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton9.Name = "toolStripButton9";
			this.toolStripButton9.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton9.Text = "Disable points";
			this.toolStripButton9.Click += new System.EventHandler(this.ToolStripButton9Click);
			// 
			// toolStripButton7
			// 
			this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
			this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton7.Name = "toolStripButton7";
			this.toolStripButton7.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton7.Text = "toolStripButton7";
			this.toolStripButton7.Click += new System.EventHandler(this.ToolStripButton7Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel2
			// 
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new System.Drawing.Size(30, 22);
			this.toolStripLabel2.Text = "COM";
			// 
			// serialPortSelector
			// 
			this.serialPortSelector.Name = "serialPortSelector";
			this.serialPortSelector.Size = new System.Drawing.Size(121, 25);
			this.serialPortSelector.Text = "/dev/ttyUSB0";
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(54, 22);
			this.toolStripLabel1.Text = "Baud rate";
			// 
			// baudRateSelector
			// 
			this.baudRateSelector.DropDownWidth = 40;
			this.baudRateSelector.Items.AddRange(new object[] {
			"9800",
			"19200",
			"38400",
			"115200"});
			this.baudRateSelector.Name = "baudRateSelector";
			this.baudRateSelector.Size = new System.Drawing.Size(75, 25);
			this.baudRateSelector.Text = "9600";
			// 
			// toolStripButton4
			// 
			this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
			this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton4.Name = "toolStripButton4";
			this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton4.Text = "Open serial port";
			this.toolStripButton4.Click += new System.EventHandler(this.openSerialWriter);
			// 
			// toolStripButton5
			// 
			this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
			this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton5.Name = "toolStripButton5";
			this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton5.Text = "Close serial port";
			this.toolStripButton5.Click += new System.EventHandler(this.ToolStripButton5Click);
			// 
			// toolStripButton6
			// 
			this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
			this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton6.Name = "toolStripButton6";
			this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton6.Text = "Draw all to serial port";
			this.toolStripButton6.Click += new System.EventHandler(this.ToolStripButton6Click);
			// 
			// toolStripMaxX
			// 
			this.toolStripMaxX.Name = "toolStripMaxX";
			this.toolStripMaxX.Size = new System.Drawing.Size(23, 25);
			this.toolStripMaxX.Text = "200";
			// 
			// toolStripMaxY
			// 
			this.toolStripMaxY.Name = "toolStripMaxY";
			this.toolStripMaxY.Size = new System.Drawing.Size(23, 25);
			this.toolStripMaxY.Text = "200";
			// 
			// toolStripAppCloseButton
			// 
			this.toolStripAppCloseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripAppCloseButton.Name = "toolStripAppCloseButton";
			this.toolStripAppCloseButton.Size = new System.Drawing.Size(91, 22);
			this.toolStripAppCloseButton.Text = "Close application";
			this.toolStripAppCloseButton.Click += new System.EventHandler(this.ToolStripAppCloseButton);
			// 
			// treeView1
			// 
			this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.treeView1.Location = new System.Drawing.Point(436, 39);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(454, 388);
			this.treeView1.TabIndex = 2;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(902, 439);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.dotDrawing);
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		}
	}
