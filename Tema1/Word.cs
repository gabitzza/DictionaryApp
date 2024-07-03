using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1
{
    internal class Word
    {
        public class word
        {
            public string Name { get; set; }
            public string Category { get; set; }
            public string Description { get; set; }
            public string ImagePath { get; set; }
            public bool Showed { get; set; }

            // Constructor
            public word(string name, string category, string description, string imagePath, bool showed)
            {
                Name = name;
                Category = category;
                Description = description;
                ImagePath = imagePath;
                showed= false;
            }
        }
    }
}
