Projekt prostego API do zarządzania kodami promocyjnymi

Założenia:
• Kod składa się z: nazwy, kodu (unikalnego ciągu znaków), ilości możliwych wyświetleń (pobrań),
informacji czy jest aktywny.
• Każdorazowe pobranie kodu (wyświetlenie) zmniejsza ilość możliwych wyświetleń.
• Wyświetlić można tylko kody z ilością możliwych wyświetleń większą od 0.
• Wyświetlić można tylko aktywne kody.

Aplikacja powinna umożliwiać:
• Dodanie nowego kodu.
• Możliwość zmiany nazwy kodu promocyjnego.
• Pobranie kodu (wyświetlenie).
• Oznaczenie kodu jako nieaktywny.
• Usunięcie kodu.
• Przechowywać podstawowe informacje o historii zmian.

Wykorzystanie bibliotek zewnętrznych:
• Moq - do mockowania obiektów, w tym przypadku odpowiedzi repozytorium na potrzeby testów komend i zapytań
• MediatR - prosta biblioteka umożliwiająca łatwą implementację CQRS

Docelowo aplikacja powinna wykorzystać Entity Framework oraz bazę danych SQL, na ten moment jest statyczne pseuo repozytorium kodów oraz historii biznesowej. 
Wspomniana historia biznesowa jest jedynie robocza i dostępna tylko poprzez debug w kodzie