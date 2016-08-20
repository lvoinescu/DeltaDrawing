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
using System.Collections.Concurrent;
using System.Drawing;
using System.Threading;

namespace Dot.Writers
{

	public class AsyncDeltaPointsWriter: IDeltaPointsWriter
	{
		const int MAX_QUEUE_SIZE = 200;
		
		Thread writingThread;
		volatile bool listening;
		readonly object syncLock = new object();
		ConcurrentQueue<List<Point>> pointsQueue = new ConcurrentQueue<List<Point>>();
		
		IDeltaPointsWriter pointWriter;
		
		public AsyncDeltaPointsWriter(IDeltaPointsWriter pointWriter)
		{
			this.pointWriter = pointWriter;
		}

		public void Open()
		{
			listening = true;
			writingThread = new Thread(new ThreadStart(init));
			writingThread.Start();
		}

		
		void init()
		{
			pointWriter.Open();
			
			while (listening) {
				List<Point> points;
				
				lock (syncLock) {
					if (pointsQueue.Count > 0) {
						if (pointsQueue.TryDequeue(out points)) {
							pointWriter.WritePoints(points);
						}
					} else {
						Monitor.Wait(syncLock);
					}
				}
			}
		}
		
		
		public void WritePoints(List<Point> points)
		{
			lock (syncLock) {
				pointsQueue.Enqueue(points);
				Monitor.Pulse(syncLock);
			}
		}

		public bool CanWrite()
		{
			return true;
		}

		public void Close()
		{
			listening = false;
		}
	}
}
