﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Alus.Tests
{
    [TestClass]
    public class ReadWriteToFileTest
    {
        [TestMethod]
        public void ReadWriteToFile()
        {
            Alus.ReadAndWriteFromFile readwrite = new Alus.ReadAndWriteFromFile();
            Bar bar = new Bar("Fabai","54.734658,25.2540789",4.4,"Ateities gatvė 21","ChIJwZrwhxOR3UYRVOQRS3PA5PI","6");
            readwrite.WriteLineToFile(bar);
            List<Bar> barList;
            barList = readwrite.ReadFile();
            Bar bar2 = barList[0];
            Assert.IsNotNull(bar2.Name);
            Assert.IsNotNull(bar2.Address);
        }
    }
}
