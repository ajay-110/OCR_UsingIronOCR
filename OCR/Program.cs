using IronOcr;

namespace OCR
{
    public class Program
    {
        static void Main(string[] args)
        {
            var directoryPath = args[0];
            foreach (var file in Directory.GetFiles(directoryPath))
            {
                ExtractFile(file);
            }
        }

        internal static void ExtractFile(string filePath)
        {
            try
            {
                Console.WriteLine("Processing: " + Path.GetFullPath(filePath));
                var directory = Path.GetDirectoryName(filePath);
                var filename = Path.GetFileNameWithoutExtension(filePath);
                var outputFilePath = Path.Combine(directory ?? "", filename + ".txt");
                var Ocr = new IronTesseract();
                using (var Input = new OcrInput(filePath))
                {
                    var Result = Ocr.Read(Input);
                    Result.SaveAsTextFile(outputFilePath);
                }
                if (File.Exists(outputFilePath))
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine("SUCCESS.");
                    Console.ResetColor();
                }
            }
            catch(Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                var message = string.Format("ERROR at {0}:-\nMessage: {1}\nStackTrace: {2}", Path.GetFullPath(filePath), ex.Message, ex.StackTrace);
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
    }
}

