XLocalizer.Translate.MyMemoryTranslate

Instructions to use this package :

- This package requires Rapid API Key, must be obtained from https://rapidapi.com/translated/api/mymemory-translation-memory
- Add the API key to user secrets :

````
{
  "XLocalizer.Translate": {
    "RapidApiKey": "xxx-rapid-api-key-xxx"
  }
}
````

- Register in startup:
````
services.AddHttpClient<ITranslator, MyMemoryTranslateService>();
````

Repository: https://github.com/LazZiya/XLocalizer.Translate.MyMemoryTranslate
Docs: https://docs.ziyad.info/en/XLocalizer/v1.0/translate-services-mymemory.md