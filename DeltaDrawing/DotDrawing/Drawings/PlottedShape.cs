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
	public class PlottedShape : IDrawing
	{
		public event RedrawRequiredHandler RedrawRequired;

		const int RADIUS = 3;
		const int CROSS_SIZE = 4;
		
		IDrawing parent;

		Rectangle bounds;
		List<SimpleLine> simpleLines;
		
		public List<Point> Points {
			get;
			set;
		}

		public IDrawing Parent {
			get {
				return parent;
			}
		}

		public bool NeedsRedrawing {
			get;
			set;
		}
		
		public PlottedShape()
		{
			Points = new List<Point>();
			simpleLines = new List<SimpleLine>();
			bounds = new Rectangle();
		}
		
		public PlottedShape(List<Point> points)
		{
			Point[] pointArray = points.ToArray();
			this.Points = new List<Point>(points.Count);
			simpleLines = new List<SimpleLine>(points.Count + 1);
			
			foreach (Point p in points) {
				Points.Add(new Point(p.X, p.Y));
			}
			
			for (int i = 0; i < Points.Count - 2; i++) {
				var simpleLine = new SimpleLine(Points[i], Points[i + 1]);
				simpleLine.Parent = this;
				Components.Add(simpleLine);
			}
			bounds = GetBounds();
		}
		
		public void AddPoint(Point p)
		{
			Points.Add(p);
			bounds = GetBounds();
			if (Points.Count > 1) {
				Components.Add(new SimpleLine(Points[Points.Count - 2], Points[Points.Count - 1]));
			}
		}


		public void Draw(Graphics graphics)
		{
			bounds = GetBounds();
			Pen pen = new Pen(Color.Black, 2);
			
			foreach (SimpleLine line in Components) {
				line.Draw(graphics);
			}
			
			foreach (Point p in Points) {
				Point center = new Point(p.X, p.Y);
				center.Offset(-RADIUS / 2, -RADIUS / 2);
				graphics.DrawEllipse(Pens.Blue, new RectangleF(center, new Size(RADIUS, RADIUS)));
			}
			
			if (Selected) {
				Rectangle r = bounds;
				r.Inflate(-3, -3);
				graphics.DrawRectangle(new Pen(Color.Red, 1), r);
				DrawCenter(graphics);
			}
			
			//NeedsRedrawing = false;
		}
		
		protected void DrawCenter(Graphics graphics)
		{
			graphics.DrawLine(Pens.Red, Center.X, Center.Y - CROSS_SIZE, Center.X, Center.Y + CROSS_SIZE);
			graphics.DrawLine(Pens.Red, Center.X - CROSS_SIZE, Center.Y, Center.X + CROSS_SIZE, Center.Y);
		}

		public List<SimpleLine> Components {
			get {
				return simpleLines;
			}
			set {
				simpleLines = value;
				foreach (var simpleLine in simpleLines) {
					simpleLine.Parent = this;
				}
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
				return bounds;
			}
		}
		
		Rectangle GetBounds()
		{
			
			if (Points.Count < 1)
				return new Rectangle(0, 0, 0, 0);
			
			int minX = int.MaxValue, minY = int.MaxValue, maxX = int.MinValue, maxY = int.MinValue;
			
			foreach (Point p in Points) {
				if (minX > p.X) {
					minX = p.X;
				}
				
				if (maxX < p.X) {
					maxX = p.X;
				}
				
				if (minY > p.Y) {
					minY = p.Y;
				}
				
				if (maxY < p.Y) {
					maxY = p.Y;
				}
				
			}
			
			var rectangle = Rectangle.FromLTRB(minX, minY, maxX, maxY);
			rectangle.Inflate(RADIUS, RADIUS);
			if (Selected)
				rectangle.Inflate(2, 2);
			return rectangle;
		}

		public void Update()
		{
			NeedsRedrawing = true;
			if (parent != null)
				parent.NeedsRedrawing = true;
			RedrawRequired(this);
		}
		
		public Point Center {
			get {
				return new Point(Bounds.Left + Bounds.Width / 2, Bounds.Top + Bounds.Height / 2);
			}
		}
	}
}
