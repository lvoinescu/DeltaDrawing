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

namespace DeltaDrawing.DeltaOut.Dot.Writers.Serial
{
	/// <summary>
	/// Description of ConsolePointWriter.
	/// </summary>
	public class ConsolePointWriter : IDeltaPointsWriter
	{
		byte[] START_LINE = System.Text.Encoding.ASCII.GetBytes("SL");
		byte[] END_LINE = System.Text.Encoding.ASCII.GetBytes("EL");
		
		public ConsolePointWriter()
		{
		}

		public void Open()
		{
			Console.WriteLine("Console writer opened.");
		}

		public void WritePoints(IList<Point> points)
		{
			Console.Write(System.Text.Encoding.ASCII.GetChars(START_LINE, 0, 2));
			Console.Write(System.Text.Encoding.ASCII.GetChars(BitConverter.GetBytes((ushort)points.Count)));
			foreach (var point in points) {
				Console.Write(System.Text.Encoding.ASCII.GetChars(PointToBytes(point)), 0, 4);
			} 
			Console.WriteLine(System.Text.Encoding.ASCII.GetChars(END_LINE, 0, 2));
		}

		public void Close()
		{
			Console.WriteLine("Console writer closed.");
		}
		
		static byte[] PointToBytes(Point point)
		{
			byte[] output = new byte[4];
			byte[] xBytes = BitConverter.GetBytes((ushort)point.X);
			byte[] yBytes = BitConverter.GetBytes((ushort)point.Y);
			
			xBytes.CopyTo(output, 0);
			yBytes.CopyTo(output, 2);
			
			return output;
		}
		
	}
}
