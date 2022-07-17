namespace auth_service
{
    public static class Log
    {
        public static void InfoW(string info)
        {
            Console.WriteLine($"[INFO] {info}");
        }

        public static void ExceptionW(string error,string summary,string nameOfMethod,string nameOfFile)
        {
            Console.WriteLine($"Inside [{nameOfFile}]\nNamed Method [{nameOfMethod}]\n[SUMMARY]\n[{summary}]\n[EXCEPTION] WITH THAT \n{error}");
        }
    }
}
