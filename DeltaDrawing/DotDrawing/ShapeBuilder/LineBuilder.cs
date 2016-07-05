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

namespace DeltaDrawing.DotDrawing.ShapeBuilder
{
	public class LineBuilder : BuilderTool
	{
		
		
		DotDrawing dotDrawing;
		PlottedShape shape;
		State state;
		bool snapToGrid = true;

		public event BuildEndedHandler BuildFinished;
		
		public PlottedShape Shape {
			get {
				return shape;
			}
		}
		
		public bool Active {
			get ;
			set ;
		}

		public PlottedShape Begin()
		{
			Active = true;
			shape = new PlottedShape();
			dotDrawing.Drawings.Add(shape);
			return shape;
		}
		public PlottedShape End()
		{
			if (state == State.NOT_INITIALIZED)
				return null;
			
			if (shape.Points.Count > 0) {
				shape.Points.RemoveAt(shape.Points.Count - 1);
				shape.Components.RemoveAt(shape.Components.Count - 1);
				Region invalidatedRegion = new Region(shape.Bounds);
				dotDrawing.Invalidate(invalidatedRegion);
			}
			state = State.NOT_INITIALIZED;
			
			Active = false;
			return shape;
		}
		
		public void Attach(DotDrawing dotDrawing)
		{
			this.dotDrawing = dotDrawing;
			dotDrawing.MouseDown += OnMouseDown;
			dotDrawing.MouseUp += OnMouseUp;
			dotDrawing.MouseMove += MouseMove;
			dotDrawing.KeyDown += KeyDown;
		}

		void OnMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (!Active)
				return;
			
			Point p;
			switch (state) {
				case State.NOT_INITIALIZED:
					shape.Points = new List<Point>();
					
					p = e.Location;
					if (snapToGrid) {
						int gridSize = dotDrawing.GridSize;
						p = new Point((int)e.Location.X / gridSize * gridSize, (int)e.Location.Y / gridSize * gridSize);
					} else
						p = e.Location;
					
					shape.AddPoint(p);
					shape.AddPoint(p);
					state = State.STARTED;
					break;
				case State.STARTED:
					
					if (snapToGrid) {
						int gridSize = dotDrawing.GridSize;
						p = new Point((int)e.Location.X / gridSize * gridSize, (int)e.Location.Y / gridSize * gridSize);
					} else
						p = e.Location;
					shape.AddPoint(p);
					//shape.Components.Add(new SimpleLine(shape.Points[shape.Points.Count - 2], shape.Points[shape.Points.Count - 1]));
					shape.Components.Add(new SimpleLine(shape.Points[shape.Points.Count - 1], p));
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
					shape.Components[shape.Components.Count - 1].P2 = p;
				} else {
					shape.Points[shape.Points.Count - 1] = e.Location;
					shape.Components[shape.Components.Count - 1].P2 = e.Location;
				}
			
				dotDrawing.Invalidate(shape.Bounds);
				
			}
		}

 
		void KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape){
				BuildFinished(this, new ShapeBuildArgs(shape));
				Active = false;
				state =  State.NOT_INITIALIZED;
			}
		}
	}
}
