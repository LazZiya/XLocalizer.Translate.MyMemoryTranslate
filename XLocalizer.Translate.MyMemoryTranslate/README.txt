XLocalizer.Translate.MyMemoryTranslate

This package contains twoservices, 
 - MyMemoryTranslateService: directly connected to mymemory translate api's
 - MyMemoryTranslateServiceRapidApi: connected to mymemory translate via RapidApi

MyMemoryTranslateService
==========================
Can be used anonymously without providing any additional parameters, but the daily limit is low!
For more details see https://mymemory.translated.net/doc/usagelimits.php.

If you want to increase the free daily limit:
- Get a key from https://mymemory.translated.net/doc/keygen.php
- add the key and a valid email address to user secrets:

````
{
  "XLocalizer.Translate": {
    "MyMemory": {
        "Email" : "...",
        "Key": "..."
    }
  }
}
````

- Register in startup:
````
services.AddHttpClient<ITranslator, MyMemoryTranslateService>();
````



 MyMemoryTranslateServiceRapidApi
 ================================
- This service requires Rapid API Key, must be obtained from https://rapidapi.com/translated/api/mymemory-translation-memory
- Add the API key to user secrets :

````
{
  "XLocalizer.Translate": {
    "RapidApiKey": "..."
  }
}
````

- Register in startup:
````
services.AddHttpClient<ITranslator, MyMemoryTranslateServiceRapidApi>();
````

Repository: https://github.com/LazZiya/XLocalizer.Translate.MyMemoryTranslate
Docs: https://docs.ziyad.info/en/XLocalizer/v1.0/translate-services-mymemory.md