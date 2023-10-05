using SQLite;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishtopwatch.Models
{
    [Table("stopwatches")]
    public class StopwatchModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100), Unique, NotNull]
        public string Name { get; set; }

        public string Description { get; set; }

        [NotNull]
        public string StartTime { get; set; }

        public string MostRecentStartTime { get; set; }

        [NotNull]
        public uint ElapsedTime { get; set; } = 0;

        [NotNull]
        public bool IsRunning { get; set; } = true;

        public string Color { get; set; }
    }
}
