using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Models
{
    /// <summary>
    /// Class responsible for producing objects at each printing iteration.
    /// </summary>
    class Print
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public int Count { get; set; }

        public Print()
        {
        }

        public Print(int id, string text, string date, int count)
        {
            Id = id;
            Text = text;
            Date = date;
            Count = count;
        }
    }
}
