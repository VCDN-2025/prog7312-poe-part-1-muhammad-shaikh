Municipal Services SA 

A C# .NET Framework (Windows Forms) application to streamline municipal service engagement for South African residents.
Part 1 implements the Report Issues flow and a user engagement feature; Events/Announcements and Service Status are visually present but disabled for future parts.

**Features**

Main Menu (polished UI): hero header, “how it works” strip, and three cards

Report Issues 

Local Events & Announcements  (disabled for Part 2)

Service Request Status  (disabled for Part 2)

Report Issue form: Location, Category, Description (with live counter), Attachments (JPG/PNG/PDF)

Engagement strategy: progress bar + positive micro-messages encouraging completion

Custom data structures (required):

SimpleLinkedList<T> — bespoke singly-linked list (no List<T>, no arrays for storage)

IssueRepository — in-memory store using the custom list

Note: Data is in-memory for this part (clears when the app closes). Persistence comes in later parts.

**Tech Stack**

Language: C#

Framework: .NET Framework 4.8 (Windows Forms)

IDE: Visual Studio 2022 (Windows)

**Prerequisites**

Windows with Visual Studio 2022 installed

.NET Framework 4.8 Developer Pack installed in VS (Project targets .NET Framework 4.8)

**Getting Started (Compile & Run)**

Clone the repo

git clone <REPO_URL>


Open the solution

In File Explorer, open the cloned folder and double-click MunicipalServicesApp.sln

Or in VS 2022: File → Open → Project/Solution… → select MunicipalServicesApp.sln

Build

VS: Build → Rebuild Solution

Run

Press F5 (Debug) or Ctrl+F5 (Start without debugging)

If VS prompts for a target framework, select .NET Framework 4.8 and let it restore.


**How to Use**

Main Menu

Click Report Issues (only enabled card). Other cards are labeled “Coming soon”.

Report an Issue

Location: Enter an address or landmark (e.g., 123 Florence Nzama St, Durban CBD).

Category: Choose one (Sanitation, Roads & Stormwater, Water & Utilities, Electricity, Parks & Recreation, Safety & Security, Housing, Other).

Description: Provide at least 20 characters (live counter shows progress).

Attachments (optional): Click Add Images / Documents… (JPG/PNG/PDF). Use Remove Selected to delete.

Engagement meter

A progress bar and short encouraging messages guide you to completion.

Submit

Click Submit to store the report (in memory). A dialog shows the Reference ID and session totals.


**Custom Data Structures (required by spec)**

SimpleLinkedList<T>
Custom, singly-linked list with:

Add(T item)

Contains(T item)

RemoveWhere(Func<T,bool> predicate)

ForEach(Action<T> action)

Count (property)

IssueRepository
Stores all IssueReport items using SimpleLinkedList<IssueReport>.

Add(IssueReport report)

Count

(Exposes internal enumeration for future features)

IssueReport

Reference, Location, Category, Description, Attachments: SimpleLinkedList<string>, CreatedAt, Status

ReferenceGenerator

Generates unique references like MSA-YYYY-####

No built-in generic collections (e.g., List<T>) are used for storing issues or attachments.


**Engagement Strategy (what’s implemented & why)**

This app uses lightweight micro-feedback: a progress bar plus positive prompts (e.g., “Halfway there!”) to encourage completion and reduce drop-off. The design aligns with local e-service adoption guidance—keep interactions simple, give immediate feedback, and support trust in municipal processes.

**Reference:**
Hart, T.G.B., et al. (2020) ‘Innovation for development in South Africa: Experiences with basic service technologies in distressed municipalities’, Forum for Development Studies, 47(1), pp. 2–347. Available at: https://strathprints.strath.ac.uk/73688/
 (Accessed: 10 September 2025).

** Project Structure (high level)**
MunicipalServicesApp/
├─ MunicipalServicesApp.sln
├─ Program.cs
├─ MainForm.cs                # Card-style main menu (Report Issues enabled)
├─ ReportIssueForm.cs         # Location, Category, Description, Attachments + engagement UI
├─ Domain/
│  ├─ IssueReport.cs
│  ├─ ReferenceGenerator.cs
│  └─ IssueRepository.cs      # uses SimpleLinkedList<IssueReport>
└─ DataStructures/
   └─ SimpleLinkedList.cs     # custom linked list (no List<T>)



**Video Demo & Repo Links**

YouTube demo: https://youtu.be/KDfixBBLBog

GitHub repository: https://github.com/VCDN-2025/prog7312-poe-part-1-muhammad-shaikh.git

**Troubleshooting**

Solution won’t open in VS: open MunicipalServicesApp.sln from the repo root; ensure VS 2022 + .NET Framework 4.8 Dev Pack.

Build errors about framework: right-click project → Properties → Target framework: .NET Framework 4.8.

Images/PDF won’t attach: only .jpg, .jpeg, .png, .pdf are allowed in Part 1.
