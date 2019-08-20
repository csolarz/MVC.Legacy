using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC.Legacy.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Criptotest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string textoPlano = "Hola mundo";

            CryptoUtil crypto = new CryptoUtil();
            string EncryptedString = crypto.Encrypt(textoPlano);

            string resultado = crypto.Decrypt(EncryptedString);

            Assert.AreEqual(textoPlano, resultado);
        }
    }
}
