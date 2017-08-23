using SQLite.Net.Interop;

namespace XConcepThreee3.Classes
{
    public interface IConfig
    {
        string DirectoryDB { get; }

        ISQLitePlatform Platform { get; }

    }
}
