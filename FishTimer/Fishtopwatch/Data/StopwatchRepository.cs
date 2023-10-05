using SQLite;
using Fishtopwatch.Models;

namespace Fishtopwatch.Data
{
    public class StopwatchRepository
    {
        SQLiteAsyncConnection Database;

        public async void Init()
        {
            if (Database != null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<StopwatchModel>();
        }

        public async Task<List<StopwatchModel>> GetAllStopwatches()
        {
            Init();
            return await Database.Table<StopwatchModel>().ToListAsync();
        }

        public async Task<int> DeleteAllStopwatches()
        {
            Init();
            return await Database.DeleteAllAsync<StopwatchModel>();
        }

        public async Task<StopwatchModel> GetStopwatchByName(string stopwatchName)
        {
            Init();
            return await Database.Table<StopwatchModel>().Where(_ => _.Name == stopwatchName).FirstOrDefaultAsync();
        }

        public async Task<bool> AddStopwatch(StopwatchModel stopwatch)
        {
            Init();
            var result = await Database.InsertAsync(stopwatch);
            return result > 0;
        }
        
        public async Task<bool> UpdateStopwatch(StopwatchModel stopwatch)
        {
            Init();
            var result = await Database.UpdateAsync(stopwatch);
            return result > 0;
        }

        public async Task<bool> DeleteStopwatch(StopwatchModel stopwatch)
        {
            Init();
            var result = await Database.DeleteAsync(stopwatch);
            return result > 0;
        }
    }
}
