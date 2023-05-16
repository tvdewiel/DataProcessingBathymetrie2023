using DataSetManager;
using PredictionStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpatialInterpolationModel
{
    public interface IModel
    {
        double Z(double x, double y);
    }
}
