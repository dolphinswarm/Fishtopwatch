using SQLite;
using FishTimer.Models;

namespace FishTimer.Data
{
    public class TimerRepository
    {
        SQLiteAsyncConnection Database;

        public async void Init()
        {
            if (Database != null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<TimerModel>();
        }

        public async Task<List<TimerModel>> GetAllTimers()
        {
            Init();
            return await Database.Table<TimerModel>().ToListAsync();
        }

        public async Task<int> DeleteAllTimers()
        {
            Init();
            return await Database.DeleteAllAsync<TimerModel>();
        }

        public async Task<TimerModel> GetTimerByName(string timerName)
        {
            Init();
            return await Database.Table<TimerModel>().Where(_ => _.Name == timerName).FirstOrDefaultAsync();
        }

        public async Task<bool> AddTimer(TimerModel timer)
        {
            Init();
            var result = await Database.InsertAsync(timer);
            return result > 0;
        }
        
        public async Task<bool> UpdateTimer(TimerModel timer)
        {
            Init();
            var result = await Database.UpdateAsync(timer);
            return result > 0;
        }

        public async Task<bool> DeleteTimer(TimerModel timer)
        {
            Init();
            var result = await Database.DeleteAsync(timer);
            return result > 0;
        }
    }
}
