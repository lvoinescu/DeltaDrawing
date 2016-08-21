/*
 * Created by SharpDevelop.
 * User: Sam
 * Date: 8/8/2015
 * Time: 9:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using DeltaDrawing.DeltaOut.Dot.Writers;
using DeltaDrawing.DeltaOut.Dot.Writers.Serial;
using NUnit.Framework;
namespace UnitTests
{
	[TestFixture]
	public class DeltaWritersTest
	{
		[Test]
		public void TestConsoleWriter()
		{
			 
			IDeltaPointsWriter consoleWriter = new ConsolePointWriter();
			IList<Point> points = new List<Point>();
			points.Add(new Point(100, 200));
			points.Add(new Point(150, 250));
			points.Add(new Point(200, 300));
			
			
			consoleWriter.WritePoints(points);
			
			consoleWriter.Close();
		}
		
		
		[Test]
		public void TestAsyncWriter1Line()
		{
			 
			IDeltaPointsWriter consoleWriter = new ConsolePointWriter();
			IDeltaPointsWriter asyncWriter = new AsyncDeltaPointsWriter(consoleWriter);
			asyncWriter.Open();
			
			IList<Point> points = new List<Point>();
			points.Add(new Point(100, 200));
			points.Add(new Point(150, 250));
			points.Add(new Point(200, 300));
			
			asyncWriter.WritePoints(points);
			asyncWriter.Close();
			
		}
		
		
		[Test]
		public void TestAsyncWriter2Lines()
		{
			IDeltaPointsWriter consoleWriter = new ConsolePointWriter();
			IDeltaPointsWriter asyncWriter = new AsyncDeltaPointsWriter(consoleWriter);
			asyncWriter.Open();
			
			IList<Point> line1 = new List<Point>();
			line1.Add(new Point(100, 200));
			line1.Add(new Point(150, 250));
			line1.Add(new Point(200, 300));
			
			IList<Point> line2 = new List<Point>();
			line2.Add(new Point(100, 200));
			line2.Add(new Point(150, 250));
			line2.Add(new Point(200, 300));
			
			asyncWriter.WritePoints(line1);
			asyncWriter.WritePoints(line2);
			asyncWriter.WritePoints(line1);
			asyncWriter.WritePoints(line2);
			
			System.Threading.Thread.Sleep(1000);
			asyncWriter.Close();
			
		}
	}
}
