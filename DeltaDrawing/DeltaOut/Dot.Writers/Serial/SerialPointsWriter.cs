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
using System.IO.Ports;

namespace DeltaDrawing.DeltaOut.Dot.Writers.Serial
{
	/// <summary>
	/// Description of SerialPointsWriter.
	/// </summary>
	public class SerialPointsWriter : IDeltaPointsWriter
	{
		byte[] START_LINE = System.Text.Encoding.ASCII.GetBytes("SL");
		byte[] END_LINE = System.Text.Encoding.ASCII.GetBytes("EL");
		readonly SerialPort serialPort;
		
		public SerialPointsWriter(SerialPort serialPort)
		{
			this.serialPort = serialPort;
		}

		public void Open()
		{
			if (!serialPort.IsOpen) {
				serialPort.Open();
			}
		}

		public void WritePoints(IList<Point> points)
		{
			serialPort.Write(System.Text.Encoding.ASCII.GetString(START_LINE, 0, 2));
			serialPort.Write(System.Text.Encoding.ASCII.GetString(BitConverter.GetBytes((ushort)points.Count)));
			foreach (var point in points) {
				serialPort.Write(System.Text.Encoding.ASCII.GetString((PointToBytes(point)), 0, 4));
			} 
			serialPort.WriteLine(System.Text.Encoding.ASCII.GetString(END_LINE, 0, 2));
		}

		public void Close()
		{
			serialPort.Close();
		}

		
		private static byte[] PointToBytes(Point point)
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
