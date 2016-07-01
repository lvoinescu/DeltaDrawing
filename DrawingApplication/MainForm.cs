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
using System;
using System.Windows.Forms;
using DeltaDrawing.DotDrawing.ShapeBuilder;

namespace DrawingApplication
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		MouseLineBuilder lineBuilder = new MouseLineBuilder();
		public MainForm()
		{
 
			InitializeComponent();
			
			lineBuilder.attach(this.dotDrawing);
		}
		void ToolStripButton1Click(object sender, EventArgs e)
		{
			if (toolStripButton1.Checked) {
				dotDrawing.Drawings.Add(lineBuilder.Begin());
			} else {
				var plottedShape = lineBuilder.End();
				if (plottedShape != null)
					dotDrawing.Drawings.Add(plottedShape);
			}
		}
		
		
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			const int WM_KEYDOWN = 0x100;
			const int WM_SYSKEYDOWN = 0x104;
			
			if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN)) {
				switch (keyData) {
						
					case Keys.Escape:
						dotDrawing.Drawings.Add(lineBuilder.End());
						break;
					case Keys.Down:
						this.Parent.Text = "Down Arrow Captured";
						break;
						
					case Keys.Up:
						this.Parent.Text = "Up Arrow Captured";
						break;
						
					case Keys.Tab:
						this.Parent.Text = "Tab Key Captured";
						break;
						
					case Keys.Control | Keys.M:
						this.Parent.Text = "<CTRL> + M Captured";
						break;
						
					case Keys.Alt | Keys.Z:
						this.Parent.Text = "<ALT> + Z Captured";
						break;
				}
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}
		
	}
}
