/*
 * Created by SharpDevelop.
 * User: Sam
 * Date: 6/27/2016
 * Time: 10:04 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;

namespace DeltaDrawing.DotDrawing.Drawings
{
	public interface IDrawing
	{
		List<SimpleLine> Components { get; }
		bool Selected { get; set; }
		void Draw(Graphics graphics);
		Rectangle Bounds { get; }
		List<Point> Points {get; set;}
	}
}
