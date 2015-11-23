using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandUtilityTest
{
    class AssertUtility
    {
        public static void Throws<T>(Action func) where T : Exception
        {
            var thrown = false;
            try
            {
                func();
            }
            catch (T)
            {
                thrown = true;
            }

            if (!thrown)
            {
                throw new AssertFailedException(
                    String.Format("An exception of type {0} was expected, but not thrown", typeof(T))
                    );
            }
        }
    }
}