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
using System.Drawing;
using DeltaDrawing.DotDrawing.Drawings;

namespace DeltaDrawing.DotDrawing.ShapeBuilding
{
	/// <summary>
	/// Description of BuilderTool.
	/// </summary>
	public abstract class AbstractBuilder
	{
		public event BuildEndedHandler BuildFinished;
		
		protected PlottedShape shape;
		protected DotDrawing dotDrawing;
		protected State state;
		protected bool snapToGrid = false;
		
		public abstract void Attach(DotDrawing dotDrawing);
		
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
			
			//Active = false;
			return shape;
		}
		
		public PlottedShape Shape {
			get {
				return shape;
			}
		}

		public bool SnapToGrid {
			get {
				return snapToGrid;
			}
			set {
				snapToGrid = value;
			}
		}

		public bool Active { get ; set; }
	}
}
