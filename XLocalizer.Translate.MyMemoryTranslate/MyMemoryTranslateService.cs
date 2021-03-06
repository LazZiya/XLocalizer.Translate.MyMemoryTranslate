﻿using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using Newtonsoft.Json;
using System.Net;

namespace XLocalizer.Translate.MyMemoryTranslate
{
    /// <summary>
    /// MyMemory translation service
    /// </summary>
    public class MyMemoryTranslateService : ITranslator
    {
        /// <summary>
        /// Service name
        /// </summary>
        public string ServiceName => "MyMemory Translate";

        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        private readonly string _url;

        /// <summary>
        /// Initialize MyMemory translate service
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public MyMemoryTranslateService(HttpClient httpClient, IConfiguration configuration, ILogger<MyMemoryTranslateService> logger)
        {
            _httpClient = httpClient ?? throw new NotImplementedException(nameof(httpClient));

            var email = configuration["XLocalizer.Translate:MyMemory:Email"];
            var key = configuration["XLocalizer.Translate:MyMemory:Key"];
            
            _url = string.Empty;

            if (!string.IsNullOrWhiteSpace(email))
            {
                _url += $"&de={email}";
            }

            if (!string.IsNullOrWhiteSpace(key))
            {
                _url += $"&key={key}";
            }

            _logger = logger;
        }

        /// <summary>
        /// Run async translation task
        /// </summary>
        /// <param name="source">Source language e.g. en</param>
        /// <param name="target">Target language e.g. tr</param>
        /// <param name="text">Text to be translated</param>
        /// <param name="format">Text format: html or text</param>
        /// <returns><see cref="TranslationResult"/></returns>
        public async Task<TranslationResult> TranslateAsync(string source, string target, string text, string format)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://api.mymemory.translated.net/get?q={text}&langpair={source}|{target}{_url}");
                _logger.LogInformation($"Response: {ServiceName} - {response.StatusCode}");
                /*
                 * Sample response
                 * "{ 
                 *     "responseData": {
                 *         "translatedText":"Yorum Yap",
                 *         "match":1
                 *     },
                 *     "quotaFinished":false,
                 *     "mtLangSupported":null,
                 *     "responseDetails":"",
                 *     "responseStatus":200,
                 *     "responderId":"7",
                 *     "exception_code":null,
                 *     "matches":[
                 *         { "id":"450807151","segment":"Comment","translation":"Yorum Yap","source":"en-GB","target":"tr-TR","quality":"74","reference":null,"usage-count":15,"subject":"All","created-by":"MateCat","last-updated-by":"MateCat","create-date":"2020-05-26 23:07:40","last-update-date":"2020-05-26 23:07:40","match":1}, 
                 *         { "id":"443769201","segment":"Comment","translation":"Comment","source":"en-US","target":"tr-TR","quality":"74","reference":null,"usage-count":1,"subject":"All","created-by":"MateCat","last-updated-by":"MateCat","create-date":"2019-06-19 16:12:03","last-update-date":"2019-06-19 16:12:03","match":0.98}
                 *         ]
                 * }"
                 */
                var responseContent = await response.Content.ReadAsStringAsync();

                var responseDto = JsonConvert.DeserializeObject<MyMemoryTranslateResult>(responseContent);

                return new TranslationResult
                {
                    Text = responseDto.ResponseData.TranslatedText,
                    StatusCode = response.StatusCode,
                    Target = target,
                    Source = source
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"Error {ServiceName} - {e.Message}");
            }

            return new TranslationResult
            {
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Text = text,
                Target = target,
                Source = source
            };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="text"></param>
        /// <param name="translation"></param>
        /// <returns></returns>
        public bool TryTranslate(string source, string target, string text, out string translation)
        {
            var trans = Task.Run(() => TranslateAsync(source, target, text, "text")).GetAwaiter().GetResult();

            if (trans.StatusCode == HttpStatusCode.OK)
            {
                translation = trans.Text;
                return true;
            }

            translation = text;
            return false;
        }
    }
}
