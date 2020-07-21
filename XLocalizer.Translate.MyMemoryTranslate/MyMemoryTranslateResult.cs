using System.Net;
using Newtonsoft.Json;

namespace XLocalizer.Translate.MyMemoryTranslate
{
    /// <summary>
    /// MyMemory service tranlate result
    /// </summary>
    public class MyMemoryTranslateResult
    {
        /// <summary>
        /// Response data
        /// </summary>
        [JsonProperty("responseData")]
        public MyMemoryResposeData ResponseData { get; set; }

        /// <summary>
        /// Response status
        /// </summary>
        [JsonProperty("responseStatus")]
        public HttpStatusCode ResponseStatus { get; set; }
    }

    /// <summary>
    /// MyMemory respsponse data
    /// </summary>
    public class MyMemoryResposeData
    {
        /// <summary>
        /// Translated text
        /// </summary>
        [JsonProperty("translatedText")]
        public string TranslatedText { get; set; }
    }
}
