#Full Stack Challenge

Ein Backend mit CRUD Funktionalität erstellen.
Daten Seed wird bereitgestellt. 
Frontend für die Backendfunktionen erstellen. 

###Zweck
Aufbauen einer sauberen, erweiterbaren Entwicklungsstrucktur.
Eine zeitliche Begrenzung einhalten. 

###Tech-Stack

Alle Projekte verwenden .Net Core 3.1

Backend
- JsonFlatFileDataStore
- dependency injection (unity)

Frontend
- WPF
- Prism (MVVM)
- dependency injection (unity)
- xunit
- moq

###Übersicht

Projekte in diesem Repository:
- DevicesModule: Devices ViewModel und View
- DeviceService: Auf Datei basiertes Repository und Seed
- DevicesModule.Tests: Devices ViewModel Tests
- Eppendorf_FSC: Wpf Shell und DI
- Core: Domain Objekte, MVVM base Klassen, Interfaces

###Offene Punkte / Optimierungen

####DB-Repository
Erstellen eines Repositories mit Datenbankanbindung auf Basis von Entity Framework. Die Konfiguration könnte in einer Config-Datei oder der Anwendung hinterlegt werden.

####Multi-User Support
Die Anwendung kann von mehreren Benutzern zeitgleich verwendet werden.
Das Frontend reagiert auf Änderungen im Repository automatisch. Für das File-Repository kann FileSystemWatcher verwendet werden. Für eine mögliche Datenbank Trigger oder Polling.

####Tests
Tests sind etwas zu einfach gehalten und bilden nicht genügend Fälle ab. Aktuell wird nur das ViewModel getestet.

####Entfernen einer Zeile mit Entf/Del-Taste
Aktuell werden Zeilen über einen Knopf entfernt.

####Device Id
Ids werden nicht neu vergeben und automatisch über das ViewModel gesetzt. Dies sollte noch einmal betrachtet/geklärt werden.

####Fehlerbehandlung
Fehlerbehandlung im Devices ViewModel/View.
Fehlerbehandlung im File-Repository.

####Commands erweitern
Die DeviceViewModel Commands sollten um CanExecute erweitert werden.

####ViewModel und View Projekt aufteilen
Ein Vorteil wäre, dass man die View einfacher austauschen kann und das ViewModel losgelöst existiert.

###Bekannte Probleme

Das verwendete File-Repository wird mit der ersten Benutzeraktion initialisiert. Dies blockiert die Anwendung manchmal. 
**Aktueller Fix:** Einen Moment warten.

Bei der Erstellung eines neuen Devices kann dieses direkt gelöscht werden. Dies entfernt die letzte Zeile und es kann kein neuer Eintrag mehr angelegt werden.
**Aktueller Fix:** Ein Neustart der Anwendung ist erforderlich. 


