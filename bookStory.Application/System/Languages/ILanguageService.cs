﻿using bookStory.ViewModels.Common;
using bookStory.ViewModels.System.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Application.System.Languages
{
    public interface ILanguageService
    {
        Task<List<LanguageVm>> GetAll();
    }
}