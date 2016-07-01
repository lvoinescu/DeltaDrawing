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
using DeltaDrawing.DotDrawing.Drawings;

namespace DeltaDrawing.DotDrawing.ShapeBuilder
{
	public class MouseLineBuilder : BuilderTool
	{
		
		internal enum State
		{
			NOT_INITIALIZED,
			STARTED,
			ENDED

		}
		
		DotDrawing dotDrawing;
		PlottedShape shape;
		State state;
		bool snapToGrid = true;

		public PlottedShape Shape {
			get {
				return shape;
			}
		}
		

		public PlottedShape Begin()
		{
			shape = new PlottedShape();
			return shape;
		}
		public PlottedShape End()
		{
			if (state == State.NOT_INITIALIZED)
				return null;
			
			if (shape.Points.Count > 0) {
				shape.Points.RemoveAt(shape.Points.Count - 1);
				Region invalidatedRegion = new Region(shape.Bounds);
				dotDrawing.Invalidate(invalidatedRegion);
			}
			state = State.NOT_INITIALIZED;
			return new PlottedShape(shape.Points);
		}
		public void attach(DotDrawing dotDrawing)
		{
			this.dotDrawing = dotDrawing;
			dotDrawing.MouseDown += OnMouseDown;
			dotDrawing.MouseUp += OnMouseUp;
			dotDrawing.MouseMove += MouseMove;
		}

		void OnMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			
			Point p;
			switch (state) {
				case State.NOT_INITIALIZED:
					shape.Points = new List<Point>();
					
					p = e.Location;
					if (snapToGrid) {
						int gridSize = dotDrawing.GridSize;
						p = new Point((int)e.Location.X / gridSize * gridSize, (int)e.Location.Y / gridSize * gridSize);
					}
					else
						p = e.Location;
					
					shape.AddPoint(p);
					shape.AddPoint(p);
					state = State.STARTED;
					break;
				case State.STARTED:
					
					if (snapToGrid) {
						int gridSize = dotDrawing.GridSize;
						p = new Point((int)e.Location.X / gridSize * gridSize, (int)e.Location.Y / gridSize * gridSize);
					}
					else
						p=e.Location;
					
					shape.Components.Add(new SimpleLine(shape.Points[shape.Points.Count - 2], p));
					shape.AddPoint(p);
					Region region = new Region(shape.Bounds);
					dotDrawing.Invalidate(region);
					break;
				case State.ENDED:

					break;
			}
		}

		void OnMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			//NOPE
		}

		void MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (state == State.NOT_INITIALIZED)
				return;
			
			if (shape == null || shape.Points == null || shape.Points.Count < 1) {
				return;
			}
			
			if (shape.Points.Count > 1) {
				Point lastPoint = shape.Points[shape.Points.Count - 1];
				Point penultimatePoint = shape.Points[shape.Points.Count - 2];
				
				if (snapToGrid) {
					int gridSize = dotDrawing.GridSize;
					Point p = new Point((int)e.Location.X / gridSize * gridSize, (int)e.Location.Y / gridSize * gridSize);
					shape.Points[shape.Points.Count - 1] = p;
					//shape.Components.Add(new SimpleLine(shape.Points[shape.Points.Count - 2], p));
				} else {
					shape.Points[shape.Points.Count - 1] = e.Location;
					//shape.Components.Add(new SimpleLine(shape.Points[shape.Points.Count - 2], e.Location));
				}
			
				dotDrawing.Invalidate(shape.Bounds);
				
			}
		}
	}
}
