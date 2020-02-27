using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Extensions
{
    static class RandomExtensions
    {
        public static int Next(this Random thisObj, int[] inputValues)
        {
            int position = new Random().Next(inputValues.Length);
            return inputValues[position];
        }
    }
}
