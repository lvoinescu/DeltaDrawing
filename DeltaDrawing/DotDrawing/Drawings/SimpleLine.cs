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

namespace DeltaDrawing.DotDrawing.Drawings
{

	public class SimpleLine : IDrawing
	{
		public event RedrawRequiredHandler RedrawRequired;

		Point p1, p2;
		IDrawing parent;
		
		public Point P1 {
			get {
				return p1;
			}
			set {
				p1 = value;
			}
		}

		public Point P2 {
			get {
				return p2;
			}
			set {
				p2 = value;
			}
		}

		public bool NeedsRedrawing {
			get;
			set;
		}
		
		public SimpleLine(Point p1, Point p2)
		{
			this.p1 = p1;
			this.p2 = p2;
		}

		public void Draw(Graphics graphics)
		{
			
			if (Highlighted) {
				Pen pen1 = new Pen(Color.Green, 2);
				graphics.DrawLine(pen1, p1, p2);
			} else {
				Pen pen2 = new Pen(Color.Black, 2);
				graphics.DrawLine(Pens.Black, p1, p2);
			}
			NeedsRedrawing = false;
		}

		public List<SimpleLine> Components {
			get {
				throw new NotImplementedException();
			}
		}

		public IDrawing Parent {
			get {
				return parent;
			}
			set {
				parent = value;
			}
		}
		
		public bool Highlighted {
			get;
			set;
		}

		public bool Selected {
			get;
			set;
		}
		
		public Rectangle Bounds {
			get {
				return Rectangle.FromLTRB(p1.X < p2.X ? p1.X : p2.X, p1.Y < p2.Y ? p1.Y : p2.Y,
					p2.X > p1.X ? p2.X : p1.X, P2.Y > P1.Y ? P2.Y : P1.Y);
			}
		}

		public List<Point> Points {
			get {
				return new List<Point>(new []{ p1, p2 });
			}
			set {
				this.Points = value;
			}
		}
		
		public override string ToString()
		{
			return string.Format("[SimpleLine P1={0}, P2={1}]", p1, p2);
		}

		public void Update()
		{
			NeedsRedrawing = true;
			if (RedrawRequired != null) {
				RedrawRequired(this);
			}
		}
	}
}
