using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP02PDMI6.Models
{
    [Table("tasks")]
    public class TaskItem
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("priority")]
        public EPriority Priority { get; set; }
    }

    public enum EPriority
    {
        High = 3,
        Medium = 2,
        Low = 1,
        None = 0
    }
}
