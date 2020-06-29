using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Models
{
    class Print
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }

        public Print()
        {
        }

        public Print(int id, string text, string date)
        {
            Id = id;
            Text = text;
            Date = date;
        }
    }
}
