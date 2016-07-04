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
		
		int width;
		int height;
		int gridSize;
		
		double scale;

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
		
		
		Random r = new Random(255);
		
		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			
			base.OnPaint(e);
						
			int red = r.Next(255);
			int green = r.Next(255);
			int blue = r.Next(255);
			
			Color color = Color.FromArgb(100, red, green, blue);
			//e.Graphics.FillRectangle(new SolidBrush(color), e.ClipRectangle);
			
			var graphics = e.Graphics;
			if (gridDrawer != null)
				gridDrawer.Draw(graphics);
			
			if (Drawings != null)
				foreach (IDrawing drawing in Drawings) {
				
					//if (e.ClipRectangle.IntersectsWith(drawing.Bounds)) {
					drawing.Draw(graphics);
					//}
				}
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
			double minH = int.MaxValue;
			IDrawing closestDrawing = null;
			IDrawing previousTestedDrawing = null;
			foreach (IDrawing drawing in Drawings) {
				
				foreach (var component in drawing.Components) {
					component.Selected = false;
					Rectangle testRectangle = new Rectangle(component.Bounds.Location, component.Bounds.Size);
					testRectangle.Inflate(HOVER_DS, HOVER_DS);
					if (testRectangle.Contains(e.Location)) {
						double h = Geometry.Geometry.TriangleHeight(component.Points[0], component.Points[1], e.Location);
						if (minH > h && h < HOVER_DS) {
							minH = h;
							closestDrawing = component;
							previousTestedDrawing = closestDrawing;
							closestDrawing.Selected = true;
						}
					}
				}
			
				Invalidate(drawing.Bounds);
				Parent.Text = string.Format("H={0}", minH);
			}
		}
	}
}
