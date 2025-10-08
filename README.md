# 🔍 Search API

This project is a demo of a Search Engine API that queries multiple search engines online for input words and returns the number of hits from each engine.

---

## 🧩 Description
**Tech used:** .NET 8, C#, xUnit, Moq, ReqNRoll, HTML, JavaScript  

The application is a **.NET Web API** written in C# using a **Clean Architecture** structure.  
It consists of four layers:
- **Domain** – Core entities  
- **Application** – Business logic and orchestration  
- **Infrastructure** – Implementations for external APIs (as of now Google and Bing)  
- **Presentation** – Web API endpoints and a static HTML/JS frontend  

The frontend is a **static HTML/JavaScript** page located in the Presentation layer, using the Web API to serve the static frontend.

---

## ⚙️ Setup & Run

### 1. Clone the repository
`git clone https://github.com/igundhammar/searchapi`

### 2. Make sure you have .NET 8 installed on your computer before running this project
`cd SearchApi.Presentation`
`dotnet run`

(or just run it in Visual Studio 😃)

The API will start at https://localhost:7051
Swagger UI is available at https://localhost:7051/swagger

---

## 🧪 Testing
The solution includes xUnit, Moq, and ReqNRoll tests for behavior-driven and unit testing.

---

## 🚀 Features
- Search via multiple engines (Google & Bing)

- Summarized and per-word hit counts

- Clean Architecture with clear separation of concerns

- Simple browser-based frontend interface


