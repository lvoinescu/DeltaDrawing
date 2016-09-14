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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DeltaDrawing.DotDrawing.Drawings;

namespace DeltaDrawing.DotDrawing.ShapeBuilding
{
	public class LineBuilder : AbstractBuilder
	{
		

		public override void Attach(DotDrawing dotDrawing)
		{
			this.dotDrawing = dotDrawing;
			dotDrawing.MouseDown += OnMouseDown;
			dotDrawing.MouseMove += MouseMove;
			dotDrawing.KeyDown += KeyDown;
		}

		void OnMouseDown(object sender, MouseEventArgs e)
		{
			if (!Active)
				return;
			
			Point p;
			switch (state) {
				case State.NOT_INITIALIZED:
					shape.Points = new List<Point>();
					dotDrawing.AddShape(shape);
					
					p = e.Location;
					if (snapToGrid) {
						int gridSize = dotDrawing.GridSize;
						p = new Point((int)e.Location.X / gridSize * gridSize, (int)e.Location.Y / gridSize * gridSize);
					} else
						p = e.Location;
					
					shape.AddPoint(p);
					shape.AddPoint(p);
					var simpleLine = new SimpleLine(p, p);
					simpleLine.Parent = shape;
					shape.Components.Add(simpleLine);
					state = State.STARTED;
					break;
				case State.STARTED:
					if (snapToGrid) {
						int gridSize = dotDrawing.GridSize;
						p = new Point((int)e.Location.X / gridSize * gridSize, (int)e.Location.Y / gridSize * gridSize);
					} else
						p = e.Location;
					
					shape.AddPoint(p);
					
					var firstLine = new SimpleLine(shape.Points[shape.Points.Count - 1], p);
					firstLine.Parent = shape;
					shape.Components.Add(firstLine);
					break;
				case State.ENDED:

					break;
			}
			shape.Update();
		}


		void MouseMove(object sender, MouseEventArgs e)
		{
			if (state == State.NOT_INITIALIZED)
				return;
			
			if (shape == null || shape.Points == null || shape.Points.Count < 1) {
				return;
			}
			
			if (shape.Points.Count > 1) {
				Point lastPoint = shape.Points[shape.Points.Count - 1];
				Point beforeLastPoint = shape.Points[shape.Points.Count - 2];
				
				if (snapToGrid) {
					int gridSize = dotDrawing.GridSize;
					Point p = new Point((int)e.Location.X / gridSize * gridSize, (int)e.Location.Y / gridSize * gridSize);
					
					shape.Points[shape.Points.Count - 1] = p;
					shape.Components[shape.Components.Count - 1].P2 = p;
				} else {
					shape.Points[shape.Points.Count - 1] = e.Location;
					shape.Components[shape.Components.Count - 1].P2 = e.Location;
				}
				
				shape.Components[shape.Components.Count - 1].Update();
				shape.Update();
			}
		}

 
		void KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape) {
				OnBuildFinished();
				Active = false;
				if (shape.Points.Count > 0) {
					shape.Points.RemoveAt(shape.Points.Count - 1);
					shape.Components.RemoveAt(shape.Components.Count - 1);
					shape.Update();
				}
				state = State.NOT_INITIALIZED;
			}
		}
	}
}
