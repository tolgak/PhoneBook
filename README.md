# PhoneBook
## Teknolojik altyapı
* PostGreSQL database 
  * elephantsql.com üzerinde
* EntityFramework.Core 
* RabbitMq - Message Queue altyapısı 
  * CloudAMQP üzerinde
* Credential bilgileri için PhoneBook.DataAPI uygulamasının appsetting.json dosyasına veya Documents dizininde cloud.txt dosyasına bakılabilir. 

## Proje yapılanması
* MockDataGenerator - mocker
  * console uygulaması
  * veritabanında örnek veri yaratıyor.
* PhoneBook.DataAPI - microservice
  * asp.net core uygulaması
  * Person, Contactınfo ve Report controller ları ile RestAPI işlevi sunuyor
* PhoneBook.ReportHandler - microservice
  * Winforms uygulaması
  * RestAPI üzerinden RabbitMq aracılığı ile gelen requesti yakalayıp rapor üretiyor.
  * Veritabanına erişimi yok, RestAPI nin sağladığı olanağı kullanıyor.
