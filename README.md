## Zasady zaliczenia projektu
1. Wykorzystanie CSS lub innych bibliotek i mechanizmów w celu ujednolicenia wyglądu aplikacji. (0.25 pkt.)
2. Wykorzystanie bazy danych. Baza danych powinna zawierać przynajmniej dwie tabele. Aplikacja powinna umożliwić dodanie/edycję/kasowanie i wyświetlenie listy wybranych elementów. (0.25)
3. Aplikacja do komunikacji z bazą danych wykorzystuje ADO NET. (0.25)
4. Aplikacja do komunikacji z bazą danych wykorzystuje Entity Framework Core. (0.25)
5. Komunikacja z bazą danych w przeważającej części oparta jest o procedury składowane. (0.25)
6. Aplikacja obsługuje relację jeden do wielu (np. produkt do którego dodawana jest kategoria produktu wybierana z listy rozwijalnej). (0.5)
7. Aplikacja obsługuje relację wiele do wielu (np. z produktem może być związanych wiele kategorii). (0.5)
8. Wykorzystanie mechanizmu uwierzytelnienia użytkownika. Wybrane strony powinny być dostępne wyłącznie dla zalogowanego użytkownika. (0.25)
9. Hasło użytkownika przechowywane jest w bazie danych jako skrót. (0.25)
10. Aplikacja definiuje własny serwis i wykorzystuje mechanizm wstrzykiwania serwisów. (0.5)
11. Wykorzystanie mechanizmu Session do przechowania wybranych informacji. (0.25)
12. Wprowadzane dane powinny podlegać walidacji. Projekt powinien zawierać przynajmniej jeden wczytywany element na którym odbędzie się podwójna walidacja (np. element obowiązkowy i sprawdzenie formatu). (0.25)
13. Aplikacja wykorzystuje mechanizm filtrów jako atrybutów klasy lub komponentu pośredniczącego. (0.25)
14. Struktura aplikacji ma charakter warstwowy. Oprócz standardowych warstw powinna występować przynajmniej jedna dodatkowa warstwa (np. warstwa dostępu do danych). (0.25)
15. Dodatkowe elementy (własna inwencja). (0.25)
16. Ocena subiektywna prowadzącego. (0.25)
17. Odpowiedź na pytania prowadzącego dotyczące znajomości strony technicznej zrealizowanego projektu. (0.25)

Razem 5 pkt.

Uwaga:  
Jeżeli przedstawiany projekt okaże się "kopią" innego projektu lub z odpowiedzi na pytania (patrz punkt 17) będzie wynikało, iż nie jest znana techniczna strona implementacji aplikacji, punkty z projektu nie zostaną przyznane.

## Uwagi przed użyciem
- W bazie danych tabele Ingredient ze składnikami, Category z kategoriami potraw i Dish z potrawami są tworzone automatycznie przez ORM Entity Framework Core wykorzystany w modelu code first.
- Tabela User przechowująca zarejestrowane konta musi być stworzona ręcznie. W pliku User.sql znajduje się kod używany do stworzenia tabeli User i procedur składowanych używanych do zarządzania tabelą User przez klasę UserSqlDB.
- W appsettings.json wstawiamy connection stringa do naszej bazy danych.
