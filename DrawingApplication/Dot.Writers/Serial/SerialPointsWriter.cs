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
using Dot.Writers;

namespace DrawingApplication.Dot.Writers.Serial
{
	/// <summary>
	/// Description of SerialPointsWriter.
	/// </summary>
	public class SerialPointsWriter : IDeltaPointsWriter
	{
		const String START_LINE ="S";
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

		public void WritePoints(List<Point> points)
		{
			serialPort.Write(START_LINE);
			points.ForEach(p => serialPort.Write(PointToBytes(p), 0, 4));
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
