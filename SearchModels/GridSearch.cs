using DataSetManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchModels
{
    public class GridSearch : IDataSearch
    {
        private GridDataSet gridDataSet;

        public GridSearch(GridDataSet gridDataSet)
        {
            this.gridDataSet = gridDataSet;
        }

        private (int, int) FindCell(double x, double y)
        {
            if (!gridDataSet.XYBoundary.WithinBounds(x, y)) throw new SearchModelException("out of bounds");
            int i = (int)((x - gridDataSet.XYBoundary.MinX) / gridDataSet.delta);
            int j = (int)((y - gridDataSet.XYBoundary.MinY) / gridDataSet.delta);
            if (i == gridDataSet.NX) i--;
            if (j == gridDataSet.NY) j--;
            return (i, j);
        }
        public List<XYZ> FindNearestNeighbours(double x, double y, int n)
        {
            try
            {
                SortedList<double, List<XYZ>> nn = new SortedList<double, List<XYZ>>();
                (int i, int j) = FindCell(x, y);
                ProcessCell(nn, i, j, x, y, n);
                int ring = 0;
                while (nn.Count < n)
                {
                    ring++;
                    ProcessRing(i, j, ring, nn, x, y, n);
                }
                ProcessRing(i, j, ring + 1, nn, x, y, n);
                return (List<XYZ>)ListFromSortedList(nn).Take(n).ToList();
            }
            catch (Exception ex) { throw new SearchModelException("FNNgrid"); }
        }
        private void ProcessCell(SortedList<double, List<XYZ>> nn, int i, int j, double x, double y, int n)
        {
            foreach (XYZ p in gridDataSet.GridData[i][j])
            {
                double dsquare = Math.Pow(p.X - x, 2) + Math.Pow(p.Y - y, 2);
                if ((nn.Count < n) || (dsquare < nn.Keys[nn.Count - 1]))
                {
                    if (nn.ContainsKey(dsquare)) nn[dsquare].Add(p);
                    else nn.Add(dsquare, new List<XYZ>() { p });
                }
            }
        }
        private bool IsValidCell(int i, int j)
        {
            if ((j < 0) || (j >= gridDataSet.NY)) return false;
            if ((i < 0) || (i >= gridDataSet.NX)) return false;
            return true;
        }
        private void ProcessRing(int i, int j, int ring, SortedList<double, List<XYZ>> nn, double x, double y, int n)
        {
            for (int gx = i - ring; gx <= i + ring; gx++)
            {
                //onderste rij
                int gy = j - ring;
                if (IsValidCell(gx, gy)) ProcessCell(nn, gx, gy, x, y, n);
                //bovenste rij
                gy = j + ring;
                if (IsValidCell(gx, gy)) ProcessCell(nn, gx, gy, x, y, n);
            }
            for (int gy = j - ring + 1; gy <= j + ring - 1; gy++)
            {
                //linker kolom
                int gx = i - ring;
                if (IsValidCell(gx, gy)) ProcessCell(nn, gx, gy, x, y, n);
                //rechter kolom
                gx = i + ring;
                if (IsValidCell(gx, gy)) ProcessCell(nn, gx, gy, x, y, n);
            }
        }
        private List<XYZ> ListFromSortedList(SortedList<double, List<XYZ>> nn)
        {
            List<XYZ> list = new List<XYZ>();
            foreach (List<XYZ> l in nn.Values)
            {
                foreach (XYZ v in l) list.Add(v);
            }
            return list;
        }
    }
}
