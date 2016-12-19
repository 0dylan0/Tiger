using Core;
using Core.Caching;
using Core.Domain.Localization;
using Core.Infrastructure;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Localization
{
    public class LocalizationService
    {
        private const string LOCALSTRINGRESOURCES_ALL_KEY = "kunlun.lsr.all-{0}";

        private const string LOCALSTRINGRESOURCES_PATTERN_KEY = "kunlun.lsr.";

        private readonly IWorkContext _workContext;

        private readonly IDbConnection _context;

        private readonly ICacheManager _cacheManager;

        public LocalizationService(ICacheManager cacheManager, IWorkContext workContext, IDbConnection context)
        {
            _cacheManager = cacheManager;
            _workContext = workContext;
            _context = context;
        }

        public virtual IList<LocaleStringResource> GetAllResources(string languageId)
        {
            return _context.Query<LocaleStringResource>("select * from LocaleStringResource where languageid=@LanguageId", new { LanguageId = languageId }).ToList();
        }

        public virtual Dictionary<string, KeyValuePair<int, string>> GetAllResourceValues(string languageId)
        {
            string key = string.Format(LOCALSTRINGRESOURCES_ALL_KEY, languageId);
            return _cacheManager.Get(key, () =>
            {
                string sql = "select * from LocaleStringResource a left join Language b on a.languageid=b.code where languageid=@LanguageId order by resourcename";

                var connection = EngineContext.Current.Resolve<IDbConnection>();


                //var list = connection.Query<LocaleStringResource, Language, LocaleStringResource>(sql, (p, v) =>
                //{
                //    p.Language = v;
                //    return p;
                //}, new {LanguageId = languageId}, splitOn: "LanguageId").ToList();


                var locales = _context.Query<LocaleStringResource>(sql, new { LanguageId = languageId }).ToList();
                var dictionary = new Dictionary<string, KeyValuePair<int, string>>();
                foreach (var locale in locales)
                {
                    var resourceName = locale.ResourceName.ToLowerInvariant();
                    if (!dictionary.ContainsKey(resourceName))
                        dictionary.Add(resourceName, new KeyValuePair<int, string>(locale.Id, locale.ResourceValue));
                }
                return dictionary;
            });
        }

        public virtual string GetResource(string resourceKey)
        {
            //if (_workContext.CurrentLanguage != null)
            //{
            //    return GetResource(resourceKey, _workContext.CurrentLanguage.Code);
            //}
            return "";
        }

        public virtual string GetResource(string resourceKey, string languageId, string defaultValue = "", bool returnEmptyIfNotFound = false)
        {
            string result = string.Empty;
            if (resourceKey == null)
            {
                resourceKey = string.Empty;
            }
            resourceKey = resourceKey.Trim().ToLowerInvariant();

            var resources = GetAllResourceValues(languageId);
            if (resources.ContainsKey(resourceKey))
            {
                result = resources[resourceKey].Value;
            }

            if (String.IsNullOrEmpty(result))
            {
                if (!String.IsNullOrEmpty(defaultValue))
                {
                    result = defaultValue;
                }
                else if (!returnEmptyIfNotFound)
                {
                    result = resourceKey;
                }
            }
            return result;
        }

    }
}
