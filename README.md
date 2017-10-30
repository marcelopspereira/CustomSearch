# CustomSearch
A custom search solution to to accelerate and simplify the search implementation and usage experience using Azure Search or Bing Custom Search.

This repository has five main projects described below, use all the projects to have a better experience to implement Search features in your project. See the Architecture screenshot below to understand how the project works and each documentation to understand how it works each feature.

![architecture](https://github.com/DXBrazil/CustomSearch/blob/master/imgs/architecture.png)

# Custom Search API  / Azure Search and Bing Custom Search providers
Asp.net core 2.0 WebApi wrapper for Azure Search and Bing Custom Search

To make other system use the Azure Search or Bing Custom Search without the need to consume those APIs directly and to facilitate the general usage.

[Search API](./docs/searchapi.md)

# Custom Crawler
A Custom Crawler to crawl the websites and save the data in SQL Database. Simple to use just send the URL in the arguments to get it started.

[Crawler](./docs/Crawler.md)

# Create Index
Console Application to create or update Azure Search Synonym Map using Excel or CSV reader.

[Create Index](./docs/CreateIndex.md)

# WebPortal / Front-End
WebPortal to connect the APIs and show the search results. Bing like experience.

[Web Portal](./docs/Readme.md)

# CustomSearch.tests / Unit Tests
Unit test project to test the API results.

[Unit Test](./docs/UnitTest.md)


Help us and contribute to make this project a valuable asset to implement Azure Search or Bing Custom Search!