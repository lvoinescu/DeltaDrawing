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
		
		const int ANGLE_STEP = 10;
		const int CENTER_PAD = 6;
		
		readonly DotDrawing dotDrawing;
		IDrawing selectedDrawing;
		List<Point> pointsToRotate;
		int angle;
		
		
		public bool Activated { get; set; }
		
		public RotateAction(DotDrawing dotDrawing)
		{
			this.dotDrawing = dotDrawing;
			dotDrawing.MouseDown += dotDrawing_MouseDown;
			dotDrawing.MouseMove += dotDrawing_MouseMove;
			dotDrawing.MouseUp += dotDrawing_MouseUp;
			dotDrawing.MouseWheel += dotDrawing_MouseWheel;
		}

		void dotDrawing_MouseDown(object sender, MouseEventArgs e)
		{
			selectedDrawing = CheckStart(e.Location);
			if (selectedDrawing != null) {
				Activated = true;
				pointsToRotate = selectedDrawing.Points;
			}
			
		}

		void dotDrawing_MouseWheel(object sender, MouseEventArgs e)
		{
			if (!Activated)
				return;
			
			var delta = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
			
			angle += delta;
			
			ITransformation transformation = new Rotation(e.Location, angle);
			List<Point> newPoints = transformation.Transform(pointsToRotate);
			
			List<SimpleLine> lines = new LineConnection(newPoints).Lines();
			
			selectedDrawing.Points = newPoints;
			selectedDrawing.Components = lines;
			
			selectedDrawing.Update();
		}
		
		void dotDrawing_MouseMove(object sender, MouseEventArgs e)
		{
			if(!Activated)
				return;

		}

		void dotDrawing_MouseUp(object sender, MouseEventArgs e)
		{
			Activated = false;
		}
		
		IDrawing CheckStart(Point p)
		{
			foreach (var drawing in dotDrawing.Drawings) {
			
				if (drawing.Selected && drawing.Bounds.Contains(p)) {
					return drawing;
				}
			}
			return null;
		}
	}
}
