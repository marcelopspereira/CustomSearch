# Search API

The Search API is WEB API .NET Core project that will help you to call Azure Search or Bing Custom Search in a very easy way. The Search API will provide an easy to consume API that you can use in any application you desire. After you set the search provider and configure the keys, you can use our WebPortal front-end to start using the service and query the data that you need.

## Solution Architecture

![architecture](https://github.com/DXBrazil/CustomSearch/blob/master/imgs/architecture.png)

## Search API Architecture

![archsearchapi](https://github.com/DXBrazil/CustomSearch/blob/master/imgs/archsearchapi.png)

## Choose your search provider

![bingorazure](https://github.com/DXBrazil/CustomSearch/blob/master/imgs/bingorazure.png)


# Setup 

You will need to create all the services required in Azure or Bing Custom Search and provide the required keys in the config.secrets.json file.

- [Create an Azure Search](https://docs.microsoft.com/en-us/azure/search/search-create-service-portal) - In your subscription and copy the primary key. For this application we must use the ADMIN KEY.

- [Bing Custom Search](https://docs.microsoft.com/en-us/azure/search/search-create-service-portal)- Create a new Bing Custom Search Service and set what sites do you want to search. 

## Creating the config.secrets.json
Insert the config.secrets.json file in the root of the SearchAPI Project.

```json
{
  "AzureSearch": {
    "SearchServiceName": "<your_azure_search_name>",
    "SearchServiceAdminApiKey": "<your_azure_search_key>",
    "SearchServiceIndex": "<your_azure_search_index>"
  },
    "BingSearch": {
        "SubscriptionKey": "<your_bing_custom_search_key>",
        "CustomConfigId": "<your_customconfig_key>"
    }
}
```
## Configure the Azure environment and Bing Custom Search

### Azure Search
You will have to import the index using the GUI Options in Azure and provide the SQL Database for Azure Search, it will create the indexer and columns to use in your search.

## Configure the search provider

http://<your_azure_searchapi_site>.azurewebsites.net/setting/provider/bing    -> Set Bing as your current search provider 

http://<your_azure_searchapi_site>.azurewebsites.net/setting/provider/azure   -> Set Azure Search as your current search provider
