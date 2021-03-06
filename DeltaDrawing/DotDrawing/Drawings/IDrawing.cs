﻿/*
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
	public delegate void RedrawRequiredHandler(IDrawing sender);
	
	
	public interface IDrawing
	{
		event RedrawRequiredHandler RedrawRequired;
		
		
		List<Point> Points { get; set; }
		List<SimpleLine> Components { get; set; }
		
		bool Highlighted { get; set; }
		bool Selected { get; set; }
		bool NeedsRedrawing { get; set; }
		
		IDrawing Parent{ get; }
		
		Point Center { get; }
		Rectangle Bounds { get; }
		
		void Draw(Graphics graphics);
		void Update();
	}
}
