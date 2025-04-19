# extractor-c

A weekend project in C# / .NET that uses GPT-4 to extract structured data from PDF resumes.

## Features

- Extract contact info, work history, skills, and education from PDFs
- Two-stage AI processing for better accuracy:
  - Initial data extraction
  - Verification and cleanup
- Simple web interface for uploads and viewing results

## Security Note

Before production use, implement XSS protection since GPT may potentially repeat content from user input.

## Setup

1. Clone the repository
2. Add your OpenAI credentials to `appsettings.json`:

```json
"OpenAI": {
  "APIKey": "[your API key]",
  "OrganizationID": "[your organization ID]"
}
```

3. Install PdfPig via NuGet
4. Build and run the application
5. Access at `localhost:5051`

## Usage

Simply upload a PDF resume through the web interface and receive structured JSON data that can be copied or further processed as needed.

## Extending

The modular design makes it easy to add extraction stages or support other document types beyond resumes.