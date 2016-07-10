/*
 * Created by SharpDevelop.
 * User: Sam
 * Date: 6/27/2016
 * Time: 9:31 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DeltaDrawing.DotDrawing.Drawings;

namespace DeltaDrawing.DotDrawing
{
	/// <summary>
	/// Description of DotDrawing.
	/// </summary>
	public partial class DotDrawing : UserControl
	{
		const int HOVER_DS = 10;
		Random r = new Random(255);
		
		int gridSize;
		
		Rectangle previousDrawnRegion;
		public int GridSize {
			get {
				return gridDrawer.GridSize;
			}
			set {
				gridDrawer.GridSize = value;
			}
		}
		GridDrawer gridDrawer;
		
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<IDrawing> Drawings {
			get;
			set;
		}
		
		public DotDrawing()
		{
			Drawings = new List<IDrawing>();
			gridDrawer = new GridDrawer(this, 10);
			InitializeComponent();
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw |
			ControlStyles.DoubleBuffer | ControlStyles.UserPaint, true);
		}

		public void AddShape(IDrawing shape)
		{
			shape.RedrawRequired += shape_RedrawRequired;
			Drawings.Add(shape);
		}
		
		void shape_RedrawRequired(IDrawing sender)
		{
			sender.NeedsRedrawing = true;
			var toInvalidate = GetRegionToRedraw(sender.Bounds);
			//Rectangle toInvalidate = Rectangle.Union(previousDrawnRegion, rectangle);
			//previousDrawnRegion.Inflate(10, 10);
			Invalidate(toInvalidate);
			previousDrawnRegion = toInvalidate;
			
			
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

						
			
			int red = r.Next(255);
			int green = r.Next(255);
			int blue = r.Next(255);
			
			Rectangle ri = e.ClipRectangle;
			
			GetRegionToRedraw(ri);
			
			Color color = Color.FromArgb(100, red, green, blue);
			//e.Graphics.FillRectangle(new SolidBrush(color), ri);
			
			var graphics = e.Graphics;
			if (gridDrawer != null)
				gridDrawer.Draw(graphics);
			
			if (Drawings != null)
				foreach (IDrawing drawing in Drawings) {
					if (drawing.NeedsRedrawing) {
						drawing.Draw(graphics);
						drawing.NeedsRedrawing = false;
					}
				}
			base.OnPaint(e);
		}
		
		
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			const int WM_KEYDOWN = 0x100;
			const int WM_SYSKEYDOWN = 0x104;
			
			if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN)) {
				switch (keyData) {
						
					case Keys.Escape:
						break;
					case Keys.Down:
						this.Parent.Text = "Down Arrow Captured";
						break;
						
					case Keys.Up:
						this.Parent.Text = "Up Arrow Captured";
						break;
						
					case Keys.Tab:
						this.Parent.Text = "Tab Key Captured";
						break;
						
					case Keys.Control | Keys.M:
						this.Parent.Text = "<CTRL> + M Captured";
						break;
						
					case Keys.Alt | Keys.Z:
						this.Parent.Text = "<ALT> + Z Captured";
						break;
				}
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		void DotDrawingMouseMove(object sender, MouseEventArgs e)
		{
			IDrawing closestDrawing = GetClosestDrawing(e.Location);
			if (closestDrawing != null) {
				UpdateHighLighted(closestDrawing);
				GetRegionToRedraw(closestDrawing.Bounds);
			}
		}
		
		
		void DotDrawingMouseDown(object sender, MouseEventArgs e)
		{
			IDrawing closestDrawing = GetClosestDrawing(e.Location);
			
			if (closestDrawing != null) {
				foreach (var drawing in Drawings) {
					if (closestDrawing.Parent != drawing) {
						drawing.Selected = false;
						drawing.NeedsRedrawing = true;
					}
				}
				if (closestDrawing.Highlighted) {
					if (closestDrawing.Parent != null) {
						closestDrawing.Parent.Selected = true;
						closestDrawing.Parent.NeedsRedrawing = true;
					}
				}
				GetRegionToRedraw(closestDrawing.Bounds);
			}
		}
		
		void UpdateHighLighted(IDrawing highLightedDrawing)
		{
			foreach (IDrawing drawing in Drawings) {
				drawing.Highlighted = false;
				foreach (var component in drawing.Components) {
					if (component != highLightedDrawing) {
						if (component.Highlighted) {
							component.Highlighted = false;
							drawing.Update();
						}
					}
				}
			}
			highLightedDrawing.Highlighted = true;
			if (highLightedDrawing.Parent != null) {
				highLightedDrawing.Parent.Highlighted = true;
				highLightedDrawing.Parent.Update();
			}
		}
		
		IDrawing GetClosestDrawing(Point p)
		{
			double minH = int.MaxValue;
			IDrawing closestDrawing = null;
			
			foreach (IDrawing drawing in Drawings) {
				foreach (var component in drawing.Components) {
					Rectangle testRectangle = new Rectangle(component.Bounds.Location, component.Bounds.Size);
					
					testRectangle.Inflate(HOVER_DS, HOVER_DS);
					if (testRectangle.Contains(p)) {
						double h = Geometry.Geometry.TriangleHeight(component.Points[0], component.Points[1], p);
						if (minH > h && h < HOVER_DS) {
							minH = h;
							closestDrawing = component;
						}
					}
				}
			}
			
			Parent.Text = string.Format("H={0}", minH);
			return closestDrawing;
		}
		
		Rectangle GetRegionToRedraw(Rectangle startRegion)
		{
			Rectangle regionToDraw = new Rectangle(startRegion.Location, startRegion.Size);
			
			foreach (IDrawing drawing in Drawings) {
				
				if (drawing.NeedsRedrawing || drawing.Bounds.IntersectsWith(regionToDraw)) {
					drawing.NeedsRedrawing = true;
					regionToDraw = Rectangle.Union(regionToDraw, drawing.Bounds);
					
					foreach (IDrawing d in Drawings) {
						if (d.Bounds.IntersectsWith(regionToDraw)) {
							d.NeedsRedrawing = true;
							regionToDraw = Rectangle.Union(regionToDraw, d.Bounds);
						}
					}
				}
			}
			return regionToDraw;
		}
		
	}
}
