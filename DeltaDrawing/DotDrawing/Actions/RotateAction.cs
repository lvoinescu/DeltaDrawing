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
using DeltaDrawing.DotDrawing.ShapeBuilding.Transformation;

namespace DeltaDrawing.DotDrawing.Actions
{
	/// <summary>
	/// Description of TranslateAction.
	/// </summary>
	public class RotateAction
	{
		const int CENTER_PAD = 6;
		readonly DotDrawing dotDrawing;
		Point initialLocation;
		IDrawing selectedDrawing;
		
		public bool Activated { get; set; }
		
		public RotateAction(DotDrawing dotDrawing)
		{
			this.dotDrawing = dotDrawing;
			dotDrawing.MouseDown += dotDrawing_MouseDown;
			dotDrawing.MouseMove += dotDrawing_MouseMove;
			dotDrawing.MouseUp += dotDrawing_MouseUp;
		}

		void dotDrawing_MouseDown(object sender, MouseEventArgs e)
		{
			selectedDrawing = CheckStart(e.Location);
			if (selectedDrawing != null) {
				Activated = true;
				initialLocation = e.Location;
			}
			
		}

		void dotDrawing_MouseMove(object sender, MouseEventArgs e)
		{
			if (!Activated)
				return;
			

			int angle = (int)Geometry.Geometry.Angle3Points(selectedDrawing.Center, initialLocation, e.Location);
			
			if(Math.Pow(initialLocation.X, 2) + Math.Pow(initialLocation.Y, 2) >
				Math.Pow(e.Location.X, 2) + Math.Pow(e.Location.Y, 2)) {
				angle = -angle;
			}
			
			ITransformation transformation = new Rotation(selectedDrawing.Center, angle);
			List<Point> newPoints = transformation.Transform(selectedDrawing.Points);
			
			List<SimpleLine> lines = new LineConnection(newPoints).Lines();
			
			selectedDrawing.Points = newPoints;
			selectedDrawing.Components = lines;
			
			selectedDrawing.Update();

		}

		void dotDrawing_MouseUp(object sender, MouseEventArgs e)
		{
			Activated = false;
		}
		
		IDrawing CheckStart(Point p)
		{
			foreach (var drawing in dotDrawing.Drawings) {

				Point topRightCorner = new Point(drawing.Bounds.Left + drawing.Bounds.Width,
					                       drawing.Bounds.Top);
				initialLocation = p;
				
				Point c1 = topRightCorner;
				Point c2 = topRightCorner;
				c1.Offset(-CENTER_PAD, -CENTER_PAD);
				c2.Offset(CENTER_PAD, CENTER_PAD);
				
				Rectangle r = Rectangle.FromLTRB(c1.X, c1.Y, c2.X, c2.Y);
				
				if (drawing.Selected && r.Contains(p)) {
					return drawing;
				}
			}
			return null;
		}
	}
}
