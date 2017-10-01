using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandInterface.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandInterface.Utility.Tests
{
    [TestClass()]
    public class StringExtensionsTests
    {
        [TestMethod()]
        public void ReplaceKeepingIndentTest()
        {
            var testString = @"
def func():
    <Target>
";
            var expectString = @"
def func():
    a = 1
    b = 1
";
            var newString = @"a = 1
b = 1";

            Assert.AreEqual(expectString,
                testString.ReplaceKeepingIndent("<Target>", newString));
        }

        [TestMethod()]
        public void ReplaceKeepingIndentTest2()
        {
            var testString = @"
<Tag <Target>></Tag>
";
            var expectString = @"
<Tag a = 1
     b = 1></Tag>
";
            var newString = @"a = 1
b = 1";

            Assert.AreEqual(expectString,
                testString.ReplaceKeepingIndent("<Target>", newString));
        }
    }
}