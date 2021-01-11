# OtpBankBeugro
Beugró feladat

Szerver oldal:

- OtpFileServerWebApi

	IIS webszerverben hosztolt webalkalmazás
	A web.config konfigurációs fájlba létre kell hozni a "FileStoreFolderName" bejegyzést.
	Az itt megadott mappát a webalkalmazás gyökérkönyvtárához képest kell létrehozni a fájlrendszerben.
	(Ha a webszolgáltatás fizikai elérési útja c:\inetpub\wwwroot, akkor a mappa a c:\inetpub\wwwroot\{mappanév} legyen)

	DokumentumokController
	asp.net web api controller, három műveletet tartalmaz

		- Get 
		  visszaadja a web.config "FileStoreFolderName" konfigurációs bejegyzésben megadott, a fájlrendszerben található mappa tartalmát,
		  a mappában elhelyezett fájlokat.
		  	
		- Get 
		  filenév paraméterrel visszaadja a "FileStoreFolderName" mappában lévő file-t, 
		  a fájl base64 enkódolt. 

		- POST feltölt egy a paraméterben megadott fájlt a szerverre, a web.config bejegyzésben megadott mappába.
		  A fájl nevet a hívás paramétereként kell megadni. A fájl a POST hívás body részében kerül a szerverre. 

	FileManager
	Fájlműveleteket tartalmazza. A web.api controller használja a műveleteinek elvégzéséhez.
	A kontroller DI framework segítségével példányosítja a FileManager-t.

		- Upload
		  Feltölt egy fájlt, melynek tartalma base64 kódolású

		- Download
		  visszaadja a kért file tartalmát base64 kódolással

		- GetFolderFiles
		  visszaadja a web.config-ban beállított mappában elhelyezett fájlok listáját

- OtpFileServerWebApi.Test
	
	MS teszt project, Moq mocking framework felhasználásával

	- Unit tesztek a DokumentumokController-hez
		unit teszt a mappa tartalmának visszaadására
		unit teszt file letöltésére
		unit teszt a file feltöltésére

	- Unit tesztek a FileManager-hez
		unit teszt a mappa tartalmának felolvasására
		unit teszt file felolvasására, a file tartalma base64 encodolt
		unit teszt file célmappában történő elhelyeyésére

Kliens oldal:

- OtpFileClientWinForms

	Klasszikus windows forms alkalmazás

    ClientForm

	Megjelenítő felület. DataGridView komponens listázza a szerveroldalon elhelyezett fájlok neveit
	Minden fájlnév mellett található egy letöltés gomb. Ennek megnyomásával a felhasználó egy megadott mappába mentheti a letöltött fájlt.
	A képernyőn található egy fájl feltöltés gomb. Ezt kiválasztva a felhasználó a saját fájlrendszeréből fájlt tölthet fel a szerverre.
	A feltöltött fájl a fájlokat tartalmazó listában azonnal megjelenik.

	OtpFileClientProxy
	
	HttpClient-et használ a get és post kérések lebonyolítására. 
	A kérésekhez az alap url-t konstansként tartalmazza (RequestUri)
	A szerver oldali hibák kezelését saját exception típus segíti. (OtpApiException) 
	A http hibakódok és http hibaüzenetek is ebben a típusban kezeltek.

	- GetFolderFiles
	  Szerver oldalról lekérdezi és visszaadja a fájlok listáját.

	- DownloadFile
	  Letölti a szerveről a kiválasztott fájlt. A fájl base64 encodolással van ellátva amíg a http csatornán eljut a kliensre.

	- UploadFile
	  Feltölti a szerverre a kiválasztott fájlt. A fájl base64 encodolással van ellátva amíg a http csatornán eljut a szerverre.

- OtpFileClientWinForms.Test
  Integrációs tesztek a kliens oldalról indított kommunikáció teszteléséhez

