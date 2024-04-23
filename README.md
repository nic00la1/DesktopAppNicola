# Symulator Bankomatu
<h1>Włączenie projektu</h1>
Aby odpalić projekt na swojej maszynie, wystarczy zmienić ścieżkę do pliku JSON w następujących plikach:

* TransactionService.cs,
* RegisterService.cs 
* Program.cs


<h2>Działanie programu: </h2>


### Logowanie użytkowników
Aplikacja pozwala użytkownikom zalogować się za pomocą numeru karty i PIN-u. Jeśli ktoś poda złe dane więcej niż trzy razy, konto zostaje zablokowane.

### Zarządzanie transakcjami
Dzięki tej funkcji użytkownicy mogą wykonywać różne operacje bankowe. Cała ta magia dzieje się w klasie `TransactionService`.

### Inicjalizacja danych
Aplikacja ładuje dane użytkowników i transakcji z plików JSON. To wszystko dzieje się w metodzie `InicjalizujDane()` w klasie `Program`.

### Interfejs użytkownika
Aplikacja ma przyjazny interfejs, który pozwala użytkownikowi na łatwe korzystanie z funkcji programu. To wszystko jest reprezentowane przez klasę `AppScreen`.

### Obsługa logowania
Klasa `LoginService` zajmuje się procesem logowania, włączając w to wyświetlanie formularza logowania oraz animacje postępu.
