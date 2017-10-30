 # Crawler - Wait a few weeks, we are Open Sourcing this asset for general usage.

A console application to download webpages and save the cleaned data in SQL Database for posterior index at Azure Search.

# Prerequisites
To use this console application is mandadory that you have access to an Azure Subscription [Try Azure](https://azure.microsoft.com/en-us/free/)

[Create an Azure Search](https://docs.microsoft.com/en-us/azure/search/search-create-service-portal) in your subscription and copy the primary key. For this application we must use the ADMIN KEY.

[SQL Database](https://azure.microsoft.com/en-us/services/sql-database/) in your subscription and copy the primary key. For this application we must use the ADMIN KEY.

Install .Net core 2.0, by downloading the right bin for your enviroment.
[Download .NET Core](https://www.microsoft.com/net/download/core)

Update the URL you want to crawl in the Program.cs:

```c#
static void Main(string[] args)
        {
            Console.WriteLine("CustomSearch.Crawler");

            if (args.Length == 0)
            {
                Console.WriteLine("Error: Invalid number of parameters");
                return;
            }

            string connectionString = args[0];
            string entrypoint = (args.Length > 1) ? args[1] : "<the_website_you_want_to_crawl>";

            Console.WriteLine($"Entrypoint: {entrypoint}");
```
# Setting UP
The crawler uses metatags to identify what will be retrieved and saved in SQL Database, you have to configure what kind of data you want to crawl and how you will find the data in the webpage.

Below is a sample of how to extract, clean and upload the data to SQL Database (See ProcessParser.cs to get the full code): 

```c#
class ProcessParser : Processor
    {
        protected override void InternalProcess(Page page)
        {
            var metaDescription = page.Html.Head.QuerySelector("meta[name='description']") as IHtmlMetaElement;
            var metaKeywords = page.Html.Head.QuerySelector("meta[name='keywords']") as IHtmlMetaElement;
            var metaCaminhoPaoNivel1 = page.Html.Head.QuerySelector("meta[name='caminhopaonivel1']") as IHtmlMetaElement;
            var metaCaminhoPaoNivel2 = page.Html.Head.QuerySelector("meta[name='caminhopaonivel2']") as IHtmlMetaElement;

```
# Crawler Pipeline

![crawler](https://github.com/DXBrazil/CustomSearch/blob/master/imgs/crawler.png)


# Running the program

To run the program run the following command or send in the arguments when you Build/Debug your Project in Visual Studio.

```cmd
dotnet run CreateIndex.dll Server=<your_database>.database.windows.net;Database=<your_db>;uid=<user_id>;password=<password>
```

# Verifying the result

The console will output the webpages that was crawled.
