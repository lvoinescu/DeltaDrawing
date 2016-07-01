/*
 * Created by SharpDevelop.
 * User: Sam
 * Date: 6/27/2016
 * Time: 9:58 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;


namespace DeltaDrawing.DotDrawing.Drawings
{

	public class GridDrawer
	{
		
		private const int DEFAULT_GRID_SIZE = 16;
		
		private int gridSize = DEFAULT_GRID_SIZE;
		private readonly UserControl diagramContainer;
		
		public GridDrawer(UserControl diagramContainer)
		{
			this.diagramContainer = diagramContainer;
		}


		public GridDrawer(UserControl diagramContainer, int gridSize)
			: this(diagramContainer)
		{
			this.gridSize = gridSize;
		}

		public void Draw(Graphics graphics)
		{
			RectangleF redrawRectangle = graphics.ClipBounds;
			
			Pen p = new Pen(Color.FromArgb(100, 200, 200, 200));
			float step = gridSize;
			
			int fromLeft = Math.Max(0, ((int)(redrawRectangle.Left / gridSize) + 1) * gridSize);
			int toLeft = Math.Min(diagramContainer.Width, 
				             (((int)(redrawRectangle.Left + redrawRectangle.Width)) / gridSize + 1) * gridSize);
			int fromTop = Math.Max(0, ((int)(redrawRectangle.Top / gridSize) - 1) * gridSize);
			int toTop = Math.Min(diagramContainer.Height, 
				            ((int)((redrawRectangle.Top + redrawRectangle.Height) / gridSize) + 1) * gridSize);
			;
			if (step < 1)
				step = 1;
			for (float i = fromLeft; i < toLeft; i += step) {
				graphics.DrawLine(p, i, 0, i, diagramContainer.Height);
			}
			for (float j = fromTop; j < toTop; j += step) {
				graphics.DrawLine(p, 0, j, diagramContainer.Width, j);
			}
		}

		public int GridSize {
			get {
				return gridSize;
			}
			set {
				gridSize = value;
			}
		}
	}
}

