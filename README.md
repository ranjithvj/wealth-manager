<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links

<!-- ABOUT THE PROJECT -->
## About The Project


There are many great portfolio trackers available on the web. However, I didn't want to upload by personal finances into an external system for privacy reasons. It was also challenging to track finances for a couple. So I created this dashboard.

Here's what this app aims to do:
* Track Mutual funds contributions, gains and XIRR% 
* Track Stock prices
* Track FD/RD contributions
* A unified dashboard to view one's current net worth

Here's why this app is safe :
* It uses a local SQlite DB for storing data, so you own your data

Here's how it works :
* For Mutual Fund contributions, the app expects the CAS ( Consolidated Account Statement ) to be uploaded 
* For Stocks, the no. of units needs to be entered manually
* For RD/FD, manual entries are needed
* The current prices of stocks/MF's are obtained from external APIs

You may also suggest changes by forking this repo and creating a pull request or opening an issue. 

### Built With

* [ASP.NET Core](https://dotnet.microsoft.com/)
* [Python](https://www.python.org/)
* [SQLite](https://www.sqlite.org/index.html)

### Uses code from following repos 
* [CAS Reader](https://github.com/codereverser/casparser)
* [AMFI NAV Reader](https://github.com/AmruthPillai/AMFI-API)
