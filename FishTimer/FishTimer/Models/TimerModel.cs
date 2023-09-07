using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishTimer.Models
{
    [Table("timers")]
    public class TimerModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250), Unique, NotNull]
        public string Name { get; set; }

        public string Description { get; set; }

        [NotNull]
        public string StartTime { get; set; }

        public string EndTime { get; set; }
    }
}
