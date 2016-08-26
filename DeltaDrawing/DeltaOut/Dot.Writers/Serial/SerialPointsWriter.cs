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
using System.Threading;

namespace DeltaDrawing.DeltaOut.Dot.Writers.Serial
{
	/// <summary>
	/// Description of SerialPointsWriter.
	/// </summary>
	public class SerialPointsWriter : IDeltaPointsWriter
	{
		byte[] START_LINE = System.Text.Encoding.ASCII.GetBytes ("S");
		byte[] END_LINE = System.Text.Encoding.ASCII.GetBytes ("E");
		readonly SerialPort serialPort;
		volatile bool active;

		public SerialPointsWriter (SerialPort serialPort)
		{
			this.serialPort = serialPort;
		}

		public void Open ()
		{
			if (!serialPort.IsOpen) {
				serialPort.Open ();
				active = true;
			}
		}

		public void WriteLine (IList<Point> points)
		{
			if (!serialPort.IsOpen) {
				Console.WriteLine ("Serial port not open!");

			}
			String start = System.Text.Encoding.ASCII.GetString (START_LINE, 0, 1);
			Console.Write (start);
			serialPort.Write (start);


			byte[] count = BitConverter.GetBytes ((ushort)points.Count);
			serialPort.Write (count, 0, 2);
			Console.WriteLine (count [0].ToString () + count [1].ToString ());

			int index = 0;
			foreach (var point in points) {
				var pointBytes = PointToBytes (point);
				Console.WriteLine ("P[" + index++ + "](" + point.X +"," + point.Y + ") =[" +
					pointBytes [0].ToString () + "," + pointBytes [1].ToString () + "]|[" + pointBytes [2].ToString () + "," + pointBytes [3].ToString () + "]");
				serialPort.Write (pointBytes, 0, 4);
			} 
			serialPort.Write (System.Text.Encoding.ASCII.GetString (END_LINE, 0, 1));
		}

		public char[] ReadResponse ()
		{
			char[] buffer = new char[2];			
			while (active && serialPort.BytesToRead < 1) {
				Thread.Sleep (100);
			}

			if (active) {
				if (serialPort.BytesToRead > 0) {
					serialPort.Read (buffer, 0, 2);
				}
			}
			return buffer;
		}

		public void Close ()
		{
			active = false;
			serialPort.Close ();
		}

		private static byte[] PointToBytes (Point point)
		{
			byte[] output = new byte[4];
			byte[] xBytes = BitConverter.GetBytes ((short)point.X);
			byte[] yBytes = BitConverter.GetBytes ((short)point.Y);
			
			xBytes.CopyTo (output, 0);
			yBytes.CopyTo (output, 2);
			
			return output;
		}
	}
}
