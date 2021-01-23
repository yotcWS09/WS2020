﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VIN_LIB;

namespace VIN_LIB.Test
{
    [TestClass]
    public class UnitTest1
    {
        static vin_info VinInfo;

        [ClassInitialize]
        static public void Init(TestContext tc)
        {
            VinInfo = new vin_info();
        }

        [TestMethod]
        public void ChekVin_ShortVinLenght_false()
        {
            
            Assert.IsFalse( VinInfo.CheckVIN("12345") );
        }

        [TestMethod]
        public void ChekVin_AvailableChers_false()
        {
            Assert.IsFalse(VinInfo.CheckVIN("123456789O1234567"));
        }
        [TestMethod]
        public void ChekVin_WMI_false()
        {
            Assert.IsFalse(VinInfo.CheckVIN("BB345678901234567"));
        }
        [TestMethod]
        public void ChekVin_VDS9_false()
        {
            Assert.IsFalse(VinInfo.CheckVIN("AA345678V01234567"));
        }
        [TestMethod]
        public void ChekVin_LastForDigits_true()
        {
            Assert.IsTrue(VinInfo.CheckVIN("AA345678X01234563"));
        }
        [TestMethod]
        public void ChekVin_LastForDigits_false()
        {
            Assert.IsFalse(VinInfo.CheckVIN("AA345678X01234568"));
        }
        [TestMethod]
        public void ChekVin_CHK_True()
        {
            Assert.IsTrue(VinInfo.CheckVIN("JHMCM56557C404453"));
        }

    }
}
