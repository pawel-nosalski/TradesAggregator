# TradesAggregator
Repository for custom trades aggregations

Written in **.NET Core 3.1, C#**, with following assumptions:

* Its entry point is a console application, which expects one input parameter, path to root data folder
* In the root data folder, there should be **Securities.xml** file in proper format, which defines valid securities list
* Apart from that, root data folder should contain folder named **Test**, which is a root folder containing trade's data files, and can have any structure below it
* All files with a mask **Trades\*.xml** with be processed
* There is only one trade file loaded to memory, files are processed sequentially
* All valid trades (linked to valid security defined in **Securities.xml**) will be processed and aggregated, not valid ones will be ignored in calculations
* **Important** - it's assumed that trades are linked to securities by security's text **code**, not numerical id
* When **Securities.xml** file can't be found/read, program exits with exception
* When particular trade's file can't be read, it's ignored, but the program continues
* After calculations are completed, their result (report) is written to disk in text, human-readable format. Location of the report file is root data folder parameter provided in the input 

It can be run by a following command:

<code>
TradesAggregator.Console "PATH_TO_ROOT_DATA_FOLDER"
</code>

e.g.

<code>
TradesAggregator.Console "C:\temp\Engineer Code Test"
</code>
