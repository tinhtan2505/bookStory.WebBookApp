﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Projects
{
    public class ProjectDeleteRequest
    {
        public int Id { get; set; }
        public string Title { set; get; }
        public string Description { set; get; }
    }
}