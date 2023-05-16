using DataSetManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchModels
{
    public interface IDataSearch
    {
        List<XYZ> FindNearestNeighbours(double x, double y, int n);
    }
}
