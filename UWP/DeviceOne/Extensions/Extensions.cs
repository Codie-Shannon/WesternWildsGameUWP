namespace DeviceOne
{
    public static class Extensions
    {
        // String to Int Converter
        // ======================================================
        // ======================================================
        public static int StringToInt(this string value)
        {
            //Try Parse String to Int
            int.TryParse(value, out int result);

            //Return Result
            return result;
        }
    }
}
