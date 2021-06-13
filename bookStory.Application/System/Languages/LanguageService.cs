using bookStory.Data.EF;
using bookStory.ViewModels.Common;
using bookStory.ViewModels.System.Languages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Application.System.Languages
{
    public class LanguageService : ILanguageService
    {
        private readonly IConfiguration _config;
        private readonly bookStoryDbContext _context;

        public LanguageService(bookStoryDbContext context,
            IConfiguration config)
        {
            _config = config;
            _context = context;
        }

        public async Task<List<LanguageVm>> GetAll()
        {
            var languages = await _context.Languages.Select(x => new LanguageVm()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return languages;
        }
    }
}