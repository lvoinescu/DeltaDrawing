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

namespace DeltaDrawing.DotDrawing.ShapeBuilding.Transformation
{
	/// <summary>
	/// Description of Rotation.
	/// </summary>
	public class Rotation : ITransformation
	{
		
		public int Angle { get; set; }
		public Point Center { get; set; }
		
		public List<Point> Transform(List<Point> points)
		{
			List<Point> transforationPoints = new List<Point>(points.Count);
			foreach (Point p in points) {
				transforationPoints.Add(RotatePoint(p, Center, Angle));
			}
			
			return transforationPoints;
		}
		
		protected Point RotatePoint(Point point, Point center, int angle)
		{
			Point p = new Point(point.X, point.Y);
			
			double s = Math.Sin(Math.PI * angle / 180);
			double c = Math.Cos(Math.PI * angle / 180);
			
			// translate point back to origin:
			p.Offset(-center.X, -center.Y);
			
			// rotate point
			double xnew = p.X * c - p.Y * s;
			double ynew = p.X * s + p.Y * c;
			
			// translate point back:
			p.X = (int)(xnew + center.X);
			p.Y = (int)(ynew + center.Y);
			return p;
		}
		
	}
}
