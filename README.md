# CatFactsApp

CatFactsApp is a simple ASP.NET Core Web API application that fetches random cat facts from the [catfact.ninja](https://catfact.ninja) API and saves them to a local .txt file. 
It includes a minimal frontend UI, Swagger documentation, and unit tests covering the core service logic.

## Features
- Fetches random cat facts from the catfact.ninja API
- Saves each fetched fact to a local .txt file
- Minimal frontend UI with a Text-to-Speech option
- Swagger UI for easy API testing
- Configurable via `appsettings.json`

## Tech Stack
- **Backend**: ASP.NET Core Web API (.NET 8)
- **Frontend**: HTML5, JavaScript
- **Testing**: xUnit, Moq
- **API**: CatFact.ninja (External)
## Project Structure

```
CatFactsApp/
├── src/
│   └── CatFactsApp/          # Web API project (.NET 8)
│       ├── Controllers/      # API Controller
│       ├── Services/         # Business logic
│       ├── Models/           # Data Transfer Objects (DTOs)
│       └── wwwroot/          # Simple Frontend (HTML/JS)
├── tests/
│   └── CatFactsApp.Tests/    # Unit tests (xUnit)
├── CatFactsApp.slnx          # Solution File
└── .gitignore                # Git exclusion rules
```
## API Endpoint

### `GET /api/CatFact/fetch`
Fetches a random cat fact from catfact.ninja and saves it to a local file.

**Success Response (200 OK):**
```json
{
  "message": "Fact fetched and saved successfully",
  "data": {
    "fact": "Blue-eyed, white cats are often prone to deafness.",
    "length": 50
  }
}
```

**Error Responses:**
- `503` — External API is temporarily unavailable
- `500` — Internal server error

---

## Configuration

In `appsettings.json`:
```json
"FileSettings": {
  "BaseUrl": "https://catfact.ninja/",
  "Path": "CatFacts.txt"
}
```
- **BaseUrl** — address of the external API
- **Path** — name/path of the local file where facts are saved
## Getting Started

1. Clone the repository:
```bash
   git clone https://github.com/krzysztof-lech/CatFactsApp.git
```
2. Enter the repository
```bash
   cd CatFactsApp
```
3. Run the Application
```bash
   dotnet run --project src/CatFactsApp
```
- After running, look for the log: Now listening on: http://localhost:XXXX.
- Open your browser and go to that address with /index.html added 
(e.g., http://localhost:5206/index.html).

4. Running Tests
```bash
   dotnet test
```

