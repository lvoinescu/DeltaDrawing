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
	/// <summary>
	/// Description of CircleBuilder.
	/// </summary>
	public class CircleBuilder : BuilderTool
	{
		const int ANGLE_STEP = 10;
		const int MIN_ANGLE_STEP = 4;
		const int MAX_ANGLE_STEP = 90;
		
		PlottedShape shape;
		DotDrawing dotDrawing;
		State state;
		bool snapToGrid = true;
		Point center;
		int angleStep = ANGLE_STEP;
		int radius = 0;
		byte clickCount = 0;

		public event BuildEndedHandler BuildFinished;

		public void Attach(DotDrawing dotDrawing)
		{
			this.dotDrawing = dotDrawing;
			state = State.NOT_INITIALIZED;
			dotDrawing.MouseDown += OnMouseDown;
			dotDrawing.MouseUp += OnMouseUp;
			dotDrawing.MouseMove += MouseMove;
			dotDrawing.MouseWheel += MouseWheel;
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
			clickCount = 0;
			return shape;
		}

		public PlottedShape End()
		{
			Active = false;
			state = State.NOT_INITIALIZED;
			return shape;
		}

		void OnMouseDown(object sender, MouseEventArgs e)
		{
			
			if (!Active)
				return;
			
			switch (state) {
				case State.NOT_INITIALIZED:
					shape.Points = new List<Point>();
					
					center = e.Location;
					if (snapToGrid) {
						int gridSize = dotDrawing.GridSize;
						center = new Point((int)e.Location.X / gridSize * gridSize, (int)e.Location.Y / gridSize * gridSize);
					} else {
						center = e.Location;
					}
					state = State.STARTED;
					clickCount++;
					break;
				case State.STARTED:
					if(clickCount == 1) {
						clickCount = 0;
						state =  State.NOT_INITIALIZED;
						Active = false;
						BuildFinished(this, new ShapeBuildArgs(shape));
					}
					break;
				case State.ENDED:
					break;
			}
		}

		void OnMouseUp(object sender, MouseEventArgs e)
		{
			//throw new NotImplementedException();
		}

		void MouseMove(object sender, MouseEventArgs e)
		{
			if (!Active)
				return;
			
			if (state == State.STARTED) {
				Pen pen = Pens.Beige;
				using (Graphics graphics = dotDrawing.CreateGraphics()) {
					Point leftTopPoint;
					Point rightBottomPoint;
					
					int dx, dy;
					
					if (snapToGrid) {
						int gridSize = dotDrawing.GridSize;
						leftTopPoint = new Point((int)e.Location.X / gridSize * gridSize, (int)e.Location.Y / gridSize * gridSize);
						dx = center.X - (int)e.Location.X / gridSize * gridSize;
						dy = center.Y - (int)e.Location.Y / gridSize * gridSize;
						
					} else {
						dx = center.X - e.Location.X;
						dy = center.Y - e.Location.Y;
						leftTopPoint = e.Location;
					}
					
					radius = (int)Math.Sqrt(dx * dx + dy * dy);
					rightBottomPoint = new Point(center.X + radius, center.Y + radius);
					
					Rectangle rect = Rectangle.FromLTRB(leftTopPoint.X, leftTopPoint.Y, rightBottomPoint.X, rightBottomPoint.Y);
					updatePoints(center, radius);
					dotDrawing.Invalidate(rect);
				}
			}
		}

		void updatePoints(Point c, int r)
		{
			shape.Points = ComputePoints(c, r);
			shape.Components = ConnectDots(shape.Points);
		}
		
		void MouseWheel(object sender, MouseEventArgs e)
		{
			var deltaAngle = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
			angleStep += deltaAngle;
			
			if (angleStep < MIN_ANGLE_STEP)
				angleStep = MIN_ANGLE_STEP;
			
			if (angleStep > MAX_ANGLE_STEP)
				angleStep = MAX_ANGLE_STEP;
			
			updatePoints(center, radius);
			
			Rectangle rect = Rectangle.FromLTRB(center.X - radius, center.Y - radius, center.X + radius, center.Y + radius);
			rect.Inflate(10, 10);
			dotDrawing.Invalidate(rect);
		}
		
		public List<Point> ComputePoints(Point center, int radius)
		{
			List<Point> points = new List<Point>();
			for (int angle = 0; angle < 360; angle += angleStep) {
				Point p = new Point(center.X, center.Y);
				var radians = (double)angle * Math.PI / 180;
				p.Offset(-(int)(radius * Math.Cos(radians)), (int)(radius * Math.Sin(radians)));
				points.Add(new Point(p.X, p.Y));
			}
			return points;
		}
			
		
		private List<SimpleLine> ConnectDots(List<Point> points)
		{
			List<SimpleLine> lines = new List<SimpleLine>();
			for (int i = 0; i < points.Count - 1; i++) {
				lines.Add(new SimpleLine(points[i], points[i + 1]));
			}
			
			if (points.Count > 1) {
				lines.Add(new SimpleLine(points[points.Count - 1], points[0]));
			}
			return lines;
		}
			
	}
}
