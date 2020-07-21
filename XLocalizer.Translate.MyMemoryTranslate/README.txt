XLocalizer.TranslationServices.MyMemoryTranslate

Instructions to use this package :

- This package requires Rapid API Key, must be obtained from https://rapidapi.com/translated/api/mymemory-translation-memory
- Add the API key to user secrets :

````
{
  "XLocalizer.TranslationServices": {
    "RapidApiKey": "xxx-rapid-api-key-xxx"
  }
}
````

- Register in startup:
````
services.AddHttpClient<ITranslationService, MyMemoryTranslateService>();
````

Repository: https://github.com/LazZiya/TranslationServices