using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSetManager
{
    public class GridDataSet
    {
        public GridDataSet(double delta, XYBoundary xYBoundary)
        {
            this.delta = delta;
            XYBoundary = xYBoundary;
            NX = (int)(XYBoundary.DX / delta);
            NY = (int)(XYBoundary.DY / delta);
            GridData = new List<XYZ>[NX][];
            for(int i=0;i<NX; i++)
            {
                GridData[i] = new List<XYZ>[NY];
                for(int j=0;j<NY;j++) { GridData[i][j]=new List<XYZ>();}
            }
        }

        public GridDataSet( XYBoundary xYBoundary, double delta, List<XYZ> data) : this(delta, xYBoundary)
        {
            foreach(XYZ point in data) AddXYZ(point);
        }
        public void AddXYZ(XYZ point)
        {
            if ((point.X < XYBoundary.MinX) || (point.Y < XYBoundary.MinY) || (point.X > XYBoundary.MaxX) || (point.Y > XYBoundary.MaxY)) throw new DataSetManagerException("out of bounds");
            int i=(int)((point.X - XYBoundary.MinX) / delta);
            int j=(int)((point.Y - XYBoundary.MinY) / delta);
            if (i == NX) i--;
            if (j == NY) j--;
            GridData[i][j].Add(point);
        }
        public double delta { get; private set; }
        public XYBoundary XYBoundary { get; private set; }
        public int NX { get;private set; }
        public int NY { get;private set; }
        public List<XYZ>[][] GridData {get;private set;}
        public void Print()
        {
            for(int i=0; i<NX; i++)
            {
                for(int j=0;j<NY;j++)
                {
                    Console.WriteLine($"({i},{j})");
                    for(int n = 0; n < GridData[i][j].Count; n++)
                    {
                        Console.WriteLine($"   ({GridData[i][j][n]})");
                    }
                }
            }
        }
    }
}
