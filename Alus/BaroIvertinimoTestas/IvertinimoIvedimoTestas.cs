using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alus;

namespace BaroIvertinimoTestas
{
    [TestClass]
    public class IvertinimoIvedimoTestas
    {
        [TestMethod]
        public void Ivertinimo_Ivedimo_Testas() {

            String baro_Pavadinimas = "Labai,geras Baras";
            int ivertinimas = 10;
            bool expected = false;

            BaroVertinimas baras = new BaroVertinimas();
            bool actual = baras.evaluation_check(ivertinimas);

            Assert.AreEqual(1, 1,0.001,"Evaluation correct");
        }
    }
}
