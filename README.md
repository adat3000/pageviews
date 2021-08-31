# Pageviews

Pageviews is a console application used for showing the top 100 records of pageviews for Wikipedia site since 2015 in machine-readable format.

## Resources

-	File’s location: [https://dumps.wikimedia.org/other/pageviews/](https://dumps.wikimedia.org/other/pageviews/)
-	Sample file: [https://dumps.wikimedia.org/other/pageviews/2015/2015-05/pageviews-20150501-010000.gz](https://dumps.wikimedia.org/other/pageviews/2015/2015-05/pageviews-20150501-010000.gz)
-	Technical Documentation: [https://wikitech.wikimedia.org/wiki/Analytics/Data_Lake/Traffic/Pageviews](https://wikitech.wikimedia.org/wiki/Analytics/Data_Lake/Traffic/Pageviews)


```bash
pageviews [Year] [Month] [Day]
```

## Usage

```csharp
# returns data from March 25, 2020
pageviews 2020 3 25

# returns data from July 01, 2015
pageviews 2015 07 01

# returns data from December 15, 2018
pageviews 2018 12 15
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[Open Source]
