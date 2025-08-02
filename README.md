# 📰 News Scraper

A C#/.NET-based web scraping application that fetches news articles from multiple online sources, filters content, and serves structured JSON data via an API. Built with clean architecture principles.
---

## ✨ Features

- 🔍 Scrapes headlines and content from popular news websites.
- 🧠 Filters out ads and irrelevant sections using XPath/CSS selectors.
- 🗃️ Returns structured JSON with title, content, publish date, and source.
- 🧱 Clean Architecture: Layered structure with interfaces and use cases.
- ⏲️ Schedulable background scraping (optional).

---

## 🧰 Tech Stack

- **Language**: C#
- **Framework**: .NET 8
- **Scraping**: [HtmlAgilityPack](https://html-agility-pack.net/)
- **Design Pattern**: Clean Architecture
- **API**: ASP.NET Web API
- **Dependency Injection**: Autofac
- **Others**: LINQ, Logging, JSON serialization


