/*
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
using DeltaDrawing.DotDrawing.ShapeBuilding;

namespace DrawingApplication
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		const int WM_KEYDOWN = 0x100;
		const int WM_KEYUP = 0x101;
		const int WM_SYSKEYDOWN = 0x104;
			
		AbstractBuilder lineBuilder = new LineBuilder();
		AbstractBuilder circleBuilder = new CircleBuilder();
		AbstractBuilder freeBuilder = new FreeBuilder();
		AbstractBuilder activeBuilder;
		
		private bool snapToGrid = false;
		public MainForm()
		{
 
			InitializeComponent();
			
			lineBuilder.Attach(this.dotDrawing);
			circleBuilder.Attach(this.dotDrawing);
			freeBuilder.Attach(this.dotDrawing);
			
			lineBuilder.BuildFinished += BuildFinished;
			circleBuilder.BuildFinished += BuildFinished;
			snapToGridButton.Checked = snapToGrid;
		}

		void BuildFinished(object sender, ShapeBuildArgs e)
		{
			//dotDrawing.Drawings.Add(e.Shape);
		}

		//		public bool PreFilterMessage(ref Message m)
		//		{
		//			if (m.Msg == WM_KEYUP) {
		//				Debug.WriteLine("Filter -> KeyUp LastKeyPressed=" + lastKeyPressed.ToString());
		//			}
		//			return false;
		//		}
		
		
		//		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		//		{
		//
		//
		//			if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN)) {
		//				switch (keyData) {
		//
		//					case Keys.Escape:
		//						break;
		//					case Keys.Down:
		//						this.Parent.Text = "Down Arrow Captured";
		//						break;
		//
		//					case Keys.Up:
		//						this.Parent.Text = "Up Arrow Captured";
		//						break;
		//
		//					case Keys.Tab:
		//						this.Parent.Text = "Tab Key Captured";
		//						break;
		//
		//					case Keys.Control | Keys.M:
		//						this.Parent.Text = "<CTRL> + M Captured";
		//						break;
		//
		//					case Keys.Alt | Keys.Z:
		//						this.Parent.Text = "<ALT> + Z Captured";
		//						break;
		//					case Keys.G:
		//						snapToGrid = true;
		//						snapToGridButton.Checked = snapToGrid;
		//						break;
		//				}
		//			}
		//
		//			if (msg.Msg == WM_KEYUP) {
		//				switch (keyData) {
		//					case Keys.G:
		//						snapToGrid = false;
		//						snapToGridButton.Checked = snapToGrid;
		//						break;
		//				}
		//			}
		//			return base.ProcessCmdKey(ref msg, keyData);
		//		}
		
		void ToolStripButton1Click(object sender, EventArgs e)
		{
			activeBuilder = lineBuilder;
			lineBuilder.Begin();
		}
		
		void ToolStripButton2Click(object sender, EventArgs e)
		{
			activeBuilder = circleBuilder;
			circleBuilder.Begin();
		}
		
		void ToolStripButton3Click(object sender, EventArgs e)
		{
			activeBuilder = freeBuilder;
			freeBuilder.Begin();
		}
		void ToolStripButton4Click(object sender, EventArgs e)
		{
			snapToGrid = snapToGridButton.Checked;
			if (activeBuilder != null) {
				activeBuilder.SnapToGrid = snapToGrid;
			}
		}
		void DotDrawingKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.G) {
				updateSnap(true);
			}
		}
		void DotDrawingKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.G) {
				updateSnap(false);
			}
		}
		
		private void updateSnap(bool snap)
		{
			snapToGrid = snap;
			snapToGridButton.Checked = snap;
			if (activeBuilder != null) {
				activeBuilder.SnapToGrid = snap;
			}
		}
		
	}
}
