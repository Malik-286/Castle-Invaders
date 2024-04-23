// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("n0vUnizFah7V8fdHmiHjk5aGDrqFV+bKal0Bzp7OMYrsMK26ElbRwm6X1JQLhFaYJx0cfhVcTHaRmy5kmwPye5FVQLOrhm3ZsYsIVDBolOSkO9IfvnQTo+SrukHQptOYySJ2/NaCfFWA6n8GIioEah6lj6mCQ0hiR/Adwk/+9ypFCRxh0Zprg/7+c4na8mS0lwgzWAh08M7/B6olV16SDthJHQlcyhe7y9SEt5LpqxG4B2o8BDzD3BlINfIN9Tg8hWQkV0y+KZBv3V59b1JZVnXZF9moUl5eXlpfXDBDLs30CVnms69fD3Y3w0NKNWU33V5QX2/dXlVd3V5eX9aarIsgqpYWAI96XXn2I67elPoutWM1bdTYsSauawNrF9y0el1cXl9e");
        private static int[] order = new int[] { 4,3,2,10,6,12,12,8,11,13,12,11,12,13,14 };
        private static int key = 95;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
