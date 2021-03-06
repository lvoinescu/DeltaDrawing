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
using System.Drawing;

namespace DeltaDrawing.DotDrawing.Geometry
{
	/// <summary>
	/// Description of Geometry.
	/// </summary>
	public class Geometry
	{

		
		
		public static double TriangleHeight(Point p1, Point p2, Point pExt)
		{
			double a = Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
			double b = Math.Sqrt((pExt.X - p2.X) * (pExt.X - p2.X) + (pExt.Y - p2.Y) * (pExt.Y - p2.Y));
			double c = Math.Sqrt((pExt.X - p1.X) * (pExt.X - p1.X) + (pExt.Y - p1.Y) * (pExt.Y - p1.Y));
			                     
			double s = (double)(a + b + c) / 2;
			
			
			double h = 2 * Math.Sqrt(s * (s - a) * (s - b) * (s - c)) / a;
			
			return h;
		}
		
		public static double Distance(Point p1, Point p2)
		{
			int dx = p1.X - p2.X;
			int dy = p1.Y - p2.Y;
			return Math.Sqrt(dx * dx + dy * dy);
		}
		
		public static double Angle3Points(Point center, Point p1, Point p2)
		{
			double a = Math.Sqrt(Math.Pow((center.X - p1.X), 2) + Math.Pow(center.Y - p1.Y, 2));
			double b = Math.Sqrt(Math.Pow((center.X - p2.X), 2) + Math.Pow(center.Y - p2.Y, 2));
			double c = Math.Sqrt(Math.Pow((p1.X - p2.X), 2) + Math.Pow(p1.Y - p2.Y, 2));
		
			if (a * b != 0) {
				return 180 * Math.Acos((a * a + b * b - c * c) / (2 * a * b))/Math.PI;
			} else
				return 0;
		
		}
		
	}
	
}
