using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppRohdeSchwarzTest
{
    public class DataMock
    {
        /// <summary>
        /// Mock data of all test items
        /// </summary>
        public static List<TestItem> MockDataList = new List<TestItem>(){ 
            new TestItem(){Major=1, Minor=1},
            new TestItem(){Major=1, Minor=2},
            new TestItem(){Major=1, Minor=3},
            new TestItem(){Major=1, Minor=4},
            new TestItem(){Major=2, Minor=1},
            new TestItem(){Major=2, Minor=2},
            new TestItem(){Major=2, Minor=3},
        };

        /// <summary>
        /// Get Major nodes (virtual nodes)
        /// </summary>
        /// <returns></returns>
        public static List<TestItemViewModel> GetMajorItems(Action updateStatusAction)
        {
            return MockDataList.Select(ti => ti.Major).Distinct().Select(mj=> new TestItemViewModel(mj, 0, updateStatusAction)).ToList();
        }

        public static List<TestItemViewModel> GetMinorItems(int major, Action updateStatusAction, TestItemViewModel parent = null)
        {
            return MockDataList.Where(ti => ti.Major == major).Select(mi => new TestItemViewModel(mi.Major, mi.Minor, updateStatusAction, parent)).ToList();
        }
    }
}
