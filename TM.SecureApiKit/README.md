# TM.SecureApiKit  
**Secure, Structured, and Production‑Ready Error Handling for ASP.NET Core**

TM.SecureApiKit is a lightweight, configurable, and production‑ready middleware that provides secure, standardized, and structured error responses for ASP.NET Core APIs.

It helps developers deliver consistent error formats, improve debugging with trace IDs, and optionally hide sensitive exception details in production environments.

---

## 🚀 Features

- Centralized error handling for ASP.NET Core APIs  
- Structured JSON error responses  
- Automatic inclusion of:
  - `traceId`
  - `timestamp`
  - `path`
  - `method`
  - `status`
- Optional exposure of:
  - exception message
  - exception type
  - stack trace (for development)
- Automatic exception → HTTP status mapping:
  - `ArgumentException` → 400  
  - `UnauthorizedAccessException` → 403  
  - `KeyNotFoundException` → 404  
  - Others → 500  
- Customizable support contact (e.g., support email)  
- Zero external dependencies  
- Simple one‑line integration  
- Production‑ready and extensible  

---

## 📦 Installation

Install via NuGet:

```bash
dotnet add package TM.SecureApiKit


Or via the NuGet Package Manager:

Install-Package TM.SecureApiKit

🛠 Usage
Add the middleware in your Program.cs or Startup.cs:
app.UseSecureErrorHandling();


With configuration:
app.UseSecureErrorHandling(options =>
{
    options.IncludeExceptionMessage = false; // Hide exception message in production
    options.IncludeExceptionType = false;    // Hide exception type
    options.IncludeExceptionDetails = false; // Hide stack trace
    options.SupportEmail = "support@myapp.com";
});




📄 Example Error Response
{
  "traceId": "0HNIGI2LC1B9E:00000001",
  "status": 500,
  "path": "/api/boom/boom",
  "method": "GET",
  "timestamp": "2025-12-11T21:05:33.123Z",
  "type": "Exception",
  "message": "Test error",
  "support": "support@myapp.com"
}



(Fields depend on your configuration.)

🧩 Why Use This Package?
- Makes your API error responses consistent
- Improves debugging with trace IDs and timestamps
- Avoids leaking sensitive information in production
- Reduces boilerplate code
- Works with any ASP.NET Core project
- Zero dependencies, minimal footprint

📚 Roadmap
- Add logging integration (Serilog, Microsoft.Extensions.Logging)
- Add environment‑based presets (Development / Production)
- Add custom exception mapping (e.g., NotFoundException → 404)
- Add correlation ID propagation
- Add ProblemDetails (RFC 7807) support

🤝 Contributing
Contributions are welcome.
Feel free to open issues or submit pull requests on GitHub.

📜 License
This project is licensed under the MIT License.
