using DyeDurhamSorter;

namespace DyeDurhamNameSorter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No filename is provided");
                return;
            }

            var processor = new NameProcessor(new NameReaderWriter(new FileUtility(), new NameValidator()), new NameSorter());
            
            try
            {
                var sortedCount = processor.ProcessSortFile(args[0]);
                Console.WriteLine(String.Format("{0} names sorted and saved.", sortedCount));
            }
            catch (CustomException ex)
            {
                Console.WriteLine(ex);
            }
            
        }
    }
}
