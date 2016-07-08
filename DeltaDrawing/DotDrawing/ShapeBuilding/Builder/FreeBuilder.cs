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

	public class FreeBuilder : AbstractBuilder
	{
		const int DEFAULT_CAPACITY = 100;
		bool paint = false;
		

		public override void Attach(DotDrawing dotDrawing)
		{
			this.dotDrawing = dotDrawing;
			dotDrawing.MouseDown += OnMouseDown;
			dotDrawing.MouseUp += OnMouseUp;
			dotDrawing.MouseMove += MouseMove;
		}
		
		void OnMouseDown(object sender, MouseEventArgs e)
		{
			if (!Active)
				return;
			paint = true;
			Point p;
			switch (state) {
				case State.NOT_INITIALIZED:
					shape.Points = new List<Point>(DEFAULT_CAPACITY);
					shape.Components = new List<SimpleLine>(DEFAULT_CAPACITY + 1);
					if (snapToGrid) {
						int gridSize = dotDrawing.GridSize;
						p = new Point((int)e.Location.X / gridSize * gridSize, (int)e.Location.Y / gridSize * gridSize);
					} else
						p = e.Location;
					shape.AddPoint(p);
					shape.Components.Add(new SimpleLine(shape.Points[shape.Points.Count - 1], p));
					Region region = new Region(shape.Bounds);
					dotDrawing.Invalidate(region);
					state = State.STARTED;
					break;
				case State.STARTED:
					
					
					break;
				case State.ENDED:

					break;
			}
		}

		void OnMouseUp(object sender, MouseEventArgs e)
		{
			paint = false;
			if (state == State.NOT_INITIALIZED)
				return;
			
			if (shape.Points.Count > 0) {
				Region invalidatedRegion = new Region(shape.Bounds);
				dotDrawing.Invalidate(invalidatedRegion);
				shape = new PlottedShape();
				dotDrawing.Drawings.Add(shape);
			}
			
			state = State.NOT_INITIALIZED;
		}

		void MouseMove(object sender, MouseEventArgs e)
		{

			if (state == State.NOT_INITIALIZED)
				return;
			
			if (!paint)
				return;
			
			Point newPoint;
			if (snapToGrid) {
				int gridSize = dotDrawing.GridSize;
				newPoint = new Point((int)e.Location.X / gridSize * gridSize, (int)e.Location.Y / gridSize * gridSize);
			} else {
				newPoint = e.Location;
			}
				
			if (shape.Points.Count > 1) {
				Point lastPoint = shape.Points[shape.Points.Count - 1];
				shape.Components.Add(new SimpleLine(lastPoint, newPoint));
			
			}
				
			shape.Points.Add(newPoint);
			dotDrawing.Invalidate(shape.Bounds);

		}
		
		
	}
}
