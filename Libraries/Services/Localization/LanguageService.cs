using Core.Caching;
using Core.Domain.Localization;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Localization
{
    public class LanguageService
    {
        private const string LANGUAGES_BY_ID_KEY = "kunlun.language.id-{0}";

        private const string LANGUAGES_ALL_KEY = "kunlun.language.all";

        public readonly IDbConnection _context;

        private readonly ICacheManager _cacheManager;

        public LanguageService(ICacheManager cacheManager, IDbConnection context)
        {
            _cacheManager = cacheManager;
            _context = context;
        }

        public virtual IList<Language> GetAllLanguages()
        {
            var languages = _cacheManager.Get(LANGUAGES_ALL_KEY, () =>
            {
                return _context.Query<Language>("select code ,name ,sort_id as Seq,insert_date as insertDate,insert_user as insertUser,update_date as updateDate ,update_user as updateuser from Language order by code").ToList();
            });

            return languages;
        }

        public virtual Language GetLanguageById(string languageId)
        {
            if (string.IsNullOrEmpty(languageId))
                return null;

            string key = string.Format(LANGUAGES_BY_ID_KEY, languageId);
            return _cacheManager.Get(key, () =>
             _context.QuerySingleOrDefault<Language>("select code ,name ,sort_id as Seq,insert_date as insertDate,insert_user as insertUser,update_date as updateDate ,update_user as updateuser from Language where code=@languageId", new { languageId = languageId }));
        }

    }
}
