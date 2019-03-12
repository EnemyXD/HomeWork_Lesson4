using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mission2
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int,int> resCount = new Dictionary<int, int>();
            List<int> q = new List<int>() { 1,1,2,3,4,5,6,7,8,9,0,4,6,4,3,8 };           
            HashSet<int> y = new HashSet<int>();

            foreach (var item in q)
            {
                y.Add(item);
            }

            foreach (var item in y)
            {

                int z = q.Where(h => h == item).Count();
                resCount.Add(item,z);

            }

            foreach (var item in resCount)
            {

                Console.WriteLine(item);
            }

            Console.ReadKey();

        }
    }
}
