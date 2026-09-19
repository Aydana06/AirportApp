# AirportApp

Онгоцны буудлын **зорчигч бүртгэл (check-in)** болон **суудал хуваарилалт**-ыг бодит цагийн (real-time) шинэчлэлттэйгээр удирддаг .NET шийдэл. Нэг backend API-г веб болон desktop гэсэн хоёр өөр төрлийн клиент хуваалцдаг олон-төслийн (multi-project) архитектуртай.

---

## Систем архитектур

```
                        ┌─────────────────────┐
                        │     Airport-API      │  ← REST API + SignalR Hub
                        │  (ASP.NET Core Web)  │
                        └──────────┬───────────┘
                                   │ ашигладаг
                        ┌──────────▼───────────┐
                        │    AirportLibrary     │  ← Domain, Repository, Service
                        │   (Class Library)     │
                        └──────────┬───────────┘
                                   │
                        ┌──────────▼───────────┐
                        │   SQLite (airport.db) │
                        └───────────────────────┘

        ┌───────────────────┐         ┌────────────────────┐
        │   AirportWebApp    │         │   AgentNativeApp    │
        │ (Razor Pages, веб) │         │ (WinForms, desktop) │
        │  HTTP-ээр API руу  │         │  Repository-г шууд  │
        │      хандана       │         │   дуудаж ажилладаг  │
        └───────────────────┘         └────────────────────┘

                        ┌───────────────────┐
                        │    AirportTest     │  ← MSTest + Moq unit тестүүд
                        └───────────────────┘
```

---

## Төслүүд

| Төсөл | Төрөл | Зорилго |
|---|---|---|
| `Airport-API` | ASP.NET Core Web API | REST endpoint-ууд, SignalR Hub, Swagger |
| `AirportLibrary` | Class Library | Domain model, Repository, Service (бизнес логик) |
| `AirportWebApp` | ASP.NET Core Razor Pages | Веб клиент, API руу HTTP-ээр хандана |
| `AgentNativeApp` | WinForms (.NET 8, Windows) | Онгоцны буудлын ажилтанд зориулсан desktop клиент |
| `AirportTest` | MSTest + Moq | `AirportLibrary`-ийн Service давхаргын unit тест |

---

## Технологийн стек

- **.NET 8**
- **ASP.NET Core Web API** + **SignalR** (бодит цагийн push мэдэгдэл)
- **SQLite** (`Microsoft.Data.Sqlite`, raw ADO.NET — EF Core ашиглаагүй)
- **Swagger / Swashbuckle** — API баримтжуулалт
- **WinForms** — desktop клиент, QR код (`QRCoder`) дэмжлэгтэй
- **MSTest + Moq** — unit тест

---

## Өгөгдлийн сангийн бүтэц

```
Flights(Id, FlightCode, Status)
Passenger(Id, Name, PassportNo, FlightId, SeatNo)
Seat(Id, SeatNo, FlightId, isTaken)
```

Анхны ачаалалтад (`airport.db` файл байхгүй үед) жишээ өгөгдөл (3 нислэг, 3 зорчигч, 6 суудал) автоматаар үүснэ.

---

## API Endpoints

### Flight

| Method | Endpoint | Тайлбар |
|---|---|---|
| GET | `/api/flight` | Бүх нислэгийн жагсаалт |
| GET | `/api/flight/{id}` | ID-аар нэг нислэг авах |
| PUT | `/api/flight/UpdateStatus` | Нислэгийн төлөв шинэчлэх (`FlightCode`, `Status`) → SignalR-аар `FlightStatusUpdated` event push хийнэ |

### Seat

| Method | Endpoint | Тайлбар |
|---|---|---|
| GET | `/api/seat/{flightId}` | Тухайн нислэгийн бүх суудал |
| GET | `/api/seat/{flightId}/available` | Тухайн нислэгийн сул суудлууд |

### CheckIn

| Method | Endpoint | Тайлбар |
|---|---|---|
| POST | `/api/checkin` | `PassportNo`, `SeatNo`, `FlightId`-ээр зорчигчийг бүртгэж суудал онооно → SignalR-аар `SeatTaken` event push хийнэ |

### SignalR Hubs

| Hub URL | Event | Тайлбар |
|---|---|---|
| `/flighthub` | `FlightStatusUpdated` | Нислэгийн төлөв өөрчлөгдөхөд бүх клиентэд илгээгдэнэ |
| `/seathub` | `SeatTaken` | Суудал онооход бүх клиентэд илгээгдэнэ |

---

## Ажиллуулах заавар

### Шаардлага
- .NET 8 SDK
- Visual Studio 2022 (WinForms дэмжлэгтэй), эсвэл VS Code + `dotnet` CLI

### 1. Backend API ажиллуулах

```bash
cd Airport-API
dotnet run
```

Swagger UI: `https://localhost:<port>/swagger`

### 2. Веб клиент ажиллуулах

```bash
cd AirportWebApp
dotnet run
```

> `AirportWebApp/Program.cs`-д API-ийн хаяг одоогоор `https://localhost:7221/` гэж hardcoded байгаа тул, `Airport-API`-г ажиллуулсан бодит порттой тааруулах шаардлагатай.

### 3. Desktop клиент ажиллуулах (зөвхөн Windows)

`AgentNativeApp/Program.cs` дотор `dbPath`-г өөрийн компьютерийн замаар солиод, Visual Studio-гоос ажиллуул (эсвэл `dotnet run` — Windows дээр):

```csharp
string dbPath = "<чиний-repo-замаас>/Airport-API/airport.db";
```

### 4. Unit тест ажиллуулах

```bash
cd AirportTest
dotnet test
```

---

## Мэдэгдэж буй асуудлууд / Цаашид засах зүйлс

- [ ] `AgentNativeApp/Program.cs`-ийн hardcoded DB зам — config эсвэл харьцангуй зам болгох
- [ ] `SeatRepository.AssignSeatToPassenger`-ийн Command-уудад `.Transaction` property заавал зааж өгөх (одоогоор transaction доторх query алдаа өгөх магадлалтай)
- [ ] `airport.db`-г Git-ээс хасаж (`.gitignore`-д `*.db` нэмэх), орлуулан seed script нэмэх
- [ ] `AgentNativeApp`-ийг шууд DB хандалтын оронд `Airport-API`-руу HTTP-ээр хандахаар шилжүүлэх
- [ ] Root-level `.sln` үүсгэж бүх 5 project-ыг нэг дор нээх боломжтой болгох
- [ ] Ашиглагдаагүй `Microsoft.EntityFrameworkCore` dependency-г `AirportLibrary.csproj`-оос хасах

---

## Зохиогч

**Болат Айдана** — [GitHub](https://github.com/Aydana06)