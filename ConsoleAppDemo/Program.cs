

using DataSetManager;

namespace ConsoleAppDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            double delta = 25.0;
            List<XYZ> data=new List<XYZ>();
            data.Add(new XYZ(0, 10, 10));
            data.Add(new XYZ(10, 15, 11));
            data.Add(new XYZ(40, 40, 12));
            data.Add(new XYZ(60, 30, 13));
            data.Add(new XYZ(80, 11, 14));
            data.Add(new XYZ(90, 17, 15));
            data.Add(new XYZ(100, 25, 16));
            XYBoundary xyBoundary = new XYBoundary(0, 100, 0, 50);
            GridDataSet gdSet= new GridDataSet(xyBoundary,delta,data);
            gdSet.Print();
        }
    }
}