 # Create Index - Wait a few weeks, we are Open Sourcing this asset for general usage.
Console application to Create Azure Search Synonym Map.

We created an easy way to implmement/update synonyms in Azure Search, you will use a console application + Excel to upload synonyms to the index of Azure Search.

Azure Search has in preview the synonym feature and you can use in your projects: [Azure Search](https://azure.microsoft.com/en-us/services/search/) index with [Synonym Map](https://azure.microsoft.com/en-us/blog/azure-search-synonyms-public-preview/).

# Prerequisites
To use this console application is mandadory that you have access to an Azure Subscription [Try Azure](https://azure.microsoft.com/en-us/free/)

[Create an Azure Search](https://docs.microsoft.com/en-us/azure/search/search-create-service-portal) in your subscription and copy the primary key. For this application we must use the ADMIN KEY.

Install .Net core 2.0, by downloading the right bin for your enviroment.
[Download .NET Core](https://www.microsoft.com/net/download/core)

# CSV Files
The program uses three files to create the Azure Search Index.

1. [Dictionary](./docs/csvtemplate/dictionary.csv) - Update here your synonyms that will be uploaded to Azure Search Index

Each of those file has its own layout, if there is any need to change its layout the program have to be updated.


# Setting UP
The program has a setting file appsettings.json all the information needed to execute the program will be found there.

The file must be placed in the same folder of createindex.dll

```json
{
  "AzureSearch": {
    "ServiceName": "<your-service-name>",
    "ApiKey": "<service admin key>",
    "IndexName": "<your index name>",
    "internetbankingCSV": "<Exam CSV file Path>",
    "SynonymCSV": "<Synonym CSV file path>",
    "DictionaryCSV": "<Dictionary CSV file path>"
  }
}
```

# Running the program

To run the program run the following command.

```cmd
dotnet run CreateIndex.dll
```

# Verifying the result

To check if the index was create properly access the Azure Portal and perfrom some queries using Search Explorer.


## Use Postman or another API Testing tool:

Do a get operation using this API:
```html
https://<your service>.search.windows.net/synonymmaps?api-version=2016-09-01-Preview

api-key: <your_key>
````

You will see a new synonym map created in the response.
