using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFormsStarterKit
{
    // Any platform-specific project must implement this interface
    // to construct the connection string
    public interface IDatabaseConnection
    {
        SQLite.SQLiteConnection DbConnection();
    }
}
