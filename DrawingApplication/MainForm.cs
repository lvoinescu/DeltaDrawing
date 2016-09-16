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
using System.IO;
using System.IO.Ports;
using System.Reflection;
using System.Windows.Forms;
using DeltaDrawing.DeltaOut.Dot.Writers;
using DeltaDrawing.DeltaOut.Dot.Writers.Serial;
using DeltaDrawing.DotDrawing.Drawings;
using DeltaDrawing.DotDrawing.ShapeBuilding;
using System.Collections.Generic;
using System.Drawing;

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
		
		
		
		bool snapToGrid = false;
		bool drawCircles = false;
		
		IDeltaPointsWriter asyncSerialWriter;

		public MainForm()
		{
			InitializeComponent();
			
			lineBuilder.Attach(this.dotDrawing);
			circleBuilder.Attach(this.dotDrawing);
			freeBuilder.Attach(this.dotDrawing);
			
			
			lineBuilder.BuildFinished += lineBuilder_BuildFinished;
			circleBuilder.BuildFinished += lineBuilder_BuildFinished;
			freeBuilder.BuildFinished += lineBuilder_BuildFinished;
			
			snapToGridButton.Checked = snapToGrid;
			
		}

		void ToolStripAppCloseButton(object sender, System.EventArgs e)
		{
			asyncSerialWriter.Close();
			Application.Exit();
		}


		void lineBuilder_BuildFinished(object sender, ShapeBuildArgs e)
		{
			var count = dotDrawing.Drawings.Count;
			var shapeNode = new TreeNode(count + ":" + dotDrawing.Drawings[count - 1].Points.Count);
			foreach (Point p in e.Shape.Points) {
				TreeNode pointNode = new TreeNode(p.X + "," + p.Y);
				shapeNode.Nodes.Add(pointNode);
			}
			treeView1.Nodes.Add(shapeNode);
			
			if (asyncSerialWriter != null) {
				asyncSerialWriter.WriteLine(ScaleTransform(e.Shape.Points));
			}
		}

		List<Point> ScaleTransform(List<Point> points)
		{
			int maxX = int.Parse(toolStripMaxX.Text);
			int maxY = int.Parse(toolStripMaxY.Text);

			List<Point> scaledPoints = new List<Point>();
			for (int i = 0; i < points.Count; i++) {
				scaledPoints.Add(new Point(
					(int)((double)maxX * ((double)points[i].X / this.dotDrawing.Width)) - maxX / 2,
					(int)((double)maxY * ((double)points[i].Y / this.dotDrawing.Height) - maxY / 2)
				));
			}
			return scaledPoints;
		}

		void ToolStripButton1Click(object sender, EventArgs e)
		{
			activeBuilder = lineBuilder;
			freeBuilder.Active = false;
			circleBuilder.Active = false;
			
			toolStripButton2.Checked = false;
			toolStripButton3.Checked = false;
			
			lineBuilder.Begin();
		}

		void ToolStripButton2Click(object sender, EventArgs e)
		{
			activeBuilder = circleBuilder;
			freeBuilder.Active = false;
			lineBuilder.Active = false;
			
			toolStripButton1.Checked = false;
			toolStripButton3.Checked = false;
			
			circleBuilder.Begin();
		}

		void ToolStripButton3Click(object sender, EventArgs e)
		{
			activeBuilder = freeBuilder;
			lineBuilder.Active = false;
			circleBuilder.Active = false;
			if (toolStripButton3.Checked) {
				freeBuilder.Begin();
				toolStripButton1.Checked = false;
				toolStripButton2.Checked = false;
			} else {
				freeBuilder.Active = false;
			}
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

		void updateSnap(bool snap)
		{
			snapToGrid = snap;
			snapToGridButton.Checked = snap;
			if (activeBuilder != null) {
				activeBuilder.SnapToGrid = snap;
			}
		}

		
		void openSerialWriter(object sender, EventArgs e)
		{
			initSerialWriter();
		}

		void initSerialWriter()
		{
			if (asyncSerialWriter == null) {
				
				SerialPort serialPort = new SerialPort(serialPortSelector.Text, 
					                        int.Parse(baudRateSelector.Text));
				serialPort.RtsEnable = true;
				serialPort.DtrEnable = true;
				serialPort.Handshake = Handshake.None;
				asyncSerialWriter = new AsyncDeltaPointsWriter(new SerialPointsWriter(serialPort));
			} else {
				asyncSerialWriter.Close();
			}
			
			asyncSerialWriter.Open();
		}

		void ToolStripButton5Click(object sender, EventArgs e)
		{
			if (asyncSerialWriter != null) {
				asyncSerialWriter.Close();
			}
		}

		void ToolStripButton6Click(object sender, EventArgs e)
		{
			initSerialWriter();
			foreach (var drawing in dotDrawing.Drawings) {
				if (drawing is PlottedShape) {
					asyncSerialWriter.WriteLine (ScaleTransform (drawing.Points));
				}
			}
		}
		
		void ToolStripButton7Click(object sender, EventArgs e)
		{
			dotDrawing.Drawings.Clear();
			treeView1.Nodes.Clear();
			dotDrawing.Invalidate();
		}
		
		void ToolStripSeparator4Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.ShowDialog();
			
			if (!String.IsNullOrEmpty(sfd.FileName)) {
				saveDrawing(sfd.FileName);
			}
		}

		void saveDrawing(string fileName)
		{
			StreamWriter writer = new StreamWriter(fileName);
			foreach (IDrawing drawing in dotDrawing.Drawings) {
				saveDrawing(drawing, writer);
				writer.WriteLine();
			}
			writer.Flush();
			writer.Close();
		}
		
		void saveDrawing(IDrawing drawing, StreamWriter writer)
		{
			for (int i = 0; i < drawing.Points.Count; i++) {
				Point p = drawing.Points[i];
				writer.Write(p.X + "," + p.Y);
				if (i != drawing.Points.Count - 1) {
					writer.Write("|");
				}
			}
		}
		void ToolStripButton8Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.ShowDialog();
			
			if (!String.IsNullOrEmpty(ofd.FileName)) {
				openDrawing(ofd.FileName);
			}
			dotDrawing.Invalidate();
		}

		void ToolStripButton9Click(object sender, EventArgs e)
		{
			drawCircles = !drawCircles;
			foreach (IDrawing drawing in dotDrawing.Drawings) {
				if (drawing is PlottedShape) {
					PlottedShape pShape = drawing as PlottedShape;
					pShape.DrawCircles = drawCircles;
					pShape.NeedsRedrawing = true;
				}
			}
			dotDrawing.Invalidate();
		}

		void openDrawing(String fileName)
		{
			StreamReader reader = new StreamReader(fileName);
			var line = reader.ReadLine();
			while (!String.IsNullOrEmpty(line)) {
				PlottedShape shape = new PlottedShape();
				shape.Points = new List<Point>();
				
				foreach (var stringPoint in line.Split('|')) {
					string[] coordinates = stringPoint.Split(',');
					Point p = new Point(int.Parse(coordinates[0]), int.Parse(coordinates[1]));
					shape.Points.Add(p);
				}
				
				List<SimpleLine> lines = new LineConnection(shape.Points).Lines();
				shape.Components = lines;
				shape.NeedsRedrawing = true;
				
				dotDrawing.AddShape(shape);
				line = reader.ReadLine();
			}
		}
	}
}
