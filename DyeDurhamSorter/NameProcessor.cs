using DyeDurhamSorter.Interfaces;

namespace DyeDurhamSorter
{
    public class NameProcessor : INameProcessor
    {
        private readonly INameReaderWriter _readerWriter;
        private readonly INameSorter _sorter;
        public NameProcessor(INameReaderWriter readerWriter, INameSorter sorter)
        {
            _readerWriter = readerWriter;
            _sorter = sorter;
        }

        public virtual int ProcessSortFile(string fileName)
        {
            var names = _readerWriter.ReadNames(fileName);
            var sortedNames = _sorter.SortNames(names);
            _readerWriter.SaveNames(sortedNames);
            return sortedNames.Count;
        }
    }
}
