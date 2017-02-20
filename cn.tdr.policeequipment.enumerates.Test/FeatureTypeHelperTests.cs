using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.tdr.policeequipment.enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.tdr.policeequipment.enumerates.Test
{
    [TestClass()]
    public class FeatureTypeHelperTests
    {
        [TestMethod()]
        public void GetInfoTest()
        {
            var data = FeatureType.Add.GetInfo();
            Assert.IsInstanceOfType(data, typeof(FeatureTypeModel));
        }
    }
}