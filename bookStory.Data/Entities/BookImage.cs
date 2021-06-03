﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Data.Entities
{
    public class BookImage
    {
        public int Id { get; set; }

        public int IdBook { get; set; }

        public string ImagePath { get; set; }

        public string Caption { get; set; }

        public bool IsDefault { get; set; }

        public DateTime DateCreated { get; set; }

        public int SortOrder { get; set; } // thứ tự

        public long FileSize { get; set; }

        public Book Book { get; set; }
    }
}
