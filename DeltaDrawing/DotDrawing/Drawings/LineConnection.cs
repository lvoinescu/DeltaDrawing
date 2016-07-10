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

namespace DeltaDrawing.DotDrawing.Drawings
{
	/// <summary>
	/// Description of LineConnection.
	/// </summary>
	public class LineConnection
	{
		List<Point> points;
		
		public LineConnection(List<Point> points)
		{
			this.points = points;
		}
		
		public List<SimpleLine> Lines()
		{
			List<SimpleLine> lines = new List<SimpleLine>();
			for (int i = 0; i < points.Count - 1; i++) {
				var simpleLine = new SimpleLine(points[i], points[i + 1]);
				lines.Add(simpleLine);
			}
			
			return lines;
		}
	}
}
