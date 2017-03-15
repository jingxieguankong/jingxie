using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cn.tdr.policeequipment.module.test
{
    using models;

    [TestClass]
    public class UpLoadDataPackageTest
    {
        [TestMethod]
        public void UpLoadDataPackageAct()
        {
            var xml = @"
                <Data>
                    <EQType> 0201 </EQType>
                    <EQID> 723997 </EQID>
                    <CommandID> F003 </CommandID>
                    <Content> 1103021213030680230000032B010050010269 </Content>
                    <Time> 2017 - 03 - 02 18:19:07 </Time >
                </Data> ";

            var pkg = new UpLoadDataPackage(xml);
            Assert.IsNotNull(pkg);
        }
    }
}
