
namespace Logger.Interfaces
{
    public interface IFileWriter
    {
        public void Initialize(string filePath);
        public void WriteLine();
    }
}
